namespace Raisin.AbsCom.Net
{
    using Raisin.AbsBuff;

    /// <summary>
    /// 道口号，类似于 TCP/IP 中的端口号
    /// </summary>
    /// <param name="code"></param>
    public readonly struct SpoutId(uint code)
    {
        public readonly uint Code = code;
    }

    /// <summary>
    /// 频道，在已有流的基础上复用出的流，像 TCP/IP 中的连接一样提供双向双工数据流
    /// </summary>
    public interface IChannel
    {
        /// <summary>
        /// 本地道口号
        /// </summary>
        public SpoutId Local { get; }

        /// <summary>
        /// 远端道口号
        /// </summary>
        public SpoutId Remote { get; }

        /// <summary>
        /// 入口数据流
        /// </summary>
        public IBuffRead<byte> Rx { get; }

        /// <summary>
        /// 出口数据流
        /// </summary>
        public IBuffRead<byte> Tx { get; }
    }

    public interface IChannel<out TRx, TRxSlice, out TTx, TTxSlice>: IChannel
        where TRx: struct, IBuffRead<TRxSlice>
        where TRxSlice: struct, ISliceRef<byte, TRxSlice>
        where TTx: struct, IBuffWrite<TTxSlice>
        where TTxSlice: struct, ISliceMut<byte, TTxSlice>
    {
        public new TRx Rx { get; }

        public new TTx Tx { get; }
    }
}
