namespace Infra
{
    public record RequestDto
    (
        string MethodName,
        int MaxRequests
    );
}
