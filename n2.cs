using System;
using System.IO;

namespace Task_2
{
    enum Sevirity
    {
        Trace,
        Debug,
        Information,
        Warning,
        Error,
        Critical,
        Severity
    }

    class Program
    {
        public sealed class Logger : IDisposable
        {
            private readonly StreamWriter _logWriter;
            public Logger(string filePath)
            {
                _logWriter = new StreamWriter(filePath);
            }

            public void Log(string MessageForLog, Sevirity sevirity)
            {
                _logWriter.WriteLine($"[{DateTime.Now:G}][{sevirity}]: {MessageForLog}");
            }

            public void Dispose()
            {
                _logWriter.Dispose();
                GC.SuppressFinalize(this);
            }

            ~Logger()
            {
                _logWriter.Dispose();
            }

        }


        static void Main(string[] args)
        {
            if (args.Length >= 1)
            {
                using (Logger logger = new Logger(args[0]))
                {
                    Console.WriteLine("Logger named {0} were created\n", args[0]);
                    logger.Log("No information", Sevirity.Information);
                    logger.Log("Error with mind", Sevirity.Error);
                    logger.Log("Critical", Sevirity.Critical);
                    logger.Log("Something interesting", Sevirity.Debug);
                    logger.Log("Bye!", Sevirity.Trace);
                }
            } else
            {
                Console.WriteLine("Error! No arguments were given\n");
                Environment.Exit(-1);
            }
            
        }
    }
}