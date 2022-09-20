using Microsoft.AspNetCore.Mvc;
using TWJobs.Api.Common.Assemblers;
using TWJobs.Api.Common.Dtos;
using TWJobs.Api.Jobs.Dtos;
using TWJobs.Api.Jobs.Services;

namespace TWJobs.Api.Jobs.Controllers;

[ApiController]
[Route("/api/jobs")]
public class JobController : ControllerBase
{
    private readonly IJobService _jobService;
    private readonly IAssembler<JobDetailResponse> _jobDetailAssembler;
    private readonly IPagedAssembler<JobSummaryResponse> _jobSummaryPagedAssembler;

    public JobController(
        IJobService jobService,
        IAssembler<JobDetailResponse> jobDetailAssembler,
        IPagedAssembler<JobSummaryResponse> jobSummaryPagedAssembler)
    {
        _jobService = jobService;
        _jobDetailAssembler = jobDetailAssembler;
        _jobSummaryPagedAssembler = jobSummaryPagedAssembler;
    }

    [HttpGet(Name = "FindAllJobs")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<JobSummaryResponse>))]
    public IActionResult FindAll([FromQuery] int page, [FromQuery] int size)
    {
        var body = _jobService.FindAll(page, size);
        return Ok(_jobSummaryPagedAssembler.ToPagedResource(body, HttpContext));
    }

    [HttpGet("{id}", Name = "FindJobById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JobDetailResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    public IActionResult FindById([FromRoute] int id)
    {
        var body = _jobService.FindById(id);
        return Ok(_jobDetailAssembler.ToResource(body, HttpContext));
    }

    [HttpPost(Name = "CreateJob")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(JobDetailResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
    public IActionResult Create([FromBody] JobRequest jobRequest)
    {
        var body = _jobService.Create(jobRequest);
        return CreatedAtAction(
            nameof(FindById),
            new { Id = body.Id },
            _jobDetailAssembler.ToResource(body, HttpContext)
        );
    }

    [HttpPut("{id}", Name = "UpdateJobById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JobDetailResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    public IActionResult UpdateById([FromRoute] int id, [FromBody] JobRequest jobRequest)
    {
        var body = _jobService.UpdateById(id, jobRequest);
        return Ok(_jobDetailAssembler.ToResource(body, HttpContext));
    }

    [HttpDelete("{id}", Name = "DeleteJobById")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    public IActionResult DeleteById([FromRoute] int id)
    {
        _jobService.DeleteById(id);
        return NoContent();
    }
}