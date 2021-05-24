using CourseWork2021.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace CourseWork2021
{
    class Index : IIndex
    {
        public ConcurrentDictionary<string, List<string>> indexDictionary { get; set; }
        public delegate void AddOrUpdateValue(string key, string value);
        public AddOrUpdateValue addValue;

        public void AddKey(string word, string file)
        {
            addValue += AddValueToDictionary;
           indexDictionary.AddOrUpdate(word,file,)
        }

        public string[] GetIndexFiles(string word)
        {
            throw new NotImplementedException();
        }

        private void AddValueToDictionary(string key, string value)
        {

        }

        private void UpdateValueInDictionatry(string key, string value)
        {

        }
    }
}
