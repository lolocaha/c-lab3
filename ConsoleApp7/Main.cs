using static Consoleapp7.Program;

namespace ConsoleApp7
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\n=== МЕНЮ ===");
                Console.WriteLine("1 - Задание 1 (массивы)");
                Console.WriteLine("2 - Задание 2 (минимальный из повторяющихся)");
                Console.WriteLine("3 - Задание 3 (матричное выражение)");
                Console.WriteLine("4 - Задание 4 (бинарный файл)");
                Console.WriteLine("5 - Задание 5 (багаж, XML)");
                Console.WriteLine("6 - Задание 6 (текст, по одному числу)");
                Console.WriteLine("7 - Задание 7 (текст, несколько чисел)");
                Console.WriteLine("8 - Задание 8 (строки заданной длины)");
                Console.WriteLine("0 - Выход");

                int choice = ReadInt("Выберите пункт: ");
                Console.WriteLine();

                switch (choice)
                {
                    case 1: Task1(); break;
                    case 2: Task2(); break;
                    case 3: Task3(); break;
                    case 4: Task4(); break;
                    case 5: Task5(); break;
                    case 6: Task6(); break;
                    case 7: Task7(); break;
                    case 8: Task8(); break;
                    case 0: return;
                    default: Console.WriteLine("Нет такого пункта"); break;
                }
            }
        }

        static void Task1()
        {
            Console.WriteLine("1 - первый массив (ввод), 2 - второй (случайный), 3 - третий (зигзаг)");
            int sub = ReadInt("Какой массив: ");
            switch (sub)
            {
                case 1:
                    int n = ReadInt("Строк n: ");
                    int m = ReadInt("Столбцов m: ");
                    if (n <= 0 || m <= 0)
                    {
                        Console.WriteLine("Размеры должны быть больше 0");
                        return;
                    }
                    Console.WriteLine(new OneTwoThree(n, m));
                    break;
                case 2:
                    int n2 = ReadInt("Размер n: ");
                    if (n2 <= 0)
                    {
                        Console.WriteLine("Размер должен быть больше 0");
                        return;
                    }
                    Console.WriteLine(new OneTwoThree(n2));
                    break;
                case 3:
                    int n3 = ReadInt("Размер n: ");
                    if (n3 <= 0)
                    {
                        Console.WriteLine("Размер должен быть больше 0");
                        return;
                    }
                    Console.WriteLine(new OneTwoThree(n3, true));
                    break;
                default:
                    Console.WriteLine("Нет такого пункта");
                    break;
            }
        }

        static void Task2()
        {
            OneTwoThree t1 = new OneTwoThree(new double[,] { { 1, 2, 2 }, { 3, 1, 5 } });
            Console.WriteLine(t1);
            Console.WriteLine($"Ответ: {t1.MinDouble()} (ожидается 1)\n");

            OneTwoThree t2 = new OneTwoThree(new double[,] { { 1, 2 }, { 3, 4 } });
            double? r2 = t2.MinDouble();
            Console.WriteLine(r2 == null ? "Повторов нет (так и должно быть)\n" : $"Ошибка: {r2}\n");

            OneTwoThree t3 = new OneTwoThree(new double[,] { { 7, 7 }, { 1, 2 } });
            Console.WriteLine($"Только 7 повторяется: {t3.MinDouble()} (ожидается 7)");
        }

        static void Task3()
        {
            OneTwoThree A = new OneTwoThree(new double[,] { { 1, 2 }, { 3, 4 } });
            OneTwoThree B = new OneTwoThree(new double[,] { { 0, 1 }, { 1, 0 } });
            OneTwoThree C = new OneTwoThree(new double[,] { { 2, 0 }, { 1, 3 } });
            Console.WriteLine("A:\n" + A);
            Console.WriteLine("B:\n" + B);
            Console.WriteLine("C:\n" + C);
            Console.WriteLine("C^T:\n" + C.Transpose());
            OneTwoThree result = A + 2 * B - 3 * C.Transpose();
            Console.WriteLine("A + 2*B - 3*C^T =\n" + result);
            Console.WriteLine("Ожидается:\n        -5         1\n         5        -5");
        }

        static void Task4()
        {
            DocsWork.binFile4("binfile4.bin");
            ShowBin("binfile4.bin");
            DocsWork.BinFile41("binfile4.bin");
        }

        static void Task5()
        {
            DocsWork.binFile5("binfile5.bin");
            DocsWork.binFile51("binfile5.bin");
        }

        static void Task6()
        {
            DocsWork.txtFile6("binfile6.txt");
            DocsWork.txtFile61("binfile6.txt");
        }

        static void Task7()
        {
            DocsWork.txtFile7("binfile7.txt");
            ShowFile("binfile7.txt");
            DocsWork.txtFile71("binfile7.txt");
        }

        static void Task8()
        {
            File.WriteAllLines("test8.txt", new string[] { "abc", "hello", "xyz", "world", "ok" });
            Console.WriteLine("Исходный файл:");
            ShowFile("test8.txt");
            DocsWork.txtFile8("test8.txt", "text8_copy.txt");
            Console.WriteLine("Результат (для длины 5 ожидается hello и world):");
            ShowFile("text8_copy.txt");
        }

        static int ReadInt(string message)
        {
            Console.Write(message);
            int result;
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.Write("Введите целое число: ");
            }
            return result;
        }

        static void ShowFile(string path)
        {
            if (File.Exists(path))
            {
                foreach (string line in File.ReadAllLines(path))
                {
                    Console.WriteLine(line);
                }
            }
        }

        static void ShowBin(string path)
        {
            using (BinaryReader br = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                Console.Write("Содержимое файла: ");
                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    Console.Write(br.ReadInt32() + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
