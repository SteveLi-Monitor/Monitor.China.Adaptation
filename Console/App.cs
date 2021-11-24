using Serilog;
using System;
using System.Threading.Tasks;

namespace Console
{
    internal class App
    {
        public async Task StartAsync()
        {
            Log.Debug($"{nameof(App)}.{nameof(StartAsync)}: Started.");

            while (true)
            {
                System.Console.WriteLine(
                    Environment.NewLine +
                    "Menu: " + Environment.NewLine +
                    "1. Import parts" + Environment.NewLine +
                    "0. Exit" + Environment.NewLine);

                var optionString = System.Console.ReadLine();

                if (int.TryParse(optionString, out int option))
                {
                    switch (option)
                    {
                        case 0:
                            Environment.Exit(0);
                            break;

                        default:
                            Log.Error($"Invalid option '{option}'. Please enter a correct option.");
                            break;
                    }
                }
                else
                {
                    Log.Error($"Invalid option '{optionString}'. Please enter an integer.");
                }
            }
        }
    }
}
