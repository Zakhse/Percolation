using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PercolationLIB
{
	public class PercolationCell
	{
		public bool Filled { get; }
		public double rndDouble { get; }
		public PercolationCell(bool value, double probability)
		{
			group = -1;
			Filled = value;
			rndDouble = probability;
		}
		private int group;
		public int Group
		{
			get
			{
				return group;
			}
			set
			{
				if (Filled)
				{
					group = value;
				}
			}
		}
	}
}
