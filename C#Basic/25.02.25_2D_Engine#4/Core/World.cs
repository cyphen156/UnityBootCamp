namespace _25._02._25_2D_Engine_4.Core
{
    public class World
    {
        static private World instance;
        public List<GameObject> gameObjects = new List<GameObject>();
        int useGameObjectCount = 0;
        public int layers = 5;
        private World() { }
        public static World GetInstance()
        {
            if (instance == null)
            {
                instance = new World();
            }
            return instance;
        }

        public void Instantiate(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
            useGameObjectCount++;
        }

        public virtual void Update()
        {
            for (int i = 0; i < gameObjects.Count; ++i)
            {
                if (gameObjects[i] != null)
                {
                    gameObjects[i].Update();
                }
            }
        }

        public virtual void Render()
        {
            for (int i = 0; i < gameObjects.Count; ++i)
            {
                gameObjects[i].Render();
            }
            //for (int j = 0; j < layers; ++j)
            //{
            //    for (int i = 0; i < gameObjects.Count; ++i)
            //    {
            //        if (gameObjects[i] != null && gameObjects[i].layer == j)
            //        {
            //            gameObjects[i].Render();
            //        }
            //    }
            //}

            for (int Y = 0; Y < Engine.backBuffer.GetLength(0); ++Y)
            {
                for (int X = 0; X < Engine.backBuffer.GetLength(1); ++X)
                {
                    //if (Engine.frontBuffer[Y, X] != Engine.backBuffer[Y, X])
                    //{
                    //    Engine.frontBuffer[Y, X] = Engine.backBuffer[Y, X];
                        Console.SetCursorPosition(X, Y);
                        Console.Write(Engine.backBuffer[Y, X]);
                    //}
                }
            }

        }
        public List<GameObject> GetAllGameObjects()
        {
            return gameObjects;
        }
        public bool IsCollision(GameObject Origin, GameObject other)
        {
            Collider otherCollider = other.GetComponent<Collider>();
            if (Origin.GetComponent<Collider>().IsCollisionEnter(otherCollider))
            {
                Console.WriteLine("충돌함");
                return true;
            }
     
            return false;
        }

        public void Reset() 
        {
            useGameObjectCount = 0;
            for (int i = 0; i < gameObjects.Count; ++i)
            {
                gameObjects[i] = null;
            }
        }

        public void SelectionSort()
        {
            for (int i = 0; i < gameObjects.Count; ++i)
            {
                for (int j = i + 1; j < gameObjects.Count; ++j)
                {
                    if (gameObjects[i].layer - gameObjects[j].layer > 0)
                    {
                        GameObject temp = gameObjects[i];
                        gameObjects[i] = gameObjects[j];
                        gameObjects[j] = temp;
                    }
                }
            }
        }
    }
}
