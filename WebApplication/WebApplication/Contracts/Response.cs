
namespace WebApplication.Contracts
{
    /// <summary>
    ///     Definition of the Response
    /// </summary>
    public interface IResponse<T> : IResponse    {
        /// <summary>
        /// </summary>
        T Payload { get; set; }
    }
}
