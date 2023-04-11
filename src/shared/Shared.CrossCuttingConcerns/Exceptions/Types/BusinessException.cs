namespace Shared.CrossCuttingConcerns.Exceptions.Types;

public class BusinessException : Exception
{
    public BusinessException(string message)
        : base(message) { }
}
