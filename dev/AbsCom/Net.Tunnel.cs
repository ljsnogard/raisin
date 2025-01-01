namespace Raisin.AbsCom.Net
{
    using Cysharp.Threading.Tasks;
    
    using OneOf;
    using OneOf.Types;

    public interface ITunnelError
    { }

    public interface ITunnel
    {
        /// <summary>
        /// 中断所有活动的 Channel 后关闭当前通道
        /// </summary>
        /// <returns></returns>
        public UniTask ShutdownAsync();

        /// <summary>
        /// 在指定的道口号上监听某个上的来自远端的频道开通请求
        /// </summary>
        /// <param name="local">要监听的门号</param>
        /// <returns></returns>
        public UniTask<OneOf<IListener, ITunnelError>> ListenAsync(in SpoutId local);

        /// <summary>
        /// 向远端请求建立一个新的频道 
        /// </summary>
        /// <param name="local">本地道口号，为0时由隧道随机选取一个</param>
        /// <param name="remote">远端道口号，不能为0</param>
        /// <returns></returns>
        public UniTask<OneOf<IChannel, ITunnelError>> DredgeAsync(in SpoutId local, in SpoutId remote);

        /// <summary>
        /// 向远端请求关闭一个已有的频道
        /// </summary>
        /// <param name="local"></param>
        /// <param name="remote"></param>
        /// <returns></returns>
        public UniTask<OneOf<None, ITunnelError>> SiltAsync(in SpoutId local, in SpoutId remote);
    }
}
