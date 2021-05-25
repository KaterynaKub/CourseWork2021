using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork2021.Services
{
    class TimerService
    {
        private Stopwatch timer;

        public TimerService()
        {
            timer = new Stopwatch();
        }

        public void MeasureTimeParallelThreadsAsync(int countOfThreads)
        {
            GC.Collect();
            timer = new Stopwatch();
            timer.Start();
            IndexerService IndexerService = new(countOfThreads);
            IndexerService.RunThreadsAsync().Wait();
            timer.Stop();
            Console.WriteLine($"Time : {timer.ElapsedMilliseconds} miliseconds");
        }
    }
}
