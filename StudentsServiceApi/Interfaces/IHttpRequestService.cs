namespace StudentsServiceApi.Interfaces
{
    public interface IHttpRequestService
    {
        Task<string> SendRequestAsync(string url);

    }
}
