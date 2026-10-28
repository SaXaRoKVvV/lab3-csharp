using System;

namespace Lab3
{
    /// <summary>
    /// Главный класс программы.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Точка входа. Выводит меню и запускает
        /// выбранное задание.
        /// </summary>

        public static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("=== Лабораторная работа №3." +
                                  " Вариант 10 ===");
                Console.WriteLine("1 - Задание 1 (заполнение" +
                                  " двумерных массивов)");
                Console.WriteLine("2 - Задание 2 (элементы больше" +
                                  " элементов главной диагонали)");
                Console.WriteLine("3 - Задание 3 (матричное" +
                                  " выражение)");
                Console.WriteLine("4 - Задание 4 (бинарные файлы)");
                Console.WriteLine("5 - Задание 5 (структуры и" +
                                  " XML-сериализация)");
                Console.WriteLine("6 - Задание 6 (текстовый файл," +
                                  " одно число в строке)");
                Console.WriteLine("7 - Задание 7 (текстовый файл," +
                                  " несколько чисел в строке)");
                Console.WriteLine("8 - Задание 8 (текстовый файл," +
                                  " последний символ строки)");
                Console.WriteLine("0 - Выход");
                Console.Write("Выбор: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RunTask1();
                        break;
                    case "2":
                        RunTask2();
                        break;
                    case "3":
                        RunTask3();
                        break;
                    case "4":
                        FileTasks.SolveTask4();
                        break;
                    case "5":
                        FileTasks.SolveTask5();
                        break;
                    case "6":
                        FileTasks.SolveTask6();
                        break;
                    case "7":
                        FileTasks.SolveTask7();
                        break;
                    case "8":
                        FileTasks.SolveTask8();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный пункт меню.");
                        break;
                }
            }
        }

        /// <summary>
        /// Задание 1. Создает три массива
        /// разными конструкторами и выводит их.
        /// </summary>
        private static void RunTask1()
        {
            Console.WriteLine();
            Console.WriteLine("--- Задание 1 ---");

            int n = Input.ReadPositiveInt
                ("Введите количество строк n: ");
            int m = Input.ReadPositiveInt
                ("Введите количество столбцов m: ");

            Console.WriteLine
                ("Первый массив (ввод с клавиатуры по столбцам):");
            Matrix first = new Matrix(n, m);
            Console.WriteLine(first);

            int size = Input.ReadPositiveInt
                ("Введите размер n " +
                 "для квадратных массивов: ");

            Console.WriteLine("Второй массив (шахматная доска):");
            Matrix second = new Matrix(size, MatrixType.Chessboard);
            Console.WriteLine(second);

            Console.WriteLine("Третий массив (змейка):");
            Matrix third = new Matrix(size, MatrixType.Snake);
            Console.WriteLine(third);
        }

        /// <summary>
        /// Задание 2. Считает элементы,
        /// большие любого элемента главной диагонали.
        /// </summary>
        private static void RunTask2()
        {
            Console.WriteLine();
            Console.WriteLine("--- Задание 2 ---");

            int n = Input.ReadPositiveInt
                ("Введите количество строк n: ");
            int m = Input.ReadPositiveInt
                ("Введите количество столбцов m: ");

            Matrix matrix = new Matrix
                (n, m, -10, 10);
            Console.WriteLine("Исходная матрица:");
            Console.WriteLine(matrix);

            Console.WriteLine
            ("Количество элементов, больших любого" +
             " элемента главной диагонали: "
                + matrix.CountGreaterThanDiagonal());
        }

        /// <summary>
        /// Задание 3. Вычисляет выражение A * C^T + B * C.
        /// </summary>
        private static void RunTask3()
        {
            Console.WriteLine();
            Console.WriteLine("--- Задание 3 ---");

            int n = Input.ReadPositiveInt
                ("Введите размер квадратных матриц n: ");

            Matrix a = new Matrix(n, n, -5, 6);
            Matrix b = new Matrix(n, n, -5, 6);
            Matrix c = new Matrix(n, n, -5, 6);

            Console.WriteLine("Матрица A:");
            Console.WriteLine(a);
            Console.WriteLine("Матрица B:");
            Console.WriteLine(b);
            Console.WriteLine("Матрица C:");
            Console.WriteLine(c);

            Matrix result = a * c.Transpose() + b * c;
            Console.WriteLine("Результат A * C^T + B * C:");
            Console.WriteLine(result);
        }
    }
}
