using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _25._02._25_2D_Engine_4.Core
{
    class FileManager
    {
        int word = 8;
        private static FileManager instance;

        public static FileManager GetInstance()
        {
            if (instance == null)
            {
                instance = new FileManager();
            }
            return instance;
        }

        public string[] ReadFileByByte(string path)
        {
            string fullPath = GetFullPath(path);

            string scan = "";

            byte[] buffer = new byte[word];
            if (!File.Exists(fullPath))
            {
                Console.WriteLine("파일이 없음");
            }
            else
            {
                FileStream fs = new FileStream(fullPath, FileMode.Open);
                while (fs.Position < fs.Length)
                {
                    int temp = fs.Read(buffer, 0, (int)word);
                    scan += Encoding.UTF8.GetString(buffer, 0, temp);
                }
                fs.Close();
            }

            Console.WriteLine(scan);
            return scan.Split("\r\n");
        }

        public string[] ReadFileByStreamReader(string path)
        {
            string fullPath = GetFullPath(path);
            string scan = "";

            if (!File.Exists(fullPath))
            {
                Console.WriteLine("파일이 없음");
            }
            else
            {
                StreamReader sr = new StreamReader(fullPath);

                while (!sr.EndOfStream)
                {
                    scan += sr.ReadLine();
                }
                sr.Close();
            }

            return scan.Split("\r\n");
        }

        private string GetFullPath(string path)
        {
            // 프로젝트 폴더 경로 설정
            string projectRoot = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            // 프로젝트 폴더 기준으로 Data/level01.map 경로 설정
            return Path.Combine(projectRoot, path);
        }
    }
}
