using TWJobs.Core.Models;

namespace TWJobs.Core.Repositories.Jobs;

public interface IJobRepository : ICrudRepository<Job, int>, IPagedRespository<Job>
{ }