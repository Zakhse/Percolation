using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Percolation
{
	class Program
	{
		static void Main()
		{
			bool experimentalMode = false;
			do
			{
				uint sizeOfMatrix = 500;
				int stepProbability = 5; //from 1 to 100
				int numberOfExperiments = 100;
				double probability;
				int intProbability;
				bool isPercolation;
				DateTime time;
				DateTime startTime = DateTime.Now;
				if (experimentalMode) //experimatal mode
				{

					for (intProbability = 0; intProbability <= 100; intProbability = intProbability + stepProbability)
					{

						probability = intProbability / 100.0;
						time = DateTime.Now;
						for (int i = 0; i < numberOfExperiments; i++)
						{
							DateTime tempTime = DateTime.Now;
							PercolationCell[,] matrix = Methods.CreatePercolationMatrix(sizeOfMatrix, probability);
							//Console.Write("{0} ",i);
							//Console.Write("Created {0:0.##} sec. ",(DateTime.Now-tempTime).TotalSeconds);
							tempTime = DateTime.Now;
							isPercolation = Methods.CheckPercolation(matrix);
							//Console.WriteLine("Checked {0:0.##} sec. {1} ",(DateTime.Now - tempTime).TotalSeconds,isPercolation);

							/*if ((i - 1) % 1 == 0)
							{
								Console.WriteLine(i);
							}*/
						}
						Console.Write("Probability = {0} : ", probability);
						Console.WriteLine("{0:0.##} sec.", (DateTime.Now - time).TotalSeconds);
					}
				}
				else//single-matrix mode
				{
					sizeOfMatrix = Methods.InputPositiveIntegerNumber("size of matrix");
					probability = Methods.InputProbability();
					PercolationCell[,] matrix = Methods.CreatePercolationMatrix(sizeOfMatrix, probability);
					isPercolation = Methods.CheckPercolation(matrix,true);
					Methods.PrintPercolationMatrix(matrix);
					Console.WriteLine("There is percolation: {0}",isPercolation);
				}
				Console.WriteLine("Total time : {0}", DateTime.Now - startTime);
				Console.WriteLine("Press ESC to exit or another key to continue...");
			} while (Console.ReadKey(true).Key != ConsoleKey.Escape);
			//Repeats the program if user press Esc
		}
	}
}
