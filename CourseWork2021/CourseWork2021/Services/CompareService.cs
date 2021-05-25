using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork2021.Services
{
    public class CompareService
    {
        static public bool Compare()
        {
            IndexerService indexerServiceSerial = new(1);
            IndexerService indexerServiceParallel = new(8);
            indexerServiceSerial.RunThreadsAsync().Wait();
            indexerServiceParallel.RunThreadsAsync().Wait();
            ConcurrentDictionary<string, IEnumerable<string>> dictionarySerial = indexerServiceSerial.Index.IndexDictionary;
            ConcurrentDictionary<string, IEnumerable<string>> dictionaryParallel = indexerServiceParallel.Index.IndexDictionary;
            foreach (var (word, files) in dictionarySerial)
            {

                if (dictionaryParallel.TryGetValue(word, out IEnumerable<string> value))
                {
                    if (value.Except(files).Any() || files.Except(value).Any())
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
