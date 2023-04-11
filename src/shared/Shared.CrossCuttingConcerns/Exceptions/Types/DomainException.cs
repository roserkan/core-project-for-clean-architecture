namespace Shared.CrossCuttingConcerns.Exceptions.Types;

public class DomainException : Exception
{
    public DomainException(string message)
        : base(message) { }
}
