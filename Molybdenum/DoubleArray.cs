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
        const int size = 12;
        
        private byte[,] data = new byte[size, size] 
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

        /// <summary>
        /// Сумма всех элементов строки
        /// </summary>
        /// <param name="row">Номер строки</param>
        /// <returns>Сумма</returns>
        public int TotalRow(int row)
        {
            // Защита от дурака
            if (row < 0) return 0;
            if (row >= size) return 0;

            // Получение суммы
            int Sum = 0;
            for (int i = 0; i < size; i++)
                Sum += data[row, i];
            return Sum;
        }

        /// <summary>
        /// Выдаёт одну строку как SingleArray объект
        /// </summary>
        /// <param name="row">Номер строки</param>
        /// <returns>Строка</returns>
        public SingleArray Row(int row)
        {
            // Защита от дурака
            if (row < 0) return null;
            if (row >= size) return null;

            // Создание строки
            SingleArray NewRow = new SingleArray();
            for (int i=0; i<size; i++)
                NewRow.Data[i] = data[row, i];
            return NewRow;
        }

        /// <summary>
        /// Обнуление строки. То есть, содержимое строки превращается в нули.
        /// </summary>
        /// <param name="row">Номер строки</param>
        public void ZeroRow(int row)
        {
            for (int i = 0; i < size; i++)
                data[row, i] = 0;
        }


        /// <summary>
        /// Выдаёт список незадействованных связей
        /// </summary>
        /// <returns></returns>
        public List<int> GetFree()
        {
            List<int> FreeCells = new List<int>();

            for (int i = 0; i < size; i++)
                if (Row(i).Total > 0) FreeCells.Add(i);

            return FreeCells;
        }

    }
}
