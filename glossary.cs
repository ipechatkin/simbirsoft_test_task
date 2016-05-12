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
    internal class MyGlossary : IEnumerable
    {
        private List<string> keyWordList;
        //private System.Int32 position = -1;

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
                keyWordList = new List<string>();

                while ((s = sr.ReadLine()) != null)
                {
                    keyWordList.Add(s);
                }
            }
        }

        /// <summary>
        /// Реализация метода GetEnumerator() интерфейса IEnumerable.
        /// </summary>
        /// <returns>Возвращаемое значение - List&lt;System.String&gt;.Enumerator</returns>
        public IEnumerator GetEnumerator()
        {
            //return (IEnumerator)this;
            return keyWordList.GetEnumerator();
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
