
namespace Infra
{
    public record ClientDTO
    (        
        int TimeOut ,
        string Uri,
        RequestType RequestType,
        object? Entity = null
    );
}
