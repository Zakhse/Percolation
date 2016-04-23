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
			RefreshData(true, true, true);
		}//Form1

		private void RefreshGUI()
		{

		}
		private void RefreshData(bool experiment = false, bool probability = false, bool size = false)
		{
			if (experiment)
			{
				experimantalMode = experimantalMode_checkBox.Checked;
			}
			if (probability)
			{
				this.probability = probability_trackBar.Value / 100.0;
				probability_label.Text = String.Format("Probability = {0:0.##}", this.probability);
			}
			if (size)
			{
				heightOfMatrix = (uint)heightOfMatrix_numericUpDown.Value;
				widthOfMatrix = (uint)widthOfMatrix_numericUpDown.Value;
			}
		}


		private void experimantalMode_checkBox_CheckedChanged(object sender, EventArgs e)
		{
			RefreshData(true);
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


		private void start_button_Click(object sender, EventArgs e)
		{
			if (experimantalMode)
			{

			}
			else
			{
				DateTime startTime = DateTime.Now;
				PercolationCell[,] matrix = Methods.CreatePercolationMatrix(heightOfMatrix, widthOfMatrix, probability);
				bool isPercolation = Methods.CheckPercolation(matrix, true);
				log_richTextBox.Text += Methods.StringPercolationMatrix(matrix);
				log_richTextBox.Text += String.Format("There is percolation: {0}", isPercolation);
				log_richTextBox.Text += String.Format("Total time : {0}", DateTime.Now - startTime);
				log_richTextBox.SelectionStart = log_richTextBox.TextLength;
				log_richTextBox.ScrollToCaret();

			}
		}
	}
}
