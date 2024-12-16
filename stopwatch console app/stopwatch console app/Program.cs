using System;
using System.Threading;

public class Stopwatch
{
    // Fields
    private TimeSpan timeElapsed;
    private bool isRunning;
    private Timer timer;

    // Events
    public event StopwatchEventHandler OnStarted;
    public event StopwatchEventHandler OnStopped;
    public event StopwatchEventHandler OnReset;

    // Delegate
    public delegate void StopwatchEventHandler(string message);

    // Constructor
    public Stopwatch()
    {
        timeElapsed = TimeSpan.Zero;
        isRunning = false;
    }

    // Methods
    public void Start()
    {
        if (!isRunning)
        {
            isRunning = true;
            OnStarted?.Invoke("Stopwatch Started!");
            timer = new Timer(Tick, null, 0, 1000);
        }
    }

    public void Stop()
    {
        if (isRunning)
        {
            isRunning = false;
            timer?.Dispose();
            OnStopped?.Invoke("Stopwatch Stopped!");
        }
    }

    public void Reset()
    {
        Stop();
        timeElapsed = TimeSpan.Zero;
        OnReset?.Invoke("Stopwatch Reset!");
    }

    private void Tick(object state)
    {
        if (isRunning)
        {
            timeElapsed = timeElapsed.Add(TimeSpan.FromSeconds(1));
            Console.WriteLine($"Time Elapsed: {timeElapsed}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();

            // Subscribe to events
            stopwatch.OnStarted += EventMessageHandler;
            stopwatch.OnStopped += EventMessageHandler;
            stopwatch.OnReset += EventMessageHandler;

            // UI Loop
            while (true)
            {
                Console.WriteLine("Welcome to Stopwatch Console Application");
                Console.WriteLine("       Press S to Start\n       press T to Stop\n       press R to Reset\n       press Q to Quit");
                var input = Console.ReadKey().Key;

                if (input == ConsoleKey.S)
                {
                    stopwatch.Start();
                }
                else if (input == ConsoleKey.T)
                {
                    stopwatch.Stop();
                }
                else if (input == ConsoleKey.R)
                {
                    stopwatch.Reset();
                }
                else if (input == ConsoleKey.Q)
                {
                    break;
                }
            }
        }

        // Event handler method
        static void EventMessageHandler(string message)
        {
            Console.WriteLine(message);
        }
    }

}
