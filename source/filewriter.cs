using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.MemoryMappedFiles;

namespace SSoftTest
{
    /// <summary>
    /// Класс FileWriter предназначен для создания и записи очередного html-файла.
    /// </summary>
    class FileWriter
    {
        private System.String fileName;
        private System.String content;

        /// <summary>
        /// Конструктор класса FileWriter.
        /// </summary>
        /// <param name="fFileName">Имя выходного файла.</param>
        /// <param name="fContent">Содержимое выходного файла.</param>
        public FileWriter(System.String fFileName, System.String fContent)
        {
            fileName = fFileName;
            content = "<!DOCTYPE HTML><html><head><title>" + fileName + "</title></head><body>" +
                    fContent + " </body> </html>";
        }

        /// <summary>
        /// Публичный метод CreateNextPart. Назначение - создание следующего файла.
        /// </summary>
        public void CreateNextPart()
        {
            //Console.WriteLine(System.Text.ASCIIEncoding.Unicode.GetByteCount(content) + ":"  + fileName);
            //Console.WriteLine(content);

            MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile(fileName, FileMode.Create, "xxx", 
                //System.Text.ASCIIEncoding.Unicode.GetByteCount(content) + 0);
                System.Text.ASCIIEncoding.ASCII.GetByteCount(content));

            MemoryMappedViewStream stream = mmf.CreateViewStream();

            //StreamWriter sw = new StreamWriter(stream, System.Text.UnicodeEncoding.Unicode);
            StreamWriter sw = new StreamWriter(stream, System.Text.ASCIIEncoding.Default);
            sw.Write(content);

            
            sw.Close();
            stream.Dispose();
            mmf.Dispose();
        }
    }
}
