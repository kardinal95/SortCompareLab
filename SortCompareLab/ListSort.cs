using System;
using System.Collections;
using System.Collections.Generic;

namespace SortCompareLab
{
    /// <summary>
    ///     Содержит алгоритмы для сортировки обьектов типа <see cref="IList" />
    ///     Обьекты в <see cref="IList{T}" /> должны реализовывать <see cref="IComparable" />
    /// </summary>
    static class ListSort
    {
        /// <summary>
        ///     Сортирует <see cref="IList{T}" /> используя пузырьковый алгоритм сортировки
        ///     Обьекты в <see cref="IList{T}" /> должны реализовывать <see cref="IComparable" />
        /// </summary>
        /// <typeparam name="T">Тип элементов в <see cref="IList{T}" /></typeparam>
        /// <param name="list"><see cref="IList{T}" /> для сортировки</param>
        public static void BubbleSort<T>(this IList<T> list)
            where T : IComparable
        {
            var ready = false; // Флаг для пропуска излишних циклов сортировки
            for (var length = list.Count; length > 0 && !ready; length--)
            {
                ready = true; // По умолчанию нет изменений
                for (var index = 0; index < length - 1; index++)
                {
                    if (list[index].CompareTo(list[index + 1]) <= 0)
                    {
                        continue;
                    }
                    list.Swap(index, index + 1);
                    ready = false;
                }
            }
        }

        /// <summary>
        ///     Сортирует <see cref="IList{T}" /> используя алгоритм сортировки вставками
        ///     Обьекты в <see cref="IList{T}" /> должны реализовывать <see cref="IComparable" />
        /// </summary>
        /// <typeparam name="T">Тип элементов в <see cref="IList{T}" /></typeparam>
        /// <param name="list"><see cref="IList{T}" /> для сортировки</param>
        public static void InsertSort<T>(this IList<T> list)
            where T : IComparable
        {
            for (var index = 1; index < list.Count; index++)
            {
                var current = list[index];
                var selected = index - 1;
                // Сравнить теукущий элемент с предыдущими поочередно
                // Сдвинуть вперед если элемент массива больше
                // Положить текущий после сравниваемого в противном случае
                while (selected >= 0 && list[selected].CompareTo(current) > 0)
                {
                    list[selected + 1] = list[selected];
                    selected--;
                }
                list[selected + 1] = current;
            }
        }

        /// <summary>
        ///     Сортирует <see cref="IList{T}" /> используя пирамидальный алгоритм сортировки
        ///     Обьекты в <see cref="IList{T}" /> должны реализовывать <see cref="IComparable" />
        /// </summary>
        /// <typeparam name="T">Тип элементов в <see cref="IList{T}" /></typeparam>
        /// <param name="list"><see cref="IList{T}" /> для сортировки</param>
        public static void HeapSort<T>(this IList<T> list)
            where T : IComparable
        {
            for (var size = list.Count - 1; size > 1; size--)
            {
                MakeHeap(list, size + 1);
                list.Swap(0, size);
            }
        }

        /// <summary>
        ///     Составляет пирамиду из подмассива данного <see cref="IList{T}" />
        ///     Обьекты в <see cref="IList{T}" /> должны реализовывать <see cref="IComparable" />
        /// </summary>
        /// <remarks>
        ///     Пирамида - тип бинарного дерева в котором корневой элемент больше чем его потомки
        /// </remarks>
        /// <typeparam name="T">Тип элементов в <see cref="IList{T}" /></typeparam>
        /// <param name="list"><see cref="IList{T}" /> для сортировки</param>
        /// <param name="size">Размер подмассива</param>
        private static void MakeHeap<T>(this IList<T> list, int size)
            where T : IComparable
        {
            for (var head = size / 2; head >= 0;
                 head--) // Исключаем нижние уровни, начинаем с верхнего
            {
                var ready = false; // Флаг для избежания излишних циклов

                var sub = head; // Работаем с поддеревом
                while (sub * 2 + 1 < size && !ready)
                {
                    // Берем левое поддерево
                    // Если существует правое поддерево больше левого - берем его
                    // Меняем элементы по необходимости и двигаемся дальше
                    var child = sub * 2 + 1;
                    if (child + 1 < size && list[child].CompareTo(list[child + 1]) < 0)
                    {
                        child++;
                    }
                    if (list[sub].CompareTo(list[child]) < 0)
                    {
                        list.Swap(sub, child);
                        sub = child;
                    }
                    else
                    {
                        ready = true; // Пирамида готова если не было изменений
                    }
                }
            }
        }

        /// <summary>
        ///     Обменивает местами данные элементы в <see cref="IList{T}" />
        ///     Обьекты в <see cref="IList{T}" /> должны реализовывать <see cref="IComparable" />
        /// </summary>
        /// <typeparam name="T">Тип элементов в <see cref="IList{T}" /></typeparam>
        /// <param name="list">Массив в котором производится обмен</param>
        /// <param name="first">Индекс первого элемента для обмена</param>
        /// <param name="second">Индекс второго элемента для обмена</param>
        private static void Swap<T>(this IList<T> list, int first, int second)
        {
            var temp = list[first];

            list[first] = list[second];
            list[second] = temp;
        }
    }
}