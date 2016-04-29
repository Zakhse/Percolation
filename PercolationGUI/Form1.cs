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


namespace PercolationGUI
{
	public partial class Form1 : Form
	{
		IniSettings ini = new IniSettings("config.ini");
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
		public Form1()
		{
			InitializeComponent();
			LoadSettings();
			bw.DoWork += Experiment;
			RefreshData(true, true, true, true, true);
			RefreshGUI();
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
			pointsCurve.Label.IsVisible = false;
			//Code for graph's visualization
		}//Form1

		private void LoadSettings()
		{
			Default();
			try
			{
				if (ini.KeyExists("probability", "Configuration"))
				{
					probability_numericUpDown.Value = decimal.Parse(ini.ReadINI("Configuration", "probability"));
				}
				if (ini.KeyExists("number_of_experiments", "Configuration"))
				{
					numberOfExperiments_numericUpDown.Value = decimal.Parse(ini.ReadINI("Configuration", "number_of_experiments"));
				}
				if (ini.KeyExists("height_of_matrix", "Configuration"))
				{
					heightOfMatrix_numericUpDown.Value = decimal.Parse(ini.ReadINI("Configuration", "height_of_matrix"));
				}
				if (ini.KeyExists("width_of_matrix", "Configuration"))
				{
					widthOfMatrix_numericUpDown.Value = decimal.Parse(ini.ReadINI("Configuration", "width_of_matrix"));
				}
				if (ini.KeyExists("probability_step", "Configuration"))
				{
					probabilityStep_numericUpDown.Value = decimal.Parse(ini.ReadINI("Configuration", "probability_step"));
				}
				if (ini.KeyExists("experimental_mode", "Configuration"))
				{
					experimantalMode_checkBox.Checked = bool.Parse(ini.ReadINI("Configuration", "experimental_mode"));
				}
				RefreshData(true, true, true, true, true);
				RefreshGUI();
			}
			catch (Exception)
			{
				MessageBox.Show("config.ini is corrupted!\nAll values is set to default");
				Default(false, true);
			}
		}//Sets all values to default

		private void Default(bool points = false, bool values = false)
		{
			if (points)
			{
				this.points.Clear();
				graph_zedGraphControl.Invalidate();
			}
			if (values)
			{
				numberOfExperiments_numericUpDown.Value = 100;
				probability_numericUpDown.Value = 0.001m;
				probabilityStep_numericUpDown.Value = 0.001m;
				heightOfMatrix_numericUpDown.Value = 10m;
				widthOfMatrix_numericUpDown.Value = 10m;
				experimantalMode_checkBox.Checked = false;
				RefreshData(true, true, true, true, true);
				RefreshGUI();
			}
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
			ini.Write("Configuration", "experimental_mode", experimantalMode.ToString());
		}//Changing experimental/single-matrix mode in GUI

		private void heightOfMatrix_numericUpDown_ValueChanged(object sender, EventArgs e)
		{
			RefreshData(false, false, true);
			ini.Write("Configuration", "height_of_matrix", heightOfMatrix.ToString());
		}//Changing height in GUI

		private void widthOfMatrix_numericUpDown_ValueChanged(object sender, EventArgs e)
		{
			RefreshData(false, false, true);
			ini.Write("Configuration", "width_of_matrix", widthOfMatrix.ToString());
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
			ini.Write("Configuration", "probability_step", decProbabilityStep.ToString());
		}//Changing probability step in GUI

		private void log_richTextBox_TextChanged(object sender, EventArgs e)
		{
			log_richTextBox.SelectionStart = log_richTextBox.TextLength;
			log_richTextBox.ScrollToCaret();
		}

		private void numberOfExperiments_numericUpDown_ValueChanged(object sender, EventArgs e)
		{
			RefreshData(false, false, false, false, true);
			ini.Write("Configuration", "number_of_experiments", numberOfExperiments.ToString());
		}//Changing number of experiments per probability in GUI

		private void Experiment(object sender, DoWorkEventArgs e)
		{
			Default(true);
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

		private void graph_zedGraphControl_ZoomEvent(ZedGraphControl sender, ZoomState oldState, ZoomState newState)
		{
			if (pane.XAxis.Scale.Min <= 0.0)
			{
				pane.XAxis.Scale.Min = -0.0;
			}

			if (pane.XAxis.Scale.Max >= 1.0)
			{
				pane.XAxis.Scale.Max = 1.0;
			}

			if (pane.YAxis.Scale.Min <= 0.0)
			{
				pane.YAxis.Scale.Min = 0.0;
			}

			if (pane.YAxis.Scale.Max >= 1.0)
			{
				pane.YAxis.Scale.Max = 1.0;
			}
		}
	}
}
