using System;
using System.Collections.Generic;

namespace SortCompareLab
{
    class Program
    {
        public static void Main(string[] args)
        {
            var sortMethods = new Dictionary<string, Action<int[]>>
            {
                {"Пузырьковая сортировка", ListSort.BubbleSort},
                {"Сортировка вставками", ListSort.InsertSort},
                {"Пирамидальная сортировка", ListSort.HeapSort},
                {"Встроенные класс", Array.Sort}
            };
            var handler = new Handler(sortMethods);
            handler.Run();
        }
    }
}