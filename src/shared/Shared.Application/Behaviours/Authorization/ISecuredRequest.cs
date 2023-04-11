namespace Shared.Application.Behaviours.Authorization;

public interface ISecuredRequest
{
    public string[] Roles { get; }
}
