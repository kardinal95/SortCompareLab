using System;

namespace SortCompareLab.Commands
{
    class RandomCommand : ICommand
    {
        private readonly Handler _handler;
        public string Name => "random";
        public string ShortDescription => "установить случайную последовательность массива";

        public string Description =>
            "Устанавливает случайные элементы массива. Количество передается как аргумент функции.\n" +
            "При вызове функции без параметров используется количество по умолчанию (1000).\n" +
            "Количество элементов не может быть меньше или равно 0.";

        public string Usage =>
            "\'random x\', где x (опционально) - количество элементов массива (целое число).";

        public RandomCommand(Handler handler)
        {
            _handler = handler;
        }

        public void Execute(params string[] arguments)
        {
            if (arguments.Length <= 1)
            {
                try
                {
                    var randomCount = 1000;
                    if (arguments.Length != 0)
                    {
                        randomCount = Convert.ToInt32(arguments[0]);
                    }
                    if (randomCount <= 0)
                    {
                        Console.WriteLine(
                            "Ошибка - количество элементов не может быть меньше или равно 0!");
                    }
                    else
                    {
                        FillRandom(randomCount);
                        Console.WriteLine(
                            "Последовательность успешно задана. Количество элементов в массиве: {0}",
                            randomCount);
                        return;
                    }
                }
                catch (Exception e)
                {
                    switch (e)
                    {
                        case OverflowException _:
                            Console.WriteLine("Ошибка - слишком большое количество элементов!");
                            break;
                        case FormatException _:
                            Console.WriteLine("Ошибка - введенный параметр не является числом!");
                            break;
                        default:
                            throw;
                    }
                }
            }
            else
            {
                Console.WriteLine("Ошибка - слишком много аргументов!");
            }
            Console.WriteLine("Попробуйте \"usage random\"");
        }

        /// <summary>
        ///     Заполняет массив случайными элементами (целыми числами)
        /// </summary>
        /// <param name="randomCount">Количество элементов</param>
        private void FillRandom(int randomCount)
        {
            var random = new Random();
            _handler.Sequence.Clear();
            for (var i = 0; i < randomCount; i++)
            {
                _handler.Sequence.Add(random.Next());
            }
        }
    }
}