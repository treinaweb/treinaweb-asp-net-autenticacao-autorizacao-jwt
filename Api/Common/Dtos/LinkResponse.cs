namespace TWJobs.Api.Common.Dtos;

public class LinkResponse
{
    public string? Href { get; set; }
    public string Type { get; set; }
    public string Rel { get; set; }

    public LinkResponse(string? href, string type, string rel)
    {
        this.Href = href;
        this.Type = type;
        this.Rel = rel;
    }
}