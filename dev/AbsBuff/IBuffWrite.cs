namespace Raisin.AbsBuff
{
    using System.Threading;

    using Cysharp.Threading.Tasks;
    
    using OneOf;

    public interface IBuffWriteError
    { }

    public interface IBuffWrite<TDat>
    {
        public UniTask<OneOf<ISliceMut<TDat>, IBuffWriteError>> WriteAsync(uint length, CancellationToken token = default);
    }

    public interface IBuffWrite<TSliceMut, TDat, TErr>: IBuffWrite<TDat>
        where TSliceMut: struct, ISliceMut<TDat, TSliceMut>
        where TErr: struct, IBuffWriteError
    {
        public new UniTask<OneOf<TSliceMut, TErr>> WriteAsync(uint length, CancellationToken token = default);
    }
}
