using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SSoftTest
{
    /// <summary>
    /// Класс-обертка, реализующий следующую функциональность: создание объекта для чтения (StreamReader), чтение строки из входного файла,
    /// корректное освобождение ресурсов (Dispose). Данный класс создан (несмотря на то, что в классе StreamReader и в его базовых классах
    /// уже реализована такая функциональность) в целях возможной дальнейшей масштабируемости (например, может быть добавлен метод для
    /// контроля содержимого файла и выброса исключений при непрохождении контроля).
    /// </summary>
    class FileReader : IDisposable
    {
        private StreamReader sr = null;
        
        /// <summary>
        /// Конструктор класса FileReader/
        /// </summary>
        /// <param name="fFileName">Путь ко входному файлу.</param>
        public FileReader(string fFileName)
        {
            sr = new StreamReader(new FileStream(fFileName, FileMode.Open), 
                System.Text.Encoding.Default);
        }

        /// <summary>
        /// Чтение строки из файла
        /// </summary>
        /// <returns>Текущая прочитанная строка входного файла</returns>
        public string ReadLine()
        {
            return sr.ReadLine();
        }

        /// <summary>
        /// Освобождает все ресурсы, используемые экземпляром класса FileReader.
        /// </summary>
        public void Dispose()
        {
            if (sr != null)
            {
                sr.Close();
            }
        }
    }
}
