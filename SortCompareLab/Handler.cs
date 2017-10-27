using System;
using System.Collections.Generic;
using SortCompareLab.Commands;

namespace SortCompareLab
{
    /// <summary>
    ///     Класс отвечает за обработку команд (ввода/вывода с консоли)
    /// </summary>
    class Handler
    {
        public Dictionary<string, ICommand> CommandMap { get; } =
            new Dictionary<string, ICommand>();

        public List<ICommand> Commands { get; } = new List<ICommand>();
        public bool Running { get; set; } = true;

        public int Iterations { get; set; } = 100;
        public List<int> Sequence { get; } = new List<int>();

        public Dictionary<string, Action<int[]>> SortMethods { get; } =
            new Dictionary<string, Action<int[]>>();

        /// <summary>
        ///     Инициализирует обработчик
        /// </summary>
        /// <param name="sortMethods">Словарь методов сортировки с которыми работает программа</param>
        public Handler(Dictionary<string, Action<int[]>> sortMethods)
        {
            foreach (var method in sortMethods)
            {
                SortMethods.Add(method.Key, method.Value);
            }
            AddCommands();
            FillCommandMap();
        }

        /// <summary>
        ///     Начинает работу обработчика команд
        /// </summary>
        public void Run()
        {
            while (Running)
            {
                Console.Write("Введите команду: ");
                var rawInput = Console.ReadLine();
                if (rawInput == null)
                {
                    continue;
                }
                var parsed = rawInput.Split(new[] {' ', '\t'},
                                            StringSplitOptions.RemoveEmptyEntries);
                if (parsed.Length == 0)
                {
                    continue;
                }

                var arguments = new string[parsed.Length - 1];
                Array.Copy(parsed, 1, arguments, 0, arguments.Length); // Отбрасываем саму команду
                var command = CheckCommand(parsed[0]); // Преобразование в рабочий формат
                command.Execute(arguments);
            }
        }

        /// <summary>
        ///     Проверяет, существует ли команда в списке доступных.
        ///     Если существует - возвращает команду.
        ///     Если не существует - возвращает команду <see cref="NotFoundCommand" />
        ///     Работает с командами, реализующими интерфейс <see cref="ICommand" />
        /// </summary>
        /// <param name="command">Название команды</param>
        /// <returns>Команда реализующая интерфейс <see cref="ICommand" /></returns>
        private ICommand CheckCommand(string command)
        {
            if (CommandMap.ContainsKey(command))
            {
                return CommandMap[command];
            }
            var notFound = new NotFoundCommand(this) {Name = command};
            return notFound;
        }

        /// <summary>
        ///     Заполняет карту комманд (для вызова пользователя)
        ///     Командный лист не содержит служебных комманд!
        /// </summary>
        private void FillCommandMap()
        {
            foreach (var command in Commands)
            {
                CommandMap.Add(command.Name, command);
            }
        }

        /// <summary>
        ///     Добавляет команды, доступные для вызова пользователем
        /// </summary>
        private void AddCommands()
        {
            // Добавлены должны быть все команды пользователя
            // Служебные команды НЕ ДОБАВЛЯЮТСЯ
            Commands.Add(new HelpCommand(this));
            Commands.Add(new UsageCommand(this));
            Commands.Add(new ExitCommand(this));
            Commands.Add(new IterationsCommand(this));
            Commands.Add(new SequenceCommand(this));
            Commands.Add(new RandomCommand(this));
            Commands.Add(new TestCommand(this));
        }
    }
}