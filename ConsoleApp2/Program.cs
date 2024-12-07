using System;
using System.Collections;
using System.Collections.Generic;

// Класс Car представляет автомобиль с тремя свойствами: название, год выпуска и максимальная скорость
public class Car
{
    public string Name { get; set; } 
    public int ProductionYear { get; set; } 
    public int MaxSpeed { get; set; } 

    // Конструктор для инициализации свойств
    public Car(string name, int productionYear, int maxSpeed)
    {
        Name = name;
        ProductionYear = productionYear;
        MaxSpeed = maxSpeed;
    }

    // Переопределение метода ToString 
    public override string ToString()
    {
        return $"Name: {Name}, Year: {ProductionYear}, Max Speed: {MaxSpeed}";
    }
}

// Класс CarCatalog для хранения массива автомобилей и реализации различных итераторов
public class CarCatalog : IEnumerable<Car> // Интерфейс, который позволяет перебирать коллекцию
{
    private Car[] cars; // Массив автомобилей

    // Конструктор для инициализации массива автомобилей
    public CarCatalog(Car[] cars)
    {
        this.cars = cars;
    }

    // Итератор для прямого прохода по массиву.
    //Этот интерфейс определяет методы и свойства, которые позволяют осуществлять перебор элементов в коллекции. 
    public IEnumerator<Car> GetEnumerator()
    {
        for (int i = 0; i < cars.Length; i++)
        {
            yield return cars[i]; // yield - метод, который может возвращать последовательность значений
        }
    }

    // Итератор для обратного прохода по массиву
    public IEnumerable<Car> ReverseIterator()
    {
        for (int i = cars.Length - 1; i >= 0; i--)
        {
            yield return cars[i]; // yield - метод, который может возвращать последовательность значений
        }
    }

    // Итератор с фильтром по году выпуска
    public IEnumerable<Car> FilterByYear(int year)
    {
        foreach (var car in cars)
        {
            if (car.ProductionYear == year)
            {
                yield return car; // yield - метод, который может возвращать последовательность значений
            }
        }
    }

    // Итератор с фильтром по максимальной скорости
    public IEnumerable<Car> FilterByMaxSpeed(int maxSpeed)
    {
        foreach (var car in cars)
        {
            if (car.MaxSpeed == maxSpeed)
            {
                yield return car; // yield - метод, который может возвращать последовательность значений
            }
        }
    }

    // Реализация IEnumerable для поддержки foreach (метод для использования класса CarCatalog в foreach циклах)
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Car[] cars = {
            new Car("Ferrari", 2015, 250),
            new Car("Skoda", 2018, 200),
            new Car("BMW", 2023, 240),
            new Car("Audi", 2020, 220)
        };

        // Создаем каталог автомобилей
        CarCatalog carCatalog = new CarCatalog(cars);

        // Прямой проход
        Console.WriteLine("Direct iteration using foreach:");
        foreach (var car in carCatalog)
        {
            Console.WriteLine(car);
        }
        Console.WriteLine("\nDirect iteration using GetEnumerator():");
        IEnumerator<Car> enumerator = carCatalog.GetEnumerator(); // Интерфейс для перебора + метод для поочередного перебора элементов carCatalog
        while (enumerator.MoveNext()) // Movenext() - метод, который передвигает курсор на следующий элемент коллекции
        {
            Console.WriteLine(enumerator.Current); // Свойство, которое возвращает текущий элемент коллекции на котором сейчас находится enumerator
        }

        // Обратный проход
        Console.WriteLine("\nReverse iteration:");
        foreach (var car in carCatalog.ReverseIterator())
        {
            Console.WriteLine(car);
        }

        // Проход с фильтром по году выпуска
        Console.WriteLine("\nFilter by Production Year (2018):");
        foreach (var car in carCatalog.FilterByYear(2018))
        {
            Console.WriteLine(car);
        }

        // Проход с фильтром по максимальной скорости
        Console.WriteLine("\nFilter by Max Speed (250):");
        foreach (var car in carCatalog.FilterByMaxSpeed(250))
        {
            Console.WriteLine(car);
        }
    }
}