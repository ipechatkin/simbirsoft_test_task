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
    class MyGlossary : IEnumerable
    {
        private List<string> keyWords;
        private Tuple<string, System.Object> tuple;
        private Tuple<string, System.Object> currentTuple;

        /// <summary>
        /// Конструктор класса "Словарь"
        /// </summary>
        /// <param name="fFileName">Путь к файлу словаря</param>
        /// <remarks>Для хранения ключевых слов из словаря используются тип List&lt;System.String&gt; и Tuple произвольной длины</remarks>
        public MyGlossary(string fFileName)
        {
            using (StreamReader sr = new StreamReader(fFileName, System.Text.Encoding.Default))
            {
                string s = "";
                keyWords = new List<string>();

                while ((s = sr.ReadLine()) != null)
                {
                    keyWords.Add(s);
                }
            }

            tuple = new Tuple<string, System.Object>("-1", ToTuple(keyWords.Count()));
            currentTuple = tuple;
        }

        //private System.String GetTupleElement(int n, Tuple<string, System.Object> fTuple)
        //{
        //    if (0 == n)
        //    {
        //        return fTuple.Item1;
        //    }
        //    else
        //    {
        //        return GetTupleElement(n - 1, (Tuple<string, System.Object>)fTuple.Item2);
        //    }
        //}


        private Tuple<string, System.Object> ToTuple(int n)
        {
            if (1 == n)
            {
                return Tuple.Create(keyWords[0], (System.Object)null);
            }
            else
            {
                return new Tuple<string, System.Object>(keyWords[n - 1], ToTuple(n - 1));
            }
        }


        /// <summary>
        /// Реализация метода GetEnumerator() интерфейса IEnumerable.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public MyGlossary GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            bool result = true;

            currentTuple = (Tuple<string, System.Object>)currentTuple.Item2;

            if (null == currentTuple)
            {
                Reset();
                result = false;
            }
            return result;
        }

        public void Reset()
        {
            currentTuple = tuple;
        }

        public object Current
        {
            get 
            {
                return currentTuple.Item1;
            }
        }
    }
}
