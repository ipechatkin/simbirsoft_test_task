using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SSoftTest
{
    /// <summary>
    /// Класс, реализующий интерфейс пользователя.
    /// </summary>
    class UserShell
    {
        System.String[] cmdParams;

        /// <summary>
        /// Конструктор UserShell.
        /// </summary>
        public UserShell()
        {
            cmdParams = new System.String[] {"", "", "index", "10"};
        }

        /// <summary>
        /// Метод ShowCorrectUsage. Назначение - подсказка пользователю о том, как правильно формировать параметры командной строки.
        /// </summary>
        private void ShowCorrectUsage()
        {
            Console.WriteLine("usage: SSoftText.exe fin gls [fout] [n], where\n");
            Console.WriteLine("fin - path to source file,");
            Console.WriteLine("gls - path to glossary file,");
            Console.WriteLine("fout - path to destination file, \"index\" - default,");
            Console.WriteLine("n - number of lines in each of destination files, 10 - default");
            
        }

        /// <summary>
        /// Метод Exec. Назначение - разбор аргументов командной строки и запуск функционала.
        /// </summary>
        /// <param name="args">Массив аргументов командной строки.</param>
        public void Exec(string[] args)
        {
            if ((2 > args.Count()) || (4 < args.Count()))
            {
                ShowCorrectUsage();
                Console.ReadKey();
            }
            else
            {
                try
                {
                    args.CopyTo(cmdParams, 0);

                    Converter converter = new Converter(cmdParams[0], cmdParams[2], cmdParams[1], 
                            Convert.ToInt32(cmdParams[3]));
                    converter.DoWork();
                    Console.WriteLine("Работа успешно завершена.\n");
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
                }                    
                
                //Console.ReadKey();
            }
        }
    }
}
