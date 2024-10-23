namespace Raisin.AbsCom.Net
{
    using System.IO.Pipelines;

    public interface IChannel
    {
        public GateId LocalGate { get; }

        public GateId RemoteGate { get; }

        public PipeReader Rx { get; }

        public PipeWriter Tx { get; }
    }
}
