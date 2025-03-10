using SDL2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DEngine
{
    public class SpriteRenderer : Component
    {
        public int orderLayer;

        public char Shape; //Mesh, Spirte
        public SDL.SDL_Color color;
        public int spriteSize = 30;

        protected bool isAnimaion = false;
        protected IntPtr myTexture;
        protected IntPtr mySurface;

        protected int spriteIndexX = 0;
        protected int spriteIndexY = 0;

        public SDL.SDL_Color colorKey;

        protected string filename;

        public float elapsedTime = 0;

        private SDL.SDL_Rect sourceRect;        // 이미지
        private SDL.SDL_Rect destinationRect;   // 스크린 사이즈

        public float processTime = 100.0f;
        public int maxCellCountX = 5;
        public int maxCellCountY = 5;
        public SpriteRenderer()
        {

        }

        public override void Update()
        {

            //SDL.SDL_Rect myRect;

            //SDL.SDL_RenderFillRect(Engine.Instance.myRenderer, ref myRect);
            int X = gameObject.transform.x;
            int Y = gameObject.transform.y;


            //X,Y 위치에 Shape 출력
            //            Console.SetCursorPosition(X, Y);
            //            Console.Write(Shape);
            // Console Render
            //Engine.backBuffer[Y, X] = Shape;

            // bitMap Update
            //myRect.x = X * spriteSize;
            //myRect.y = Y * spriteSize;
            //myRect.w = spriteSize;
            //myRect.h = spriteSize;
            //SDL.SDL_SetRenderDrawColor(Engine.Instance.myRenderer, color.r, color.g, color.b, color.a);
            //SDL.SDL_RenderDrawPoint(Engine.Instance.myRenderer, X, Y);

            destinationRect.x = X * spriteSize;
            destinationRect.y = Y * spriteSize;
            destinationRect.w = spriteSize;
            destinationRect.h = spriteSize;

            unsafe
            {
                //이미지 정보 가져와서 할일이 있음
                SDL.SDL_Surface* surface = (SDL.SDL_Surface*)(mySurface);

                if (isAnimaion)
                {
                    if (elapsedTime >= processTime)
                    {
                        spriteIndexX++;
                        spriteIndexX = spriteIndexX % maxCellCountX;
                        elapsedTime = 0;
                    }
                    else
                    {
                        elapsedTime += Time.deltaTime;
                    }


                    int cellSizeX = surface->w / 5;
                    int cellSizeY = surface->h / 5;
                    sourceRect.x = cellSizeX * spriteIndexX;
                    sourceRect.y = cellSizeY * spriteIndexY;
                    sourceRect.w = cellSizeX;
                    sourceRect.h = cellSizeY;
                }
                else
                {
                    sourceRect.x = 0;
                    sourceRect.y = 0;
                    sourceRect.w = surface->w;
                    sourceRect.h = surface->h;
                }
            }
        }

        public virtual void Render()
        {
            int X = gameObject.transform.x;
            int Y = gameObject.transform.y;

            //Console
            Engine.backBuffer[Y, X] = Shape;

            unsafe
            {
                SDL.SDL_RenderCopy(Engine.Instance.myRenderer,
                    myTexture,
                    ref sourceRect,
                    ref destinationRect);
            }
        }

        public void LoadBmp(string inFilename, bool inIsAnimation = false)
        {
            // 접근 경로 커스텀
            string projectFolder = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            isAnimaion = inIsAnimation;
            string filePath = projectFolder + "\\Resources\\" ;
            // SDL C사용 C#은 접근할 수 있는 함수가 없음
            mySurface = SDL.SDL_LoadBMP(filePath.ToString() + inFilename);
            unsafe
            {
                SDL.SDL_Surface* surface = (SDL.SDL_Surface*)mySurface;
                SDL.SDL_SetColorKey(mySurface, 1, SDL.SDL_MapRGB(surface->format,
                                    colorKey.r, colorKey.g, colorKey.b));
            }

            // 메모리(힙)에 있는 데이터를 VRAM에 옮기는 작업
            myTexture = SDL.SDL_CreateTextureFromSurface(Engine.Instance.myRenderer, mySurface);
        }
    }
}
