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
using ZedGraph;
using Ini_Settings;


namespace PercolationGUI
{
	public partial class Form1 : Form
	{
		string temp;
		bool experimantalMode;
		uint heightOfMatrix;
		uint widthOfMatrix;
		decimal probability;

		//for experimental mode
		decimal decProbabilityMin = 0;
		decimal decProbabilityMax = 1;
		decimal decProbabilityStep;
		int numberOfExperiments;//per each probability
		PointPairList points = new PointPairList();

		GraphPane pane;
		LineItem pointsCurve;

		static BackgroundWorker bw = new BackgroundWorker();
		IniFile ini = new IniFile("config.ini");
		public Form1()
		{
			InitializeComponent();
			bw.DoWork += Experiment;
			RefreshData(true, true, true, true, true);
			RefreshGUI();
			LoadSettings();

			pane = graph_zedGraphControl.GraphPane;
			pane.Title.Text = "Graph";
			pane.XAxis.Scale.Min = 0.0;
			pane.XAxis.Scale.Max = 1.0;
			pane.YAxis.Scale.Min = 0.0;
			pane.YAxis.Scale.Max = 1.0;
			graph_zedGraphControl.AxisChange();
			pane.XAxis.Title.Text = "Probability of filling the lattice";
			pane.YAxis.Title.Text = "Probability of the percolation";
			pointsCurve = pane.AddCurve("Scatter", points, Color.Blue, SymbolType.Circle);
			//pointsCurve.Line.IsVisible = false;
			pointsCurve.Symbol.Fill.Color = Color.Blue;
			pointsCurve.Symbol.Fill.Type = FillType.Solid;
			pointsCurve.Symbol.Size = 5;
			//Code for graph's visualization
		}//Form1

		private void LoadSettings()
		{
			if (ini.KeyExists("probability", "Configuration"))
			{
				temp = ini.ReadINI("probability", "");
				probability_numericUpDown.Value = decimal.Parse(ini.ReadINI("probability", ""));
			}
			else
				probability_numericUpDown.Value = 0.001m;

			if (ini.KeyExists("height_of_matrix", ""))
				heightOfMatrix_numericUpDown.Value = uint.Parse(ini.ReadINI("height_of_matrix", ""));
			else
				heightOfMatrix_numericUpDown.Value = 10m;

			if (ini.KeyExists("width_of_matrix", ""))
				widthOfMatrix_numericUpDown.Value = decimal.Parse(ini.ReadINI("width_of_matrix", ""));
			else
				widthOfMatrix_numericUpDown.Value=10m;

			if (ini.KeyExists("probabiliy_step", ""))
				probabilityStep_numericUpDown.Value = decimal.Parse(ini.ReadINI("probability_step", ""));
			else
				probabilityStep_numericUpDown.Value = 0.001m;

			if (ini.KeyExists("number_of_experiments", ""))
				numberOfExperiments_numericUpDown.Value = decimal.Parse(ini.ReadINI("number_of_experiments", ""));
			else
				numberOfExperiments_numericUpDown.Value = 100m;

			if (ini.KeyExists("experimental_mode", ""))
				experimantalMode_checkBox.Checked = bool.Parse(ini.ReadINI("experimental_mode", ""));
			else
				experimantalMode_checkBox.Checked = false;

			RefreshData(true, true, true, true, true);
		}

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
		}//Refreshes GUI elements

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

		/// <summary>
		/// Adds new point to the graph
		/// </summary>
		/// <param name="x">X of the point</param>
		/// <param name="y">Y of the point</param>
		private void AddPointToGraph(double x, double y)
		{
			points.Add(x, y);
			graph_zedGraphControl.Invalidate();
		}

		private void experimantalMode_checkBox_CheckedChanged(object sender, EventArgs e)
		{
			RefreshData(true);
			RefreshGUI();
			ini.Write("", "experimental_mode", experimantalMode.ToString());
		}//Changing experimental/single-matrix mode in GUI

		private void heightOfMatrix_numericUpDown_ValueChanged(object sender, EventArgs e)
		{
			RefreshData(false, false, true);
			ini.Write("", "height_of_matrix", heightOfMatrix.ToString());
		}//Changing height in GUI

		private void widthOfMatrix_numericUpDown_ValueChanged(object sender, EventArgs e)
		{
			RefreshData(false, false, true);
			ini.Write("", "width_of_matrix", widthOfMatrix.ToString());
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
			ini.Write("Configuration", "probability", probability.ToString());
		}//Changing probability in GUI

		private void probabilityStep_numericUpDown_ValueChanged(object sender, EventArgs e)
		{
			RefreshData(false, false, false, true);
			ini.Write("", "probability_step", decProbabilityStep.ToString());
		}//Changing probability step in GUI

		private void log_richTextBox_TextChanged(object sender, EventArgs e)
		{
			log_richTextBox.SelectionStart = log_richTextBox.TextLength;
			log_richTextBox.ScrollToCaret();
		}

		private void numberOfExperiments_numericUpDown_ValueChanged(object sender, EventArgs e)
		{
			RefreshData(false, false, false, false, true);
			ini.Write("", "number_of_experiments",numberOfExperiments.ToString());
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
				AddPointToGraph((double)probability, (double)percolationProbability);
			}
		}//Method for experimental mode to work in background
	}
}
