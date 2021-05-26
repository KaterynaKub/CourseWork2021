using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork2021.Interfaces
{
    public interface IIndexerService
    {
        IIndex Index { get; }
        int CountOfThreads { get; set; }
        string RootDir { get; set; }
        IEnumerable<string> GetFileNames();
    }
}
