using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molybdenum
{
    class Searcher
    {
        // Создадим генератор случайных чисел
        static Random random = new Random();

        // Количество итераций
        int Iterations = 1000000;

        // Список результатов.
        // Комментарий переводчика. Понятия не имею, зачем тут нужен трёхмерный массив, по всем данным достаточно двумерного.
        // Я же использую одномерный массив списков элементов класса Point.
        List<Point>[] Memory;

        // Создаём массив и заполняем его нулями.
        int[] Connections;

        /// <summary>
        /// Количество повторов
        /// </summary>
        int IdenticalVariants = 0;

        /// <summary>
        /// Время начала поиска
        /// </summary>
        string StartTime;

        public Searcher ()
        {
            // Запуск программы
            Console.WriteLine("*************************************");
            Console.WriteLine("************  Molybdenum  ***********");
            Console.WriteLine("***************  v 1.0  *************");
            Console.WriteLine("*************************************\n\n");
        }

        /// <summary>
        /// Запрос у пользователя количества итераций
        /// </summary>
        public void GetIterations()
        {
            bool IterGot = false;
            while (!IterGot)
            {
                Console.Write("Введите количество итераций: ");
                string Iter = Console.ReadLine();
                try
                {
                    Iterations = Convert.ToInt32(Iter);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Строка «{Iter}» не является числом! Пожалуёста, введите целое положительное число.\n");
                    continue;
                }
                IterGot = true;
            }
        }

        /// <summary>
        /// Запуск расчёта
        /// </summary>
        public void Run()
        {
            StartTime = DateTime.Now.ToString();
            GetIterations();

            Memory = new List<Point>[Iterations];
            Connections = new int[Iterations];
            SearchConnections();
            Sort();
            SearchEquals();
            PrintResults();
        }

        /// <summary>
        /// Поиск связей методом Монте-Карло
        /// </summary>
        protected void SearchConnections()
        {
            Console.WriteLine($"*** -> Поиск связей");

            // Основной цикл
            for (int i = 0; i < Iterations; i++)
            {
                if (i % 1000 == 0) Console.WriteLine($"Итерация №{i}");

                // Создание объектов A и B.
                // A - матрица инцидентности, строка - порядковый номер узла, столбик - в направлении какого узла есть мостик, 1 - есть мостик, 0 - нет мостика.
                // B - матрица свободных узлов, к которым может идти присоединение
                DoubleArray A = new DoubleArray();
                SingleArray B = new SingleArray();
                List<Point> memory = new List<Point>();

                // Выполняем общий поиск пока не найдём все связи
                while (B.Total != 0 && A.Total != 0)
                {
                    // Поис ко массивам
                    int n1 = Search(B);
                    if (n1 == -1) continue;
                    SingleArray A_Row = A.Row(n1);

                    // Проверим, не пустой ли слой. Чтобы не тормозилось.
                    if (A_Row.Total == 0)
                    {
                        B.Data[n1] = 0;
                        continue;
                    }
                    int n2 = Search(A_Row);

                    // Если не нашли, продолжим цикл
                    if (n1 == -1 || n2 == -1) continue;

                    // Обнулим найденные элементы
                    A.ZeroRow(n1);
                    B.Data[n1] = 0;
                    A.Data[n2, n1] = 0;

                    // Добавим в список
                    memory.Add(new Point(n1, n2));
                }

                // Удаление первого элемента
                //memory.RemoveAt(0);

                // Добавление в основной список. ToList() используется для создания нового объекта
                Memory[i] = memory.ToList();

                // Запишем количество значений
                Connections[i] = memory.Count;
            }
        }

        /// <summary>
        /// Поиск совпадений
        /// </summary>
        /// <param name="SA">SearchArray – массив для поиска</param>
        /// <returns></returns>
        private int Search(SingleArray SA)
        {
            // Проверим, не нулевой ли блок мы рассматриваем
            if (SA == null) return -1;
            if (SA.Total == 0) return -1;

            // Получим случайное число
            List<int> FreeCells = SA.GetFree();
            int RandomIndex = random.Next(0, FreeCells.Count - 1);
            int RandomValue = FreeCells[RandomIndex];

            return RandomValue;
        }

        /// <summary>
        /// Функция для сравнения двух связей
        /// </summary>
        /// <param name="p1">Первая связь</param>
        /// <param name="p2">Вторая связь</param>
        /// <returns>Результат сравнения</returns>
        private int ComparePoints(Point p1, Point p2)
        {
            return p1.X - p2.X;
        }

        /// <summary>
        /// Сравнение двух массивов точек
        /// </summary>
        /// <param name="X">Первый массив</param>
        /// <param name="Y">Второй массив</param>
        /// <returns>Равны ли массивы</returns>
        private bool Equals(List<Point> X, List<Point> Y)
        {
            if (X.Count != Y.Count) return false;
            for (int i = 0; i < X.Count; i++)
            {
                if (X[i].X != Y[i].X) return false;
                if (X[i].Y != Y[i].Y) return false;
            }

            return true;
        }

        /// <summary>
        /// Сортировка списка по мостам
        /// </summary>
        private void Sort()
        {
            // Отсортируем все элементы внутри отдельных блоков
            Console.WriteLine($"*** -> Сортировка");
            foreach (List<Point> list in Memory)
                list.Sort(ComparePoints);
        }

        /// <summary>
        /// Поиск количества повторов
        /// </summary>
        /// <returns></returns>
        private void SearchEquals()
        {
            // Найдём повторы
            Console.WriteLine($"*** -> Поиск повторов");
            for (int i = 0; i < Iterations; i++)
            {
                if (i % 1000 == 0) Console.WriteLine($"Итерация №{i}");

                for (int j = i + 1; j < Iterations; j++)
                    if (Equals(Memory[i], Memory[j]))
                        IdenticalVariants++;
            }
        }

        private void PrintResults()
        {
            // Вычисление cреднеарифметического количества присоединенных молекул
            double MeanMolecules = Connections.Average();

            // Выведем в CSV файл
            Console.WriteLine($"*** -> Сохранение в файл");
            string CurTime = DateTime.Now.ToString();
            string CurTimeName = CurTime.Replace(':', '-');
            using (StreamWriter FS = new StreamWriter($"{CurTimeName} – Moliblenum result.csv", false, Encoding.UTF8))
            {
                FS.WriteLine("Поиск возможных расположений молекул на поверхности молиоксометаллата");
                FS.WriteLine($"Дата и время запуска;{StartTime}");
                FS.WriteLine($"Дата и время остановки;{CurTime}");
                FS.WriteLine($"Количество итераций;{Iterations}");
                FS.WriteLine($"Количество одинаковых вариантов;{IdenticalVariants}");
                FS.WriteLine($"Среднеарифметическое количество присоединенных молекул;{MeanMolecules}");
                FS.WriteLine($"");
                FS.WriteLine($"№;Соединения;1;2;3;4;5;6;7;8;9;10;11;12");

                for (int i = 0; i < Iterations; i++)
                {
                    string Outline = $"{i};{Connections[i]};";
                    foreach (Point P in Memory[i])
                    {
                        Outline += $"{P.X}<->{P.Y};";
                    }
                    FS.WriteLine(Outline);
                }
            }

            //Вывод результата
            Console.WriteLine("\n\n************  РЕЗУЛЬТАТ  ************\n");
            Console.WriteLine($"Количество одинаковых вариантов: {IdenticalVariants}");
            Console.WriteLine($"Среднеарифметическое количество присоединенных молекул: {MeanMolecules}");
            Console.WriteLine("\n*************************************");
            Console.ReadKey();
        }
    }
}
