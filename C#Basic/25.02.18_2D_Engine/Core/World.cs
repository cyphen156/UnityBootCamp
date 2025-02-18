namespace _25._02._18_2D_Engine.Core
{
    public class World
    {
        static private World instance;
        public GameObject[] gameObjects = new GameObject[100];
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

        public void Instanciate(GameObject gameObject)
        {
            gameObjects[useGameObjectCount] = gameObject;
            useGameObjectCount++;
        }

        public virtual void Update()
        {
            for (int i = 0; i < gameObjects.Length; ++i)
            {
                if (gameObjects[i] != null)
                {
                    gameObjects[i].Update();
                }
            }
        }

        public virtual void Render()
        {
            for (int j = 0; j < layers; ++j)
            {
                for (int i = 0; i < gameObjects.Length; ++i)
                {
                    if (gameObjects[i] != null && gameObjects[i].layer == j)
                    {
                        gameObjects[i].Render();
                    }
                }
            }
        }

        public GameObject IsCollision(GameObject Origin)
        {
            foreach (GameObject go in gameObjects)
            {
                if (go is Player)
                {
                    continue;
                }
                Collider otherCollider = go.GetComponent<Collider>();
                if (otherCollider != null && Origin.GetComponent<Collider>().IsCollisionEnter(otherCollider))
                {
                    Console.WriteLine("충돌함");
                    return go;
                }
            }
            return null;
        }

        public void Reset() 
        {
            useGameObjectCount = 0;
            for (int i = 0; i < gameObjects.Length; ++i)
            {
                gameObjects[i] = null;
            }
        }
    }
}
