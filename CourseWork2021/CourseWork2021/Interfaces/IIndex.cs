using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace CourseWork2021.Interfaces
{
    interface IIndex
    {
        ConcurrentDictionary<string, List<string>> IndexDictionary { get; set; }
        void TryAddKey(string word, string file);
        List<string> GetIndexFiles(string word);
    }
}
