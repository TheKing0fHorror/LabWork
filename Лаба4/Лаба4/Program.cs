using System;
using System.IO;
using System.Text;

namespace Лаба4
{
    // Программа решает квадратное уравнение и вылавливает ошибки:
    // 1) Дискриминант < 0;
    // 2) Коэффициент а = 0;
    // 3) Некорректные данные в файле с коэф-ами;
    // 4) Файла не существует.

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n" + "\t" + "Программа решает квадратное уравнение по данным из файла" + "\n");
            Console.ForegroundColor = ConsoleColor.White;

            double a = 0;
            double b = 0;
            double c = 0;
            double D;

            try
            {
                StreamReader valuesABC = new StreamReader("values.txt");
                string strValuesABC = valuesABC.ReadToEnd();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("a b c");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(strValuesABC);
                Console.ForegroundColor = ConsoleColor.White;
                strValuesABC = strValuesABC.Replace("\r\n", " ");
                string[] splitValues = strValuesABC.Split(" ");
                string[,] setsValuesABC = ConvertArray(splitValues, 3);

                double save;
                for (byte i = 0; i < setsValuesABC.GetLength(0); i++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("\n" + "Решение квадратного уравнения по набору переменных ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"({i + 1}) ");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(":");
                    Console.ForegroundColor = ConsoleColor.White;

                    for (byte j = 0; j < setsValuesABC.GetLength(1); j++)
                    {
                        if (double.TryParse(setsValuesABC[i, j], out save))
                        {
                            switch (j)
                            {
                                case 0:
                                    a = save;
                                    if (a == 0)
                                        throw new CoefficientAZero();
                                    break;

                                case 1:
                                    b = save;
                                    break;

                                case 2:
                                    c = save;
                                    break;
                            }
                        }
                        else
                            throw new IncorrectData();
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    D = Math.Pow(b, 2) - 4 * a * c;
                    if (D < 0)
                    {
                        throw new DiscriminantZero();
                    }
                    else if (D == 0)
                    {
                        Console.WriteLine("\t" + "X1 = X2 = " + (-b / (2 * a)));
                    }
                    else if (D > 0)
                    {
                        Console.WriteLine("\t" + "X1 = " + (-b + Math.Sqrt(D) / (2 * a)));
                        Console.WriteLine("\t" + "X2 = " + (-b - Math.Sqrt(D) / (2 * a)));
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            catch (IncorrectData ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Error error = new Error();
                error.LogJournal(ex);
            }
            catch (CoefficientAZero ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Error error = new Error();
                error.LogJournal(ex);
            }
            catch (DiscriminantZero ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Error error = new Error();
                error.LogJournal(ex);
            }
            catch (FileNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Error error = new Error();
                error.LogJournal(ex);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Error error = new Error();
                error.LogJournal(ex);
            }
        }

        private static string[,] ConvertArray(string[] splitValues, byte widthArray)
        {
            int heightArray = splitValues.Length / 3;
            string[,] setsValuesABC = new string[heightArray, widthArray];

            int row, col;
            for (byte i = 0; i < splitValues.Length; i++)
            {
                row = i / widthArray;
                col = i % widthArray;
                setsValuesABC[row, col] = splitValues[i];
            }

            return setsValuesABC;
        }
    }
}