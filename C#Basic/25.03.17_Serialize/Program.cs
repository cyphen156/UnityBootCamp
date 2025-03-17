using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace _25._03._17_Serialize
{
    class HelloWorld
    {
        public int Gold;
        public int HP;
        public int MP;
        public HelloWorld(int inGold = 100, int inHP = 100, int inMP = 100)
        {
            Gold = inGold;
            HP = inHP;
            MP = inMP;
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            HelloWorld h = new HelloWorld(0, 100, 20);

            StreamWriter sw = new StreamWriter("./data.dat");
            sw.WriteLine(h.Gold);
            sw.WriteLine(h.HP); 
            sw.WriteLine(h.MP);
            sw.Close();

            //Serialize
            StreamReader sr = new StreamReader("./data.dat");

            if (sr != null)
            {
                StreamWriter sw2 = new StreamWriter("./data2.dat");

                string DataGold = sr.ReadLine();
                string DataHP = sr.ReadLine();
                string DataMP = sr.ReadLine();

                HelloWorld h2 = new HelloWorld(int.Parse(DataGold), int.Parse(DataHP), int.Parse(DataMP));

                sw2.WriteLine(h2.Gold);
                sw2.WriteLine(h2.HP);
                sw2.WriteLine(h2.MP);

                sw2.Close();
                sr.Close();

            }

            // nuget 패키지를 통한 JSON 파일 읽기/쓰기

            string data = "{Gold : 10, HP : 20, MP : 30 }";
            JObject json = JObject.Parse(data);

            Console.WriteLine("Json :: ");
            Console.WriteLine(json);
            Console.WriteLine();
            
            Console.WriteLine("Tostring");
            Console.WriteLine(json.ToString());
            Console.WriteLine();

            Console.WriteLine("Get All Keys");
            Console.WriteLine(json.Properties());
            Console.WriteLine();

            Console.WriteLine("JSON Keys");
            foreach (var property in json.Properties())
            {
                Console.WriteLine(property.Name);  // Gold, HP, MP 출력
            }
            Console.WriteLine();

            Console.WriteLine("ContiansKey is returns bool");
            Console.WriteLine(json.ContainsKey("Gold"));
            Console.WriteLine();

            Console.WriteLine("value :: Gold");
            Console.WriteLine(json.Value<int>("Gold"));
            Console.WriteLine();



            Console.WriteLine( "--------------------------");
            string jsonData = JsonConvert.SerializeObject(h);
            Console.WriteLine(jsonData);
            HelloWorld h3 = JsonConvert.DeserializeObject<HelloWorld>(jsonData);

            Console.WriteLine(h3.MP);
        }
    }
}
