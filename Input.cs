using System;

namespace Lab3
{
    /// <summary>
    /// Ввод данных с клавиатуры с проверкой корректности.
    /// </summary>
    public static class Input
    {
        /// <summary>
        /// Считывает целое число, повторяя запрос
        /// до корректного ввода.
        /// </summary>
        public static int ReadInt(string message)
        {
            while (true)
            {
                Console.Write(message);
                string line = Console.ReadLine();
                int result;

                if (int.TryParse(line, out result))
                {
                    return result;
                }

                Console.WriteLine
                    ("Ошибка: нужно ввести целое число.");
            }
        }

        /// <summary>
        /// Считывает целое число строго больше нуля.
        /// </summary>
        public static int ReadPositiveInt(string message)
        {
            while (true)
            {
                int result = ReadInt(message);

                if (result > 0)
                {
                    return result;
                }

                Console.WriteLine
                    ("Ошибка: число должно быть больше нуля.");
            }
        }

        /// <summary>
        /// Считывает вещественное число, повторяя запрос
        /// до корректного ввода.
        /// </summary>
        public static double ReadDouble(string message)
        {
            while (true)
            {
                Console.Write(message);
                string line = Console.ReadLine();
                double result;

                if (double.TryParse(line, out result))
                {
                    return result;
                }

                Console.WriteLine("Ошибка: нужно ввести число.");
            }
        }
    }
}
