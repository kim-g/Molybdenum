using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molybdenum
{
    internal class Program
    {
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

            // Основной цикл
            for (int i=0; i < Iterations; i++)
            {
                // Создание объектов A и B. Более точные их названия не даны.

                DoubleArray A = new DoubleArray();
                SingleArray B = new SingleArray();

            }
        }
    }
}
