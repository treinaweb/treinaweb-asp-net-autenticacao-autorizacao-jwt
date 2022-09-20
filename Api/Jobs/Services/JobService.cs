using FluentValidation;
using TWJobs.Api.Common.Dtos;
using TWJobs.Api.Jobs.Dtos;
using TWJobs.Api.Jobs.Mappers;
using TWJobs.Core.Exceptions;
using TWJobs.Core.Models;
using TWJobs.Core.Repositories;
using TWJobs.Core.Repositories.Jobs;

namespace TWJobs.Api.Jobs.Services;

public class JobService : IJobService
{
    private readonly IJobRepository _jobRepository;
    private readonly IJobMapper _jobMapper;
    private readonly IValidator<JobRequest> _jobRequestValidator;

    public JobService(
        IJobRepository jobRepository,
        IJobMapper jobMapper,
        IValidator<JobRequest> jobRequestValidator)
    {
        _jobRepository = jobRepository;
        _jobMapper = jobMapper;
        _jobRequestValidator = jobRequestValidator;
    }

    public JobDetailResponse Create(JobRequest jobRequest)
    {
        _jobRequestValidator.ValidateAndThrow(jobRequest);
        var jobToCreate = _jobMapper.ToModel(jobRequest);
        var createdJob = _jobRepository.Create(jobToCreate);
        return _jobMapper.ToDetailResponse(createdJob);
    }

    public void DeleteById(int id)
    {
        if (!_jobRepository.ExistsById(id))
        {
            throw new ModelNotFoundException($"Job with id {id} not found");
        }
        _jobRepository.DeleteById(id);
    }

    public ICollection<JobSummaryResponse> FindAll()
    {
        return _jobRepository.FindAll()
            .Select(job => _jobMapper.ToSummaryResponse(job))
            .ToList();
    }

    public PagedResponse<JobSummaryResponse> FindAll(int page, int size)
    {
        var paginationOption = new PaginationOptions(page, size);
        var pagedResult = _jobRepository.FindAll(paginationOption);
        return _jobMapper.ToPagedSummaryResponse(pagedResult);
    }

    public JobDetailResponse FindById(int id)
    {
        var job = _jobRepository.FindById(id);
        if (job is null)
        {
            throw new ModelNotFoundException($"Job with id {id} not found");
        }
        return _jobMapper.ToDetailResponse(job);
    }

    public JobDetailResponse UpdateById(int id, JobRequest jobRequest)
    {
        _jobRequestValidator.ValidateAndThrow(jobRequest);
        if (!_jobRepository.ExistsById(id))
        {
            throw new ModelNotFoundException($"Job with id {id} not found");
        }
        var jobToUpdate = _jobMapper.ToModel(jobRequest);
        jobToUpdate.Id = id;
        var updatedJob = _jobRepository.Update(jobToUpdate);
        return _jobMapper.ToDetailResponse(updatedJob);
    }
}