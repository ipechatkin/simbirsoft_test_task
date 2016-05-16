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
        private char[] sentenceTerm = { '.', '!', '?' };
        private string[] sentenceTermStr = { ".", "!", "?", ".<br>", "!<br>", "?<br>" };
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

        private List<string> SplitWithTerms(string fLine)
        {
            List<string> res = new List<string>();

            int oldPosition = 0;
            int index = System.Int32.MaxValue;


            while (-1 != index)
            {
                index = fLine.IndexOfAny(sentenceTerm, oldPosition);

                if (-1 != index)
                {
                    res.Add(fLine.Substring(oldPosition, index - oldPosition + 1));

                    oldPosition = index + 1;
                }
                else
                {
                    System.String str = fLine.Substring(oldPosition);

                    if (str != "")
                    {
                        res.Add(str);
                    }
                }
            }

            res[res.Count() - 1] += "<br>";

            return res;
        }

        private bool EndsWithSentenceTerm(System.String fLine)
        {
            for (System.Int32 index = 0; index < sentenceTermStr.Length; index++)
            {
                if (fLine.EndsWith(sentenceTermStr[index]))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Метод GetSentence предназначен для временного хранения строк файла, из которых состоят 
        /// "длинные" предложения, и для отслеживания строк, последний символ которых - 
        /// терминальный для предложений
        /// </summary>
        /// <param name="fLine">Строка входного файла (с уже произведенной заменой ключевых слов)</param>
        private IEnumerable<List<string>> GetSentence(System.String fLine)
        {
            // разбить строку на предложения (части)
            var content = SplitWithTerms(fLine);

            foreach(string part in content)
            {
                // предложение завершено, т.е. part заканчивается терминальным символом
                if(EndsWithSentenceTerm(part))
                {
                    currentSentence.Add(part);
                    List<string> completeSentence = new List<string>(currentSentence);
                    currentSentence.Clear();
                    yield return completeSentence;
                }
                else
                {
                    currentSentence.Add(part);
                }
            }
        }

        /// <summary>
        /// Метод ProcessLineWithHtmlTags производит замену слов, совпадающих с 
        /// ключевыми словами из словаря, следующим образом:
        /// word -&gt; &lt;b&gt;&lt;i&gt;word&lt;/i&gt;&lt;/b&gt;
        /// </summary>
        /// <param name="fLine">Строка входного файла</param>
        /// <returns>Строка, в которой произведена замена слов, совпадающих с ключевыми словами 
        /// из словаря</returns>
        /// <remarks></remarks>
        private System.String ProcessLineWithHtmlTags(System.String fLine)
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
                using (FileWriter fw = new FileWriter(outFileName))
                {
                    System.String str_in;

                    while((str_in = fr.ReadLine()) != null)
                    {
                        System.String str_out = ProcessLineWithHtmlTags(str_in);

                        var en = GetSentence(str_out).GetEnumerator();

                        while (en.MoveNext())
                        {
                            int lines = en.Current.Count();

                            if(lineCounter + lines > linePerFile)
                            {
                                lineCounter = 0;
                                fw.StepOver();
                            }

                            foreach (string str in en.Current)
                            {
                                if (str != "")
                                {
                                    fw.WriteLine(str);
                                    
                                    if (str.EndsWith("<br>"))
                                    {
                                        ++lineCounter;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
