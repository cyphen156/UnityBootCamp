using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25._02._24.Core
{
    public class Goal : GameObject
    {
        public Goal(int inX, int inY, char inShape) : base(inX, inY, inShape)
        {
            layer = 3;
        }
        public void NextGame()
        {
            Console.WriteLine("다음 게임을 진행하세요");
            World.GetInstance().Reset();
            Console.WriteLine("월드를 초기화합니다.");
            Engine.GetInstance().Load();
        }
    }
}
