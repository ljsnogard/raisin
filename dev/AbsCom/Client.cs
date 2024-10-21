namespace Raisin.AbsCom
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using static Raisin.AbsCom.RequestAsyncExtensions;

    public interface IClient
    {
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="command">所请求执行的程序的 Uri</param>
        /// <param name="parameters">执行程序所需的参数</param>
        /// <returns></returns>
        public RequestAsync Request<P>(Uri command, P parameters)
            where P : IAsyncEnumerable<SegmentRead>;
    }

    static class Demo
    {
        static async Task RunDemoAsync(IClient client)
        {
            using var task = client.Request(new Uri("/"), new NoSegments());
            var response = await task.GetResponseAsync();
        }
    }
}
