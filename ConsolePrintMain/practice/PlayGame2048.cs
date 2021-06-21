using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePrintMain.practice
{
    public struct LocationIndex
    {
        public int RowIndex { get; set; }
        public int ColIndex { get; set; }

        public LocationIndex(int row, int col) : this()
        {
            RowIndex = row;
            ColIndex = col;
        }

    }
    public class PlayGame2048
    {

        private static Random Random;
        private int[] colArray;
        private int[] removeZeroArray;
        private List<LocationIndex> locations;
        private int[,] ChangeArray { get; }
        public int[,] Map { get; }

        public PlayGame2048()
        {
            Map = new int[4, 4];
            colArray = new int[Map.GetLength(0)];
            removeZeroArray = new int[4];
            Random = new Random();
            locations = new List<LocationIndex>();
            ChangeArray = new int[4, 4];
        }
        /// <summary>
        /// 打印数组地图
        /// </summary>
        private void ConsolePrint()
        {
            Console.Clear();
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    Console.Write(Map[i, j] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine("键盘 ↑↓←→ 控制移动");
        }

        public void ConsolePlayGame()
        {
            CreatNewRandomNumber();
            CreatNewRandomNumber();

            ConsolePrint();

            while (true)
            {

                KeyDownMove(Console.ReadKey());

                //移动后对比
                ChangeMap();

                if (IsChangeMap)
                {
                    // 每次移动都重新生成一个
                    CreatNewRandomNumber();

                    ConsolePrint();

                    if (IsGameOver())
                    {
                        Console.WriteLine("GAME OVER");
                        break;
                    }
                }

            }

            //Console.Clear();
            //for (int i = 0; i < Map.GetLength(0); i++)
            //{
            //    for (int j = 0; j < Map.GetLength(1); j++)
            //    {
            //        Console.Write(Map[i, j] + "\t");
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine("键盘 ↑↓←→ 控制移动");
        }

        private void KeyDownMove(ConsoleKeyInfo key)
        {
            //移动前记录Map   
            Array.Copy(Map, ChangeArray, Map.Length);
            IsChangeMap = false;//假设没有发生改变

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    MoveUp();
                    break;
                case ConsoleKey.DownArrow:
                    MoveDown();
                    break;
                case ConsoleKey.LeftArrow:
                    MoveLeft();
                    break;
                case ConsoleKey.RightArrow:
                    MoveRight();
                    break;
                default:
                    break;
            }

        }
        public bool IsChangeMap { get; set; }
        /// <summary>
        /// 检测地图是否发上改变
        /// </summary>
        /// <returns></returns>
        private void ChangeMap()
        {
            for (int r = 0; r < Map.GetLength(0); r++)
            {
                for (int c = 0; c < Map.GetLength(1); c++)
                {
                    if (Map[r, c] != ChangeArray[r, c])
                    {
                        IsChangeMap = true;
                        return;
                    }
                }
            }
        }
        public bool IsGameOver()
        {
            if (locations.Count > 0)
            {
                return false;
            }
            // 水平 同时 垂直
            for (int r = 0; r < Map.GetLength(0); r++)
            {
                for (int c = 0; c < Map.GetLength(1) - 1; c++)
                {
                    if (Map[r, c] == Map[r, c + 1] || Map[c, r] == Map[c + 1, r])
                    {
                        return false;
                    }
                }
            }

            //// 水平
            //for (int r = 0; r < Map.GetLength(0); r++)
            //{
            //    for (int c = 0; c < Map.GetLength(1) - 1; c++)
            //    {
            //        if (Map[r, c] == Map[r + 1, c])
            //        {
            //            return false;
            //        }
            //    }
            //}
            //// 垂直
            //for (int c = 0; c < Map.GetLength(1); c++)
            //{
            //    for (int r = 0; r < Map.GetLength(0) - 1; r++)
            //    {
            //        if (Map[r, c] == Map[r + 1, c])
            //        {
            //            return false;
            //        }
            //    }
            //}
            return true;
        }

        /// <summary>
        /// 生成新的随机数
        /// </summary>
        private void CreatNewRandomNumber()
        {
            GetZeroElementsByDoubleArray();
            if (locations.Count > 0)
            {
                //随机生成 list索引
                int listIndex = Random.Next(0, locations.Count);
                // 将索引存的的 数据拿出来
                LocationIndex locationIndex = locations[listIndex];
                // 放入到对应的二维数组中
                Map[locationIndex.RowIndex, locationIndex.ColIndex] = Random.Next(0, 10) == 1 ? 4 : 2;

                // 然后移除该元素
                locations.RemoveAt(listIndex);
            }
        }

        /// <summary>
        /// 获取零元素
        /// </summary>
        private void GetZeroElementsByDoubleArray()
        {
            // 每次获取零元素 都必须先清空之前的元素 不然会一直叠加
            locations.Clear();
            for (int r = 0; r < Map.GetLength(0); r++)
            {
                for (int c = 0; c < Map.GetLength(1); c++)
                {
                    if (Map[r, c] == 0)
                        locations.Add(new LocationIndex(r, c));
                }
            }
        }

        #region 方向控制
        private void MoveUp()
        {
            //int[] colArray = new int[doubleArray.GetLength(1)];
            for (int col = 0; col < Map.GetLength(1); col++)
            {
                for (int row = 0; row < Map.GetLength(0); row++)
                {
                    // 从当前二维数组 的 第一行第一列 开始获取 (上移 即 从上往下开始获取)
                    colArray[row] = Map[row, col];
                }
                // 去零后开始合并 
                Merge();
                // 合并后 还原每列
                for (int row = 0; row < Map.GetLength(0); row++)
                {
                    Map[row, col] = colArray[row];
                }
            }
        }
        private void MoveDown()
        {
            //int[] colArray = new int[doubleArray.GetLength(1)];
            for (int col = 0; col < Map.GetLength(1); col++)
            {
                for (int row = Map.GetLength(0) - 1; row >= 0; row--)
                {
                    // 从当前二维数组 的 第一行第一列 开始获取 (下移 即 从下往上开始获取)
                    colArray[3 - row] = Map[row, col];
                }

                Merge();        // 去零后开始合并 
                for (int row = Map.GetLength(0) - 1; row >= 0; row--)
                {
                    Map[row, col] = colArray[3 - row];
                }
            }
        }
        private void MoveLeft()
        {
            //int[] colArray = new int[doubleArray.GetLength(1)];
            for (int row = 0; row < Map.GetLength(0); row++)
            {
                for (int col = 0; col < Map.GetLength(1); col++)
                {
                    colArray[col] = Map[row, col];
                }
                Merge();
                for (int col = 0; col < Map.GetLength(1); col++)
                {
                    Map[row, col] = colArray[col];
                }
            }
        }
        private void MoveRight()
        {
            //int[] colArray = new int[doubleArray.GetLength(1)];
            for (int row = 0; row < Map.GetLength(0); row++)
            {
                for (int col = Map.GetLength(1) - 1; col >= 0; col--)
                {
                    colArray[3 - col] = Map[row, col];
                }
                Merge();
                for (int col = Map.GetLength(1) - 1; col >= 0; col--)
                {
                    Map[row, col] = colArray[3 - col];
                }
            }
        }
        #endregion

        private void RemoveZero()
        {
            //removeZeroArray 将上次残留的数据清除  设置为零
            Array.Clear(removeZeroArray, 0, removeZeroArray.Length);
            //int[] removeZeroArray = new int[colArray.Length];
            int index = 0;
            for (int i = 0; i < colArray.Length; i++)
            {
                if (colArray[i] != 0)
                {
                    removeZeroArray[index++] = colArray[i];
                }
            }
            removeZeroArray.CopyTo(colArray, 0);
        }

        // 存的数组的引用 即内存地址 修改则是修改堆里面的数据
        private void Merge()
        {
            RemoveZero();   // 第一列获取完 开始去零
            // 只需要获取前三个 元素即可
            for (int i = 0; i < colArray.Length - 1; i++)
            {
                // 如果 数组第一个元素 和第二个元素相同 就相加 随后吧第二个元素改为零
                if (colArray[i] != 0 && colArray[i] == colArray[i + 1])
                {
                    colArray[i] += colArray[i + 1];
                    colArray[i + 1] = 0;
                }
            }
            /*
             2  4   4
             2  0   4
             0  0   0
             4  4   0
             */
            // 合并完 在去一次零
            RemoveZero();
        }
        //****************2048游戏核心算法*************************************

        /*
         1.0版本
         * 1. 上移动
         *    -- 获取每列数据(将二维数组  -->  一维数组)4    2    0    2                   4  4   2  2
         *    -- 将零元素移动到末尾4    2    2    0
         *    -- 相邻相同则合并 4   4   0   0                  8   0  4  0
         *       -- 将后一个元素累加到前一个元素上
         *       -- 将后一个元素清零
               -- 将零元素移动到末尾 
         *    -- 还原每列数据
         *     
         * 2. 下移动
         *  -- 获取每列数据(将二维数组  -->  一维数组)
         *  -- 将零元素移动到开头 
             -- 相邻相同则合并 
         *       -- 将前一个元素累加到后一个元素上
         *       -- 将前一个元素清零
         *  -- 将零元素移动到开头 
         *  -- 还原每列数据 
         *  
         * 
         * 2.0版本
         * 1. 上移动
         *    -- 从上到下获取每列数据(将二维数组  -->  一维数组)4    2    0    2                   4  4   2  2
         *    -- 调用合并数据方法
         *    -- 还原每列数据
         *     
         * 2. 下移动
         *  -- 从下到上获取每列数据(将二维数组  -->  一维数组)
             -- 调用合并数据方法
         *  -- 还原每列数据
         
         * 3.合并数据 2   2   2   0    -->   4  2  0  0
         *  -- 调用去零方法
         *    -- 相邻相同则合并 4   4   0   0                  8   0  4  0
         *       -- 将后一个元素累加到前一个元素上
         *       -- 将后一个元素清零
            -- 调用去零方法
         * 
         * 4.移动零元素方法( 将零元素移动到末尾 )2   0   2   0  -->  2   2   0  0
         */
    }
}
