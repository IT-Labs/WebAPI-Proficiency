
namespace WebApplication.Contracts
{
    public interface IManager<T>
        where T: new()
    {        IResponse<string> RefreshIfActive<T>(string token) where T : new();
    }
}
