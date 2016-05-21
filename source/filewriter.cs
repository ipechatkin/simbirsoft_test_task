using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.MemoryMappedFiles;

namespace SSoftTest
{
    /// <summary>
    /// Класс-обертка, реализующий следующую функциональность: создание объекта для записи (StreamWriter), запись строки в выходной файл,
    /// добавление в выходной файл необходимых тэгов, корректное освобождение ресурсов (Dispose), отслеживание размера записываемого файла и
    /// создание при необходимости следующего.
    /// </summary>
    class FileWriter// : IDisposable
    {
        private System.Int32 partCounter;
        private System.String fileName;
        private StreamWriter sw = null;
        private MemoryMappedFile mmf = null;
        private MemoryMappedViewAccessor accessor = null;
        private MemoryMappedViewStream stream = null;

        /// <summary>
        /// Конструктор класса FileWriter.
        /// </summary>
        /// <param name="fFileName">Имя выходного файла.</param>
        public FileWriter(System.String fFileName)
        {
            fileName = fFileName;
            partCounter = 0;
        }

        /// <summary>
        /// Запись строки в выходной файл.
        /// </summary>
        /// <param name="fLine">Строка для записи в выходной файл.</param>
        /// <remarks>Метод Writeline отслеживает количество записанных строк. При превышении лимита вызывается метод CreateNextPart.</remarks>
        public void WriteLine(System.String fLine)
        {
            sw.WriteLine(fLine);
            //byte[] buffer = Encoding.Default.GetBytes(fLine);
            //accessor.WriteArray(position, buffer, 0, buffer.Count());
            //position += buffer.Count();
        }

        /// <summary>
        /// Публичный метод CreateNextPart. Назначение - создание следующего файла
        /// </summary>
        public void CreateNextPart(System.String fContent)
        {
            System.String content = "<!DOCTYPE HTML><html><head><title>" + fileName + partCounter.ToString() + "</title></head><body>" +
                fContent + " </body> </html>";

            mmf = MemoryMappedFile.CreateFromFile(fileName + partCounter.ToString() + ".html", FileMode.Create, "xxx",
                System.Text.ASCIIEncoding.Unicode.GetByteCount(content));

            stream = mmf.CreateViewStream();

            sw = new StreamWriter(stream, System.Text.Encoding.Unicode);
            sw.WriteLine(content);

            ++partCounter;

            mmf.Dispose();
            sw.Close();
            stream.Dispose();
        }

        ///// <summary>
        ///// Освобождает все ресурсы, используемые экземпляром класса FileWriter.
        ///// </summary>
        //public void Dispose()
        //{
        //    if (mmf != null)
        //    {
        //        mmf.Dispose();
        //    }

        //    if (accessor != null)
        //    {
        //        accessor.Dispose();
        //    }

        //    if (sw != null)
        //    {
        //        //sw.Write(" </body> </html>");
        //        sw.Close();
        //    }

        //    if (stream != null)
        //    {
        //        stream.Dispose();
        //    }
        //}
    }
}
