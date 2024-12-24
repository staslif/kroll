using System;
using System.Threading;

class AnimalThread
{
    private string name;
    private int priority;
    private int distanceCovered = 0;
    private Random random = new Random();
    public AnimalThread(string name, int priority)
    {
        this.name = name;
        this.priority = priority;
    }
    public void Run()
    {
        while (distanceCovered < 100)
        {
            int step = random.Next(1, 10);
            distanceCovered += step;
            Console.WriteLine(name + " пробежал " + distanceCovered + " метров.");
            if (distanceCovered < 50)
            {
                Thread.CurrentThread.Priority = ThreadPriority.Highest;
            }
            else
            {
                Thread.CurrentThread.Priority = ThreadPriority.Normal;
            }
            Thread.Sleep(100);
        }
        Console.WriteLine(name + " достиг 100 метров!");
    }
}
class RabbitAndTurtle
{
    public static void Main()
    {
        AnimalThread rabbit = new AnimalThread("Кролик", (int)ThreadPriority.Normal);
        AnimalThread turtle = new AnimalThread("Черепаха", (int)ThreadPriority.Normal);
        Thread rabbitThread = new Thread(new ThreadStart(rabbit.Run));
        Thread turtleThread = new Thread(new ThreadStart(turtle.Run));
        rabbitThread.Start();
        turtleThread.Start();
        rabbitThread.Join();
        turtleThread.Join();
        Console.WriteLine("Финиш");
    }
}
