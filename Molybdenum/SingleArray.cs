using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molybdenum
{
    internal class SingleArray
    {
        const int size = 12;
        
        private byte[] data = new byte[size]
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};

        /// <summary>
        /// Данные массива
        /// </summary>
        public byte[] Data
        {
            get => data;
            set => data = value;
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

        /// <summary>
        /// Выдаёт список незадействованных связей
        /// </summary>
        /// <returns></returns>
        public List<int> GetFree()
        {
            List<int> FreeCells = new List<int>();

            for (int i = 0; i < size; i++)
                if (Data[i] > 0) FreeCells.Add(i);

            return FreeCells;
        }
    }
}
