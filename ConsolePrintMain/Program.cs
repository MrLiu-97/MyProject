using ConsolePrintMain.practice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsolePrintMain
{
    class Program
    {
        static Program()
        {
            //这个绑定事件必须要在引用到TestLibrary1这个程序集的方法之前,注意是方法之前,不是语句之间,就算语句是在方法最后一行,在进入方法的时候就会加载程序集,如果这个时候没有绑定事件,则直接抛出异常,或者程序终止了
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }
        static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            //获取加载失败的程序集的全名
            var assName = new AssemblyName(args.Name).FullName;
            if (args.Name == "TestLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")
            {
                //读取资源
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ConsoleApplication5.TestLibrary1.dll"))
                {
                    var bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, (int)stream.Length);
                    return Assembly.Load(bytes);//加载资源文件中的dll,代替加载失败的程序集
                }
            }
            throw new DllNotFoundException(assName);
        }
        //程序进入方法之前会加载程序集,当程序集加载失败,则会进入CurrentDomain_AssemblyResolve事件

        public static void PlayGame()
        {
            PlayGame2048 playGame = new PlayGame2048();

            //playGame.CreatNewRandomNumber();
            //playGame.CreatNewRandomNumber();

            //Console.Clear();
            //for (int i = 0; i < playGame.Map.GetLength(0); i++)
            //{
            //    for (int j = 0; j < playGame.Map.GetLength(1); j++)
            //    {
            //        Console.Write(playGame.Map[i, j] + "\t");
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine("键盘 ↑↓←→ 控制移动");

            playGame.ConsolePlayGame();
            Console.WriteLine("GG");
            Console.ReadKey();

        }
        public void OnMouseDown()
        {
            int thrNumber = 64;
            ManualResetEvent[] manu = new ManualResetEvent[thrNumber];
            ManualResetEvent.WaitAll(manu);
        }
        static void Main(string[] args)
        {
           Practice2.Demo6();
            MySQLPractice.Demo1();
            int nIndex = 3 / 3;
            char a = 'a';
            char b = a;
            a = 'b';
            bool res1 = a == b;
            bool res2 = a.Equals(b);
            Practice2.Demo08();
            Practice2.Demo01();
            //Practice1.Timer();
            Practice1.StrStr();
            Practice1.ReverseString();
            Practice1.MoveZeroes();
            Practice1.LongestPalindrome();
            Practice1.ProductExceptSelf();

            Practice1.Multiply();
            //int nIndex = 3 / 2;
            Practice1.FindMedianSortedArrays();
            Practice1.StrStr("", "");
            Practice1.Merge();

            //PlayGame();
            Practice1.IsValid("");
            Practice1.LeetCodeDemo2(6);
            Practice1.Fun();
            Practice1.ClassPractice();

            Console.ReadKey();
            // Practice1.Printg();
            // //var test = new TestLibrary1.Test();
            // //test.Point();
            //Console.ReadLine();
            Practice1.PrintN();
            Practice1.GamePlay();
            Console.ReadKey();
            Practice1.DoubleArray();
            Console.ReadKey();
            //Console.ReadLine();
            //Practice1.StartGo();
            //Practice1.PrintI();
            //Practice1.PrintJ();
            Practice1.PrintSecursion();
            //Practice1.PrintL();
            Console.ReadKey();
            //Practice1.Printg();
            //Practice1.Printd();
            //Practice1.Testdd();

        }
    }
}