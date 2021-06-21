
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;


namespace ConsolePrintMain.practice
{
    public class Practice1
    {
        public static void Printa()
        {
            // 一般可以利用DNS类的GetHostName 方法找到本地系统主机名,再用该类的GetHostByName方法找到主机的IP地址
            string localName = Dns.GetHostName();
            Console.WriteLine("主机名：{0}", localName);
            IPHostEntry localHost = Dns.GetHostEntry(localName);   //对于此类型，GetHostByName已过时，请改用GetHostEntry
            // 输出对应的 IP 地址
            foreach (IPAddress item in localHost.AddressList)
            {
                Console.WriteLine("IP 地址:", item.ToString());
            }

            // 使用 Parse方法创建IPAddress的实例
            IPAddress ip1 = IPAddress.Parse("192.168.1.1");
            Console.ReadKey();

            // 使用 IPEndPoint 类来指定 IP 地址与端口的组合
            IPAddress localIp = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEp = new IPEndPoint(localIp, 8000);
            Console.WriteLine($"The local IpEndPoint is:{localEp.ToString()}");
            Console.WriteLine($"The Address is:{localEp.Address}");
            Console.WriteLine($"The AddressFamily is:{localEp.AddressFamily}");
            Console.ReadKey();
        }

        public static void Printb()
        {
            StreamWriter sw = new StreamWriter("MyFile.txt", true, System.Text.Encoding.Unicode);
            sw.WriteLine("第一条语句。");
            sw.WriteLine("第二条语句。");
            sw.Close();
            StreamReader sr = new StreamReader("MyFile.txt", System.Text.Encoding.Unicode);
            while (sr.ReadLine() != null)
            {
                Console.WriteLine(sr.ReadLine());
            }
            sr.Close();
            Console.ReadLine();
            return;
            // NetworkStream类
            Socket netSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            NetworkStream network = new NetworkStream(netSocket);
            // 此后 程序将一直使用 NetStream 发送和接受网络数据,而不需要使用 Socket对象 NetSocket                                 

        }

        #region MyRegion

        public static void Printc()
        {
            /// Socket 类的基本使用
            /// 首先介绍套接字的创建方法,然后阐述套接字的属性与方法的使用。
            /// 1. 套接字的创建
            /// public Socket(
            /// AddressFamily addressFaimly,        // 网络类型
            /// SocketType socketType,              // 套接字类型
            /// ProtocolType protocolType           // 协议类型
            /// );
            /// 对于常规IP通信，AddressFamily 只能使用 AddressFamily.InterNetwork。
            /// SocketType 参数需要与ProtocolType 配合使用，其组合方式如下
            /// -----------------------------------------------------|
            /// SocketType:     |Dgram | Stream | Raw  |  Raw | 组合 |
            /// ProtocolType:   |Udp   | Tcp    | Icmp |  Raw | 方式 |
            /// -----------------------------------------------------|
            /// 
            // 流式套接字的创建
            Socket socket1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 数据报套接字的创建实例
            Socket socket2 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            // 原始套接字的创建实例   例如 设计Ping程序，需要使用ICMP 协议
            Socket socket3 = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp);

            /// 2. Bind(EndPoint address)
            /// 对于服务器程序,套接字必须绑定到本地 IP 地址和端口上,明确本地半相关。
            /// 3.Listen(int backlog)
            /// Listen()方法只用于面向连接的服务器方，其参数 backlog指出系统等待用户程序排队的连接数，及队列长度。
            /// 队列中的连接请求按照先入先出原则，被 Accept方法做接收处理。
            /// 4. Accept()
            /// Accept() 方法只用于面向连接的服务器方，在服务器进入监听状态后,程序执行到Accept()方法时会处于暂停状态，直到有客户请求连接。
            /// 一旦有了连接请求，则 Accept() 接受该请求，并返回一个新的套接字对象,该对象包含与该客户机通信的所有连接信息。
            /// 而最初创建的套接字仍然负责监听,并在需要时调用 Accept() 所以接受新的连接请求。
            /// 
            // 以下给出一段相应的服务器方程序:
            IPHostEntry iPHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            IPEndPoint iPEndPoint = new IPEndPoint(iPHostEntry.AddressList[0], 3456);

            Socket MySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            MySocket.Bind(iPEndPoint);
            MySocket.Listen(20);    // 队列长度为0
            Socket newSocket = MySocket.Accept();
            // 此后，新的套接字 newSocket就可以与客户机传输数据了

            /// 两种类型服务：
            /// 重复服务和并发服务。
            /// accept() 调用为实现并发服务提供了极大的方便,就是因为他会返回一个新的套接字号。
            /// 将以上最后一条修改为：
            for (; ; )
            {
                Socket newSocket1 = MySocket.Accept();  // 阻塞
                if (CreateThread())     // 创建线程
                {
                    MySocket.Close();
                    Do(newSocket1);     // 处理请求 
                    newSocket1.Close();
                }
            }

        }
        public static bool CreateThread()
        {
            return true;
        }
        public static void Do(Socket socket)
        {

        }
        #endregion

        public static void Printd()
        {
            /// Connect(EndPoint remoteEP)
            /// Connect() 是面向连接调用的客户方,主动向服务器方发送连接请求，其参数需要指定服务器方的IP 地址和端口组合。
            /// 调用 Connect() 方法后,客户机套接字将一直阻塞到连接建立；如果连接不成功，则返回异常
            /// 
            IPAddress remoteHost = IPAddress.Parse("39.100.100.20");
            IPEndPoint remoteEP = new IPEndPoint(remoteHost, 3389);
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sock.Connect(remoteEP);
            // 关闭连接的典型用法:
            sock.Shutdown(SocketShutdown.Both);
            sock.Close();
        }

        // 套接字的简单应用实例
        public static void Printe()
        {
            // 主要代码
            int dataLength;
            byte[] dataBytes = new byte[1024];
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            // 参数指定本机IP 地址（此处指所有可用的IP 地址）参数指定接收用的端口
            IPEndPoint myHost = new IPEndPoint(IPAddress.Any, 8080);
            // 将本机IP 地址和端口与套接字绑定,为接收做准备
            socket.Bind(myHost);
            // 定义远程 IP地址和端口(实际使用时应为远程主机IP地址)，为发送数据做准备
            IPEndPoint remoteIPEnd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
            // 从 IPEndPoint 得到 EndPoint类型
            EndPoint remotoHost = remoteIPEnd;
            Console.WriteLine("输入发送的消息：");
            string tmpStr = Console.ReadLine();
            // 字符串转换为字节数组
            dataBytes = System.Text.Encoding.Unicode.GetBytes(tmpStr);
            // 向远程终端发送信息
            socket.SendTo(dataBytes, dataBytes.Length, SocketFlags.None, remotoHost);
            while (true)
            {
                Console.WriteLine("等待接收...");

                // 从本地绑定的IP地址和端口接收远程终端的数据，返回接收的字节数
                dataLength = socket.ReceiveFrom(dataBytes, ref remotoHost);

                // 字节数组转换为字符串
                tmpStr = System.Text.Encoding.Unicode.GetString(dataBytes, 0, dataLength);
                Console.WriteLine("接收到信息：{0}", tmpStr);

                // 如果收到的消息是" exit",则退出循环
                if (tmpStr == "exit") break;


                Console.WriteLine("输入回送信息（exit退出）:");
                tmpStr = Console.ReadLine();
                dataBytes = System.Text.Encoding.Unicode.GetBytes(tmpStr);
                socket.SendTo(dataBytes, remotoHost);
            }
            //关闭套接字
            socket.Close();
            Console.WriteLine("对方已经推出了,请按 enter 键结束");
            Console.ReadLine();
            var a = Console.ReadKey();
        }


