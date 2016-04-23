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
			probability = probability_trackBar.Value / 100.0;
			probability_label.Text = String.Format("Probability = {0:0.##}",probability);
			experimantalMode = experimantalMode_checkBox.Checked;
			heightOfMatrix= (uint)heightOfMatrix_numericUpDown.Value;
			widthOfMatrix=(uint)widthOfMatrix_numericUpDown.Value;
		}//Form1

		private void experimantalMode_checkBox_CheckedChanged(object sender, EventArgs e)
		{
			experimantalMode = experimantalMode_checkBox.Checked;
			if (!experimantalMode_checkBox.Checked)
			{
				probability_label.Hide();
				probability_trackBar.Hide();
				heightOfMatrix_label.Hide();
				heightOfMatrix_numericUpDown.Hide();
				widthOfMatrix_label.Hide();
				widthOfMatrix_numericUpDown.Hide();
			}
			else
			{
				probability_label.Show();
				probability_trackBar.Show();
				heightOfMatrix_label.Show();
				heightOfMatrix_numericUpDown.Show();
				widthOfMatrix_label.Show();
				widthOfMatrix_numericUpDown.Show();
			}
		}//Changing experimental/single-matrix mode in GUI

		private void heightOfMatrix_numericUpDown_ValueChanged(object sender, EventArgs e)
		{
			heightOfMatrix = (uint)heightOfMatrix_numericUpDown.Value;
		}//Changing height in GUI

		private void widthOfMatrix_numericUpDown_ValueChanged(object sender, EventArgs e)
		{
			widthOfMatrix = (uint)widthOfMatrix_numericUpDown.Value;
		}//Changing width of matrix in GUI

		private void probability_trackBar_Scroll(object sender, EventArgs e)
		{
			probability = probability_trackBar.Value / 100.0;
			probability_label.Text = String.Format("Probability = {0:0.##}", probability);
		}//Changing probability in GUI
	}
}
