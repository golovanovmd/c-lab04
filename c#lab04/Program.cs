using System;

public class MyMatrix
{
    private int[,] data; // Двумерный массив для хранения элементов матрицы
    private int rows; // Количество строк в матрице
    private int columns; // Количество столбцов в матрице

    // Конструктор для инициализации матрицы с заданными размерами и заполнения случайными числами
    public MyMatrix(int rows, int columns, int minValue, int maxValue)
    {
        this.rows = rows; 
        this.columns = columns; 
        data = new int[rows, columns]; // Инициализация двумерного массива
        Random rand = new Random(); // Генератор случайных чисел

        // Заполнение матрицы случайными числами в заданном диапазоне
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                data[i, j] = rand.Next(minValue, maxValue); 
            }
        }
    }

    // Индексатор для доступа к элементам матрицы по индексу строки и столбца
    public int this[int row, int column]
    {
        get { return data[row, column]; } // Возврат элемента матрицы
        set { data[row, column] = value; } // Установка значения элемента матрицы
    }

    // Перегрузка оператора сложения для суммирования двух матриц
    public static MyMatrix operator +(MyMatrix a, MyMatrix b)
    {
        // Проверка на соответствие размеров матриц
        if (a.rows != b.rows || a.columns != b.columns)
        {
            Console.WriteLine("!!!ERROR!!! Matrices must have the same dimensions to add."); 
            return null; 
        }

        // Создание новой матрицы для результата
        MyMatrix result = new MyMatrix(a.rows, a.columns, 0, 1);
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < a.columns; j++)
            {
                result[i, j] = a[i, j] + b[i, j]; // Суммирование элементов
            }
        }
        return result; // Возврат результирующей матрицы
    }

    // Перегрузка оператора вычитания для вычитания одной матрицы из другой
    public static MyMatrix operator -(MyMatrix a, MyMatrix b)
    {
        // Проверка на соответствие размеров матриц
        if (a.rows != b.rows || a.columns != b.columns)
        {
            Console.WriteLine("!!!ERROR!!! Matrices must have the same dimensions to subtract.");
            return null; 
        }

        // Создание новой матрицы для результата
        MyMatrix result = new MyMatrix(a.rows, a.columns, 0, 1);
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < a.columns; j++)
            {
                result[i, j] = a[i, j] - b[i, j]; 
            }
        }
        return result; 
    }

    // Перегрузка оператора умножения матриц
    public static MyMatrix operator *(MyMatrix a, MyMatrix b)
    {
        // Проверка на соответствие размеров для умножения
        if (a.columns != b.rows)
        {
            Console.WriteLine("!!!ERROR!!! Number of columns in the first matrix must equal the number of rows in the second."); // Вывод ошибки
            return null; // Возврат null в случае ошибки
        }

        // Создание новой матрицы для результата
        MyMatrix result = new MyMatrix(a.rows, b.columns, 0, 1);
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < b.columns; j++)
            {
                // Умножение матриц
                for (int k = 0; k < a.columns; k++)
                {
                    result[i, j] += a[i, k] * b[k, j]; // Суммирование произведений для каждой ячейки результата
                }
            }
        }
        return result; // Возврат результирующей матрицы
    }

    // Перегрузка оператора умножения матрицы на число
    public static MyMatrix operator *(MyMatrix a, int scalar)
    {
        // Создание новой матрицы для результата
        MyMatrix result = new MyMatrix(a.rows, a.columns, 0, 1);
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < a.columns; j++)
            {
                result[i, j] = a[i, j] * scalar; // Умножение каждого элемента на скаляр
            }
        }
        return result; // Возврат результирующей матрицы
    }

    // Перегрузка оператора деления матрицы на число
    public static MyMatrix operator /(MyMatrix a, int scalar)
    {
        // Проверка деления на ноль
        if (scalar == 0)
        {
            Console.WriteLine("!!!ERROR!!! Cannot divide by zero."); // Вывод ошибки
            return null; // Возврат null в случае ошибки
        }

        // Создание новой матрицы для результата
        MyMatrix result = new MyMatrix(a.rows, a.columns, 0, 1);
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < a.columns; j++)
            {
                result[i, j] = a[i, j] / scalar; // Деление каждого элемента на скаляр
            }
        }
        return result; // Возврат результирующей матрицы
    }

    // Метод для вывода матрицы на консоль
    public void Print()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Console.Write(data[i, j] + "\t"); // Вывод элемента с табуляцией
            }
            Console.WriteLine(); // Переход на следующую строку
        }
    }
}


public class Program
{
    public static void Main()
    {
        // Ввод размеров матрицы
        Console.WriteLine("Введите количество строк и столбцов матрицы:");
        int rows = Convert.ToInt32(Console.ReadLine());
        int columns = Convert.ToInt32(Console.ReadLine());

        // Ввод диапазона для случайных чисел
        Console.WriteLine("Введите минимальное и максимальное значения для заполнения матрицы:");
        int minValue = Convert.ToInt32(Console.ReadLine());
        int maxValue = Convert.ToInt32(Console.ReadLine());

        // Создание двух матриц с случайными числами
        MyMatrix matrixA = new MyMatrix(rows, columns, minValue, maxValue);
        MyMatrix matrixB = new MyMatrix(rows, columns, minValue, maxValue);

        // Вывод матриц A и B
        Console.WriteLine("Матрица A:");
        matrixA.Print();
        Console.WriteLine("Матрица B:");
        matrixB.Print();

        // Сложение матриц A и B
        MyMatrix sum = matrixA + matrixB;
        if (sum != null) // Проверка на null
        {
            Console.WriteLine("Сумма A и B:");
            sum.Print();
        }

        // Вычитание матриц A и B
        MyMatrix difference = matrixA - matrixB;
        if (difference != null) // Проверка на null
        {
            Console.WriteLine("Разность A и B:");
            difference.Print();
        }

        // Пример умножения матриц
        MyMatrix matrixC = new MyMatrix(columns, rows, minValue, maxValue); // Размеры должны быть совместимы для умножения
        Console.WriteLine("Матрица C:");
        matrixC.Print();

        // Умножение матриц A и C
        MyMatrix product = matrixA * matrixC;
        if (product != null) // Проверка на null
        {
            Console.WriteLine("Произведение A и C:");
            product.Print();
        }

        // Умножение и деление матрицы A на число
        int scalar = 2;
        MyMatrix multiplied = matrixA * scalar; // Умножение на скаляр
        Console.WriteLine($"Матрица A, умноженная на {scalar}:");
        multiplied.Print();

        MyMatrix divided = matrixA / scalar; // Деление на скаляр
        Console.WriteLine($"Матрица A, деленная на {scalar}:");
        divided.Print();
    }
}