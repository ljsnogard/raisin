namespace Raisin.AbsCom.Rpc
{
    /// <summary>
    /// 为客户端的异步操作提供回调式 API，配合扩展来避免不同平台下 Task 导致的 GC 压力
    /// </summary>
    public interface IRequestAsync: IHasTryGetFn<IResponse>
    {
        public Uri Command { get; }
    }

    public interface IRequestAsync<TResponse>: IRequestAsync, ITryGet<TResponse>
        where TResponse : IResponse
    { }
}
