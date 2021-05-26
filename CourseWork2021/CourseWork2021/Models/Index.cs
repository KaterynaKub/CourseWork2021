using CourseWork2021.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace CourseWork2021
{
    public class Index : IIndex
    {
        public ConcurrentDictionary<string, IEnumerable<string>> IndexDictionary { get;} = new();

 
        public void TryAddKey(string word, string file)
        {
            IndexDictionary.AddOrUpdate(word,new List<string> { file },(key,value)=>value.Append(file));
        }
        public IEnumerable<string> GetIndexFiles(string word)
        {
            if (IndexDictionary.ContainsKey(word))
            {
                return IndexDictionary[word];
            }
            else
            {
                return new List<string> {};
            }
        }
    }
}
