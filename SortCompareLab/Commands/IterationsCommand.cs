using System;

namespace SortCompareLab.Commands
{
    class IterationsCommand : ICommand
    {
        private readonly Handler _handler;

        public IterationsCommand(Handler handler)
        {
            _handler = handler;
        }

        public string Name => "iterations";
        public string ShortDescription => "установить желаемое количество итераций";

        public string Description => "Изменяет количество итераций для проведения тестов.\n" +
                                     "Количество итераций не должно быть меньше или равно 0.";

        public string Usage => "\'iterations x\', где х - количество итераций (целое число).";

        public void Execute(params string[] arguments)
        {
            if (arguments.Length == 1)
            {
                try
                {
                    var result = Convert.ToInt32(arguments[0]);
                    if (result <= 0)
                    {
                        Console.WriteLine(
                            "Ошибка - количество итераций не может быть меньше или равно 0!");
                    }
                    else
                    {
                        _handler.Iterations = result;
                        Console.WriteLine("Установлено количество итераций: {0}",
                                          _handler.Iterations);
                        return;
                    }
                }
                catch (Exception e)
                {
                    switch (e)
                    {
                        case OverflowException _:
                            Console.WriteLine("Ошибка - слишком большое количество итераций!");
                            break;
                        case FormatException _:
                            Console.WriteLine("Ошибка - введенный параметр не является числом!");
                            break;
                        default:
                            throw; // Обнаружена критическая ошибка
                    }
                }
            }
            else
            {
                Console.WriteLine("Ошибка - неправильно введены аргументы для функции!");
            }
            Console.WriteLine("Попробуйте \"usage iterations\"");
        }
    }
}