using _2DEngine;
using System;
using System.Reflection;
using System.Text;

namespace _2DEngine
{
    public class Program
    {
        public static int Compare(GameObject first, GameObject second)
        {
            SpriteRenderer spriteRenderer1 = first.GetComponent<SpriteRenderer>();
            SpriteRenderer spriteRenderer2 = second.GetComponent<SpriteRenderer>();
            if (spriteRenderer1 == null || spriteRenderer2 == null)
            {
                return 0;
            }

            return spriteRenderer1.orderLayer - spriteRenderer2.orderLayer;
        }
        static void Main(string[] args)
        {
            Engine.Instance.Init();
            Engine.Instance.SetSortCompare(Compare);
            Engine.Instance.Load("Resources/level01.map");
            Engine.Instance.Run();

            Engine.Instance.Quit();
        }
    }
}