namespace Raisin.AbsAsync
{
    using Cysharp.Threading.Tasks;

    public static class CancellationTokenExtension
    {
        public static async UniTask GetCancellationSignalAsync(this CancellationToken token)
        {
            try
            {
                if (token.IsCancellationRequested)
                    return;
                await UniTask.Never(token);
            }
            catch (OperationCanceledException)
            {
                return;
            }
        }
    }
}
