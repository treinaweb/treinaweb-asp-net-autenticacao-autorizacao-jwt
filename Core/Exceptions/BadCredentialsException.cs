namespace TWJobs.Core.Exceptions;

public class BadCredentialsException : Exception
{
    public BadCredentialsException() : base("Bad credentials")
    { }
}