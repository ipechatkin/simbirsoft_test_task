using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SSoftTest
{
    /// <summary>
    /// Класс-обертка, реализующий следующую функциональность: создание объекта для записи (StreamWriter), запись строки в выходной файл,
    /// добавление в выходной файл необходимых тэгов, корректное освобождение ресурсов (Dispose), отслеживание размера записываемого файла и
    /// создание при необходимости следующего.
    /// </summary>
    class FileWriter : IDisposable
    {
        private System.Int32 partCounter;
        private System.String fileName;
        private StreamWriter sw = null;

        /// <summary>
        /// Конструктор класса FileWriter.
        /// </summary>
        /// <param name="fFileName">Имя выходного файла.</param>
        /// <param name="fLinePerFile">Количество строк на файл.</param>
        public FileWriter(System.String fFileName)
        {
            fileName = fFileName;
            partCounter = 0;
            CreateNextPart();
        }

        /// <summary>
        /// Запись строки в выходной файл.
        /// </summary>
        /// <param name="fLine">Строка для записи в выходной файл.</param>
        /// <remarks>Метод Writeline отслеживает количество записанных строк. При превышении лимита вызывается метод CreateNextPart.</remarks>
        public void WriteLine(System.String fLine)
        {
            sw.WriteLine(fLine);
        }

        /// <summary>
        /// Публичный метод StepOver. Может быть использован другими классами для "форсированного" создания следующего файла.
        /// </summary>
        public void StepOver()
        {
            CreateNextPart();
        }

        /// <summary>
        /// Создание следующего файла.
        /// </summary>
        private void CreateNextPart()
        {
            Dispose();

            sw = new StreamWriter(new FileStream(fileName + partCounter.ToString() + ".html", FileMode.Create),
                System.Text.Encoding.Default);

            sw.WriteLine("<!DOCTYPE HTML><html><head><title>" + fileName + partCounter.ToString() + "</title></head><body>");

            ++partCounter;
        }

        /// <summary>
        /// Освобождает все ресурсы, используемые экземпляром класса FileWriter.
        /// </summary>
        public void Dispose()
        {
            if (sw != null)
            {
                sw.Write(" </body> </html>");
                sw.Close();
            }
        }
    }
}
