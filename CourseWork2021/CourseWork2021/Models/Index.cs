using CourseWork2021.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace CourseWork2021
{
    class Index : IIndex
    {
        public ConcurrentDictionary<string, List<string>> IndexDictionary { get; set; }
 
        public void TryAddKey(string word, string file)
        {
            IndexDictionary.AddOrUpdate(word,new List<string> { file },(key,value)=>value.Append(file).ToList());
        }

        public List<string> GetIndexFiles(string word)
        {
            if (IndexDictionary.Keys.Any(key => key.Contains(word)))
            {
                return IndexDictionary[word];
            }
            else
            {
                throw new ArgumentException("such word doesn't exist"); 
            }
        }
    }
}
