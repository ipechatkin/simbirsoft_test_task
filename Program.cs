using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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
            userShell.Exec(args);
        }
    }
}
