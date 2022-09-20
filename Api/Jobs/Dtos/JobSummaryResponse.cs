using TWJobs.Api.Common.Dtos;

namespace TWJobs.Api.Jobs.Dtos;

public class JobSummaryResponse : ResourseResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
}