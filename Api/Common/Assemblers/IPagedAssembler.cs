using TWJobs.Api.Common.Dtos;

namespace TWJobs.Api.Common.Assemblers;

public interface IPagedAssembler<R> where R : ResourseResponse
{
    PagedResponse<R> ToPagedResource(PagedResponse<R> pagedResource, HttpContext context);
}