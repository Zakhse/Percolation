using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PercolationLIB;

namespace PercolationGUI
{
	public partial class Form1 : Form
	{
		bool experimantalMode;
		uint heightOfMatrix;
		uint widthOfMatrix;
		double probability;
		public Form1()
		{
			InitializeComponent();
			RefreshData();
		}//Form1

		private void experimantalMode_checkBox_CheckedChanged(object sender, EventArgs e)
		{
			RefreshData(true, false, false);
		}//Changing experimental/single-matrix mode in GUI

		private void heightOfMatrix_numericUpDown_ValueChanged(object sender, EventArgs e)
		{
			RefreshData(false, false, true);
		}//Changing height in GUI

		private void widthOfMatrix_numericUpDown_ValueChanged(object sender, EventArgs e)
		{
			RefreshData(false, false, true);
		}//Changing width of matrix in GUI

		private void probability_trackBar_Scroll(object sender, EventArgs e)
		{
			RefreshData(false, true, false);
		}//Changing probability in GUI

		private void RefreshGUI()
		{

		}
		private void RefreshData(bool experiment = true, bool probability = true, bool size = true)
		{
			if (experiment)
			{
				experimantalMode = experimantalMode_checkBox.Checked;
			}
			if (probability)
			{
				this.probability = probability_trackBar.Value / 100.0;
				probability_label.Text = String.Format("Probability = {0:0.##}", probability);
			}
			if (size)
			{
				heightOfMatrix = (uint)heightOfMatrix_numericUpDown.Value;
				widthOfMatrix = (uint)widthOfMatrix_numericUpDown.Value;
			}
		}
	}
}
