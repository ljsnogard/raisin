namespace Raisin.AbsCom.Net
{
    using Raisin.AbsBuff;

    public interface IChannel
    {
        public GateId LocalGate { get; }

        public GateId RemoteGate { get; }

        public IBuffRead<byte> Rx { get; }

        public IBuffRead<byte> Tx { get; }
    }

    public interface IChannel<TRx, TRxSlice, TTx, TTxSlice>: IChannel
        where TRx: struct, IBuffRead<TRxSlice, byte>
        where TRxSlice: struct, ISliceRef<byte, TRxSlice>
        where TTx: struct, IBuffWrite<TTxSlice, byte>
        where TTxSlice: struct, ISliceMut<byte, TTxSlice>
    {
        public new TRx Rx { get; }

        public new TTx Tx { get; }
    }
}
