namespace TWJobs.Core.Repositories;

public interface IPagedRespository<Model>
{
    PagedResult<Model> FindAll(PaginationOptions options);
}