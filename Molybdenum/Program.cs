using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molybdenum
{
    internal class Program
    {
        // Создадим генератор случайных чисел
        static Random random = new Random();

        static void Main(string[] args)
        {
            // Количество итераций
            const int Iterations = 10000;

            // Список результатов.
            // Комментарий переводчика. Понятия не имею, зачем тут нужен трёхмерный массив, по всем данным достаточно двумерного.
            // Я же использую одномерный список (массив будет в C# статичным, то есть с заранее заданным размером, а нам оно не надо) элементов класса Point.
            List<Point> Memory = new List<Point>();


            // Создаём массив и заполняем его нулями. Пока оставим, потом, скорее всего, доделаю в класс.
            int[] Connections = new int[Iterations];

            // Создадим генератор случайных чисел
            Random random = new Random();

            // Основной цикл
            for (int i=0; i < Iterations; i++)
            {
                // Создание объектов A и B. Более точные их названия не даны.

                DoubleArray A = new DoubleArray();
                SingleArray B = new SingleArray();
                List<Point> memory = new List<Point>();

                // Выполняем общий поиск пока не найдём все связи
                while (B.Total != 0 && A.Total != 0)
                {
                    // Поис ко массивам
                    int n1 = Search(B);
                    int n2 = Search(A.Row(n1));

                    // Обнулим найденные элементы
                    A.ZeroRow(n1);
                    B.Data[n1] = 0;
                    A.Data[n2, n1] = 0;

                    // Добавим в список
                    memory.Add(new Point(n1, n2));
                }

                // Удаление первого элемента
                memory.RemoveAt(0);

                // Добавление в основной список
                Memory.AddRange(memory);

                // Запишем количество значений
                Connections[i] = memory.Count;
            }
        }


        /// <summary>
        /// Поиск совпадений
        /// </summary>
        /// <param name="SA">SearchArray – массив для поиска</param>
        /// <returns></returns>
        static private int Search(SingleArray SA)
        {
            // Получим случайное число
            int RandomValue = random.Next(0, SA.Total - 1);

            // Зададим начальные значения
            int S = 0;
            int n1 = 0;

            // И пройдёмся по всем элементам B
            for (int j = 0; j < 12; j++)
            {
                S += SA.Data[j];

                // Если совпало со случайным значением
                if (RandomValue == S)
                {
                    // Запомним его и прервём цикл.
                    n1 = j;
                    break;
                }
            }

            return n1;
        }
    }
}
