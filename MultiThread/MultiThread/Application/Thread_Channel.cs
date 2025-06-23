
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MultiThread.Application
{
    public class Thread_Channel
    {
        public async Task ChannelExample()
        {
            Channel<int> channel = Channel.CreateBounded<int>(10);

            // 생산자
            Task producer = Task.Run(async () =>
            {
                for (int i = 0; i < 100; i++)
                {
                    await channel.Writer.WriteAsync(i);
                    Console.WriteLine($"Produced: 데이터 {i} 생성");
                }
                channel.Writer.Complete(); // 생산이 끝났음을 알림
            });

            // 소비자
            Task consumer = Task.Run(async () =>
            {
                await foreach (var item in channel.Reader.ReadAllAsync())
                {
                    Console.WriteLine($"Consumed: 데이터 {item} 소비");
                    // Simulate some processing
                    await Task.Delay(100);
                }
            }); 

            await Task.WhenAll(producer, consumer);


            ///==> 채널은 각 태스크가 독립적으로 실행되기 때문에
            ///코드상의 순서와 맞지 않게 실행될 수 있다.
            ///Ex) -> 소비 -> 생성 순서가 뒤바뀔 수 있음.
        }
    }
}
