namespace Raisin.AbsCom.Net
{
    using System.Threading;

    using Cysharp.Threading.Tasks;

    using OneOf;
    using OneOf.Types;
    using Raisin.AbsBuff;

    public interface IDescriptor
    {
        public SpoutId Remote { get; }
    }

    public interface IListenerError
    { }

    public interface IListener
    {
        public UniTask<OneOf<IDescriptor, IListenerError>> Incoming(CancellationToken token = default);

        public UniTask<OneOf<IChannel, IListenerError>> Accept(IDescriptor descriptor, CancellationToken token = default);

        public UniTask<OneOf<IListenerError, None>> Reject(IDescriptor descriptor, CancellationToken token = default);
    }

    public interface IListener<TDescriptor, TChannel, TErr>: IListener
        where TDescriptor: struct, IDescriptor
        where TChannel: struct, IChannel
        where TErr: struct, IListenerError
    {
        public new UniTask<OneOf<TDescriptor, TErr>> Incoming(CancellationToken token = default);

        public UniTask<OneOf<TChannel, TErr>> Accept(in TDescriptor descriptor, CancellationToken token = default);

        public UniTask<OneOf<TErr, None>> Reject(in TDescriptor descriptor, CancellationToken token = default);
    }
}
