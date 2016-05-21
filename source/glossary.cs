using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SSoftTest
{
    /// <summary>
    /// Класс словаря
    /// </summary>
    internal class MyGlossary<T> : IEnumerable where T : IEnumerable, new()
    //internal class MyGlossary : IEnumerable
    {
        private T/*List<string>*/ keyWords;

        /// <summary>
        /// Конструктор класса "Словарь"
        /// </summary>
        /// <param name="fFileName">Путь к файлу словаря</param>
        /// <remarks>Для хранения ключевых слов из словаря используется тип List&lt;System.String&gt;</remarks>
        public MyGlossary(string fFileName)
        {
            using (StreamReader sr = new StreamReader(fFileName, System.Text.Encoding.Default))
            {
                string s = "";
                keyWords = new T/*List<string>*/();

                while ((s = sr.ReadLine()) != null)
                {
                    //keyWords.Add(s);
                    Add(s);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fLine"></param>
        private void Add(string fLine)
        {
            if (keyWords is List<string>)
            {
                List<string> tmp = keyWords as List<string>;
                tmp.Add(fLine);
            }
            else
            if (keyWords is Queue<string>)
            {
                Queue<string> tmp = keyWords as Queue<string>;
                tmp.Enqueue(fLine);
            }
            else
            if (keyWords is Stack<string>)
            {
                Stack<string> tmp = keyWords as Stack<string>;
                tmp.Push(fLine);
            }
            else
            if (keyWords is Dictionary<string, int>)
            {
                Dictionary<string, int> tmp = keyWords as Dictionary<string, int>;
                tmp.Add(fLine, 0);
            }
        }

        /// <summary>
        /// Реализация метода GetEnumerator() интерфейса IEnumerable.
        /// </summary>
        /// <returns>Возвращаемое значение - List&lt;System.String&gt;.Enumerator</returns>
        public IEnumerator GetEnumerator()
        {
            //return (IEnumerator)this;
            return keyWords.GetEnumerator();
        }

        /*public bool MoveNext()
        {
            if (keyWordList.Count() - 1 == position)
            {
                Reset();
                return false;
            }

            position++;
            return true;
        }

        public void Reset()
        {
            position = -1;
        }

        public object Current
        {
            get 
            {
                return keyWordList[position];
            }
        }

        public bool Contains(string fItem)
        {
            return keyWordList.Contains(fItem);
        }*/
    }
}
