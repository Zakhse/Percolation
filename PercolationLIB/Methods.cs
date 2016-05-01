using System;

namespace PercolationLIB
{
	public class Methods
	{
		public static uint InputPositiveIntegerNumber(string name = "")
		{
			uint enteredNumber;
			Console.Write("Enter {0}: ", name);
			while (!uint.TryParse(Console.ReadLine(), out enteredNumber) || enteredNumber <= 0)
			{
				Console.Write("Ivalid value. Enter {0}: ", name);
			}
			return enteredNumber;
		}

		public static double InputProbability(string name = "probability")
		{
			double enteredNumber;
			Console.Write("Enter {0}: ", name);
			while (!double.TryParse(Console.ReadLine(), out enteredNumber) || (enteredNumber > 1.0) || (enteredNumber < 0.0))
			{
				Console.Write("Ivalid value, it must be from 0.0 to 1.0.\nEnter {0}: ", name);
			}
			return enteredNumber;
		}

		public static string StringPercolationMatrix(PercolationCell[,] matrix)
		{
			string str = "";
			/*for (int i = 0; i < matrix.GetLength(1); i++)
			{
				str += "_";
			}*/
			str += "\n";
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					if (matrix[i, j].Filled)
					{
						str += String.Format("{0}\t", matrix[i, j].Group);
					}
					else
					{
						str += String.Format("--\t");
					}
				}
				str += "\n";
			}
			/*for (int i = 0; i < matrix.GetLength(1); i++)
			{
				str += "_";
			}
			Console.WriteLine();*/
			return str;
		}

		private static Random rnd = new Random();

		//Probability must be from 0.0 to 1.0!
		public static PercolationCell[,] CreatePercolationMatrix(uint height, uint width, double probability)
		{
			PercolationCell[,] matrix = new PercolationCell[height, width];
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					double prob = rnd.NextDouble();
					if ((probability == 1) || (prob < probability))
					{
						matrix[i, j] = new PercolationCell(true, prob);
					}
					else
					{
						matrix[i, j] = new PercolationCell(false, prob);
					}
				}
			}
			return matrix;
		}

		public static bool CheckPercolation(PercolationCell[,] matrix, bool turnAllGroupsToRight = false)
		{
			bool isPercolation = false;
			int[] group = new int[matrix.GetLength(0) * matrix.GetLength(1) / 2 + 1];
			int nextGroup = 0;
			if (matrix[0, 0].Filled)
			{
				group[nextGroup] = nextGroup;
				matrix[0, 0].Group = nextGroup;
				nextGroup++;
			}
			for (uint j = 1; j < matrix.GetLength(1); j++)//top line
			{
				if (matrix[0, j].Filled)
				{
					if (matrix[0, j - 1].Filled)
					{
						matrix[0, j].Group = group[matrix[0, j - 1].Group];
					}
					else
					{
						group[nextGroup] = nextGroup;
						matrix[0, j].Group = nextGroup;
						nextGroup++;
					}
				}

			}
			for (uint i = 1; i < matrix.GetLength(0); i++)//left column
			{
				//check element
				if (matrix[i, 0].Filled)
				{
					//check upper element
					if (matrix[i - 1, 0].Filled)
					{
						matrix[i, 0].Group = group[matrix[i - 1, 0].Group];
					}
					else
					{
						group[nextGroup] = nextGroup;
						matrix[i, 0].Group = nextGroup;
						nextGroup++;
					}
				}
			}
			for (int i = 1; i < matrix.GetLength(0); i++)
			{
				for (int j = 1; j < matrix.GetLength(1); j++)
				{
					//element=0
					if (!matrix[i, j].Filled)
					{
						continue;
					}

					//upper=1
					if (matrix[i - 1, j].Filled)
					{
						//upper=1 left=1
						if (matrix[i, j - 1].Filled)
						{
							if (group[matrix[i - 1, j].Group] < group[matrix[i, j - 1].Group])
							{
								matrix[i, j].Group = group[matrix[i - 1, j].Group];
								//group[ReturnRightGroup(matrix[i, j - 1].Group, group)] = group[matrix[i - 1, j].Group];
								group[group[matrix[i, j - 1].Group]] = group[matrix[i - 1, j].Group];
								group[matrix[i, j - 1].Group] = group[matrix[i - 1, j].Group];
							}
							else
							{
								matrix[i, j].Group = group[matrix[i, j - 1].Group];
								//group[ReturnRightGroup(matrix[i - 1, j].Group, group)] = group[matrix[i, j - 1].Group];
								group[group[matrix[i - 1, j].Group]] = group[matrix[i, j - 1].Group];
								group[matrix[i - 1, j].Group] = group[matrix[i, j - 1].Group];
							}
							continue;
						}
						//upper=1 left=0
						else
						{
							matrix[i, j].Group = group[matrix[i - 1, j].Group];
							continue;
						}
					}
					//upper=0
					else
					{
						//upper=0 left=0
						if (matrix[i, j - 1].Filled)
						{
							matrix[i, j].Group = group[matrix[i, j - 1].Group];
							continue;
						}
						//upper=0 left=0
						else
						{
							group[nextGroup] = nextGroup;
							matrix[i, j].Group = nextGroup;
							nextGroup++;
						}
					}
				}
			}
			//let's turn all groups in array of groups to right ones
			for (int j = 0; j < nextGroup; j++)
			{
				group[j] = ReturnRightGroup(j, group);
			}

			int[] topLineGroups = new int[matrix.GetLength(1)];
			int[] bottomLineGroups = new int[matrix.GetLength(1)];
			for (int j = 0; j < matrix.GetLength(1); j++)
			{
				topLineGroups[j] = matrix[0, j].Group == -1 ? -1 : group[matrix[0, j].Group];

				bottomLineGroups[j] = matrix[matrix.GetLength(0) - 1, j].Group == -1 ? -2 : group[matrix[matrix.GetLength(0) - 1, j].Group];
			}
			Array.Sort(bottomLineGroups);
			for (int i = 0; i < topLineGroups.Length; i++)
			{
				if (Array.BinarySearch(bottomLineGroups, topLineGroups[i]) >= 0)
				{
					isPercolation = true;
					break;
				}
			}


			if (turnAllGroupsToRight)
			{
				for (int i = 0; i < matrix.GetLength(0); i++)
				{
					for (int j = 0; j < matrix.GetLength(1); j++)
					{
						if (matrix[i, j].Group != -1)
						{
							matrix[i, j].Group = group[matrix[i, j].Group];
						}
					}
				}
			}
			return isPercolation;
		}//Checks if there is percolation
		private static int ReturnRightGroup(int group, int[] array)
		{
			if (array == null) return group;
			if (array[group] == group) return group;
			else return ReturnRightGroup(array[group], array);
		}
	}
}