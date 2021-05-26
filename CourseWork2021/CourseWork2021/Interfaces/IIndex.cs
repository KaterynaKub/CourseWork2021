using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace CourseWork2021.Interfaces
{
    interface IIndex
    {
        ConcurrentDictionary<string, IEnumerable<string>> IndexDictionary { get; }
        void TryAddKey(string word, string file);
        IEnumerable<string> GetIndexFiles(string word);
    }
}
