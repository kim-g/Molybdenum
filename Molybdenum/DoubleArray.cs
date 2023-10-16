using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molybdenum
{
    /// <summary>
    /// Сложный двумерный массив с вункциями сумм по рядам и полной суммы
    /// </summary>
    internal class DoubleArray
    {
        private byte[,] data = new byte[12,12] 
        {
            {0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0}, 
            {1, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1}, 
            {1, 1, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0}, 
            {1, 0, 1, 0, 1, 0, 1, 1, 0, 0, 0, 0}, 
            {1, 0, 0, 1, 0, 1, 0, 1, 0, 0, 1, 0}, 
            {1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 1}, 
            {0, 0, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0}, 
            {0, 0, 0, 1, 1, 0, 1, 0, 0, 1, 1, 0}, 
            {0, 1, 1, 0, 0, 0, 1, 0, 0, 1, 0, 1}, 
            {0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1}, 
            {0, 0, 0, 0, 1, 1, 0, 1, 0, 1, 0, 1}, 
            {0, 1, 0, 0, 0, 1, 0, 0, 1, 1, 1, 0}
        };

        /// <summary>
        /// Данные массива
        /// </summary>
        public byte[,] Data 
        { 
            get => data; 
            set => data=value; 
        }

        /// <summary>
        /// Сумма всех элементов массива
        /// </summary>
        public int Total
        {
            get
            {
                int Sum = 0;
                foreach (byte item in data)
                    Sum += item;
                return Sum;
            }
        }

    }
}
