using SDL2;
using static SDL2.SDL;

namespace _25._03._04_ComponentBaseEngine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // SDL 라이브러리 초기화
            if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) < 0)
            {
                Console.WriteLine("Fail Init.");
            }
            
            // 메인 윈도우 생성
            IntPtr myWindow = SDL.SDL_CreateWindow(
                "SDL_Sample", // 창 제목
                1920, 0,         // 윈도우 시작 위치 (X, Y 좌표)
                1920, 1080,     // 윈도우 크기 (가로, 세로)
                SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            // 윈도우 이벤트 처리용 SDL 이벤트 구조체 선언
            SDL.SDL_Event myEvent;

            // 메인 렌더러 설정
            IntPtr renderer =  SDL.SDL_CreateRenderer(
                myWindow
                , -1
                //SDL.SDL_RendererFlags.SDL_RENDERER_SOFTWARE           //  CPU 렌더링
                , SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED        // GPU 가속 렌더링 활성화
                | SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC           // 수직 동기화(VSync) 활성화 (화면 티어링 방지)
                | SDL.SDL_RendererFlags.SDL_RENDERER_TARGETTEXTURE);    // 렌더링 타겟을 텍스처로 설정 가능하게 함

            Random rd = new Random();

            while (true)
            {
                SDL.SDL_PollEvent(out myEvent);                         // Win32 API PeekMessage
                if (myEvent.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    break;
                }
                else if (myEvent.type == SDL_EventType.SDL_KEYDOWN)
                {
                    if (myEvent.key.keysym.sym == SDL_Keycode.SDLK_ESCAPE)
                    {
                        break;
                    }
                }
                SDL.SDL_SetRenderDrawColor(renderer, 0, 0, 0, 0);       // 펜 색상 세팅
                SDL.SDL_RenderClear(renderer);                          // 화면 초기화

                for (int i = 0; i < 100; ++i)
                {
                    byte red = (byte)rd.Next(0, 256);
                    byte green = (byte)rd.Next(0, 256);
                    byte blue = (byte)rd.Next(0, 256);
                    byte alpha = (byte)rd.Next(0, 256);
                    SDL.SDL_SetRenderDrawColor(renderer, red, green, blue, alpha);       // 펜 색상 세팅

                    int startX = rd.Next(0, 1920 - 100);
                    int startY = rd.Next(0, 1080 - 100);

                    // min size = 10px;
                    // max size = 100px

                    //int w = rd.Next(10, 101);
                    //int h = rd.Next(10, 101);
                    //SDL_Rect newRect = new SDL_Rect{ x = startX, y = startY, w = w, h = h };
                    //SDL.SDL_RenderDrawRect(renderer, ref newRect);
                    //SDL.SDL_RenderFillRect(renderer, ref newRect);

                    // 원 그리기
                    // 반지름 r
                    int r = rd.Next(1, 100);

                    // 중심점 startX, startY
                    // 각도를 활용한 둘레 찍기
                    //for (int j = 0; j < 360; ++j)
                    //{
                    //    float radian = j * (MathF.PI / 180.0f);
                    //    float x = r * MathF.Sin(radian);
                    //    float y = r * MathF.Cos(radian);

                    //    SDL.SDL_RenderDrawPoint(renderer, (int)(startX + x), (int)(startY + y));

                    //}


                    float prevX = startX;
                    float prevY = startY;

                    // 선을 통해 원을 그리기
                    for (int j = 0; j <= 360; j += 60)
                    {
                        float radian = j * (MathF.PI / 180.0f);
                        float newX = prevX + r * MathF.Sin(radian);
                        float newY = prevY + r * MathF.Cos(radian);

                        SDL.SDL_RenderDrawLine(renderer, (int)prevX, (int)prevY, (int)newX, (int)newY);
                        prevX = newX;
                        prevY = newY;
                    }

                }
                SDL.SDL_RenderPresent(renderer);                        // 버퍼를 화면에 출력 (프레임을 렌더링)
            }

            // 메모리 해제
            SDL.SDL_DestroyRenderer(renderer);
            SDL.SDL_DestroyWindow(myWindow);
            SDL.SDL_Quit();
        }
    }
}
