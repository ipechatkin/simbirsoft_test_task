using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;


namespace SSoftTest
{
    /// <summary>
    /// Класс приложения.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Точка входа в приложение
        /// </summary>
        /// <param name="args">Массив аргументов командной строки</param>
        static void Main(string[] args)
        {
            UserShell userShell = new UserShell();

            var sw = Stopwatch.StartNew();

            userShell.Exec(args);

            sw.Stop();

            Console.WriteLine("Performance: {0}", (double)sw.ElapsedMilliseconds / 1000.0);
        }
    }
}
