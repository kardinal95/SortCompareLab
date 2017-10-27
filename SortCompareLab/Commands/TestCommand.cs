using System;
using System.Diagnostics;

namespace SortCompareLab.Commands
{
    class TestCommand : ICommand
    {
        private readonly Handler _handler;
        public string Name => "test";
        public string ShortDescription => "протестировать алгоритмы сортировки";

        public string Description =>
            "Тестирует имеющиеся алгоритмы сортировки на данном массиве.=n" +
            "Прогоняется установленное количество итераций";

        public string Usage => "\'test\'";

        public TestCommand(Handler handler)
        {
            _handler = handler;
        }

        public void Execute(params string[] arguments)
        {
            if (arguments.Length != 0)
            {
                Console.WriteLine("Ошибка - функцию следует вызывать без аргументов!");
            }
            else
            {
                Console.WriteLine("Количество итераций: {0}", _handler.Iterations);
                Console.WriteLine("Количество элементов в массиве: {0}", _handler.Sequence.Count);
                foreach (var method in _handler.SortMethods)
                {
                    var testResult = TestMethod(method.Value);
                    Console.WriteLine("{0} - {1:F6} мс", method.Key, testResult);
                }
                return;
            }
            Console.WriteLine("Попробуйте \'usage test\'");
        }

        /// <summary>
        /// Тестирует метод сортировки.
        /// Алгоритм вызывается на протяжении установленного количества итераций.
        /// </summary>
        /// <param name="method">Метод сортировки, применимый к <see cref="Array"/></param>
        /// <returns>Количество милисекунд на одну итерацию (усредненное)</returns>
        private double TestMethod(Action<int[]> method)
        {
            var watch = new Stopwatch();
            for (var i = 0; i < _handler.Iterations; i++)
            {
                var array = _handler.Sequence.ToArray();
                watch.Start();
                method.Invoke(array);
                watch.Stop();
            }
            return (double) watch.ElapsedMilliseconds / _handler.Iterations;
        }
    }
}