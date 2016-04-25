using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using PercolationLIB;


namespace PercolationGUI
{
	public partial class Form1 : Form
	{
		bool experimantalMode;
		uint heightOfMatrix;
		uint widthOfMatrix;
		decimal probability;

		//for experimental mode
		decimal decProbabilityMin = 0;
		decimal decProbabilityMax = 1;
		decimal decProbabilityStep;
		int numberOfExperiments;//per each probability
		List<Point> points = new List<Point>();

		static BackgroundWorker bw = new BackgroundWorker();
		public Form1()
		{
			InitializeComponent();
			bw.DoWork += Experiment;
			RefreshData(true, true, true, true, true);
			RefreshGUI();

		}//Form1

		private void RefreshGUI()
		{
			if (experimantalMode_checkBox.Checked)
			{
				probability_numericUpDown.Hide();
				probability_label.Hide();
				probabilityStep_numericUpDown.Show();
				probabilityStep_label.Show();
				numberOfExperiments_label.Show();
				numberOfExperiments_numericUpDown.Show();
			}
			else
			{
				probability_numericUpDown.Show();
				probability_label.Show();
				probabilityStep_numericUpDown.Hide();
				probabilityStep_label.Hide();
				numberOfExperiments_label.Hide();
				numberOfExperiments_numericUpDown.Hide();
			}
		}
		private void RefreshData(bool experiment = false, bool probability = false, bool size = false, bool probabilityStep = false, bool numberOfExperiments = false)
		{
			if (experiment)
			{
				experimantalMode = experimantalMode_checkBox.Checked;
			}
			if (probability)
			{
				this.probability = probability_numericUpDown.Value;
			}
			if (size)
			{
				heightOfMatrix = (uint)heightOfMatrix_numericUpDown.Value;
				widthOfMatrix = (uint)widthOfMatrix_numericUpDown.Value;
			}
			if (probabilityStep)
			{
				decProbabilityStep = probabilityStep_numericUpDown.Value;
			}
			if (numberOfExperiments)
			{
				this.numberOfExperiments = (int)numberOfExperiments_numericUpDown.Value;
			}
		}


		private void experimantalMode_checkBox_CheckedChanged(object sender, EventArgs e)
		{
			RefreshData(true);
			RefreshGUI();
		}//Changing experimental/single-matrix mode in GUI

		private void heightOfMatrix_numericUpDown_ValueChanged(object sender, EventArgs e)
		{
			RefreshData(false, false, true);
		}//Changing height in GUI

		private void widthOfMatrix_numericUpDown_ValueChanged(object sender, EventArgs e)
		{
			RefreshData(false, false, true);
		}//Changing width of matrix in GUI

		private void start_button_Click(object sender, EventArgs e)
		{
			if (experimantalMode)
			{
				bw.RunWorkerAsync();
			}
			else
			{
				DateTime startTime = DateTime.Now;
				PercolationCell[,] matrix = Methods.CreatePercolationMatrix(heightOfMatrix, widthOfMatrix, (double)probability);
				bool isPercolation = Methods.CheckPercolation(matrix, true);
				log_richTextBox.Text += Methods.StringPercolationMatrix(matrix);
				log_richTextBox.Text += String.Format("There is percolation: {0}", isPercolation);
				log_richTextBox.Text += String.Format("Total time : {0}", DateTime.Now - startTime);

			}
		}//Clicking start button

		private void probability_numericUpDown_ValueChanged(object sender, EventArgs e)
		{
			RefreshData(false, true, false);
		}//Changing probability in GUI

		private void probabilityStep_numericUpDown_ValueChanged(object sender, EventArgs e)
		{
			RefreshData(false, false, false, true);
		}//Changing probability step in GUI

		private void log_richTextBox_TextChanged(object sender, EventArgs e)
		{
			log_richTextBox.SelectionStart = log_richTextBox.TextLength;
			log_richTextBox.ScrollToCaret();
		}

		private void numberOfExperiments_numericUpDown_ValueChanged(object sender, EventArgs e)
		{
			RefreshData(false, false, false, false, true);
		}//Changing number of experiments per probability in GUI
		private void Experiment(object sender, DoWorkEventArgs e)
		{
			for (probability = decProbabilityMin; probability <= decProbabilityMax; probability += decProbabilityStep)
			{
				decimal percolationProbability = 0;
				int count = 0;
				for (int i = 0; i < numberOfExperiments; i++)
				{
					PercolationCell[,] matrix = Methods.CreatePercolationMatrix(heightOfMatrix, widthOfMatrix, (double)probability);
					bool isPercolation = Methods.CheckPercolation(matrix);
					if (isPercolation) percolationProbability += (1m / numberOfExperiments);
					if (isPercolation) count++;
				}
				string str = string.Format("Probability: {0}, Trues: {1}, Percolation probability: {2}\n", probability, count, percolationProbability);
				if (log_richTextBox.InvokeRequired) log_richTextBox.Invoke(
					new Action<string>((s) =>
				   {
					   log_richTextBox.Text += str;
				   }),
					str);
			}
		}
	}

}
