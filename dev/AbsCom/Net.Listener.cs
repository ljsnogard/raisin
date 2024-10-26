namespace Raisin.AbsCom.Net
{
    using System.Threading;

    using Cysharp.Threading.Tasks;

    public interface IDescriptor
    { }

    public interface IListener
    {
        public UniTask<IDescriptor> Incoming(CancellationToken token = default);

        public UniTask<IChannel> Accept(IDescriptor descriptor, CancellationToken token = default);

        public UniTask Reject(IDescriptor descriptor, CancellationToken token = default);
    }

    public interface IListener<TDescriptor, TChannel>: IListener
        where TDescriptor: struct, IDescriptor
        where TChannel: struct, IChannel
    {
        public new UniTask<TDescriptor> Incoming(CancellationToken token = default);

        public UniTask<TChannel> Accept(in TDescriptor descriptor, CancellationToken token = default);

        public UniTask Reject(in TDescriptor descriptor, CancellationToken token = default);
    }
}
