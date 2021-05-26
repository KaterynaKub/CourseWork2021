using CourseWork2021.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourseWork2021
{
    public class IndexerService
    {
        public IIndex Index { get; private set; }
        public int CountOfThreads { get; set; }

        public string RootDir { get; set; } 

        public IndexerService(int countThreads = 8, string root = @"K:\3 курс 2 семестр\parallel\test")
        {
            CountOfThreads = countThreads;
            RootDir = root;
        }
        public IEnumerable<string> GetFileNames()
        {
            IEnumerable<string> files = Directory.EnumerateFiles(RootDir, "*.txt", SearchOption.AllDirectories);
            return files;
        }

        public async Task RunThreadsAsync()
        {
            IEnumerable<string> fileNames = GetFileNames();
            int step = (int)Math.Ceiling((double)fileNames.Count() / CountOfThreads);
            Index = new Index();
           
            Task[] tasks = new Task[CountOfThreads];

            for (int i = 0; i < CountOfThreads; i++)
            {
                var filesStep = fileNames.Skip(i * step).Take(step);
                tasks[i] = Task.Run(() => CreateIndex(filesStep));
            }
            await Task.WhenAll(tasks);
        }
        private void CreateIndex(IEnumerable<string> files)
        {
            foreach (var file in files)
            {
                if (File.Exists(file))
                {
                    string result = File.ReadAllText(file);
                    List<string> words = result.Split(new[]{' ', ',', '.', '\"', ')', '(', ':', ';', '-','[',']','%','!','?','*','<','>'},StringSplitOptions.RemoveEmptyEntries).Distinct().ToList();
                    foreach (var word in words)
                    {
                        Index.AddKey(word.ToLower(), file);
                    }
                }
            }
        }

        public IEnumerable<string> GetFilesByWord(string word)
        {
            return Index.GetIndexFiles(word);
        }

        public IEnumerable<string> GetFilesByWords(string[] words)
        {
            IEnumerable<string> result = GetFilesByWord(words.First());
            foreach (var word in words.Skip(1))
            {
                IEnumerable<string> files = GetFilesByWord(word);
                result = result.Intersect(files);
            }
            return result;
        }
    }
}
