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
    class FileWriter
    {
        private System.String fileName;
        private System.String content;

        /// <summary>
        /// Конструктор класса FileWriter.
        /// </summary>
        /// <param name="fFileName">Имя выходного файла.</param>
        /// <param name="fContent"></param>
        public FileWriter(System.String fFileName, System.String fContent)
        {
            fileName = fFileName;
            content = "<!DOCTYPE HTML><html><head><title>" + fileName + "</title></head><body>" +
                    fContent + " </body> </html>";
        }

        /// <summary>
        /// Публичный метод CreateNextPart. Назначение - создание следующего файла
        /// </summary>
        public void CreateNextPart()
        {
            MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile(fileName, FileMode.Create, "xxx", 
                System.Text.ASCIIEncoding.Unicode.GetByteCount(content));

            MemoryMappedViewStream stream = mmf.CreateViewStream();

            StreamWriter sw = new StreamWriter(stream, System.Text.Encoding.Unicode);
            sw.WriteLine(content);

            mmf.Dispose();
            sw.Close();
            stream.Dispose();
        }
    }
}
