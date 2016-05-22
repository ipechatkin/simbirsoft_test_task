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

            //using (StreamWriter fw = new StreamWriter(new FileStream("fin3.txt", FileMode.Create),
            //        System.Text.Encoding.Default))
            //{
            //    for (int index = 0; index < 5; index++)
            //    {
            //        using (FileReader fr = new FileReader(@"..\..\..\test_data\fin2.txt"))
            //        {
            //            System.String str_in;

            //            while ((str_in = fr.ReadLine()) != null)
            //            {
            //                fw.WriteLine(str_in);
            //            }
            //        }  
            //    }
            //}
            //return;

            UserShell userShell = new UserShell();

            var sw = Stopwatch.StartNew();

            userShell.Exec(args);

            sw.Stop();

            Console.WriteLine("Performance: {0}", (double)sw.ElapsedMilliseconds / 1000.0);
        }
    }
}
