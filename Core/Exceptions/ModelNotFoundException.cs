namespace TWJobs.Core.Exceptions;

public class ModelNotFoundException : Exception
{
    public ModelNotFoundException(string? message = "Model not found") : base(message)
    { }
}