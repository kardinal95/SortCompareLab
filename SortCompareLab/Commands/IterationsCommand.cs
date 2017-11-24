using System;

namespace SortCompareLab.Commands
{
    class IterationsCommand : ICommand
    {
        private readonly Handler handler;

        public IterationsCommand(Handler handler)
        {
            this.handler = handler;
        }

        public string Name => "iterations";
        public string ShortDescription => "установить желаемое количество итераций";

        public string Description => "Изменяет количество итераций для проведения тестов.\n" +
                                     "Количество итераций не должно быть меньше или равно 0.";

        public string Usage => "\'iterations x\', где х - количество итераций (целое число).";

        public void Execute(params string[] arguments)
        {
            if (arguments.Length != 1)
            {
                Console.WriteLine("Ошибка - неправильно введены аргументы для функции!");
            }
            else
            {
                try
                {
                    var result = Convert.ToInt32(arguments[0]);
                    if (result <= 0)
                    {
                        Console.WriteLine("Ошибка - количество итераций должно быть больше 0!");
                    }
                    else
                    {
                        handler.Iterations = result;
                        Console.WriteLine("Установлено количество итераций: {0}",
                                          handler.Iterations);
                    }
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Ошибка - слишком большое количество итераций!");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка - введенный параметр не является числом!");
                }
            }
        }
    }
}