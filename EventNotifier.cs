using System;
using System.Diagnostics;
using System.Threading;

// Based on this article on Notifications in C# - https://www.tutorialsteacher.com/csharp/csharp-event
// Base class invokes child class, and child class sets a timer of 10 seconds and notifies base class when time is up.

namespace CSharpTestProj
{
    public class Program
    {
        static void Main(string[] args)
        {
            ProcessBusinessLogic bl = new ProcessBusinessLogic();
            bl.SteadyStateAchieved += StartEndpointDictionaryProcessing; // register with an event
            bl.StartProcess();
        }

        public static void StartEndpointDictionaryProcessing()
        {
            Console.WriteLine("Will Now Start Endpoint Processing!");
        }

    }
    public delegate void Notify();  // delegate

    public class ProcessBusinessLogic
    {
        public event Notify SteadyStateAchieved; // event
        Stopwatch stopWatch = new Stopwatch();

        public void StartProcess()
        {
            stopWatch.Start();
            Console.WriteLine("Init Started!");
            // some code here..
            OnProcessCompleted();
        }


        protected virtual void OnProcessCompleted()
        {
            while(stopWatch.ElapsedMilliseconds <= 10000)
            {
                Thread.Sleep(1);
            }
            
            SteadyStateAchieved?.Invoke();
        }
    }
}
