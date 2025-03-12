using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DEngine
{
    class GameManager : Component
    {
        public bool isGameOver = false;

        public bool isFinish = false;

        public override void Update()
        {
            if (isGameOver)
            {
                if (GameObject.Find("failObject") == null)
                {
                    Console.WriteLine("Failed");
                    GameObject failObject = new GameObject();
                    failObject.name = "failObject";
                    TextRenderer textRenderer = failObject.AddComponent<TextRenderer>();
                    textRenderer.color.r = 255;
                    textRenderer.color.g = 0;
                    textRenderer.color.b = 0;
                    textRenderer.transform.x = 10;
                    textRenderer.transform.y = 10;

                    textRenderer.SetText("실패");
                    Engine.Instance.world.Instanciate(failObject);
                }
            }

            if (isFinish)
            {
                if (GameObject.Find("successObject") == null)
                {
                    Console.WriteLine("Success");
                    GameObject successObject = new GameObject();
                    successObject.name = "successObject";
                    TextRenderer textRenderer = successObject.AddComponent<TextRenderer>();
                    textRenderer.color.r = 0;
                    textRenderer.color.g = 0;
                    textRenderer.color.b = 255;
                    textRenderer.transform.x = 10;
                    textRenderer.transform.y = 10;

                    textRenderer.SetText("성공");
                    Engine.Instance.world.Instanciate(successObject);
                }
            }
        }
    }
}
