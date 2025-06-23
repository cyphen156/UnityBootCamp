
namespace MultiThread.Application
{
    public class UnsafeToLock
    {
        class Counter
        {
            private int _value;

            public void Increment_ThreadUnsafe()
            {
                // 이 메서드는 멀티스레드 환경에서 안전하지 않음
                _value++;
            }
        }
    }
}
