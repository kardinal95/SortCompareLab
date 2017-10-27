using System;
using System.Collections.Generic;

namespace SortCompareLab.Commands
{
    class SequenceCommand : ICommand
    {
        private readonly Handler _handler;
        public string Name => "sequence";
        public string ShortDescription => "установить последовательность массива";

        public string Description =>
            "Устанавливает элементы массива. Элементы передаются как аргументы функции";

        public string Usage =>
            "\'sequence 1 2 3 ..\', где 1 2 3 .. - элементы желаемого массива (целые числа).\n" +
            "Элементы вводятся последовательно через пробел.";

        public SequenceCommand(Handler handler)
        {
            _handler = handler;
        }

        public void Execute(params string[] arguments)
        {
            if (arguments.Length != 0)
            {
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
                    _handler.Sequence.Clear();
                    _handler.Sequence.AddRange(temp);
                    Console.WriteLine(
                        "Последовательность успешно установлена. Количество элементов в массиве - {0}",
                        temp.Count);
                    return;
                }
                Console.Write("Ошибка - невозможно добавить следующие элементы: ");
                Console.WriteLine(string.Join(" ", errors));
                Console.WriteLine("Проверьте правильность ввода!");
            }
            else
            {
                Console.WriteLine("Ошибка - недостаточно аргументов!");
            }
            Console.WriteLine("Попробуйте \"usage sequence\"");
        }
    }
}