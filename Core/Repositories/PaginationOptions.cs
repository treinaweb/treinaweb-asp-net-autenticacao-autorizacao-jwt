namespace TWJobs.Core.Repositories;

public class PaginationOptions
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public PaginationOptions(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize < 1 ? 10 : pageSize;
    }
}