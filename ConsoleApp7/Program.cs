using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;

namespace Consoleapp7
{
    public class Program
    {
        public struct Baggage
        {
            public string Name;    
            public double Weight;
        }

        public struct Passenger
        {
            public Baggage[] Items;
        }
        public class OneTwoThree
        {
            private double[,] array;

            public OneTwoThree(double[,] array)
            {
                this.array = array;
            }
            public OneTwoThree(int n, int m)

            {
                array = new double[n, m];
                double input = 0;
                for (int j = 0; j < m; j++)
                {
                    for (int i = n - 1; i >= 0; i--)
                    {
                        Console.WriteLine($"Введите элемент массива [{i},{j}]: ");
                        while (!double.TryParse(Console.ReadLine(), out input))
                        {
                            Console.WriteLine("Неверный ввод, введите число: ");
                        }
                        array[i, j] = input;
                    }
                }
            }
            public OneTwoThree(int n)
            {
                Random rnd = new Random();
                array = new double[n, n];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j <= i; j++)
                    {
                        array[i, j] = rnd.Next(-100, 101);

                    }
                }
                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = i + 1; j < n; j++)
                    {
                        array[i, j] = rnd.Next(-4500, 45676) / 1000.0;
                    }
                }
            }
            public OneTwoThree(int n, bool zigzag)
            {
                if (n <= 0)
                {
                    throw new ArgumentException("Размер должен быть больше 0");
                }
                array = new double[n, n];
                int counter = 1;
                for (int d = 0; d < n; d++)
                {
                    if (d % 2 == 0)
                    {
                        for (int i = d; i >= 0; i--)
                        {
                            array[i, d - i] = counter++;
                        }
                    }
                    else
                    {
                        for (int i = 0; i <= d; i++)
                        {
                            array[i, d - i] = counter++;
                        }
                    }
                }
            }
            public double? MinDouble()
            {
                if (array == null || array.Length == 0)
                {
                    return null;
                }
                double? min = null;
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    for (int j = 0; j < array.GetLength(1); j++)
                    {
                        bool repeat = false;
                        for (int k = 0; k < array.GetLength(0); k++)
                        {
                            for (int l = 0; l < array.GetLength(1); l++)
                            {
                                if (i == k && j == l)
                                {
                                    continue;
                                }
                                if (array[i, j] == array[k, l])
                                {
                                    repeat = true;
                                    break;
                                }
                            }
                        }
                        if (repeat)
                        {
                            if (min == null || array[i, j] < min)
                            {
                                min = array[i, j];
                            }
                        }
                    }
                }
                return min;
            }
            public static OneTwoThree operator +(OneTwoThree a, OneTwoThree b)
            {
                if (a.array.GetLength(0) != b.array.GetLength(0) || a.array.GetLength(1) != b.array.GetLength(1))
                {
                    throw new ArgumentException("Размеры массивов не совпадают");
                }
                double[,] result = new double[a.array.GetLength(0), a.array.GetLength(1)];
                for (int i = 0; i < a.array.GetLength(0); i++)
                {
                    for (int j = 0; j < a.array.GetLength(1); j++)
                    {
                        result[i, j] = a.array[i, j] + b.array[i, j];
                    }
                }
                return new OneTwoThree(result);
            }
            public static OneTwoThree operator -(OneTwoThree a, OneTwoThree b)
            {
                if (a.array.GetLength(0) != b.array.GetLength(0) || a.array.GetLength(1) != b.array.GetLength(1))
                {
                    throw new ArgumentException("Размеры массивов не совпадают");
                }
                double[,] result = new double[a.array.GetLength(0), a.array.GetLength(1)];
                for (int i = 0; i < a.array.GetLength(0); i++)
                {
                    for (int j = 0; j < a.array.GetLength(1); j++)
                    {
                        result[i, j] = a.array[i, j] - b.array[i, j];
                    }
                }
                return new OneTwoThree(result);
            }
            public static OneTwoThree operator *(OneTwoThree a, OneTwoThree b)
            {
                if (a.array.GetLength(1) != b.array.GetLength(0))
                {
                    throw new ArgumentException("Размеры массивов не совпадают");
                }
                double[,] result = new double[a.array.GetLength(0), b.array.GetLength(1)];
                for (int i = 0; i < a.array.GetLength(0); i++)
                {
                    for (int j = 0; j < b.array.GetLength(1); j++)
                    {
                        for (int k = 0; k < a.array.GetLength(1); k++)
                        {
                            result[i, j] += a.array[i, k] * b.array[k, j];
                        }
                    }
                }
                return new OneTwoThree(result);
            }
            public static OneTwoThree operator *(int scalar,OneTwoThree a)
            {
                double[,] result = new double[a.array.GetLength(0), a.array.GetLength(1)];
                for (int i = 0; i < a.array.GetLength(0); i++)
                {
                    for (int j = 0; j < a.array.GetLength(1); j++)
                    {
                        result[i, j] = a.array[i, j] * scalar;
                    }
                }
                return new OneTwoThree(result);
            }
            public OneTwoThree Transpose()
            {
                double[,] result = new double[array.GetLength(1), array.GetLength(0)];
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    for (int j = 0; j < array.GetLength(1); j++)
                    {
                        result[j, i] = array[i, j];
                    }
                }
                return new OneTwoThree(result);
            }

            public override string ToString()
            {
                string result = "";
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    for (int j = 0; j < array.GetLength(1); j++)
                    {
                        result += $"{array[i, j],10:0.###}";
                    }
                    result += "\n";
                }
                return result;
            }
        }
        public class DocsWork
        {
          
            public static void binFile4(string path)
            {
                Random rnd = new Random();
                try
                {

                    using (BinaryWriter bw = new BinaryWriter(File.Open(path, FileMode.Create)))
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            bw.Write(rnd.Next(-10, 10));
                        }

                        Console.WriteLine("Бинарный файл создан");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при открытии бинарного файла: {ex.Message}");
                }

            }

            public static int BinFile41(string path)
            {
                try
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open))
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            int min = 1;
                            while (br.BaseStream.Position != br.BaseStream.Length)
                            {
                                int number = br.ReadInt32();
                                if (number < 0 && number % 2 != 0)
                                {
                                    Console.WriteLine($"Отрицательное значение: {number}");
                                    min *= number;
                                }
                            }
                            Console.WriteLine($"Сумма отрицательных значений: {min}");
                            return min;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при открытии бинарного файла: {ex.Message}");
                    return 0;
                }

            }
            public static void binFile5(string path)
            {
                Random rnd = new Random();
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Passenger));
                    using (BinaryWriter bw = new BinaryWriter(File.Open(path, FileMode.Create)))
                    {
                        int count = 6;
                        bw.Write(count);
                        for (int i = 0; i < count; i++)
                        {
                            Passenger p = new Passenger();
                            int items = rnd.Next(1, 6);
                            p.Items = new Baggage[items];
                            for (int j = 0; j < items; j++)
                            {
                                p.Items[j].Name = "чемодан";
                                p.Items[j].Weight = rnd.Next(1, 30);
                            }

                            using (StringWriter sw = new StringWriter())
                            {
                                serializer.Serialize(sw, p);
                                bw.Write(sw.ToString());
                            }
                        }
                    }
                    Console.WriteLine("Бинарный файл с багажом создан");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при создании файла: {ex.Message}");
                }
            }

            public static void binFile51(string path)
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Passenger));
                    using (BinaryReader br = new BinaryReader(File.Open(path, FileMode.Open)))
                    {
                        int count = br.ReadInt32();
                        Passenger[] passengers = new Passenger[count];
                        for (int i = 0; i < count; i++)
                        {
                            string xml = br.ReadString();
                            using (StringReader sr = new StringReader(xml))
                            {
                                passengers[i] = (Passenger)serializer.Deserialize(sr);
                            }
                        }

                        int total = 0;
                        foreach (Passenger p in passengers)
                        {
                            total += p.Items.Length;
                        }
                        double average = (double)total / count;

                        int moreThanTwo = 0;
                        int aboveAverage = 0;
                        foreach (Passenger p in passengers)
                        {
                            if (p.Items.Length > 2)
                            {
                                moreThanTwo++;
                            }
                            if (p.Items.Length > average)
                            {
                                aboveAverage++;
                            }
                        }

                        Console.WriteLine($"Среднее число единиц багажа: {average}");
                        Console.WriteLine($"Пассажиров с багажом больше двух единиц: {moreThanTwo}");
                        Console.WriteLine($"Пассажиров с багажом выше среднего: {aboveAverage}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при открытии бинарного файла: {ex.Message}");
                }
            }

            public static void txtFile6(string path)
            {
                Random rnd = new Random();
                try
                {
                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            sw.WriteLine(rnd.Next(1, 10));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при открытии текстового файла: {ex.Message}");
                }


            }
            public static int txtFile61(string path)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        int sum = 0;
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (int.TryParse(line, out int number))
                            {
                                Console.WriteLine($"Число из текстового файла: {number}");
                                sum += number * number;
                            }
                        }
                        Console.WriteLine($"Сумма квадратов чисел в текстовом файле: {sum}");
                        return sum;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при открытии текстового файла: {ex.Message}");
                    return 0;
                }
            }
            public static void txtFile7(string path)
            {
                Random rnd = new Random();
                try
                {
                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            for (int j = 0; j < rnd.Next(2, 4); j++)
                            {
                                sw.Write(rnd.Next(1, 5) + " ");
                            }
                            sw.WriteLine();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при открытии текстового файла: {ex.Message}");
                }

            }
            public static int txtFile71(string path)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        int sum = 1;
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                            foreach (string number in numbers)
                            {
                                if (int.TryParse(number, out int num))
                                {
                                    Console.WriteLine($"Число из текстового файла: {num}");
                                    sum *= num;
                                }
                            }
                        }
                        Console.WriteLine($"Произведение чисел в текстовом файле: {sum}");
                        return sum;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при открытии текстового файла: {ex.Message}");
                    return 0;
                }

            }
            public static void txtFile8(string path, string path2)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        using (StreamWriter sw = new StreamWriter(path2))

                        {   Console.WriteLine("Введите длинну строки: ");
                            int m = int.Parse(Console.ReadLine());
                            
                            string line = null;
                            while ((line = sr.ReadLine()) != null)
                            {
                                if (line.Length == m)
                                {
                                    sw.WriteLine(line);
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при открытии текстового файла: {ex.Message}");
                }
            }
        }
    }
}

        
     
        
