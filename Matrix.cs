using System;
using System.Text;

namespace Lab3
{
    /// <summary>
    /// Матрица на основе двумерного массива (задания 1-3).
    /// </summary>
    public class Matrix
    {
        private double[,] _data;

        /// <summary>
        /// Создает матрицу из готового массива 
        /// </summary>
        private Matrix(double[,] data)
        {
            _data = data;
        }

        /// <summary>
        /// Задание 1 (первый массив). Заполнение с клавиатуры
        /// по столбцам.
        /// </summary>
        public Matrix(int n, int m)
        {
            _data = new double[n, m];

            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    _data[i, j] = Input.ReadDouble
                        ("Элемент [" + i + "," + j + "]: ");
                }
            }
        }

        /// <summary>
        /// Задание 1 (второй и третий массивы). Квадратная матрица,
        /// заполненная по правилу.
        /// </summary>
        public Matrix(int n, MatrixType type)
        {
            _data = new double[n, n];

            if (type == MatrixType.Chessboard)
            {
                FillChessboard();
            }
            else
            {
                FillSnake();
            }
        }

        /// <summary>
        /// Матрица n x m из случайных целых чисел
        /// из интервала.
        /// </summary>
        public Matrix(int n, int m, int minValue, int maxValue)
        {
            _data = new double[n, m];
            Random random = new Random();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    _data[i, j] = random.Next(minValue, maxValue);
                }
            }
        }

        /// <summary>
        /// Задание 2. Считает элементы, большие любого элемента
        /// главной диагонали.
        /// </summary>
        public int CountGreaterThanDiagonal()
        {
            int rows = _data.GetLength(0);
            int cols = _data.GetLength(1);
            int diagonalLength = Math.Min(rows, cols);

            double maxDiagonal = _data[0, 0];
            for (int i = 1; i < diagonalLength; i++)
            {
                if (_data[i, i] > maxDiagonal)
                {
                    maxDiagonal = _data[i, i];
                }
            }

            int count = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (_data[i, j] > maxDiagonal)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Задание 3. Транспонирование матрицы.
        /// </summary>
        public Matrix Transpose()
        {
            int rows = _data.GetLength(0);
            int cols = _data.GetLength(1);
            double[,] result = new double[cols, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[j, i] = _data[i, j];
                }
            }

            return new Matrix(result);
        }

        /// <summary>
        /// Задание 3. Сложение матриц одинакового размера.
        /// </summary>
        public static Matrix operator +(Matrix a, Matrix b)
        {
            int rows = a._data.GetLength(0);
            int cols = a._data.GetLength(1);

            if (rows != b._data.GetLength(0) ||
                cols != b._data.GetLength(1))
            {
                throw new ArgumentException
                    ("Размеры матриц должны совпадать для сложения.");
            }

            double[,] result = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = a._data[i, j] + b._data[i, j];
                }
            }

            return new Matrix(result);
        }

        /// <summary>
        /// Задание 3. Умножение матриц.
        /// </summary>
        public static Matrix operator *(Matrix a, Matrix b)
        {
            int rowsA = a._data.GetLength(0);
            int colsA = a._data.GetLength(1);
            int colsB = b._data.GetLength(1);

            if (colsA != b._data.GetLength(0))
            {
                throw new ArgumentException("Число столбцов " +
                    "первой матрицы должно равняться числу " +
                    "строк второй.");
            }

            double[,] result = new double[rowsA, colsB];
            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < colsA; k++)
                    {
                        sum += a._data[i, k] * b._data[k, j];
                    }

                    result[i, j] = sum;
                }
            }

            return new Matrix(result);
        }

        /// <summary>
        /// Формирует строку для вывода матрицы на экран
        /// в виде таблицы.
        /// </summary>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < _data.GetLength(0); i++)
            {
                for (int j = 0; j < _data.GetLength
                         (1); j++)
                {
                    builder.Append(_data[i, j].ToString().PadLeft
                        (8));
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }

        /// <summary>
        /// Заполняет матрицу как шахматную доску
        /// </summary>
        private void FillChessboard()
        {
            int n = _data.GetLength(0);
            Random random = new Random();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        _data[i, j] = random.Next(0, 50) * 2 + 1;
                    }
                    else
                    {
                        _data[i, j] = random.Next(1, 51) * 2;
                    }
                }
            }
        }

        /// <summary>
        /// Заполняет матрицу змейкой
        /// </summary>
        private void FillSnake()
        {
            int n = _data.GetLength(0);
            int value = 1;
            int step = 0;

            for (int sum = 2 * n - 2; sum >= 0; sum--)
            {
                int low = Math.Max(0, sum - (n - 1));
                int high = Math.Min(sum, n - 1);

                if (step % 2 == 1)
                {
                    for (int i = high; i >= low; i--)
                    {
                        _data[i, sum - i] = value;
                        value++;
                    }
                }
                else
                {
                    for (int i = low; i <= high; i++)
                    {
                        _data[i, sum - i] = value;
                        value++;
                    }
                }

                step++;
            }
        }
    }
}
