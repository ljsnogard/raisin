namespace Raisin.AbsBuff
{
    using System.Threading;

    using Cysharp.Threading.Tasks;

    public interface IBuffWrite<TData>
    {
        public bool CanWrite { get; }

        public UniTask<ISliceMut<TData>> WriteAsync(uint length, CancellationToken token = default);
    }

    public interface IBuffWrite<TSliceMut, TData>: IBuffWrite<TData>
        where TSliceMut: struct, ISliceMut<TData, TSliceMut>
    {
        public new UniTask<TSliceMut> WriteAsync(uint length, CancellationToken token = default);
    }
}
