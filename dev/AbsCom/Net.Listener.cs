namespace Raisin.AbsCom.Net
{
    public interface IIncomingAsync: IHasTryGetFn<IDescriptor>
    { }

    public interface IIncomingAsync<TDescriptor>: IIncomingAsync, ITryGet<TDescriptor>
        where TDescriptor: struct , IDescriptor
    { }

    public interface IAcceptAsync: IHasTryGetFn<IChannel>
    { }

    public interface IAcceptAsync<TChannel>: IAcceptAsync, ITryGet<TChannel>
    { }

    public interface IRejectAsync
    { }

    public interface IDescriptor
    { }

    public interface IListener
    {
        public IAcceptAsync Accept(IDescriptor descriptor);

        public IRejectAsync Reject(IDescriptor descriptor);
    }

    public interface IListener<TDescriptor, TIncomingAsync, TAcceptAsync, TRejectAsync>: IListener
        where TDescriptor : struct, IDescriptor
        where TIncomingAsync: struct, IIncomingAsync
        where TAcceptAsync : struct, IAcceptAsync
        where TRejectAsync : struct, IRejectAsync
    {
        public TIncomingAsync Incoming();

        public TAcceptAsync Accept(in TDescriptor descriptor);

        public TRejectAsync Reject(in TDescriptor descriptor);
    }
}
