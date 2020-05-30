using System;

// Нахождение корня уравнения х * 2^x - 1 = 0 методом хорд

namespace practical_task4
{
    public class Program
    {
        // Нижняя и верхняя граница для поиска корня
        const double LOWER_BOUND = 0, UPPER_BOUND = 1;

        // Ввод действительного числа с клавиатуры
        public static double DoubleInput(double lBound = double.MinValue, double uBound = double.MaxValue, string info = "")
        {
            bool exit;
            double result;
            Console.Write(info);
            do
            {
                exit = double.TryParse(Console.ReadLine(), out result);
                if (!exit) Console.Write("Введено не число! Повторите ввод: ");
                else if (result <= lBound || result >= uBound)
                {
                    Console.Write("Введено недопустимое значение! Повторите ввод: ");
                    exit = false;
                }
            } while (!exit);
            return result;
        }

        // Вывод меню
        static void PrintMenu(string[] menuItems, int choice, string info)
        {
            Console.Clear();
            Console.WriteLine(info);
            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == choice) Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{i + 1}. {menuItems[i]}");
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        // Выбор пункта из меню
        static int MenuChoice(string[] menuItems, string info = "")
        {
            Console.CursorVisible = false;
            int choice = 0;
            while (true)
            {
                PrintMenu(menuItems, choice, info);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        if (choice == 0) choice = menuItems.Length;
                        choice--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (choice == menuItems.Length - 1) choice = -1;
                        choice++;
                        break;
                    case ConsoleKey.Enter:
                        Console.CursorVisible = true;
                        return choice;
                }
            }
        }

        // Значение функции f(x) = х * 2^x - 1
        static double GetFunction(double x)
        {
            return x * Math.Pow(2, x) - 1;
        }

        // Поиск корня уравнения
        public static double FindX(double userAccuracy)
        {
            // Корень
            double x = LOWER_BOUND;

            // Значения функции в левой, правой и текущей точке
            double fa, fb, fx;

            // Предыдущее значение корня
            double previousX = LOWER_BOUND;

            // Задаём начальные значения для левой и правой точки промежутка поиска корня
            double a = LOWER_BOUND, b = UPPER_BOUND;

            // Задаём начальное значение текущей точности
            double currentAccuracy = Math.Abs(a - b);

            // Цикл выполняется пока не будет достигнута нужная точность 
            while(!(currentAccuracy < userAccuracy))
            {
                // Находим значение в левой точке
                fa = GetFunction(a);

                // Находим значение в правой точке
                fb = GetFunction(b);

                // Ищем точку пересечения хорды с осью абцисс
                x = a - fa / (fb - fa) * (b - a);

                // Находим значение в этой точке
                fx = GetFunction(x);

                // Выясняем в каком отрезке (правом или левом) находится корень
                if (fa * fx < 0) b = x;
                else if (fb * fx < 0) a = x;

                // Вычисляем текущую точность
                currentAccuracy = Math.Abs(x - previousX);

                // Запоминаем значение х
                previousX = x;
            }

            return x;
        }

        static void Main(string[] args)
        {
            // Пункты меню
            string[] MENU_ITEMS = { "Найти корень с заданной точностью", "Выйти из программы" };

            // Индекс пункта - выход из программы
            const int EXIT_CHOICE = 1;

            // Индекс пункта меню, который выбрал пользователь
            int userChoice;

            // Точность нахождения корня
            double userAccuracy;

            do
            {
                // Пользователь выбирает действие (выйти или найти корень)
                userChoice = MenuChoice(MENU_ITEMS, "Программа для нахождения корня уравнения х * 2^x - 1 = 0 методом хорд\nВыберите действие:");
                if (userChoice == EXIT_CHOICE) break;
                Console.Clear();

                // Ввод требуемой точности 
                userAccuracy = DoubleInput(lBound: 0, info: "Введите требуемую точность (число больше 0): ");

                // Вывод результата
                Console.WriteLine($"Корнем уравнения является {FindX(userAccuracy)} (точность вычислений = {userAccuracy})");
                Console.WriteLine("Нажминет Enter, чтобы вернуться в меню...");
                Console.ReadLine();

            } while (true);
        }
    }
}
