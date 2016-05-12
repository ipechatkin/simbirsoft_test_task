using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace SSoftTest
{
    /// <summary>
    /// Назначение класса - преобразование очередной строки входного файла в соответствии с заданием.
    /// </summary>
    class Converter
    {
        private System.String inFileName;
        private System.String outFileName;
        private System.Int32 linePerFile;
        private System.Int32 lineCounter;
        private List<System.String> currentSentence;
        private char[] trimChars = { ' ', '\t'};
        private string[] sentenceTerm = { ".", "!", "?" };
        private bool sentenceComplete = false;
        private MyGlossary glossary;
        
        /// <summary>
        /// Конструктор класса Converter.
        /// </summary>
        /// <param name="fInFileName">Путь ко входному файлу</param>
        /// <param name="fOutFileName">Имя выходного файла</param>
        /// <param name="fGlossaryFileName">Путь к файлу словаря</param>
        /// <param name="fLinePerFile">Количество строк на файл</param>
        public Converter(System.String fInFileName, System.String fOutFileName, 
            System.String fGlossaryFileName, System.Int32 fLinePerFile)
        {
            glossary = new MyGlossary(fGlossaryFileName);
            currentSentence = new List<System.String>();
            inFileName = fInFileName;
            outFileName = fOutFileName;
            linePerFile = fLinePerFile;
        }

        /// <summary>
        /// Метод TraceSentence предназначен для временного хранения строк файла, из которых состоят 
        /// "длинные" предложения, и для отслеживания строк, последний символ которых - 
        /// терминальный для предложений
        /// </summary>
        /// <param name="fLine">Строка входного файла (с уже произведенной заменой ключевых слов)</param>
        private void TraceSentence(System.String fLine)
        {
            System.String tempStr = System.String.Copy(fLine);
            tempStr = tempStr.TrimEnd(trimChars);

            // возможно, строка заканчивается терминальным символом
            foreach (System.String str in sentenceTerm)
            {
                if (tempStr.EndsWith(str))
                {
                    currentSentence.Add(fLine);
                    sentenceComplete = true;
                    return;
                }
            }

            // строка не заканчивается терминальным символом
            currentSentence.Add(fLine);
            sentenceComplete = false;
        }

        /// <summary>
        /// Метод ProcessLine производит замену слов, совпадающих с ключевыми словами из словаря, следующим образом:
        /// word -&gt; &lt;b&gt;&lt;i&gt;word&lt;/i&gt;&lt;/b&gt;
        /// </summary>
        /// <param name="fLine">Строка входного файла</param>
        /// <returns>Строка, в которой произведена замена слов, совпадающих с ключевыми словами 
        /// из словаря</returns>
        /// <remarks></remarks>
        private System.String ProcessLine(System.String fLine)
        {
            System.String pattern = @"\b";

            System.String retLine = System.String.Copy(fLine);

            foreach (System.String str in glossary)
            {
                retLine = Regex.Replace(retLine, pattern + str + pattern, "<b><i>" + str + "</i></b>");
            }

            return retLine;
        }

        /// <summary>
        /// Метод DoWork производит построчное чтение входного файла, вызов функции ProcessLine 
        /// для преобразования считанной строки и запись преобразованной строки в выходной файл
        /// </summary>
        /// <remarks>В процессе записи преобразованных строк в выходной файл метод DoWork отслеживает 
        /// количество записанных строк и, при необходимости вызывает метод StepOver класса FileWriter 
        /// для закрытия текущего и создания следующего html-файла.</remarks>
        public void DoWork()
        {   
            using (FileReader fr = new FileReader(inFileName))
            {
                using (FileWriter fw = new FileWriter(outFileName, linePerFile))
                {
                    System.String str_in;

                    while((str_in = fr.ReadLine()) != null)
                    {
                        System.String str_out = ProcessLine(str_in);

                        ++lineCounter;

                        if(lineCounter > linePerFile)
                        {
                            lineCounter = 0;
                            fw.StepOver();
                        }

                        TraceSentence(str_out);
                            
                        if (sentenceComplete)
                        {
                            foreach (System.String str in currentSentence)
                            {
                                fw.WriteLine(str);
                            }

                            if (0 == lineCounter)
                            {
                                lineCounter = currentSentence.Count();
                            }
                            currentSentence.Clear();
                        }
                    }
                }
            }
        }
    }
}
