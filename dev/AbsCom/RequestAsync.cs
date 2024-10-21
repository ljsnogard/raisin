namespace Raisin.AbsCom
{
    /// <summary>
    /// 为客户端的异步操作提供回调式 API，配合扩展来避免不同平台下 Task 导致的 GC 压力
    /// </summary>
    public readonly struct RequestAsync: IDisposable
    {
        public Uri Command
            => throw new NotImplementedException();

        public bool HasResponse
            => throw new NotImplementedException();

        public bool TryGetResponse(out IResponse response)
            => throw new NotImplementedException();

        public void Dispose()
            => throw new NotImplementedException();

        internal RequestAsync(IClient client, uint id)
        {
            this.client_ = client;
            this.id_ = id;
        }

        private readonly IClient client_;
        private readonly uint id_;
    }

    public static class RequestAsyncExtensions
    {
        public static System.Threading.Tasks.Task<IResponse> GetResponseAsync(
            in this RequestAsync clientTask,
            System.Threading.CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
