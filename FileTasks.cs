using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Lab3
{
    /// <summary>
    /// Задачи на работу с файлами (задания 4-8).
    /// </summary>
    public static class FileTasks
    {
        /// <summary>
        /// Задание 4.10. Находит количество удвоенных нечетных
        /// чисел в бинарном файле.
        /// </summary>
        public static void SolveTask4()
        {
            string path = "task4.bin";
            FillTask4File(path);

            int count = 0;
            string content = "";

            using (BinaryReader reader = new BinaryReader
                       (File.Open(path, FileMode.Open)))
            {
                while (reader.BaseStream.Position < 
                       reader.BaseStream.Length)
                {
                    int number = reader.ReadInt32();
                    content += number + " ";

                    if (number % 2 == 0 && (number / 2) % 2 != 0)
                    {
                        count++;
                    }
                }
            }

            Console.WriteLine("Содержимое файла: " + content);
            Console.WriteLine
                ("Количество удвоенных нечетных чисел: " + count);
        }

        /// <summary>
        /// Задание 5.10. Находит название самой дешевой
        /// игрушки в XML-файле.
        /// </summary>
        public static void SolveTask5()
        {
            string path = "task5.xml";
            FillTask5File(path);

            XmlSerializer serializer = new XmlSerializer
                (typeof(List<Toy>));
            List<Toy> toys;

            using (FileStream stream = new FileStream
                       (path, FileMode.Open))
            {
                toys = (List<Toy>)serializer.Deserialize(stream);
            }

            if (toys.Count == 0)
            {
                Console.WriteLine("Файл не содержит игрушек.");
                return;
            }

            Toy cheapest = toys[0];
            for (int i = 1; i < toys.Count; i++)
            {
                Console.WriteLine(toys[i].Name + ": " +
                                  toys[i].Price + " руб.");

                if (toys[i].Price < cheapest.Price)
                {
                    cheapest = toys[i];
                }
            }

            Console.WriteLine("Самая дешевая игрушка: " + 
                              cheapest.Name + 
                              " (" + cheapest.Price + " руб.)");
        }

        /// <summary>
        /// Задание 6.10. Находит квадрат разности максимального
        /// и минимального
        /// </summary>
        public static void SolveTask6()
        {
            string path = "task6.txt";
            FillTask6File(path);

            int max = int.MinValue;
            int min = int.MaxValue;
            bool found = false;

            string[] lines = File.ReadAllLines(path);
            Console.WriteLine("Содержимое файла: " + string.Join
                (" ", lines));

            foreach (string line in lines)
            {
                int number;

                if (int.TryParse(line, out number))
                {
                    found = true;

                    if (number > max)
                    {
                        max = number;
                    }

                    if (number < min)
                    {
                        min = number;
                    }
                }
            }

            if (!found)
            {
                Console.WriteLine("В файле нет чисел.");
                return;
            }

            long difference = (long)max - min;
            Console.WriteLine("Максимум: " + max + ", " +
                              "минимум: " + min);
            Console.WriteLine("Квадрат разности: " + 
                              difference * difference);
        }

        /// <summary>
        /// Задание 7.10. Вычисляет сумму нечетных элементов
        /// (в файле по несколько чисел в строке).
        /// </summary>
        public static void SolveTask7()
        {
            string path = "task7.txt";
            FillTask7File(path);

            long sum = 0;
            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                Console.WriteLine(line);

                string[] parts = line.Split(new char[] { ' ' },
                    StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in parts)
                {
                    int number;

                    if (int.TryParse(part, out number) && 
                        number % 2 != 0)
                    {
                        sum += number;
                    }
                }
            }

            Console.WriteLine("Сумма нечетных элементов: " + sum);
        }

        /// <summary>
        /// Задание 8.10. Создает новый файл,
        /// в каждой строке которого находится
        /// последний символ соответствующей
        /// строки исходного файла.
        /// </summary>
        public static void SolveTask8()
        {
            string inputPath = "task8_in.txt";
            string outputPath = "task8_out.txt";
            FillTask8File(inputPath);

            string[] lines = File.ReadAllLines(inputPath);

            using (StreamWriter writer = new StreamWriter
                       (outputPath))
            {
                foreach (string line in lines)
                {
                    if (line.Length > 0)
                    {
                        writer.WriteLine(line[line.Length - 1]);
                    }
                    else
                    {
                        writer.WriteLine();
                    }
                }
            }

            Console.WriteLine("Исходный файл:");
            Console.WriteLine(string.Join("\n", lines));
            Console.WriteLine("Новый файл (" + outputPath + "):");
            Console.WriteLine(string.Join("\n", File.ReadAllLines
                (outputPath)));
        }

        /// <summary>
        /// Заполняет бинарный файл случайными целыми числами.
        /// </summary>
        private static void FillTask4File(string path)
        {
            Random random = new Random();

            using (BinaryWriter writer = new BinaryWriter
                       (File.Open(path, FileMode.Create)))
            {
                for (int i = 0; i < 20; i++)
                {
                    writer.Write(random.Next(-50, 50));
                }
            }
        }

        /// <summary>
        /// Заполняет XML-файл списком игрушек с помощью
        /// сериализации.
        /// </summary>
        private static void FillTask5File(string path)
        {
            List<Toy> toys = new List<Toy>();
            toys.Add(new Toy { Name = "Кукла", Price = 1500,
                MinAge = 3, MaxAge = 6 });
            toys.Add(new Toy { Name = "Мяч", Price = 500,
                MinAge = 2, MaxAge = 10 });
            toys.Add(new Toy { Name = "Конструктор",
                Price = 3000, MinAge = 6, MaxAge = 14 });
            toys.Add(new Toy { Name = "Кубики",
                Price = 350, MinAge = 1, MaxAge = 5 });
            toys.Add(new Toy { Name = "Машинка",
                Price = 800, MinAge = 3, MaxAge = 8 });

            XmlSerializer serializer = new XmlSerializer
                (typeof(List<Toy>));

            using (FileStream stream = new FileStream
                       (path, FileMode.Create))
            {
                serializer.Serialize(stream, toys);
            }
        }

        /// <summary>
        /// Заполняет текстовый файл случайными числами,
        /// по одному в строке.
        /// </summary>
        private static void FillTask6File(string path)
        {
            Random random = new Random();

            using (StreamWriter writer = new StreamWriter(path))
            {
                for (int i = 0; i < 10; i++)
                {
                    writer.WriteLine(random.Next(-20, 20));
                }
            }
        }

        /// <summary>
        /// Заполняет текстовый файл случайными числами,
        /// по несколько в строке.
        /// </summary>
        private static void FillTask7File(string path)
        {
            Random random = new Random();

            using (StreamWriter writer = new StreamWriter(path))
            {
                for (int i = 0; i < 5; i++)
                {
                    int count = random.Next(2, 6);
                    string line = "";

                    for (int j = 0; j < count; j++)
                    {
                        line += random.Next(-10, 10) + " ";
                    }

                    writer.WriteLine(line.Trim());
                }
            }
        }

        /// <summary>
        /// Заполняет текстовый файл несколькими строками текста.
        /// </summary>
        private static void FillTask8File(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine("Привет мир");
                writer.WriteLine("Это лабораторная");
                writer.WriteLine("Программирование");
                writer.WriteLine("Конец");
            }
        }
    }
}