        public static void Printg()
        {

            //int[] nums = new int[] { 1, 1, 5, 6 };
            //Printf(nums, 2);
            //Printh(nums);
            //PrintI("aaaaa", "bba");
            //练习∶
            //在控制台中，录入枪的信息I请输入枪的名称: "
            //I请输入弹匣容量:”
            //I""请输入当前弹匣内子弹数量:”
            //I请输入剩余子弹数量:”
            //ll在一行显示∶
            //ll枪的名称是: xxx,弹匣容量: XXx,弹匣子弹数∶xx,剩余子弹数: Xx。
            Console.WriteLine("请输入枪支名称：");
            string gunName = Console.ReadLine();
            Console.WriteLine("请输入弹夹容量：");
            string Bullet_clip = Console.ReadLine();
            Console.WriteLine("请输入当前弹匣内子弹数量:");
            string Number_of_bullets = Console.ReadLine();
            Console.WriteLine("请输入剩余子弹数量:");
            string Bullet_surplus = Console.ReadLine();
            Console.WriteLine("枪的名称是: " + gunName + ",弹匣容量: " + Bullet_clip + ",弹匣子弹数∶" + Number_of_bullets + ",剩余子弹数: " + Bullet_surplus + "。");
            Console.ReadKey();
        }
        public static void PrintI(string haystack, string needle)
        {
            if (needle == "") return;
            int index = haystack.IndexOf(needle);
            Console.WriteLine(index);
        }
        public static void Printh(int[] nums)
        {
            if (nums.Length == 0)
            {
                return;
            }
            int j = 0;
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[j] != nums[i])
                {
                    j++;
                    nums[j] = nums[i];
                }
            }
            Console.WriteLine(j);
            Console.WriteLine(j + 1);
            //var arr= nums.Distinct().ToArray();
            //nums = arr;
            //Array.Copy(nums,arr,)
        }
        // 多线程扫描程序
        public static int Printf(int[] nums, int k)
        {
            //ArrayList array = new ArrayList();
            //array.Add(1);
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] > k) return i;
                if (nums[i] == k) return i;
            }
            return nums.Length;
            {
                int index = Array.IndexOf(nums, k);
                int[] arr1 = new int[nums.Length + 1];
                if (index == -1)
                {
                    Array.Copy(nums, arr1, nums.Length);
                    arr1[arr1.Length - 1] = k;
                    Array.Sort(arr1);
                    index = Array.IndexOf(arr1, k);
                    return index;
                }
                return index;
            }
            //Console.WriteLine(index);
            return 0;
            int temp = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == k)
                {
                    temp = i + 1;
                }
            }
            if (temp == 0)
            {
                for (int i = 0; i < nums.Length; i++)
                {
                    if (nums[i] > k)
                    {
                        nums[i] = k;
                        Array.IndexOf(nums, k);
                    }
                }
            }
            //return;
            //System.Configuration.IConfiguration
            int setThreadNum = 10;

            // 设置最大线程数
            ThreadPool.SetMaxThreads(setThreadNum, setThreadNum);
            //for (int i = 0; i < length; i++)
            //{

            //}
        }

        #region 计算税收 
        public static void PrintI()
        {
            float height = 100f;
            int count = 0;
            float distinct = height;
            while (height / 2 >= 0.01f)
            {

                height /= 2;
                distinct += height * 2;
                count++;
                Console.WriteLine("第{0}次，结果:{1}", count, height);
            }
            Console.WriteLine("总计{0}次,累计距离{1}", count, distinct);
            return;
            // 新的个人所得税计算方法具体如下： 
            // ①	 计算应纳税所得额。
            // 应纳税所得额 = 税前薪资总额 - 社会保险和住房公积金的个人缴纳部分(500) - 费用扣除标准（3,500元）。 

            // ②	 个人所得税应纳税额 = 应纳税所得额 * 税率 - 速算扣除数
            //如: 税前工资总额 10000 应纳税所得额 = 10000 - 500 - 3500 = 6000
            //   纳税额 = 6000 * 0.2 – 555 = 645

            Console.Title = "个人所得税计算";
            Console.WriteLine("请输入员工工资：");
            string salaryStr = Console.ReadLine();

            decimal salaryInt = 0;  // 税前薪资总额

            while (!decimal.TryParse(salaryStr, out salaryInt) || salaryInt < 0)
            {
                Console.WriteLine("工资有误,请重新输入!");
                salaryStr = Console.ReadLine();
            }

            decimal insurance = 500M;     //保险及公积金
            // 应纳税所得额    = 税前薪资总额 - 社会保险和住房公积金的个人缴纳部分(500) - 费用扣除标准（3,500元）。 
            decimal income_tax = salaryInt - insurance - 3500M;
            decimal After_salary = 0;     // 个税
            if (income_tax > 0)
            {
                After_salary = Money(income_tax);
            }

            //After_salary = salaryInt - After_salary;
            //Console.WriteLine("该员工税后应得工资为：" + After_salary);

            Console.WriteLine("实收:{0} 个税:{1} 保险金：{2}", salaryInt - After_salary - insurance, After_salary, insurance);
        }

        public static void PrintJ()
        {
            Console.Write("请输入税前收入：");
            //税前薪资
            double salary = double.Parse(Console.ReadLine());
            //保险及公积金
            double insurance = 500;
            //需要交税的金额
            double balance = salary - 3500 - insurance;

            //实际交纳的税金
            double tax = 0;
            //税率
            double taxRate = 0;
            //扣除数
            double deduct = 0;

            //根据应纳税所得额 计算税率和速算扣除数
            if (balance > 0)
            {
                if (balance <= 1500)
                {
                    taxRate = 0.03;
                    deduct = 0;
                }
                else if (balance <= 4500)
                {
                    taxRate = 0.1;
                    deduct = 105;
                }
                else if (balance <= 9000)
                {
                    taxRate = 0.2;
                    deduct = 555;
                }
                else if (balance <= 35000)
                {
                    taxRate = 0.25;
                    deduct = 1005;
                }
                else if (balance <= 55000)
                {
                    taxRate = 0.3;
                    deduct = 2755;
                }
                else if (balance <= 80000)
                {
                    taxRate = 0.35;
                    deduct = 5505;
                }
                else
                {
                    taxRate = 0.45;
                    deduct = 13505;
                }
                //个人所得税应纳税额=应纳税所得额*税率-速算扣除数 
                tax = balance * taxRate - deduct;
            }
            Console.WriteLine("实收:{0} 个税:{1} 保险金：{2}", salary - tax - insurance, tax, insurance);
        }

        public static void PrintK()
        {
            //猜数字
            // 程序产生一个 1到100之间的随机数
            // 让玩家重复猜测直到猜对位置
            // "大了" "小了" "恭喜，猜对了，总共猜了?次。"
            //Console.WriteLine("请输入数字吧,康康你猜的对不对\n");
            //Console.Write("请输入（1-10）任意的数字：");
            //string numStr = Console.ReadLine();
            //int num = 0;
            //while (!int.TryParse(numStr, out num) || num < 1)
            //{

            //    Console.WriteLine("数字一定填（1-10）才行哦!\n");
            //    Console.Write("请输入（1-10）任意的数字：");
            //    numStr = Console.ReadLine();
            //}
            string Again = "N";
            do
            {
                Random random = new Random();         // 开始创建随机数
                int randomNum = random.Next(1, 11);     // 创建一个 1-10 的任意随机数
                int count = 0;
                int num = 0;
                Console.WriteLine("请输入数字吧,康康你猜的对不对\n");
                Console.WriteLine("请输入（1-10）任意的数字：");
                string numStr = Console.ReadLine();
                do
                {
                    while (!int.TryParse(numStr, out num) || num < 1)
                    {

                        Console.WriteLine("数字一定填（1-10）才行哦!");
                        Console.WriteLine("请输入（1-10）任意的数字：");
                        numStr = Console.ReadLine();
                    }
                    if (num < randomNum)
                    {
                        Console.WriteLine("猜小了哦!");
                        numStr = Console.ReadLine();
                    }
                    else if (num > randomNum)
                    {
                        Console.WriteLine("猜大了哦!");
                        numStr = Console.ReadLine();
                    }

                    count++;
                } while (num != randomNum);
                Console.WriteLine("恭喜你猜对了!可以没有奖励哈!");
                Console.WriteLine("总计猜数{0}次", count);
                Console.WriteLine("\n\n\n\n");
                Console.WriteLine("是否重开一局？(Y/N)：");
                Again = Console.ReadLine();
            } while (Again == "Y");
            Console.WriteLine("游戏已退出!");
            Console.ReadKey();
        }
        public static decimal Money(decimal income_tax)
        {
            ////保险及公积金
            //decimal insurance = 500M;
            //// 应纳税所得额    = 税前薪资总额 - 社会保险和住房公积金的个人缴纳部分(500) - 费用扣除标准（3,500元）。 
            //decimal income_tax = salaryInt - insurance - 3500M;

            decimal individual_income_tax = 0; //个人所得税应纳税额

            // 税率
            if (income_tax <= 1500)  // 应纳税所得额 不超过 1500元 的, 税率为 3%, 速算扣除数 0￥(元)
            {
                // 个人所得税应纳税额 = 应纳税所得额 * 税率 - 速算扣除数
                individual_income_tax = income_tax * (3M / 100M) - 0M;
            }
            else if (income_tax <= 4500)          // 应纳税所得额 超过 1500元 至 4500元 的, 税率为 10%, 速算扣除数 105￥(元)
            {
                individual_income_tax = income_tax * (10M / 100M) - 105M;
            }
            else if (income_tax <= 9000)          // 应纳税所得额 超过 4500元 至 9000元 的, 税率为 20%, 速算扣除数 555￥(元)
            {
                individual_income_tax = income_tax * (20M / 100M) - 555M;
            }
            else if (income_tax <= 35000)           // 应纳税所得额 超过 9000元 至 35000元 的, 税率为 25%, 速算扣除数 1005￥(元)
            {
                individual_income_tax = income_tax * (25M / 100M) - 1005M;
            }
            else if (income_tax <= 55000)           // 应纳税所得额 超过 35000元 至 55000元 的, 税率为 30%, 速算扣除数 2755￥(元)
            {
                individual_income_tax = income_tax * (30M / 100M) - 2755M;
            }
            else if (income_tax <= 80000)           // 应纳税所得额 超过 55000元 至 80000元 的, 税率为 35%, 速算扣除数 5505￥(元)
            {
                individual_income_tax = income_tax * (35M / 100M) - 5505M;
            }
            else
            {
                //  超过 80000元 的, 税率为 45%, 速算扣除数 13505￥(元)
                individual_income_tax = income_tax * (45M / 100M) - 13505M;
            }
            return individual_income_tax;
        }
        #endregion

        #region 冒泡排序&选择排序 
        public static void PrintSort()
        {
            int[] afterSort = ArraySort(new int[] { 3, 2, 1, 6, 4, 7, 5 });
        }
        public static int[] ArraySort(int[] array)
        {
            int temp = 0;
            for (int i = 0; i < array.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[minIndex] > array[j])
                    {
                        minIndex = j;
                    }
                    //if (array[i] > array[j])
                    //{
                    //    temp = array[i];
                    //    array[i] = array[j];
                    //    array[j] = temp;
                    //}
                }
                if (minIndex != i)
                {
                    temp = array[i];
                    array[i] = array[minIndex];
                    array[minIndex] = temp;
                }
            }
            return array;
        }
        #endregion

        #region 彩票生成系统 

        //Console.WriteLine("请输入篮球号码:");
        //int blueBall = int.Parse(Console.ReadLine());
        //while (blueBall < 1 || blueBall > 16)
        //{
        //    Console.WriteLine("号码不能超过1--16,请重新选择!");
        //    blueBall = int.Parse(Console.ReadLine());
        //}
        //*红球: 1--- 33 6个不能重复
        //  蓝球:1--16 1个

        // (1)在控制台中购买彩票的方法 
        //    "请输入第1个红球号码”
        //    "号码不能超过1--33""当前号码已经存在"

        // (2)随机产生一注彩票的方法 int[7]
        //         random.Next(1,34)
        //    要求:红球号码不能重复，且按照从小到大排序

        // (3)两注彩票比较的方法，返回中奖等级
        //     *先计算红球、蓝球中奖数量
        //     *在Main中测试  */
        ////备注:本行语句放到方法外类内
        ////static Random random = new Random();

        public static void StartGo()
        {
            int[] MyLottery = BuyOptionCaipiao();

            int winLever = 0;
            int count = 0;
            do
            {
                count++;

                int[] autoCreateLottery = MachineSelectBall();

                winLever = CompareLottery(MyLottery, autoCreateLottery);
                if (winLever != 0)
                {
                    Console.WriteLine("恭喜你中了{0}等奖,OMG!!!NB,累计消费{1:c}元\t", winLever, count * 2);

                }
                //Console.Write("本次花费{0:c}元", count * 2);
            } while (winLever != 1);
            Console.WriteLine("累计购买次数:" + count);
            Console.WriteLine("OGM!天啦撸!!!欧皇附体啊!一等奖!哦买嘎扥");
            Console.ReadLine();
        }
        public static int[] BuyOptionCaipiao()
        {
            int[] OptionalBall = new int[7];
            // 红球
            for (int i = 0; i < 6;)
            {
                Console.WriteLine("请输入第{0}个红球号码:", i + 1);
                int redBall = int.Parse(Console.ReadLine());

                if (redBall < 1 || redBall > 33)
                    Console.WriteLine("号码不能超过1--33,请重新选择!");
                else if (Array.IndexOf(OptionalBall, redBall) >= 0)
                    Console.WriteLine("当前号码已经存在,请重选!");
                else
                    OptionalBall[i++] = redBall;

            }
            //篮球 
            while (true)
            {
                Console.WriteLine("请输入篮球号码:");
                int blueBall = int.Parse(Console.ReadLine());
                if (blueBall >= 1 && blueBall <= 16)
                {
                    OptionalBall[6] = blueBall;
                    break;
                }
                else
                    Console.WriteLine("号码不能超过1--16,请重新选择!");
            }

            #region 废弃 
            //for (int i = 0; i < OptionalBall.Length;)
            //{
            //    while (i < OptionalBall.Length - 1)
            //    {
            //        Console.WriteLine("请输入第{0}个红球号码:", i + 1);
            //        int redBall = int.Parse(Console.ReadLine());
            //        if (redBall >= 1 && redBall <= 33)
            //        {
            //            if (Array.IndexOf(OptionalBall, redBall) >= 0)
            //                Console.WriteLine("当前号码已经存在,请重选!");
            //            else
            //                OptionalBall[i++] = redBall;
            //        }
            //        else
            //        {
            //            Console.WriteLine("号码不能超过1--33,请重新选择!");
            //        }
            //    }

            //    Console.WriteLine("请输入篮球号码:");
            //    int blueBall = int.Parse(Console.ReadLine());
            //    if (blueBall >= 1 && blueBall <= 16)
            //    {
            //        //Array.Sort(OptionalBall);
            //        OptionalBall[i++] = blueBall;
            //    }
            //    else
            //    {
            //        Console.WriteLine("号码不能超过1--16,请重新选择!");
            //    }
            //}
            #endregion

            Console.WriteLine("自选双色球号码为:");
            for (int i = 0; i < OptionalBall.Length; i++)
            {
                Console.Write(OptionalBall[i] + "\t");
            }

            Console.WriteLine("\n双色球开奖结果!");
            int[] ballResult = MachineSelectBall();
            Console.WriteLine("红球：");
            for (int i = 0; i < ballResult.Length - 1;)
            {
                Console.Write(ballResult[i++] + "\t");
            }

            Console.WriteLine("\n篮球：\n" + ballResult[6]);
            Console.WriteLine();
            Console.WriteLine();
            return OptionalBall;
        }

        static Random random = new Random();
        // 自动生成机选彩票
        public static int[] MachineSelectBall()
        {
            int[] autoBall = new int[7];
            // 随机红球
            for (int i = 0; i < 6;)
            {
                int redBall = random.Next(1, 34);
                if (Array.IndexOf(autoBall, redBall) < 0)
                {
                    autoBall[i++] = redBall;
                }
            }
            Array.Sort(autoBall, 0, 6); // autoBall.Length - 1
            //随机蓝球
            autoBall[6] = random.Next(1, 17);

            #region 废弃
            //for (int i = 0; i < autoBall.Length;)
            //{
            //    while (i < autoBall.Length - 1)
            //    {
            //        int redBall = random.Next(1, 34);
            //        while (Array.IndexOf(autoBall, redBall) >= 0)
            //        {
            //            redBall = random.Next(1, 34);
            //        }
            //        autoBall[i++] = redBall;
            //    }
            //    Array.Sort(autoBall, 0, autoBall.Length - 1);
            //    autoBall[i++] = random.Next(1, 17);
            //}
            #endregion

            return autoBall;
        }
        // 自选彩 机选彩 比较
        public static int CompareLottery(int[] optionLottery, int[] autoLottery)
        {
            int redBallWinNum = 0;
            // 先比较红球
            for (int i = 0; i < 6; i++)
            {
                if (Array.IndexOf(autoLottery, optionLottery[i], 0, 6) >= 0)
                {
                    redBallWinNum++;
                }
            }
            // 比较 蓝球
            int blueBallWinNum = optionLottery[6] == autoLottery[6] ? 1 : 0;

            // 获奖等级
            int level = 0;
            if (redBallWinNum + blueBallWinNum == 7)
                level = 1;
            else if (redBallWinNum == 6)
                level = 2;
            else if (redBallWinNum + blueBallWinNum == 6)
                level = 3;
            else if ((redBallWinNum + blueBallWinNum == 5) || redBallWinNum == 5)
                level = 4;
            else if ((redBallWinNum + blueBallWinNum == 4) || redBallWinNum == 4)
                level = 5;
            else if (blueBallWinNum == 1)   // 六等奖 二红一蓝 或者 一红一蓝 或者 一蓝 通过发现 三者都有 一蓝球 就可以中六等奖
                level = 6;
            else
                level = 0;
            return level;
        }

        #region 废弃

        //public static int[] MatchBall(int[] optionBall, int[] autoBall)
        //{
        //    int[] NumberofBall = new int[2];
        //    int redBallWinNum = 0;
        //    int blueBallWinNum = 0;
        //    //Array.IndexOf(autoBall,1,0,)
        //    for (int i = 0; i < optionBall.Length;)
        //    {
        //        while (i < optionBall.Length - 1)
        //        {
        //            if (Array.IndexOf(autoBall, optionBall[i], 0, autoBall.Length - 1) >= 0)
        //            {
        //                redBallWinNum++;
        //            }
        //            i++;
        //        }
        //        if (Array.IndexOf(autoBall, optionBall[i++], autoBall.Length - 1) >= 0)
        //            blueBallWinNum++;
        //    }
        //    NumberofBall[0] = redBallWinNum;
        //    NumberofBall[1] = blueBallWinNum;
        //    return NumberofBall;
        //}

        //public static void FinshWinnerResult(int[] winResult)
        //{

        //    if (winResult[0] + winResult[1] == 6)
        //    {
        //        Console.WriteLine("哦!天呐!!恭喜你中了一等奖!!!奖金：1个亿!!!");
        //    }
        //    else if (winResult[0] == 6 && winResult[1] == 0)
        //    {
        //        Console.WriteLine("哦!天呐!恭喜你中了二等奖!!!奖金：500万!!!");
        //    }
        //    else if (winResult[0] == 5 && winResult[1] == 1)
        //    {
        //        Console.WriteLine("恭喜你中了三等奖!!!奖金：3000元!");
        //    }
        //    else if ((winResult[0] == 5 && winResult[1] == 0) || (winResult[0] == 4 && winResult[1] == 1))
        //    {
        //        Console.WriteLine("恭喜你中了四等奖!!!奖金：200元!");
        //    }
        //    else if ((winResult[0] == 4 && winResult[1] == 0) || (winResult[0] == 3 && winResult[1] == 1))
        //    {
        //        Console.WriteLine("恭喜你中了四等奖!!!奖金：10块钱!");
        //    }
        //    else if ((winResult[0] == 2 && winResult[1] == 1) || (winResult[0] == 1 && winResult[1] == 1) || (winResult[0] == 0 && winResult[1] == 1))
        //    {
        //        Console.WriteLine("恭喜你中了五等奖!!!奖金：5块钱!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("哦天哪!很遗憾!你没有中奖!下次再接再厉哦！");
        //    }
        //}

        #endregion

        #endregion

        public static void DoubleArray()
        {
            /*       科目一    科目二     
             * 学生一
             * 学生二
             * 学生三
             */

            Console.WriteLine("请输入学生数：");
            int studentNum = int.Parse(Console.ReadLine());
            Console.WriteLine("请输入科目数：");
            int subjectNum = int.Parse(Console.ReadLine());

            int[,] scoreTable = new int[studentNum, subjectNum];
            for (int i = 0; i < scoreTable.GetLength(0); i++)
            {
                for (int j = 0; j < scoreTable.GetLength(1); j++)
                {
                    Console.WriteLine("请输入学生{0}的科目{1}成绩", i + 1, j + 1);
                    scoreTable[i, j] = int.Parse(Console.ReadLine());
                }
            }

            for (int i = 0; i < scoreTable.GetLength(0); i++)
            {
                Console.Write("学生{0}成绩:", i);
                for (int j = 0; j < scoreTable.GetLength(1); j++)
                {
                    Console.Write(scoreTable[i, j] + "\t");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        class Temp
        {
            public void Tests()
            {
                string Path_sypf = string.Empty;
                int width = 0, height = 0, padding_left = 0, padding_top = 0;
                width = 187; height = 35; padding_left = 22; padding_top = 43;
                Path_sypf = "/Upload/Logo/DFUSE.png";

                string FilePath = "";// Image_JD.localPath;
                string FilePath_JD = string.Empty;
                string ProgramFolder = AppDomain.CurrentDomain.BaseDirectory;

                //缩放

                System.Drawing.Image image = System.Drawing.Image.FromFile(ProgramFolder + FilePath).GetThumbnailImage(800, 800, () => { return false; }, IntPtr.Zero);

                Graphics g1 = Graphics.FromImage(image);
                {
                    //logo
                    System.Drawing.Image sbmp = System.Drawing.Image.FromFile(ProgramFolder + Path_sypf);
                    //缩放
                    System.Drawing.Image copyImage = sbmp.GetThumbnailImage(width, height, () => { return false; }, IntPtr.Zero);
                    //加水印
                    Graphics g = Graphics.FromImage(image);
                    Rectangle rect = new Rectangle(padding_left, padding_top, copyImage.Width, copyImage.Height);
                    g.DrawImage(copyImage, rect, 0, 0, copyImage.Width, copyImage.Height, GraphicsUnit.Pixel);
                    g.Dispose();
                    image.Save(ProgramFolder + FilePath_JD, System.Drawing.Imaging.ImageFormat.Jpeg);
                    copyImage.Dispose();
                }
            }
        }

        #region 2048
        public static void GamePlay()
        {
            /*
             *  0   0   0   0 
             *  0   2   0   8
             *  0   0   4   0
             *  0   0   0   0
             */
            int[,] tableCosole = new int[4, 4];

            int rowIndex1 = RandomIndex();
            int rowIndex2 = RandomIndex();
            int colIndex1 = RandomIndex();
            int colIndex2 = RandomIndex();

            tableCosole[rowIndex1, colIndex1] = colIndex1 == 1 ? 4 : 2;
            // 当两个列重复 重新生成新列索引
            while (colIndex1 == colIndex2)
            {
                colIndex2 = RandomIndex();
            }
            tableCosole[rowIndex2, colIndex2] = colIndex2 == 1 ? 4 : 2;
            while (true)
            {

                Console.Clear();

                for (int i = 0; i < tableCosole.GetLength(0); i++)
                {
                    for (int j = 0; j < tableCosole.GetLength(1); j++)
                    {
                        Console.Write(tableCosole[i, j] + "\t");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("开始移动键盘（↑↓←→）控制移动");
                var keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        tableCosole = MoveUpTable(tableCosole);
                        break;
                    case ConsoleKey.DownArrow:
                        tableCosole = MoveDownTable(tableCosole);
                        break;
                    case ConsoleKey.LeftArrow:
                        tableCosole = MoveLeftTable(tableCosole);
                        break;
                    case ConsoleKey.RightArrow:
                        tableCosole = MoveRightTable(tableCosole);
                        break;

                    default:
                        break;
                }

                //int count = 0;
                //rowIndex2 = RandomIndex();
                //colIndex2 = RandomIndex();
                //while (tableCosole[rowIndex2, colIndex2] != 0)
                //{

                //    colIndex2 = RandomIndex();
                //    count++;
                //    if (count == 4)
                //    {
                //        rowIndex2 = RandomIndex();
                //        count = 0;
                //        //break;
                //    }

                //}
                List<IndexClass> listDoubleArray = new List<IndexClass>();
                for (int i = 0; i < tableCosole.GetLength(0); i++)
                {
                    for (int j = 0; j < tableCosole.GetLength(1); j++)
                    {
                        if (tableCosole[i, j] == 0)
                        {
                            listDoubleArray.Add(new IndexClass(i, j));
                            //= RandomIndex() == 1 ? 4 : 2;
                        }
                    }
                }
                if (listDoubleArray.Count > 0)
                {
                    int arrayIndex = randoms.Next(0, listDoubleArray.Count);
                    IndexClass indexClass = listDoubleArray[arrayIndex];
                    tableCosole[indexClass.rowIndex, indexClass.colIndex] = RandomIndex() == 1 ? 4 : 2;

                    listDoubleArray.RemoveAt(arrayIndex);
                }
                else
                {
                    Console.WriteLine("游戏结束!");
                    break;
                }

            }
            Console.ReadKey();

        }
        public class IndexClass
        {
            public IndexClass(int rowIndex, int colIndex)
            {
                this.rowIndex = rowIndex;
                this.colIndex = colIndex;
            }
            public int rowIndex { get; set; }
            public int colIndex { get; set; }
        }
        public static int[] RandomIndexRandomRowIndex()
        {

            Random random = new Random();
            //int rowIndex1 = random.Next(0, 5);
            //int rowIndex2 = random.Next(0, 5);
            int[] rowArray = { random.Next(0, 4), random.Next(0, 4) };
            return rowArray;
        }
        static Random randoms = new Random();
        public static int RandomIndex()
        {
            return randoms.Next(0, 4);
        }

        public static int[,] MoveUpTable(int[,] tableCosole)
        {
            int[] colValueArray = new int[tableCosole.GetLength(1)];

            for (int i = 0; i < tableCosole.GetLength(1); i++)
            {
                for (int j = 0; j < tableCosole.GetLength(0); j++)
                {
                    colValueArray[j] = tableCosole[j, i];
                }

                int[] addScore = AddScore(colValueArray);

                for (int k = 0; k < tableCosole.GetLength(0); k++)
                {
                    tableCosole[k, i] = addScore[k];
                }
                //addScore = 
            }
            return tableCosole;
        }
        public static int[,] MoveDownTable(int[,] tableCosole)
        {

            int[] colValueArray = new int[tableCosole.GetLength(1)];
            for (int i = 0; i < tableCosole.GetLength(1); i++)
            {
                for (int j = tableCosole.GetLength(0) - 1; j >= 0; j--)
                {
                    colValueArray[3 - j] = tableCosole[j, i];
                }
                int[] addScore = AddScore(colValueArray);
                int index = 0;
                for (int k = tableCosole.GetLength(0) - 1; k >= 0; k--)
                {
                    tableCosole[k, i] = addScore[index++];
                }
            }
            return tableCosole;
        }

        public static int[,] MoveLeftTable(int[,] tableCosole)
        {
            int[] colValueArray = new int[tableCosole.GetLength(1)];
            for (int i = 0; i < tableCosole.GetLength(0); i++)
            {
                for (int j = 0; j < tableCosole.GetLength(1); j++)
                {
                    colValueArray[j] = tableCosole[i, j];
                }
                int[] addScore = AddScore(colValueArray);
                for (int k = 0; k < tableCosole.GetLength(1); k++)
                {
                    tableCosole[i, k] = addScore[k];
                }

            }
            return tableCosole;
        }
        public static int[,] MoveRightTable(int[,] tableCosole)
        {
            int[] colValueArray = new int[tableCosole.GetLength(1)];
            for (int i = 0; i < tableCosole.GetLength(0); i++)
            {
                for (int j = tableCosole.GetLength(1) - 1; j >= 0; j--)
                {
                    colValueArray[3 - j] = tableCosole[i, j];
                }
                int[] addScore = AddScore(colValueArray);
                int index = 0;
                for (int k = tableCosole.GetLength(1) - 1; k >= 0; k--)
                {
                    tableCosole[i, k] = addScore[index++];
                }
            }
            return tableCosole;
        }
        public static int[] AddScore(int[] colValueArray)
        {
            // 开始前先清零
            ClearZero(colValueArray);
            // 将每个列相同的值相加
            for (int i = 0; i < colValueArray.Length - 1; i++)
            {
                /* 0
                 * 2    4
                 * 2    0
                 * 4    4
                 */
                // 如果第一列 不为零并且 第一列等于第二列 就相加 并将 第二列重置为零
                if (colValueArray[i] != 0 && colValueArray[i] == colValueArray[i + 1])
                {
                    colValueArray[i] += colValueArray[i + 1];
                    colValueArray[i + 1] = 0;
                }

            }
            // 防止计算完中间有零 在清一次零
            ClearZero(colValueArray);
            return colValueArray;
        }

        public static void ClearZero(int[] colValueArray)
        {
            int[] removeZeroArray = new int[colValueArray.Length];

            // 如果每个列中间有0相隔 则去除
            int index = 0;
            for (int i = 0; i < colValueArray.Length; i++)
            {
                if (colValueArray[i] != 0)
                {
                    removeZeroArray[index++] = colValueArray[i];
                }
            }
            removeZeroArray.CopyTo(colValueArray, 0);
            //removeZeroArray.CopyTo(colValueArray, 0);
            //Array.Copy(removeZeroArray, colValueArray, removeZeroArray.Length);
            //return removeZeroArray;
        }
        #endregion

        #region LeetCode练习

        //public void FormsAuthen(Maticsoft.Model.tbl_admin model)
        //{
        //    bool createPersistentCookie = false;
        //    //将票据写入到验证中去，表明已经登录
        //    System.Web.HttpCookie authCookie = System.Web.Security.FormsAuthentication.GetAuthCookie(model.account, createPersistentCookie);
        //    System.Web.Security.FormsAuthenticationTicket ticket = System.Web.Security.FormsAuthentication.Decrypt(authCookie.Value);

        //    System.Web.Security.FormsAuthenticationTicket newTicket = new System.Web.Security.FormsAuthenticationTicket(ticket.Version,
        //        ticket.Name,
        //        ticket.IssueDate,
        //        ticket.Expiration,
        //        ticket.IsPersistent,
        //        model.id.ToString());
        //    authCookie.Value = System.Web.Security.FormsAuthentication.Encrypt(newTicket);
        //    //System.Web.Mvc.Response.Cookies.Add(authCookie);

        //    //System.Web.HttpCookie otherCookie = new System.Web.HttpCookie("erpAuth");
        //    //otherCookie.Domain = "xqlshop.com";
        //    //string strJson = "{{Mid:\"{0}\",Pid:\"{1}\"}}".format(model.account_md5, model.password_md5);
        //    //otherCookie.Value = Maticsoft.DBUtility.DESEncrypt.Encrypt(strJson, "xql_shop");
        //    //Response.Cookies.Add(otherCookie);
        //}


        public static void MoveZeroes()
        {
            {
                //有效的数独
                //请你判断一个9x9 的数独是否有效。只需要 根据以下规则 ，验证已经填入的数字是否有效即可。

                //数字1 - 9在每一行只能出现一次。
                //数字1 - 9在每一列只能出现一次。
                //数字1 - 9在每一个以粗实线分隔的3x3 宫内只能出现一次。（请参考示例图）
                //数独部分空格内已填入了数字，空白格用'.'表示。

                //注意：

                //一个有效的数独（部分已被填充）不一定是可解的。
                //只需要根据以上规则，验证已经填入的数字是否有效即可。
                //char[][] board = new char[9][]
                //{
                //    new char[]{'5','3','.','.','7','.','.','.','.'},
                //    new char[]{'6','.','.','1','9','5','.','.','.'},
                //    new char[]{'.','9','8','.','.','.','.','6','.'},
                //    new char[]{'8','.','.','.','6','.','.','.','3'},
                //    new char[]{'4','.','.','8','.','3','.','.','1'},
                //    new char[]{'7','.','.','.','2','.','.','.','6'},
                //    new char[]{'.','6','.','.','.','.','2','8','.'},
                //    new char[]{'.','.','.','4','1','9','.','.','5'},
                //    new char[]{'.','.','.','.','8','.','.','7','9'},
                //};

                char[][] board = {
                    new char[]{'5','3','.','.','7','.','.','.','.'},
                    new char[]{'6','.','.','1','9','5','.','.','.'},
                    new char[]{'.','9','8','.','.','.','.','6','.'},
                    new char[]{'8','.','.','.','6','.','.','.','3'},
                    new char[]{'4','.','.','8','.','3','.','.','1'},
                    new char[]{'7','.','.','.','2','.','.','.','6'},
                    new char[]{'.','6','.','.','.','.','2','8','.'},
                    new char[]{'.','.','.','4','1','9','.','.','5'},
                    new char[]{'.','.','.','.','8','.','.','7','9'}
                };

                int[,] intLength = new int[9, 9];


                Hashtable hashtable = new Hashtable();
                char[] charArray = new char[9];
                int sumIndex = 0;
                int charArrayIndex = 0;
                int tempIndex = 0;
                int arrayCount = 3 * 9;
                int i = 0;
                while (i < board.Length)
                {
                    for (int j = 0; j < board[i].Length; j++)
                    {
                        if (j > 0 && j % 3 == 0)
                        {

                        }


                        //if (j > 0 && j % 3 == 0 && sumIndex < arrayCount)
                        //{
                        //    sumIndex += j;
                        //    j = tempIndex;
                        //    i++;
                        //    if (i > 0 && i % 3 == 0 && sumIndex < arrayCount)
                        //    {
                        //        tempIndex += i;
                        //        i = 0;
                        //        j = tempIndex;
                        //    }
                        //}


                        //if (sumIndex == arrayCount)
                        //{
                        //    j = 0;
                        //    sumIndex = 0;
                        //    tempIndex = 0;
                        //}

                        charArray[charArrayIndex++] = board[i][j];
                        if (charArrayIndex >= charArray.Length)
                        {
                            for (int k = 0; k < charArray.Length; k++)
                            {
                                if (charArray[k] != '.')
                                {
                                    if (hashtable.ContainsKey(charArray[k]))
                                    {
                                        //return true;
                                    }
                                    hashtable.Add(charArray[k], 1);
                                }
                            }
                            hashtable.Clear();
                            charArrayIndex = 0;
                        }



                        //Console.Write(board[i][j] + "\t");
                    }
                    //i++;
                }
                Console.WriteLine();


            }
            {
                //两数之和
                //给定一个整数数组 nums和一个整数目标值 target，请你在该数组中找出 和为目标值 target 的那两个 整数，并返回它们的数组下标。
                //你可以假设每种输入只会对应一个答案。但是，数组中同一个元素在答案里不能重复出现。
                //你可以按任意顺序返回答案。

                //输入：nums = [2, 7, 11, 15], target = 9
                //输出：[0,1]
                //解释：因为 nums[0] + nums[1] == 9 ，返回[0, 1] 。
                int[] nums = { 3, 2, 4 };
                int target = 6;

                {
                    // 方案三
                    int i = 0;
                    int j = 1;
                    int len = nums.Length - 1;
                    while (nums[i] + nums[j] != target)
                    {
                        if (j >= len)
                        {
                            i++;
                            j = i + 1;
                        }
                        else
                            j++;
                    }
                    //return new int[2] { i, j };
                }

                /// 方案二
                int[] arr1 = new int[2];
                Hashtable hashtable = new Hashtable();

                for (int i = 0; i < nums.Length; i++)
                {
                    if (hashtable.ContainsKey(nums[i]))
                    {
                        arr1[0] = i;
                        arr1[1] = (int)hashtable[nums[i]];
                        //return arr1[1];
                    }
                    hashtable.Add(target - nums[i], i);
                }
                for (int i = 0; i < nums.Length; i++)
                {

                    if (hashtable.ContainsKey(nums[i]))
                    {
                        arr1[0] = i;
                        arr1[1] = (int)hashtable[nums[i]];
                        //return arr1;
                    }
                    int num = target - nums[i];
                    hashtable.Add(num, i);
                }

                // 方案一
                //HashSet<int> vs = new HashSet<int>(nums);
                int[] arr = new int[2];
                int left = 0, right = 1;
                while (left < nums.Length)
                {

                    if (right >= nums.Length)
                    {
                        left++;
                        right = left + 1;
                    }
                    else if (nums[left] + nums[right] == target)
                    {
                        arr[0] = left;
                        arr[1] = right;
                        break;
                    }
                    else
                    {
                        right++;
                    }
                }
                for (int i = 0; i < nums.Length; i++)
                {

                }
            }

            {
                //移动零  给定一个数组 nums，编写一个函数将所有 0 移动到数组的末尾，同时保持非零元素的相对顺序。
                int[] nums = { 4, 2, 4, 0, 0, 3, 0, 5, 1, 0 };// 1, 0,1
                {
                    int i = 0;
                    for (int j = 0; j < nums.Length; j++)
                    {
                        //只要不为0就往前挪
                        if (nums[j] != 0)
                        {
                            //i指向的值和j指向的值交换
                            int temp1 = nums[i];
                            nums[i] = nums[j];
                            nums[j] = temp1;
                            i++;
                        }
                    }
                }
                // 方案二
                int left = 0, right = 1;
                int temp = 0;
                while (right < nums.Length && left < nums.Length)
                {
                    if (nums[left] == 0 && nums[right] != 0)
                    {
                        temp = nums[left];
                        nums[left] = nums[right];
                        nums[right] = temp;
                        left++;
                        right++;
                    }
                    else if (nums[left] != 0)
                    {
                        left++;
                        right++;
                    }
                    else
                    {
                        right++;
                    }
                }
                // 方案一
                //int j = 0;
                temp = 0;
                for (int i = 0; i < nums.Length; i++)
                {
                    for (int j = i + 1; j < nums.Length; j++)
                    {
                        if (nums[i] == 0 && nums[j] != 0)
                        {
                            temp = nums[i];
                            nums[i] = nums[j];
                            nums[j] = temp;
                        }
                    }
                }
            }
        }

        public static string LongestPalindrome()
        {



            {
                int[] digits = { 9, 9 };

                // 方案二
                int lastIndex = digits.Length;
                for (int i = lastIndex - 1; i >= 0; i--)
                {
                    if (digits[i] != 9)
                    {
                        digits[i] += 1;
                        //return digits;
                    }
                    digits[i] = 0;
                }
                digits = new int[lastIndex + 1];
                digits[0] = 1;

                //return digits;
                //方案一
                digits[lastIndex - 1] = digits[lastIndex - 1] + 1;
                List<int> vs = new List<int>(digits);
                while (vs[lastIndex - 1] > 9)
                {
                    vs[lastIndex - 1] = 0;
                    lastIndex--;
                    if (lastIndex <= 0)
                    {
                        vs.Add(1);
                        vs.Reverse();
                        break;
                    }
                    else
                        vs[lastIndex - 1] = vs[lastIndex - 1] + 1;

                }
                vs.Reverse();

            }


            {


                /// 方案三
                int[] nums1 = { 2, 1 };
                int[] nums2 = { 1, 2 };
                Array.Sort(nums1);
                Array.Sort(nums2);
                int len1 = nums1.Length;
                int len2 = nums2.Length;
                int left = 0, right = 0;
                List<int> vs1 = new List<int>();
                while (left < len1 && right < len2)
                {
                    if (nums1[left] < nums2[right])
                    {
                        left++;
                    }
                    else if (nums1[left] > nums2[right])
                    {
                        right++;
                    }
                    else
                    {
                        vs1.Add(nums1[left]);
                        left++;
                        right++;
                    }
                }
                vs1.Sort();
                //return vs1.ToArray();


                // 方案二 不忍直视
                int arr1 = nums1.Length;
                int arr2 = nums2.Length;
                int len = arr1 > arr2 ? arr1 : arr2;

                int tempIndex = 0;
                int temp = arr1 < arr2 ? arr1 : arr2;
                List<int> vs = new List<int>();
                for (int i = 0; i < len;)
                {
                    if (arr1 == len)
                    {
                        if (nums1[i] == nums2[tempIndex])
                        {
                            vs.Add(nums1[i]);
                            nums2[tempIndex] = -1;
                            i++;
                            tempIndex = 0;
                            continue;
                        }
                    }
                    else
                    {
                        if (nums2[i] == nums1[tempIndex])
                        {
                            vs.Add(nums2[i]);
                            nums1[tempIndex] = -1;
                            i++;
                            tempIndex = 0;
                            continue;
                        }
                    }
                    tempIndex++;
                    if (tempIndex >= temp)
                    {
                        tempIndex = 0;
                        i++;
                    }
                }
                vs.Sort();
                vs.ToArray();
                // 方案一
                Hashtable hashtable = new Hashtable();
                for (int i = 0; i < nums1.Length; i++)
                {
                    for (int j = 0; j < nums2.Length; j++)
                    {
                        if (nums1[i] == nums2[j])
                        {
                            if (!hashtable.ContainsKey(i))
                            {
                                hashtable.Add(i, nums2[j]);
                                nums2[j] = -1;
                            }
                        }
                    }
                }

                int[] arry = new int[hashtable.Count];
                int index = 0;
                foreach (int item in hashtable.Values)
                {
                    arry[index++] = item;
                }
                Array.Sort(arry);
            }

            {
                int[] nums1 = { 4, 1, 2, 1, 2, 4 };

                int temp = 0;
                for (int i = 0; i < nums1.Length; i++)
                {
                    temp ^= nums1[i];

                }

                List<int> vs = new List<int>();
                for (int i = 0; i < nums1.Length; i++)
                {

                    if (vs.Contains(nums1[i]))
                        vs.Remove(nums1[i]);
                    else
                        vs.Add(nums1[i]);
                }

                Hashtable hashtable = new Hashtable();
                for (int i = 0; i < nums1.Length; i++)
                {

                    if (hashtable.ContainsKey(nums1[i]))
                        hashtable.Remove(nums1[i]);
                    else
                        hashtable.Add(nums1[i], nums1[i]);
                }
                int number = 0;
                foreach (int item in hashtable.Values)
                {
                    number = item;
                    //return item;
                }
                var sa = hashtable.Values;
                Console.WriteLine();
            }

            {
                int[] nums1 = { 4, 1, 2, 1, 2 };



                /// 方案三
                Hashtable hashtable = new Hashtable();

                for (int i = 0; i < nums1.Length; i++)
                {
                    if (hashtable.Contains(nums1[i]))
                    {
                        //return true;
                    }
                    hashtable.Add(nums1[i], 1);
                }


                /// 方案二
                Array.Sort(nums1);
                for (int i = 0; i < nums1.Length - 1; i++)
                {
                    if (nums1[i] == nums1[i + 1])
                    {
                        //return true;
                    }
                }
                /// 方案一
                Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();

                for (int i = 0; i < nums1.Length; i++)
                {
                    keyValuePairs[nums1[i]] = 1;
                }
                if (keyValuePairs.Count < nums1.Length)
                {
                    //return true
                }
            }

            {
                // 输入: nums = [1, 2, 3, 4, 5, 6, 7], k = 3
                //输出: [5, 6, 7, 1, 2, 3, 4]
                int[] nums1 = { 1, 2, 3, 4, 5, 6, 7 };//5 6 7 4 3 2 1  -1, -100, 3, 99  - 1, -100, 3, 99, 1 

                int index = nums1.Length;
                int temp = 0;
                int k = 3;
                int k1 = k;
                for (int i = 0; i < index; i++)
                {
                    temp = nums1[i];
                    nums1[i] = nums1[index - 1];
                    nums1[index - 1] = temp;
                    index--;
                }
                for (int i = 0; i < k1; i++)
                {
                    temp = nums1[i];
                    nums1[i] = nums1[k1 - 1];
                    nums1[k1 - 1] = temp;
                    k1--;
                }
                index = nums1.Length;
                for (int i = k; i < index; i++)
                {
                    temp = nums1[i];
                    nums1[i] = nums1[index - 1];
                    nums1[index - 1] = temp;
                    index--;
                }
                Console.WriteLine();
            }

            #region 方案一 
            {
                int[] nums1 = { 1, 2, 3, 4, 5, 6, 7 };
                int k = 3;

                int lastIndex = nums1.Length;
                //while (lastIndex <= k) { k -= lastIndex; }
                if (lastIndex <= k)
                {
                    k %= lastIndex;
                }

                int[] newArray = new int[lastIndex];
                int tempIndex = 0;
                for (int i = lastIndex - k; i < lastIndex; i++)
                {
                    newArray[tempIndex++] = nums1[i];
                }

                for (int i = 0; i < lastIndex - k; i++)
                {
                    newArray[tempIndex++] = nums1[i];
                }

                for (int i = 0; i < lastIndex; i++)
                {
                    nums1[i] = newArray[i];
                }
            }
            #endregion

            //for (int i = 0; i < nums1.Length - 1; i++)
            //{
            //    temp = nums1[i];
            //    if (i == k)
            //    {
            //        lastIndex = nums1.Length - k;
            //        lastIndex++;
            //    }
            //    nums1[i] = nums1[lastIndex];
            //    nums1[lastIndex] = temp;
            //    lastIndex++;

            //}
            //for (int i = 0; i < k - 1; i++)
            //{
            //    temp = nums1[i];
            //    nums1[i] = nums1[i + 1];
            //    nums1[i + 1] = temp;
            //}


            int[] nums = { 2, 2, 5 };//{ 7, 1, 5, 3, 6, 4 };  1, 2, 3, 4, 5

            {
                int min = 0;
                int total = 0;
                for (int i = 0; i < nums.Length - 1; i++)
                {
                    min = nums[i];
                    if (nums[i + 1] < min)
                    {
                        min = nums[++i];
                    }
                    while ((i < nums.Length - 2 && nums[i] + 1 == nums[i + 1]) || nums[i] == nums[i + 1])
                    {
                        i++;
                    }
                    for (int j = i + 1; j < nums.Length; j++)
                    {
                        if (nums[j] > min)
                        {
                            min = nums[j] - min;
                            total += min;

                            break;
                        }
                    }

                }
            }

            {
                int temp = 0;
                for (int i = 0; i < nums.Length; i++)
                {
                    temp += Math.Max(nums[i + 1] - nums[i], 0);
                }
                //return temp;
            }

            {

                int tempIndex = 0;
                for (int i = 1; i < nums.Length; i++)
                {
                    if (nums[i] != nums[tempIndex])
                    {
                        nums[++tempIndex] = nums[i];
                    }
                }
            }

            {
                //int[] nums = { 1, 2, };
                //if (nums.Length == 0)
                //    return 0;
                //if (nums.Length == 2)
                //    return nums[0] == nums[1] ? 1 : 2;
                int tempIndex = 0;
                for (int i = 1; i < nums.Length; i++)
                {
                    //if (nums[tempIndex] == nums[i])
                    //{
                    //    i++;
                    //}
                    if (nums[i] != nums[tempIndex])
                    {
                        nums[++tempIndex] = nums[i];
                    }
                }

                Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();
                for (int i = 0; i < nums.Length; i++)
                {
                    keyValuePairs[nums[i]] = nums[i];
                }
                //keyValuePairs.Values.ToArray().CopyTo(nums, 0);

                var arry = keyValuePairs.Values.ToArray();

                for (int i = 0; i < arry.Length; i++)
                {

                    nums[i] = arry[i];
                }
                int len = keyValuePairs.Count;
            }
            string s = "baadaada";
            //IList<IList<string>> res = new IList<IList<string>> ();
            {
                //int index = s.Length - 1;
                //for (int i = 0; i < s.Length;)
                //{
                //    if (s[i] == s[index])
                //    {
                //        i++;
                //    }
                //    index--;
                //    if (i == index)
                //    {
                //        i++;
                //        break;
                //    }
                //}
            }

            StringBuilder stringBuilder = new StringBuilder(new string('0', s.Length));
            for (int i = 0; i < s.Length; i++)
            {
                for (int j = i + 1; j < s.Length; j++)
                {
                    if (s[i] == s[j])
                    {
                        stringBuilder[i] = s[i];
                        stringBuilder[j] = s[j];
                        //i++;
                        //if (i == j - 1)
                        //{
                        //    stringBuilder[i] = s[i];
                        //}
                    }
                    if (i > 0 && s[i - 1] == s[j])
                    {
                        stringBuilder[j] = s[j];
                    }
                    //if (j - 1 == i)
                    //{
                    //    stringBuilder[i] = s[i];
                    //    break;
                    //}
                }

            }
            Console.WriteLine();
            return "aa";
        }
        public static double FindMedianSortedArrays()
        {
            int[] nums1 = { 1, 2, 4 };
            int[] nums2 = { 3, 5, 6 };
            {
                int arr1 = nums1.Length, arr2 = nums2.Length;

                int totalLength = arr1 + arr2;

                if (totalLength % 2 == 1)
                {
                    int kLength = totalLength / 2;
                    double median = GetMedian(nums1, nums2, kLength + 1);
                    return median;
                }
                else
                {
                    int kLength1 = totalLength / 2 - 1, kLength2 = totalLength / 2;
                    double median = GetMedian(nums1, nums2, kLength1 + 1) + GetMedian(nums1, nums2, kLength2 + 1) / 2d;
                    return median;
                }
            }

            {
                int length1 = nums1.Length, length2 = nums2.Length;
                int totalLength = length1 + length2;
                if (totalLength % 2 == 1)
                {
                    int midIndex = totalLength / 2;     //midIndex + 1 防止出现负数情况
                    double median = GetKthElement(nums1, nums2, midIndex + 1);
                    return median;
                }
                else
                {
                    int midIndex1 = totalLength / 2 - 1, midIndex2 = totalLength / 2;
                    double median = (GetKthElement(nums1, nums2, midIndex1 + 1) + GetKthElement(nums1, nums2, midIndex2 + 1)) / 2.0;
                    return median;
                }
            }
            ///
            {
                int[] nums;
                int m = nums1.Length;
                int n = nums2.Length;
                nums = new int[m + n];
                if (m == 0)
                {
                    if (n % 2 == 0)
                    {
                        return (nums2[n / 2 - 1] + nums2[n / 2]) / 2.0;
                    }
                    else
                    {

                        return nums2[n / 2];
                    }
                }
                if (n == 0)
                {
                    if (m % 2 == 0)
                    {
                        return (nums1[m / 2 - 1] + nums1[m / 2]) / 2.0;
                    }
                    else
                    {
                        return nums1[m / 2];
                    }
                }

                int count = 0;
                int i = 0, j = 0;
                while (count != (m + n))
                {
                    if (i == m)
                    {
                        while (j != n)
                        {
                            nums[count++] = nums2[j++];
                        }
                        break;
                    }
                    if (j == n)
                    {
                        while (i != m)
                        {
                            nums[count++] = nums1[i++];
                        }
                        break;
                    }

                    if (nums1[i] < nums2[j])
                    {
                        nums[count++] = nums1[i++];
                    }
                    else
                    {
                        nums[count++] = nums2[j++];
                    }
                }

                if (count % 2 == 0)
                {
                    return (nums[count / 2 - 1] + nums[count / 2]) / 2.0;
                }
                else
                {
                    return nums[count / 2];
                }
            }
        }

        public static int GetMedian(int[] nums1, int[] nums2, int k)
        {
            //int[] nums1 = { 1, 2, 4 };  k 2 +1
            //int[] nums2 = { 3, 5, 6 };  k 3 +1

            int arrLength1 = nums1.Length, arrLength2 = nums2.Length;
            int index1 = 0, index2 = 0;

            while (true)
            {

                if (index1 == arrLength1)
                {
                    return nums2[index2 + k - 1];
                }
                if (index2 == arrLength2)
                {
                    return nums1[index1 + k - 1];
                }

                if (k == 1)
                {
                    return Math.Min(nums1[index1], nums2[index2]);
                }
                ////////
                int nIndex = k / 2;
                int newIndex1 = Math.Min(index1 + nIndex, arrLength1) - 1;  // 2ci 1 1
                int newIndex2 = Math.Min(index2 + nIndex, arrLength1) - 1;  // 0 

                if (nums1[newIndex1] <= nums2[newIndex2])
                {
                    k = k - (newIndex1 - index1 + 1);   //3-1 = 2  2``1-1+1 2-1=1 2
                    index1 = newIndex1 + 1; // 1 1 2
                }
                else
                {
                    k = k - (newIndex2 - index2 + 1);   //1
                    index2 = newIndex2 + 1; //1
                }
            }
        }

        public static int GetKthElement(int[] nums1, int[] nums2, int k)
        {
            /* 主要思路：要找到第 k (k>1) 小的元素，那么就取 pivot1 = nums1[k/2-1] 和 pivot2 = nums2[k/2-1] 进行比较
             * 这里的 "/" 表示整除
             * nums1 中小于等于 pivot1 的元素有 nums1[0 .. k/2-2] 共计 k/2-1 个
             * nums2 中小于等于 pivot2 的元素有 nums2[0 .. k/2-2] 共计 k/2-1 个
             * 取 pivot = min(pivot1, pivot2)，两个数组中小于等于 pivot 的元素共计不会超过 (k/2-1) + (k/2-1) <= k-2 个
             * 这样 pivot 本身最大也只能是第 k-1 小的元素
             * 如果 pivot = pivot1，那么 nums1[0 .. k/2-1] 都不可能是第 k 小的元素。把这些元素全部 "删除"，剩下的作为新的 nums1 数组
             * 如果 pivot = pivot2，那么 nums2[0 .. k/2-1] 都不可能是第 k 小的元素。把这些元素全部 "删除"，剩下的作为新的 nums2 数组
             * 由于我们 "删除" 了一些元素（这些元素都比第 k 小的元素要小），因此需要修改 k 的值，减去删除的数的个数
             */

            int length1 = nums1.Length, length2 = nums2.Length;
            int index1 = 0, index2 = 0;

            while (true)
            {
                // 边界情况
                if (index1 == length1)
                {
                    return nums2[index2 + k - 1];
                }
                if (index2 == length2)
                {
                    return nums1[index1 + k - 1];
                }
                if (k == 1)
                {
                    return Math.Min(nums1[index1], nums2[index2]);
                }

                // 正常情况
                int half = k / 2;
                int newIndex1 = Math.Min(index1 + half, length1) - 1;
                int newIndex2 = Math.Min(index2 + half, length2) - 1;
                int pivot1 = nums1[newIndex1], pivot2 = nums2[newIndex2];
                if (pivot1 <= pivot2)
                {
                    k -= (newIndex1 - index1 + 1);
                    index1 = newIndex1 + 1;
                }
                else
                {
                    k -= (newIndex2 - index2 + 1);
                    index2 = newIndex2 + 1;
                }
            }
        }


        public static double FindMedianSortedArrays1()
        {
            // 1,2,3,4,5,6,7  nums3[length / 2]=4 + nums3[length / 2 - 1])=3
            int[] nums1 = { 1, 3 };
            int[] nums2 = { 2 };

            {
                int n = nums1.Length;
                int m = nums2.Length;
                int left = (n + m + 1) / 2;
                int right = (n + m + 2) / 2;
                //将偶数和奇数的情况合并，如果是奇数，会求两次同样的 k 。
                return (GetKth(nums1, 0, n - 1, nums2, 0, m - 1, left) + GetKth(nums1, 0, n - 1, nums2, 0, m - 1, right)) * 0.5;
            }



            {
                int m = nums1.Length;
                int n = nums2.Length;
                int[] newNums = new int[m + n];

                if (m == 0)
                {
                    if (n % 2 == 0)
                        return (nums2[n / 2 - 1] + nums2[n / 2]) / 2d;
                    else
                        return nums2[n / 2];
                }
                if (n == 0)
                {
                    if (m % 2 == 0)
                        return (nums1[m / 2 - 1] + nums1[m / 2]) / 2d;
                    else
                        return nums1[m / 2];
                }

                int i = 0, j = 0, count = 0;

                while (true)
                {
                    if (nums1[i] < nums2[j])
                    {
                        newNums[count++] = nums1[i++];
                    }
                    else
                    {
                        newNums[count++] = nums2[i++];
                    }

                    if (i == m)
                    {
                        while (j != n)
                        {
                            newNums[count++] = nums2[i++];
                        }
                        break;
                    }
                    if (j == n)
                    {
                        while (i != m)
                        {
                            newNums[count++] = nums1[i++];
                        }
                        break;
                    }
                }

                if (count % 2 == 0)
                {
                    return (newNums[count / 2 - 1] + newNums[count / 2]) / 2d;
                }
                else
                {
                    return newNums[count / 2];
                }

            }

            {
                int[] nums3 = nums1.Concat(nums2).ToArray();
                //int[] vs1 = nums1.Union(nums1,nums2).ToArray();
                Array.Sort(nums3);
                int length = nums3.Length;
                double number = 0d;
                if (length % 2 == 0)
                {
                    number = (nums3[length / 2] + nums3[length / 2 - 1]) / 2d;
                }
                else
                {
                    number = nums3[length / 2] / 2d;
                }
                return number;
            }
        }


        private static int GetKth(int[] nums1, int start1, int end1, int[] nums2, int start2, int end2, int k)
        {
            int len1 = end1 - start1 + 1;
            int len2 = end2 - start2 + 1;
            //让 len1 的长度小于 len2，这样就能保证如果有数组空了，一定是 len1 
            if (len1 > len2) return GetKth(nums2, start2, end2, nums1, start1, end1, k);
            if (len1 == 0) return nums2[start2 + k - 1];

            if (k == 1) return Math.Min(nums1[start1], nums2[start2]);

            int i = start1 + Math.Min(len1, k / 2) - 1;
            int j = start2 + Math.Min(len2, k / 2) - 1;

            if (nums1[i] > nums2[j])
            {
                return GetKth(nums1, start1, end1, nums2, j + 1, end2, k - (j - start2 + 1));
            }
            else
            {
                return GetKth(nums1, i + 1, end1, nums2, start2, end2, k - (i - start1 + 1));
            }
        }

        #region KMP算法
        public static int StrStr(string haystack, string needle)
        {
            haystack = "aaabaa"; needle = "ba";
            if (string.IsNullOrEmpty(needle))
            {
                return 0;
            }

            if (needle.Length > haystack.Length || string.IsNullOrEmpty(haystack))
            {
                return -1;
            }
            int num = KMP(haystack, needle);
            return KMP(haystack, needle);
        }
        private static int KMP(string haystack, string needle)
        {
            int[] next = GetNext(needle);
            int i = 0;
            int j = 0;
            while (i < haystack.Length)
            {
                if (haystack[i] == needle[j])
                {
                    j++;
                    i++;
                }
                if (j == needle.Length)
                {
                    return i - j;
                }
                else if (i < haystack.Length && haystack[i] != needle[j])
                {
                    if (j != 0)
                        j = next[j - 1];
                    else
                        i++;
                }

            }
            return -1;
        }

        private static int[] GetNext(string str)
        {
            int[] next = new int[str.Length];
            next[0] = 0;
            int i = 1;
            int j = 0;
            while (i < str.Length)
            {
                if (str[i] == str[j])
                {
                    j++;
                    next[i] = j;
                    i++;
                }
                else
                {
                    if (j == 0)
                    {
                        next[i] = 0;
                        i++;
                    }
                    else
                    {
                        j = next[j - 1];
                    }
                }
            }
            return next;
        }

        #endregion

        public static int StrStr1(string haystack, string needle)
        {
            haystack = "aaabaa"; needle = "ba";
            Dictionary<int, char> keyValuePairs = new Dictionary<int, char>();
            int index = 0;

            for (int i = 0; i < haystack.Length; i++)
            {
                if (haystack[i] == needle[index])
                {
                    keyValuePairs[i] = needle[index];
                    index++;
                }

            }

            for (int i = 0; i < needle.Length;)
            {
                if (needle[i] == haystack[index])
                {
                    keyValuePairs[index] = needle[i];
                    i++;

                }
                index++;
            }
            //var k = keyValuePairs.Keys;
            if (keyValuePairs.Count == needle.Length)
            {

                return keyValuePairs.Keys.First();
            }
            return -1;
        }
        public static void Merge()
        {
            //int[] nums1, int m, int[] nums2, int n;
            int[] nums1 = { 3, 1, 2, 1, 2, 3, 4 };
            var a = nums1.Distinct().ToArray();
            {

                int index = 0;
                for (int i = 0; i < nums1.Length; i++)
                {

                    nums1[0] = nums1[0] ^ nums1[i];




                    //index = i;
                    //if (nums1[index] != nums1[i + 1])
                    //{
                    //    index = i + 1;
                    //    //index = nums1[i];
                    //    //i += 1;
                    //}
                }
                index = nums1[0];
                return;
            }

            {
                int m = 6;
                int[] nums2 = { 1, 2, 2 };
                int n = 3;

                int index = 0;
                for (int i = 0; i < nums2.Length;)
                {

                    if (nums1[index] == 0)
                    {
                        nums1[index] = nums2[i++];
                    }
                    index++;
                }

                int temp = 0;
                for (int i = 0; i < nums1.Length; i++)
                {
                    int currentIndex = i;
                    for (int j = i + 1; j < nums1.Length; j++)
                    {
                        if (nums1[currentIndex] > nums1[j])
                        {
                            currentIndex = j;
                        }
                        //if (nums1[i] > nums1[j])
                        //{
                        //    temp = nums1[i];
                        //    nums1[i] = nums1[j];
                        //    nums1[j] = temp;
                        //}
                    }
                    if (currentIndex != i)
                    {
                        temp = nums1[i];
                        nums1[i] = nums1[currentIndex];
                        nums1[currentIndex] = temp;
                    }
                }

                var ar = nums1.Concat(nums2).ToArray();
                // [-1,0,0,1,2,2,3,3,3]
                Array.Sort(ar);
                Array.Copy(ar, 3, nums1, 0, nums1.Length);
            }
        }
        public static void ReverseString()
        {
            char[] s2 = { 'h', 'e', 'l', 'l', 'o' };

            {
                char[] s1 = new char[s2.Length];
                int index = 0;
                for (int i = s2.Length - 1; index < i; i--)
                {
                    char temp = s2[i];  // temp = 4 o
                    s2[i] = s2[index];  // 4 o = 0 h
                    s2[index++] = temp; // 0 h = 4 o
                }
                //char temp = ' ';
                //int index1 = 0;
                //for (int i = s2.Length - 1; index1 < i; i--)
                //{
                //    temp = s2[i];
                //    s2[i] = s2[index1];
                //    s2[index1++] = temp;
                //}
            }

            {
                char[] s1 = new char[s2.Length];
                int index = 0;
                for (int i = s2.Length - 1; i >= 0; i--)
                {
                    s1[index++] = s2[i];
                }
                s1.CopyTo(s2, 0);
            }
        }
        public static void ReverseWords(string s)
        {
            string str = "Let's take LeetCode contest";
            //Stack<char> stack = new Stack<char>();
            //stack.p
            {
                s = "Let's take LeetCode contest";
                List<char> ans = new List<char>();
                Stack<char> stack = new Stack<char>();
                int index = 0;
                while (index < s.Length)
                {
                    if (s[index] == ' ')
                    {
                        while (stack.Count > 0)
                        {
                            ans.Add(stack.Pop());
                        }

                        ans.Add(' ');
                    }
                    else
                    {
                        stack.Push(s[index]);
                    }

                    index++;
                }
                //return ans.ToArray().ToString();

            }

            string[] arrStr = str.Split(' ');


            string itemStr = "";
            for (int i = 0; i < arrStr.Length; i++)
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int j = arrStr[i].Length - 1; j >= 0; j--)
                {
                    stringBuilder.Append(arrStr[i][j]);

                }
                arrStr[i] = stringBuilder.ToString();
                //stringBuilder.Append(string.Join("", arrStr[i].Reverse()) + " ");
                //stringBuilder.Append(" ");
            }
            string ss = string.Join(" ", arrStr);
            //stringBuilder.Remove(str.Length, 1);
            arrStr = itemStr.Split(' ');
            var a = str.Reverse();
            //itemStr = stringBuilder.ToString();
            var qws = itemStr.Substring(0, itemStr.Length - 1);
            Console.WriteLine();
        }
        public static string LeetCodeDemo()
        {
            string[] strs = { "abab", "aba", "ab" };//{ "flower", "xflow", "xflight" }; // 

            if (strs.Length == 0)
            {
                return "";
            }

            string res = "";
            for (int i = 0; i < strs[0].Length; i++)
            {
                for (int j = 1; j < strs.Length; j++)
                {
                    if (i >= strs[j].Length || !strs[0][i].Equals(strs[j][i]))
                    {
                        return res;
                    }

                }
                res = strs[0].Substring(0, i + 1);
            }

            Console.WriteLine(res);
            return res;

            #region 我老了 写的代码都这么冗余 
            {


                List<char> listOneStr = new List<char>();

                if ((strs.Length > 0 && strs.Length <= 200) || (strs[0].Length > 0 && strs[0].Length <= 200))
                {

                }
                else
                {
                    return "";
                }

                for (int j = 0; j < strs[0].Length; j++)
                {
                    listOneStr.Add(strs[0][j]);
                }


                for (int i = 1; i < strs.Length; i++)
                {
                    if (strs[i].Length > 200 || strs[i].Length == 0) return "";

                    List<char> listTwoStr = new List<char>();

                    for (int j = 0; j < strs[i].Length;)
                    {
                        foreach (var c in listOneStr)
                        {
                            if (listTwoStr.Count >= strs[i].Length)
                                break;
                            if (c == strs[i][j++])
                                listTwoStr.Add(c);
                            else
                                break;

                        }
                        listOneStr.Clear();
                        listOneStr = listTwoStr;
                        break;
                    }
                }

                if (listOneStr.Count == 0) return "";

                string strSplicing = string.Join("", listOneStr);
                return strSplicing;
            }
            #endregion
        }

        public bool ContainsDuplicate(int[] nums)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                dic[nums[i]] = 1;
            }
            return !(nums.Length == dic.Count);
        }

        public static void LeetCodeDemo1()
        {
            int[] nums = { 1, 2, 3, 1 };
            //Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();
            //for (int i = 0; i < nums.Length; i++)
            //{
            //    keyValuePairs[nums[i]] = 1;
            //}
            //if (keyValuePairs.Count == nums.Length)
            //    return false;
            //else
            //    return true;

            if (nums.Length == 0)
            {
                return;
            }
            Array.Sort(nums);
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == nums[i + 1])
                {
                    return;
                }
            }

            {
                string aaa = "";
                //int[] nums = new int[10000];
                for (int i = 0; i < 10000; i++)
                {
                    nums[i] = i;
                }


                int index = 0;
                int takeData = 500;
                while (index * takeData < nums.Length)
                {
                    Array.Sort(nums);
                    int[] t = nums.Skip(index * takeData).Take(takeData).ToArray();
                    for (int i = 0; i < t.Length; i++)
                    {
                        if (nums.ToList().FindAll(n => n == nums[i]).Count > 1)
                        {
                            return;
                        }
                    }
                    index++;
                }
            }
            {
                if (nums.Length == 0)
                {
                    return;
                }
                for (int i = 0; i < nums.Length; i++)
                {
                    if (nums.ToList().FindAll(n => n == nums[i]).Count > 1)
                    {
                        return;
                    }

                }
            }
        }

        public static bool IsValid(string s)
        {
            string s1 = "(([]){})";
            {
                for (int i = 0; i < s1.Length; i++)
                {
                    char str = 'A';
                    switch (s1[i])
                    {
                        case '(':
                            str = ')';
                            break;
                        case '[':
                            str = ']';
                            break;
                        case '{':
                            str = ']';
                            break;
                        default:
                            break;
                    }
                    for (int j = 1 + i; j < s1.Length; j++)
                    {
                        if (s1[j] == str)
                        {
                            //s1 = s1.de(s1[i], '').Replace(str, ' ');//.Remove(s1[j])
                        }
                    }

                    //s1.Remove()
                }
            }

            {
                s = "(([]){})";
                int tempLength = s.Length / 2;
                for (int i = 0; i < tempLength; i++)
                {
                    s = s.Replace("()", "").Replace("[]", "").Replace("{}", "");
                }
                return s.Length == 0;
            }

            {

                if (s1.Length % 2 != 0)
                {
                    return false;
                }
                string[] strArray = { "()", "{}", "[]" };
                Dictionary<char, char> keyValuePairs = new Dictionary<char, char>();


                string temp = "";
                string str = str = s1[0].ToString() + s1[s1.Length - 1].ToString();
                int i = strArray.Contains(str) ? 0 : 1;

                for (i = 1; i < s1.Length - 1; i++)
                {


                    str = s1[i].ToString() + s1[i += 1].ToString();
                    if (!strArray.Contains(str))
                    {
                        return false;
                    }
                    temp += str;
                }
                //i += 1;
                //keyValuePairs[s1[i]]

                return true;
            }
        }

        public static int LeetCodeDemo2(int n)
        {
            int v1 = 1;
            int v2 = 1;
            for (int i = 1; i < n; i++)
            {
                int v = v1 + v2;
                v1 = v2;
                v2 = v;
            }
            return v2;
            //Random randomx = new Random();
            //List<int> vs = new List<int>();

            //Dictionary<List<int>, int> keyValuePairs = new Dictionary<List<int>, int>();
            //int index = 0;
            //int temp = randomx.Next(1, 3);
            //while (true)
            //{
            //    if (temp == n)
            //    {
            //        keyValuePairs.Add(vs, index++);
            //        break;
            //    }
            //    vs.Add(temp);
            //    temp += randomx.Next(1, 3);

            //}
        }

        public static string Multiply()
        {
            string num1 = "123", num2 = "456";
            {
                StringBuilder stringBuilder = new StringBuilder(new string('0', num1.Length + num2.Length));
                for (int i = num1.Length - 1; i >= 0; i--)
                {
                    for (int j = num2.Length - 1; j >= 0; j--)
                    {
                        int num = stringBuilder[i + j + 1] - '0' + (num1[i] - '0') * (num2[j] - '0');
                        stringBuilder[i + j + 1] = (char)(num % 10 + '0');
                        stringBuilder[i + j] += (char)(num / 10);

                        //num = stringBuilder[i + j + 1];
                        //stringBuilder.Append((num1[i] - '0') * (num2[j] - '0'));
                    }
                }
                return stringBuilder.ToString().TrimStart('0').Length == 0 ? "0" : stringBuilder.ToString().TrimStart('0');
            }

            {
                StringBuilder sum = new StringBuilder(new string('0', num1.Length + num2.Length));
                for (int i = num1.Length - 1; i >= 0; i--)
                {
                    for (int j = num2.Length - 1; j >= 0; j--)
                    {
                        int next = sum[i + j + 1] - '0' + (num1[i] - '0') * (num2[j] - '0');
                        sum[i + j + 1] = (char)(next % 10 + '0');
                        sum[i + j] += (char)(next / 10);
                    }
                }
                string res = sum.ToString().TrimStart('0');
                return res.Length == 0 ? "0" : res;

            }

            {
                // 保存计算结果
                string res = "0";

                // num2 逐位与 num1 相乘
                for (int i = num2.Length - 1; i >= 0; i--)
                {
                    int carry = 0;
                    // 保存 num2 第i位数字与 num1 相乘的结果
                    StringBuilder temp = new StringBuilder();
                    // 补 0 
                    for (int j = 0; j < num2.Length - 1 - i; j++)
                    {
                        temp.Append(0);
                    }
                    int n2 = num2[i] - '0';

                    // num2 的第 i 位数字 n2 与 num1 相乘
                    for (int j = num1.Length - 1; j >= 0 || carry != 0; j--)
                    {
                        int n1 = j < 0 ? 0 : num1[j] - '0';
                        int product = (n1 * n2 + carry) % 10;
                        temp.Append(product);
                        carry = (n1 * n2 + carry) / 10;
                    }
                    string a = temp.ToString();
                    var n = string.Join("", a.Reverse().Select(x => x));
                    // 将当前结果与新计算的结果求和作为新的结果
                    res = addStrings(res, n);
                }
                //return res;

            }
            {
                int allSum = 0;
                int index1 = 0;
                for (int j = num2.Length - 1; j >= 0; j--)
                {
                    string sum = "0";
                    int index = 0;
                    int carry = 0;
                    for (int i = num1.Length - 1; i >= 0; i--)
                    {

                        int n1 = num1[i] - '0';
                        int n2 = num2[j] - '0';
                        string temp = Sum(n1 * n2, index);
                        index++;
                        int product = (n1 * n2 + carry) % 10;
                        carry = (n1 * n2 + carry) / 10;
                        //sum += temp;
                    }
                    //for (int j = num1.length() - 1; j >= 0 || carry != 0; j--)
                    //{
                    //    int n1 = j < 0 ? 0 : num1.charAt(j) - '0';
                    //    int product = (n1 * n2 + carry) % 10;
                    //    temp.append(product);
                    //    carry = (n1 * n2 + carry) / 10;
                    //}

                    //sum = Sum(sum, index1);
                    index1++;
                    //allSum += sum;
                }
            }
            return string.Empty;
        }
        public static int[] ProductExceptSelf()
        {

            int[] nums = { 1, 2, 3, 4 };//MyClass.arrays;//{ 1, 2, 3, 4 };

            int len = nums.Length;
            int[] arr1 = new int[len];
            {
                //  输入：nums = [1, 2, 3]
                //  输出：[[],[1],[2],[1,2],[3],[1,3],[2,3],[1,2,3]]
                //  var newArray = nums.Select();


            }

            {
                arr1[0] = 1;
                for (int i = 1; i < arr1.Length; i++)
                {
                    arr1[i] = nums[i - 1] * arr1[i - 1];
                }

                int R = 1;
                for (int i = arr1.Length - 1; i >= 0; i--)
                {
                    arr1[i] = arr1[i] * R;
                    R *= nums[i];
                }
            }

            {
                int length = nums.Length;
                int[] answer = new int[length];

                // answer[i] 表示索引 i 左侧所有元素的乘积
                // 因为索引为 '0' 的元素左侧没有元素， 所以 answer[0] = 1
                answer[0] = 1;
                for (int i = 1; i < length; i++)
                {
                    // 1 1 2 6
                    answer[i] = nums[i - 1] * answer[i - 1];
                }

                // R 为右侧所有元素的乘积
                // 刚开始右边没有元素，所以 R = 1
                int R = 1;
                for (int i = length - 1; i >= 0; i--)
                {
                    /*
                     * i = 3;
                     answer[3] * R = 6 *1; 6
                      R *= nums[3]; = 4 *1; 4
                      i--;
                      i = 2;
                      answer[2] * 4= 2 *4; 8
                      R *= nums[2]; = 4 *3; 12
                      i--;
                       i = 1;
                       answer[1] * 12= 1 *12; 12
                      R *= nums[1]; = 12 *2; 24
                        i--;
                       i = 0;
                       answer[0] * 12= 1 *24; 24
                      R *= nums[0]; = 24 *1; 24
                     */
                    // 对于索引 i，左边的乘积为 answer[i]，右边的乘积为 R
                    answer[i] = answer[i] * R; // 6 8 12 24
                                               // R 需要包含右边所有的乘积，所以计算下一个结果时需要将当前值乘到 R 上
                    R *= nums[i];   // 4 12 24 24
                }
                return answer;

            }

            {
                int index = 1;
                int res = 1;
                int arrIndex = 0;
                for (int i = 0; i < len;)
                {

                    if (index == i)
                    {
                        index++;
                    }
                    if (index < len)
                    {
                        res *= nums[index++];
                    }

                    if (index == len)
                    {
                        i++;
                        index = 0;
                        arr1[arrIndex++] = res;
                        res = 1;
                    }
                }
            }

            {
                int index = 1;
                int res = 0;
                int oldRes = 0;
                for (int i = 0; i < nums.Length;)
                {

                    if (i > 0) index = 0;

                    if (index < nums.Length)
                    {
                        if (nums[index] == 1)
                        {
                            index++;
                            continue;
                        }
                        if (index == i)
                        {
                            index++;
                        }

                        if (res != nums[i])
                        {
                            nums[i] = nums[index] * nums[++index];
                            res = nums[i];
                        }
                        else
                        {
                            nums[i] *= nums[index];
                        }
                        index++;
                    }

                    if (index == nums.Length)
                    {
                        i++;
                    }
                }
            }
            return nums;
        }
        public static IList<IList<int>> ThreeSum()
        {
            int[] nums = { -4, -2, -2, -2, 0, 1, 2, 2, 2, 3, 3, 4, 4, 6, 6 };//    -4, -2, -2, -2, 0, 1, 2, 2, 2, 3, 3, 4, 4, 6, 6
            {
                IList<IList<int>> result = new List<IList<int>>();
                int len = nums.Length;
                if (len < 3) return result;
                Array.Sort(nums);
                for (int i = 0; i < len; i++)
                {
                    if (nums[i] > 0) break;//排序之后 如果第一个数都大于零  那么就不可能有等于 零 的数字
                    if (i > 0 && nums[i] == nums[i - 1])
                    {
                        continue;
                    }
                    int head = i + 1, tail = len - 1;
                    while (head < tail)
                    {
                        int sum = nums[i] + nums[head] + nums[tail];
                        if (sum == 0)
                        {
                            result.Add(new List<int>() { nums[i], nums[head], nums[tail] });

                            while (head < tail && nums[head] == nums[head + 1]) { head++; }
                            head++;
                            while (head < tail && nums[tail] == nums[tail - 1]) { tail--; }
                            tail--;
                        }
                        else if (sum > 0) tail--;   // 和 如果 大于零 就往 左移

                        else if (sum < 0) head++;   // 和 如果 小于零 就往 右移

                    }
                }
            }



            {
                IList<IList<int>> result = new List<IList<int>>();
                int len = nums.Length;
                if (len < 3) return result;
                Array.Sort(nums);// -4, -1, -1, 0, 1, 2   
                {
                    for (int i = 0; i < len - 2; i++)
                    {
                        // 如果没有负数则不可能为 0
                        if (nums[i] > 0) break;

                        while (i > 0 && nums[i] == nums[i - 1])
                        {
                            i++;
                        }
                        int head = i + 1, tail = len - 1;
                        while (head < tail)
                        {
                            int sum = nums[i] + nums[head] + nums[tail];
                            if (sum == 0)
                            {
                                result.Add(new List<int>() { nums[i], nums[head], nums[tail] });

                                while (head < tail && nums[tail] == nums[tail - 1]) { tail--; }
                                tail--;
                                while (head < tail && nums[head] == nums[head + 1]) { head++; }
                                head++;
                            }

                            else if (sum > 0)
                                tail--;
                            else if (sum < 0)
                                head++;
                        }




                        //if (head >= tail)
                        //{

                        //    head = i + 1;
                        //    tail = nums.Length - 1;
                        //    while (nums[i] == nums[i - 1])
                        //    {
                        //        i++;
                        //        head = i + 1;
                        //        tail = nums.Length - 1;
                        //    }
                        //}

                    }
                }

            }
            Tuple<int, int, int> tuple = new Tuple<int, int, int>(0, 0, 0);

            List<Tuple<int, int, int>> listTuple = new List<Tuple<int, int, int>>();
            {
                IList<IList<int>> result = new List<IList<int>>();
                int len = nums.Length;
                for (int i = 0; i < len - 1; i++)
                {

                    int index = i + 1;

                    while (index != len)
                    {
                        int index2 = index + 1;
                        while (index2 != len)
                        {

                            if (nums[i] + nums[index] + nums[index2] == 0)
                            {


                                var a1 = listTuple.FindIndex(t => t.Equals(tuple));
                                tuple = Tuple.Create(nums[i], nums[index], nums[index2]);
                                if (a1 == -1)
                                {
                                    result.Add(new List<int>() { nums[i], nums[index], nums[index2] });
                                    listTuple.Add(tuple);
                                }
                            }
                            index2++;
                        }
                        index++;
                    }
                    //for (int j = i + 1; j < nums.Length; j++)
                    //{
                    //    for (int k = j + 1; k < nums.Length; k++)
                    //    {
                    //        if (nums[i] + nums[j] + nums[k] == 0)
                    //        {
                    //            tuple = Tuple.Create(nums[i], nums[j], nums[k]);

                    //            var a1 = listTuple.FindIndex(t => t.Equals(tuple));

                    //            if (a1 == -1)
                    //            {
                    //                result.Add(new List<int>() { nums[i], nums[j], nums[k] });
                    //                listTuple.Add(tuple);
                    //            }
                    //        }
                    //    }

                    //}

                }
                var l = result[0][0];
                var a = result.Distinct();
                result.Add(new List<int>() { 1, 2, 3 });
                return result;
            }
        }
        /**
    * 对两个字符串数字进行相加，返回字符串形式的和
*/
        public static string addStrings(string num1, string num2)
        {
            StringBuilder builder = new StringBuilder();
            int carry = 0;
            int j = num2.Length - 1;
            for (int i = num1.Length - 1; i >= 0 || carry != 0; i--)
            {
                int x = i < 0 ? 0 : num1[i] - '0';
                int y = j < 0 ? 0 : num2[j] - '0';
                int sum = (x + y + carry) % 10;
                builder.Append(sum);
                carry = (x + y + carry) / 10;
                j--;
            }
            var n = string.Join("", builder.ToString().Reverse().Select(x => x));
            return n;
        }
        public static string Sum(int temp, int index)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(temp);
            for (int k = 0; k < index; k++)
            {
                //temp *= 10;
                stringBuilder.Append(0);
            }

            return stringBuilder.ToString();
        }
        #endregion
        public static void PrintN()
        {
            int a = 1;
            int b = a;
            a = 2;
            Console.WriteLine(b);
            b = a;
            Console.WriteLine(b);
            int[] arr = new int[1] { 1 };
            int[] arr2 = arr;
            arr[0] = 2;
            Console.WriteLine(arr2[0]);

            Boolean bools = true;
            Console.WriteLine(bools);
            Add(1, 2, 3, 4, 5);
            Add();

            // 交错数组
            int[][] vsm = new int[4][];
            vsm[0] = new int[1];
            vsm[1] = new int[2];
            vsm[2] = new int[5];
            vsm[3] = new int[3];
            vsm[0][0] = 1;
            vsm[1][1] = 12;
            vsm[2][2] = 3;
            vsm[3][2] = 4;
            for (int i = 0; i < vsm.Length; i++)
            {
                for (int j = 0; j < vsm[i].Length; j++)
                {
                    Console.Write(vsm[i][j] + "\t");
                }
                Console.WriteLine();
            }
        }
        public static int Add(params int[] arg)
        {
            int sumNum = 0;
            for (int i = 0; i < arg.Length; i++)
            {
                sumNum += arg[i];
            }
            return sumNum;
        }
        public static void DayOfMonth(int year, int month, int day)
        {
            // 定义方法:根据年月日，计算当天是本年的第几天
            // 2016年 3月 5日
            // 累加1、2正月天数31+ 29
            // 再累加当月天数 +5
            // 提示∶将每月天数存储到数组中 
            int[] days = new int[12];
            for (int i = 0; i < 12; i++)
            {
                days[i] = ShowMonthDay(year, i + 1);
            }
            int addNum = day;
            for (int i = 0; i < month - 1; i++)
            {
                addNum += days[i];
            }
            Console.WriteLine("当天是本年的第{0}天", addNum);
        }
        public static void StudentScore()
        {
            Console.WriteLine("请输入年份：");
            int year = int.Parse(Console.ReadLine());
            Console.WriteLine("请输入月份：");
            int month = int.Parse(Console.ReadLine());
            Console.WriteLine("请输入天：");
            int day = int.Parse(Console.ReadLine());
            DayOfMonth(year, month, day);

            Console.ReadKey();

            Console.WriteLine("请输入学生总数：");
            int totalStudents = int.Parse(Console.ReadLine());
            float[] vsa = new float[totalStudents];
            for (int i = 0; i < vsa.Length;)
            {
                Console.WriteLine("请输入第{0}个学生成绩：", i + 1);
                float score = float.Parse(Console.ReadLine());
                // 成绩 必须得在 0~100之间
                if (score >= 0 && score <= 100)
                    vsa[i++] = score;
                else
                    Console.WriteLine("成绩有误,请重新输入!");
            }

            for (int i = 0; i < vsa.Length; i++)
            {
                Console.WriteLine("第{0}个学生成绩为:{1}", i + 1, vsa[i]);
            }
        }

        #region 类的实践
        public class Wife
        {
            private int _age;
            //private string _name;

            public string Name;
            //{ get => _name; set => _name = value; }
            //public int Age { get; set; }
            public int Age
            {
                get => _age;

                set => _age = value;
                //{
                //if (value >= 18 && value <= 20) { _age = value; }
                //else
                //{
                //    throw new Exception("老了,换个年轻点的");
                //}

                //}
            }

            public Wife()
            {
                Console.WriteLine("OK");
            }
            public Wife(string name) : this()
            {
                this.Name = name;
            }
            public Wife(string name, int age) : this(name)
            {
                this.Age = age;
            }
            public void SetName(string name)
            {
                this.Name = name;
            }
            public void SetAge(int age)
            {
                this.Age = age;
            }
            public void GetName()
            {
                Console.WriteLine(this.Name);
            }
        }
        public static void ClassPractice()
        {
            Wife wife = new Wife();
            wife.SetName("01");
            wife.SetAge(18);
            Wife[] wivesArray = new Wife[4];
            wivesArray[0] = wife;
            wivesArray[1] = new Wife("03", 40);
            wivesArray[2] = new Wife("04", 20);
            wivesArray[3] = new Wife("05", 25);

            // 查找年龄最小的老婆(返回Wife类型的引用)
            Wife minWife = FindYongAge(wivesArray);
        }
        public static Wife FindYongAge(Wife[] Wife)
        {
            Wife wives = Wife[0];
            for (int i = 1; i < Wife.Length; i++)
            {
                if (wives.Age > Wife[i].Age)
                {
                    wives = Wife[i];
                }
            }
            return wives;
        }

        public struct MyStruct
        {

            //public MyStruct() { }

            public int RIndex { get; set; }
            public int CIndex { get; set; }
            public MyStruct(int rIndex, int cIndex)
            {
                this.RIndex = rIndex;
                this.CIndex = cIndex;
            }
        }
        public static void Fun()
        {
            User u1 = new User("tq", "666");
            User u2 = new User("ct", "123");
            User u3 = new User("wz", "888");
            User u4 = new User("lf", "168");
            User u5 = new User("wt", "wtt");
            UserList list = new UserList(3);
            list.Add(u1);
            list.Add(u2);
            list.Add(u3);
            list.Add(u4);
            list.Add(u4);
            list.Add(u5);
            list.InsertElement(3, new User("ml", "000"));
            list.InsertElement(1, new User("ml0", "000"));

            list.RemoveElement(1);
            for (int i = 0; i < list.Count; i++)
            {
                User user = list.GetElement(i);
                user.PrintUser();
            }
        }
        class User
        {
            public string LoginId { get; set; }
            public string PassWord { get; set; }
            public User()
            {

            }
            public User(string loginId, string passWord)
            {
                this.LoginId = loginId;
                PassWord = passWord;
            }
            public void PrintUser()
            {
                Console.WriteLine("账号：{0}--->密码：{1}", LoginId, PassWord);
            }

        }
        class UserList
        {
            // 真正存储用户的字段
            private User[] data = null;
            private int currentIndex;
            public int Count
            {
                get { return currentIndex; }
            }
            public UserList() : this(3)
            {
                //List<string> vs = new List<string>();
                //vs.re
                //vs.in
                //data = new User[3];
            }
            public UserList(int capacity)
            {
                data = new User[capacity];
            }

            /// <summary>
            /// 插入功能
            /// </summary>
            public void InsertElement(int index, User user)
            {
                {
                    //Add(user);
                    User[] userArray = new User[++currentIndex];
                    userArray[index] = user;
                    int tempIndex = 0;
                    for (int i = 0; i < userArray.Length; i++)
                    {
                        if (i == index)
                            continue;
                        userArray[i] = data[tempIndex++];
                    }
                    data = userArray;
                }

            }

            /// <summary>
            /// 删除功能
            /// </summary>
            public void RemoveElement(int index)
            {
                User[] userArr = new User[--currentIndex];
                int tempIndex = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    if (i == index)
                        continue;
                    userArr[tempIndex++] = data[i];
                }
                data = userArr;
            }

            //public void OptionArray(User[] userArray, int index)
            //{
            //    int tempIndex = 0;
            //    for (int i = 0; i < userArray.Length; i++)
            //    {
            //        if (i == index)
            //            continue;
            //        userArray[i] = data[tempIndex++];
            //    }
            //    data = userArray;
            //}

            public void Add(User user)
            {
                //if (currentIndex < data.Length)
                //{
                //    data[currentIndex++] = user;
                //    return;
                //}

                //data[?] = value; 
                // 如果容量不够 ?
                //扩容∶开辟更大的数组拷贝原始数据替换引用

                CheckArrayLength();

                data[currentIndex++] = user;
            }
            /// <summary>
            /// 改写之后-->
            /// </summary>
            /// <returns></returns>
            public void CheckArrayLength()
            {
                if (currentIndex >= data.Length)
                {
                    User[] userBigArray = new User[data.Length + 1];
                    data.CopyTo(userBigArray, 0);
                    data = userBigArray;
                }
            }

            public User GetElement(int index)
            {
                User user = data[index];
                return user;
            }
        }
        #endregion

        #region 递归计算 
        public static int Fnql(int num)
        {
            //提到递归，我们可能会想到的一个实例便是斐波那契数列。斐波那契数列就是如下的数列：

            //0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, …，总之，就是第N(N > 2)个数等于第(N - 1)个数和(N - 2)个数的和。用递归算法实现如下：
            if (num <= 0)
                return 0;
            if (num == 1)
                return 1;

            //Fnql(3 - 1)+Fnql(3 - 2); Fnql(2 - 1)+Fnql(2 - 2)  Fnql(1 - 1)+Fnql(1 - 2); return 1;
            // Fnql(3 - 1) 3-1=2=> 2-1=1=>  return 1 ; Fnql(3 - 2) => 3-2=1=> return 1
            // Fnql(4 - 1) 4-1=3=> 3-1=2 > 2-1=1 > return 1 ;Fnql(4 - 2) 4-2=2 => 2-2=0;
            //Fnql(6 - 1)+Fnql(6 - 2);Fnql(5 - 1)+Fnql(5 - 2);Fnql(4 - 1)+Fnql(4 - 2);Fnql(3 - 1)+Fnql(3 - 2); Fnql(2 - 1)+Fnql(2 - 2)  ; return 1;
            //                                                           3+0                    1+1=2                    1+0=1
            return Fnql(num - 1) + Fnql(num - 2);

        }
        public static void PrintSecursion()
        {

            int res = Fnql(4);
            Console.WriteLine(res);
        }
        public static int SecursionA(int num)
        {
            // 5*4*3*2*1
            /*5*5-1
             * 4*4-1
             * 3*3-1
             * 2*2-1
             * 1
             */
            if (num == 1)
            {
                return 1;
            }
            return num * SecursionA(num - 1);
        }

        //编写一个函数，计算当参数为8时的结果为多少 ? 
        //1-2 + 3 - 4 + 5 - 6+7-8......
        public int returnSum(int n)
        {
            int sum = 0;
            for (int i = 1; i <= n; i++)
            {
                int k = i;

                if (k % 2 == 0)
                {
                    k = -k;
                }
                sum += k;
            }
            return sum;
        }
        public static int Secursion(int num)
        {
            //编写一个函数，计算当参数为8时的结果为多少 ? 
            //1-2 + 3 - 4 + 5 - 6+7-8......

            //num - (Secursion(num - 1) + Secursion(num - 2));
            /**
             *  8-(8-1)4
             *  7-(7-1)4
             *  6-(6-1)3
             *  5-(5-1)3
             *  4-[(4-1)==2]  4-2=2 return 2
             *  3-[(3-1)==1]  3-1=2 return 2
             *  2-(2-1)return 1
             *  1-(1-1)
             * num*(Secursion(num +2)-1)      2
             */
            if (num == 1)
            {
                return 1;
            }
            if (num % 2 == 0)
            {
                //num = -num;
                return Secursion(num - 1) - num;
            }
            //Secursion(num - 1) - num;  （2-1）-2 （2-1）变成了 return 1   1-2=-1  （3-1）-3
            //Secursion(num - 1) - num; （1-1）-1  return 1  （3-1）-3
            //Console.WriteLine(com);3*（3-1）-1 2*(（2-1）-2) 1*（1-1）-1 return 1

            //Secursion(3)
            //（4-1）-4=>(3-1)+3 => (2-1)-2 => return 1 一直向下传递
            //[return 1=>(2-1) =1-2=-1]; [return -1=>(3-1) -1+3=2];[return 2=>(4-1) 这里的4-1 就变成了 2; 2-4=-2]  向上回归
            return Secursion(num - 1) + num;
        }
        #endregion

        #region 显示日历 
        public static void PrintL()
        {
            // 1.在控制台中现实年历的方法 
            // * --调用12遍现实月历
            // 2在控制台中现实月历的方法
            // --显示表头Console.WriteLine("日\t—\t二\t......"");
            // --计算当月1日星期数，输出空白(\t)
            // Csonsole.Write(""\t"") 
            // --计算当月天数，输入1\t 2 3 4 * 
            // --每逢周六换行
            // *3.根据年月日，计算星期数[赠送] 
            // * 4.计算指定月份的天数
            // *5.判断闰年的方法
            // *2月闰年29天平年28天
            //  *年份能被4整除但是不能被100整除
            // 年份能被400整除
            string res = "";
            do
            {
                Console.WriteLine("请输入年份：");
                string inputYear = Console.ReadLine();
                int year = 0;

                while (!int.TryParse(inputYear, out year) || ShowYear(inputYear) == "年份有误")
                {
                    Console.WriteLine("年份有误,请重新输入：");
                    inputYear = Console.ReadLine();
                }
                Console.WriteLine("是否切换年份?(Y/N)");
                res = Console.ReadLine();
            } while (res == "Y");
            Console.WriteLine();
        }

        public static string ShowYear(string inputYear)
        {
            //string years = "";
            if (inputYear.Length == 4)
            {
                for (int i = 1; i <= 12; i++)
                {
                    //years += inputYear + "年" + i + "月";
                    Console.WriteLine(inputYear + "年" + i + "月");
                    ShowMonth(int.Parse(inputYear), i);
                }
                return "";
            }
            return "年份有误";
        }

        public static void ShowMonth(int year, int month)
        {
            Console.WriteLine("日\t—\t二\t三\t四\t五\t六");

            int dayOfweek = GetDayOfWeek(year, month, 1);
            for (int i = 0; i < dayOfweek; i++)
            {
                Console.Write("\t");
            }

            int Monthes = ShowMonthDay(year, month);
            for (int i = 1; i <= Monthes; i++)
            {
                #region 废弃 
                //int dayOfweek = GetDayOfWeek(year, month, i);
                //if (i == 1)
                //{
                //    //dayOfweek = GetDayOfWeek(year, month, i);
                //    switch (dayOfweek)
                //    {
                //        case 1:
                //            Console.Write("\t");
                //            break;
                //        case 2:
                //            Console.Write("\t\t");
                //            break;
                //        case 3:
                //            Console.Write("\t\t\t");
                //            break;
                //        case 4:
                //            Console.Write("\t\t\t\t");
                //            break;
                //        case 5:
                //            Console.Write("\t\t\t\t\t");
                //            break;
                //        case 6:
                //            Console.Write("\t\t\t\t\t\t");
                //            break;
                //        default:
                //            break;
                //    }
                //}
                #endregion

                Console.Write(i + "\t");
                if (GetDayOfWeek(year, month, i) == 6)
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
            //return "";
        }
        /// <summary>
        /// 计算指定月份天数
        /// </summary>
        /// <returns></returns>
        public static int ShowMonthDay(int year, int month)
        {
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12: return 31;
                case 4:
                case 6:
                case 9:
                case 11: return 30;
                case 2:
                    return (IsRunYear(year)) ? 29 : 28;
                default:
                    break;
            }
            return 0;
            #region 废弃 
            //int days = 0;
            //switch (month)
            //{
            //    case 1:
            //    case 3:
            //    case 5:
            //    case 7:
            //    case 8:
            //    case 10:
            //    case 12: days = 31; break;
            //    case 4:
            //    case 6:
            //    case 9:
            //    case 11: days = 30; break;
            //    case 2:
            //        if (IsRunYear(year)) days = 29; else days = 28; break;
            //    default:
            //        break;
            //}
            //return days;
            #endregion
        }

        /// <summary>
        /// .根据年月日，计算星期数
        /// 返回 0-6 的数字 0代表 周日
        /// </summary>
        /// <returns></returns>
        public static int GetDayOfWeek(int year, int month, int day)
        {
            DateTime dateTime = new DateTime(year, month, day);
            return (int)dateTime.DayOfWeek;
        }
        /// <summary>
        /// 计算闰年
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static bool IsRunYear(int year)
        {
            // *5.判断闰年的方法
            // *2月闰年29天平年28天
            //  *年份能被4整除但是不能被100整除
            // 年份能被400整除
            if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 计算秒数 
        public static void PrintM()
        {
            //ll每日一练∶
            //1.根据分钟数计算总秒数
            //2根据分钟数 小时数 计算总秒数
            //3.根据分钟数 小时数 天数计算总秒数
            //DateTime date=new DateTime(,)
            //int minute = 60;
            Console.WriteLine("请输入分钟：");
            int minute = int.Parse(Console.ReadLine());
            Console.WriteLine(GetTotalSeconds(minute) + "S");
            Console.WriteLine("请输入小时：");
            int hour = int.Parse(Console.ReadLine());
            Console.WriteLine(GetTotalSeconds(minute, hour) + "S");
            Console.WriteLine("请输入天数：");
            int day = int.Parse(Console.ReadLine());
            Console.WriteLine(GetTotalSeconds(minute, hour, day) + "S");
            //GetTotalSeconds(,)
        }
        public static int GetTotalSeconds(int Minute)
        {
            return Minute * 60;
        }
        public static int GetTotalSeconds(int Minutes, int Hours)
        {
            //Minutes = Minutes + (Hours * 60);
            return GetTotalSeconds(Minutes + Hours * 60);
        }
        public static int GetTotalSeconds(int Minutes, int Hours, int Days)
        {
            //Minutes = Minutes + ((Hours + Days * 24) * 60);
            //return GetSecondsFromMinutes(Minutes);
            return GetTotalSeconds(Minutes, Hours + Days * 24);
        }
        #endregion
    }
}
