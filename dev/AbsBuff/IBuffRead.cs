namespace Raisin.AbsBuff
{
    using System.Threading;

    using Cysharp.Threading.Tasks;
    
    using OneOf;

    public interface IBuffReadError
    { }

    public interface IBuffRead<TDat>
    {
        public UniTask<OneOf<ISliceRef<TDat>, IBuffReadError>> ReadAsync(uint length, CancellationToken token = default);
    }

    public interface IBuffRead<TSliceRef, TDat, TErr>: IBuffRead<TDat>
        where TSliceRef: struct, ISliceRef<TDat, TSliceRef>
        where TErr: struct, IBuffReadError
    {
        public new UniTask<OneOf<TSliceRef, TErr>> ReadAsync(uint length, CancellationToken token = default);
    }
}
