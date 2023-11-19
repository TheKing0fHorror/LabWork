using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Лаба3
{
    internal class Program
    {
        delegate int Operation(int x, int y);

        static void Main(string[] args)
        {
            Operation summ = (x, y) => x + y;
            Operation mult = (x, y) => x * y;

            Console.WriteLine("\n");
            DrawFuncTable(summ, 5, 5);
            Console.WriteLine("\n");
            DrawFuncTable(mult, 5, 5);
        }

        private static void DrawFuncTable(Operation operation, int height, int weight)
        {
            int[,] funcTable = new int[height + 1, weight + 1];
            // Заполняем шапку таблицы значениями в матрице
            for (int i = 0; i <= height; i++)
                funcTable[i, 0] = i;
            for (int i = 0; i <= weight; i++)
                funcTable[0, i] = i;
            // Заполняем основную часть таблицы значениями в матрице
            for (int i = 1; i <= height; i++)
            {
                for (int j = 1; j <= weight; j++)
                {
                    funcTable[i, j] = operation(i, j);
                }
            }
            // Отстраиваем таблицу в консоли
            for (int i = 0; i <= height; i++)
            {
                for (int j = 0; j <= weight; j++)
                {
                    if ((i == 0 && j != 0) || (i != 0 && j == 0) || (i == 0 && j == 0))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write($"{funcTable[i, j],7}{'|',7}");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write($"{funcTable[i, j],7}{'|',7}");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                }
                Console.WriteLine();
                for (int j = 0; j <= weight; j++)
                {
                    if ((i == 0 && j != 0) || (i != 0 && j == 0) || (i == 0 && j == 0))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write("--------------");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("--------------");
                        Console.ForegroundColor = ConsoleColor.White;

                    }
                }
                Console.WriteLine();
            }
        }
    }
}
