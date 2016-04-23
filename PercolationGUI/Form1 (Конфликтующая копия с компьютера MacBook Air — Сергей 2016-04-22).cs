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
		bool experimentalMode;
		uint heightOfMatrix;
		uint widthOfMatrix;
		double probability;
		public Form1()
		{
			InitializeComponent();
			RefreshData();
			RefreshGUI();
			//uint sizeOfMatrix = 500;
			//int stepProbability = 5; //from 1 to 100
			//int numberOfExperiments = 100;
			//double probability;
			//int intProbability;
			//bool isPercolation;

			/*if (experimentalMode) //experimatal mode
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

						if ((i - 1) % 1 == 0)
						{
							Console.WriteLine(i);
						}
					}
					Console.Write("Probability = {0} : ", probability);
					Console.WriteLine("{0:0.##} sec.", (DateTime.Now - time).TotalSeconds);
				}
			}*/
			//else//single-matrix mode
			//{


		}//Form1

		private void experimantalMode_checkBox_CheckedChanged(object sender, EventArgs e)
		{
			RefreshData();
			RefreshGUI();
		}//Changing experimental/single-matrix mode in GUI

		private void heightOfMatrix_numericUpDown_ValueChanged(object sender, EventArgs e)
		{
			RefreshData();
		}//Changing height of matrix in GUI

		private void widthOfMatrix_numericUpDown_ValueChanged(object sender, EventArgs e)
		{
			RefreshData();
		}//Changing width of matrix in GUI

		private void probability_trackBar_Scroll(object sender, EventArgs e)
		{
			RefreshData();
		}//Changing probability in GUI

		private void RefreshGUI()
		{
			/*if (experimantalMode_checkBox.Checked)
			{
				probability_label.Show();
				probability_trackBar.Show();
				heightOfMatrix_label.Show();
				heightOfMatrix_numericUpDown.Show();
				widthOfMatrix_label.Show();
				widthOfMatrix_numericUpDown.Show();
			}
			else
			{
				probability_label.Hide();
				probability_trackBar.Hide();
				heightOfMatrix_label.Hide();
				heightOfMatrix_numericUpDown.Hide();
				widthOfMatrix_label.Hide();
				widthOfMatrix_numericUpDown.Hide();
			}*/
		}//RefreshGUI

		private void RefreshData()
		{
			probability = probability_trackBar.Value / 100.0;
			probability_label.Text = String.Format("Probability = {0:0.##}", probability);
			heightOfMatrix = (uint)heightOfMatrix_numericUpDown.Value;
			widthOfMatrix = (uint)widthOfMatrix_numericUpDown.Value;
			experimentalMode = experimantalMode_checkBox.Checked;
		}//RefreshData

		private void start_button_Click(object sender, EventArgs e)
		{
			if (experimentalMode)
			{

			}
			else //single-matrix mode
			{
				DateTime startTime = DateTime.Now;
				PercolationCell[,] matrix = Methods.CreatePercolationMatrix(heightOfMatrix, widthOfMatrix, probability);
				bool isPercolation = Methods.CheckPercolation(matrix, true);
				richTextBox1.Text += Methods.StringPercolationMatrix(matrix);
				richTextBox1.Text += string.Format("\nThere is percolation: {0}\n", isPercolation);
				richTextBox1.Text += string.Format("Total time : {0}\n", DateTime.Now - startTime);
			}
		}//start_button_Click

		private void richTextBox1_TextChanged(object sender, EventArgs e)
		{
			richTextBox1.SelectionStart = richTextBox1.TextLength;
			richTextBox1.ScrollToCaret();
		}//scrolls richTextBox1 to the end
	}
}
