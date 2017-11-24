using System;
using System.Collections.Generic;

namespace SortCompareLab.Commands
{
    class SequenceCommand : ICommand
    {
        private readonly Handler handler;
        public string Name => "sequence";
        public string ShortDescription => "установить последовательность массива";

        public string Description =>
            "Устанавливает элементы массива. Элементы передаются как аргументы функции";

        public string Usage =>
            "\'sequence 1 2 3 ..\', где 1 2 3 .. - элементы желаемого массива (целые числа).\n" +
            "Элементы вводятся последовательно через пробел.";

        public SequenceCommand(Handler handler)
        {
            this.handler = handler;
        }

        public void Execute(params string[] arguments)
        {
            if (arguments.Length == 0)
            {
                Console.WriteLine("Ошибка - недостаточно аргументов!");
                return;
            }
            var temp = new List<int>();
            var errors = new List<string>();
            foreach (var argument in arguments)
            {
                try
                {
                    temp.Add(Convert.ToInt32(argument));
                }
                catch (Exception e) when (e is OverflowException || e is FormatException)
                {
                    errors.Add(argument);
                }
            }
            if (errors.Count == 0)
            {
                handler.Sequence.Clear();
                handler.Sequence.AddRange(temp);
                Console.WriteLine("Последовательность установлена. Кол-во элементов - {0}",
                                  temp.Count);
            }
            else
            {
                Console.Write("Ошибка - невозможно добавить следующие элементы: ");
                Console.WriteLine(string.Join(" ", errors));
                Console.WriteLine("Проверьте правильность ввода!");
            }
        }
    }
}