namespace Raisin.AbsCom.Net
{
    using Raisin.AbsAsync;

    public readonly struct GateId
    {
        public readonly uint Code;
    }

    public interface ITunnel<TShutdownAsync, TListenAsync, TConnectAsync, TDisconnectAsync>
        where TShutdownAsync: struct, IShutdownAsync
        where TListenAsync: struct, IListenAsync
        where TConnectAsync: struct, IConnectAsync
        where TDisconnectAsync: struct, IDisconnectAsync
    {
        /// <summary>
        /// 关闭当前通道
        /// </summary>
        /// <returns></returns>
        public TShutdownAsync Shutdown();

        /// <summary>
        /// 监听某个频道号上的来自远端的频道连接请求
        /// </summary>
        /// <param name="gate">要监听的门号</param>
        /// <returns></returns>
        public TListenAsync Listen(in GateId gate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="local"></param>
        /// <param name="remote"></param>
        /// <returns></returns>
        public TConnectAsync Connect(in GateId local, in GateId remote);

        public TDisconnectAsync Disconnect(in GateId local, in GateId remote);
    }

    public interface IListenAsync: IHasTryGetFn<IListener>
    { }

    public interface IListenAsync<TListener> : IListenAsync, ITryGet<TListener>
        where TListener : struct, IListener
    { }

    public interface IConnectAsync
    { }

    public interface IDisconnectAsync
    { }

    public interface IShutdownAsync
    { }
}
