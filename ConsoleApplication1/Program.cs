using System;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main(string[] args)
		{
			do
			{
				double a;

				//FastRandom.Random rnd = new FastRandom.Random();
				Random rnd = new Random();
				DateTime startTime = DateTime.Now;
				for (ulong i = 0; i < 500*500*20*20; i++)
				{
					//Console.WriteLine(rnd.NextDouble());
					a = rnd.NextDouble();
				}
				TimeSpan timePassed = DateTime.Now - startTime;
				Console.WriteLine(timePassed);
			} while (Console.ReadKey().Key != ConsoleKey.Escape);
		}
	}
}
