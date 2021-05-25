using CourseWork2021.Services;
using System;
using System.Threading.Tasks;

namespace CourseWork2021
{
    class Program
    {
        static void Main(string[] args)
        {
            TimerService timerService = new();
            for (int i = 1; i <= 10; i++)
            {
                timerService.MeasureTimeParallelThreadsAsync(i);
            }
            CompareService compareService = new();
            bool areEquel = CompareService.Compare();
            Console.Write("Dictionaries are ");
            Console.Write(areEquel ? "Equel" : "Not equel");
        }
    }
}
