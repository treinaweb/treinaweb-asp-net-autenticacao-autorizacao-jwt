namespace TWJobs.Api.Jobs.Dtos;

public class JobRequest
{
    public string Title { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public ICollection<string> Requirements { get; set; } = new List<string>();
}