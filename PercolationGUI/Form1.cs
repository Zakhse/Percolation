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
using System.IO;
using PercolationLIB;
using ZedGraph;


namespace PercolationGUI
{
	public partial class Form1 : Form
	{
		IniSettings ini = new IniSettings("config.ini");
		string pathToPoints = "points of graph.xml";
		bool experimentalMode;
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
			bw.WorkerSupportsCancellation = true;
			bw.DoWork += Experiment;
			bw.RunWorkerCompleted +=backgroundWorker_RunWorkerCompleted;

			InitializeComponent();
			LoadSettings();

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
			try//Settings
			{
				if (ini.KeyExists("probability", "Configuration"))
					probability_numericUpDown.Value = decimal.Parse(ini.ReadINI("Configuration", "probability"));
				else Default(false, false, true);
				if (ini.KeyExists("number_of_experiments", "Configuration"))
					numberOfExperiments_numericUpDown.Value = decimal.Parse(ini.ReadINI("Configuration", "number_of_experiments"));
				else Default(false, true);
				if (ini.KeyExists("height_of_matrix", "Configuration"))
					heightOfMatrix_numericUpDown.Value = decimal.Parse(ini.ReadINI("Configuration", "height_of_matrix"));
				else Default(false, false, false, false, true);
				if (ini.KeyExists("width_of_matrix", "Configuration"))
					widthOfMatrix_numericUpDown.Value = decimal.Parse(ini.ReadINI("Configuration", "width_of_matrix"));
				else Default(false, false, false, false, false, true);
				if (ini.KeyExists("probability_step", "Configuration"))
					probabilityStep_numericUpDown.Value = decimal.Parse(ini.ReadINI("Configuration", "probability_step"));
				else Default(false, false, false, true);
				if (ini.KeyExists("experimental_mode", "Configuration"))
					experimentalMode_checkBox.Checked = bool.Parse(ini.ReadINI("Configuration", "experimental_mode"));
				else Default(false, false, false, false, false, false, true);
				RefreshData(true, true, true, true, true);
				RefreshGUI();
			}
			catch (Exception)
			{
				MessageBox.Show("config.ini is corrupted!\nAll values is set to default");
				Default(false, true,true,true,true,true,true);
				RefreshData(true, true, true, true, true);
				RefreshGUI();
			}

			try//Deserialization
			{
				if (File.Exists(pathToPoints))
					points = Serialization.Deserialize<PointPairList>(pathToPoints);
			}
			catch (Exception)
			{
				MessageBox.Show("point of graph.xml is corrupted!\nList of points is set to empty");
			}
		}//Loads settings

		private void LoadSettingsToResume()
		{
			try//Settings
			{
				if (ini.KeyExists("probability", "Last Experiment"))
					probability_numericUpDown.Value = decimal.Parse(ini.ReadINI("Last Experiment", "probability"));
				else Default(false, false, true);
				if (ini.KeyExists("number_of_experiments", "Last Experiment"))
					numberOfExperiments_numericUpDown.Value = decimal.Parse(ini.ReadINI("Last Experiment", "number_of_experiments"));
				else Default(false, true);
				if (ini.KeyExists("height_of_matrix", "Last Experiment"))
					heightOfMatrix_numericUpDown.Value = decimal.Parse(ini.ReadINI("Last Experiment", "height_of_matrix"));
				else Default(false, false, false, false, true);
				if (ini.KeyExists("width_of_matrix", "Last Experiment"))
					widthOfMatrix_numericUpDown.Value = decimal.Parse(ini.ReadINI("Last Experiment", "width_of_matrix"));
				else Default(false, false, false, false, false, true);
				if (ini.KeyExists("probability_step", "Last Experiment"))
					probabilityStep_numericUpDown.Value = decimal.Parse(ini.ReadINI("Last Experiment", "probability_step"));
				else Default(false, false, false, true);
				if (ini.KeyExists("experimental_mode", "Last Experiment"))
					experimentalMode_checkBox.Checked = bool.Parse(ini.ReadINI("Last Experiment", "experimental_mode"));
				else Default(false, false, false, false, false, false, true);
				RefreshData(true, true, true, true, true);
				RefreshGUI();
			}
			catch (Exception)
			{
				MessageBox.Show("config.ini is corrupted!\nAll values is set to default");
				Default(false, true, true, true, true, true, true);
				RefreshData(true, true, true, true, true);
				RefreshGUI();
			}
		}//Loads config to resume

		private void SerializePoints()
		{
			Serialization.Serialize(points, pathToPoints);
		}

		private void Default(bool points = false, bool numberOfExperiments = false,
			bool probability = false, bool probabilityStep = false,
			bool heightOfMatrix = false, bool widthOfMatrix = false,
			bool experimentalMode = false)
		{
			if (points)
			{
				this.points.Clear();
				graph_zedGraphControl.Invalidate();
				File.Delete(pathToPoints);
				return;
			}
			if (numberOfExperiments)
				numberOfExperiments_numericUpDown.Value = 100;
			if (probability)
				probability_numericUpDown.Value = 0.001m;
			if (probabilityStep)
				probabilityStep_numericUpDown.Value = 0.001m;
			if (heightOfMatrix)
				heightOfMatrix_numericUpDown.Value = 10m;
			if (widthOfMatrix)
				widthOfMatrix_numericUpDown.Value = 10m;
			if (experimentalMode)
				experimentalMode_checkBox.Checked = false;
			RefreshData(true, true, true, true, true);
			RefreshGUI();
		}

		private void RefreshGUI()
		{
			if (experimentalMode_checkBox.Checked)
			{
				probability_numericUpDown.Hide();
				probability_label.Hide();
				probabilityStep_numericUpDown.Show();
				probabilityStep_label.Show();
				numberOfExperiments_label.Show();
				numberOfExperiments_numericUpDown.Show();
				resume_button.Enabled = true;
				heightOfMatrix_numericUpDown.Maximum = 1000;
				widthOfMatrix_numericUpDown.Maximum = 1000;
			}
			else
			{
				probability_numericUpDown.Show();
				probability_label.Show();
				probabilityStep_numericUpDown.Hide();
				probabilityStep_label.Hide();
				numberOfExperiments_label.Hide();
				numberOfExperiments_numericUpDown.Hide();
				resume_button.Enabled = false;
				heightOfMatrix_numericUpDown.Maximum = 100;
				widthOfMatrix_numericUpDown.Maximum = 100;
			}
			graph_zedGraphControl.Invalidate();
		}//Refreshes GUI elements

		private void RefreshData(bool experiment = false, bool probability = false, bool size = false, bool probabilityStep = false, bool numberOfExperiments = false)
		{
			if (experiment)
			{
				experimentalMode = experimentalMode_checkBox.Checked;
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
			ini.Write("Configuration", "experimental_mode", experimentalMode.ToString());
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
			if (experimentalMode)
			{
				start_button.Enabled = false;
				pause_button.Enabled = true;
				resume_button.Enabled = false;
				experimentalMode_checkBox.Enabled = false;
				Default(true);
				decProbabilityMin = 0;
				bw.RunWorkerAsync();
			}
			else
			{
				DateTime startTime = DateTime.Now;
				PercolationCell[,] matrix = Methods.CreatePercolationMatrix(heightOfMatrix, widthOfMatrix, (double)probability);
				bool isPercolation = Methods.CheckPercolation(matrix, true);
				log_richTextBox.Text += Methods.StringPercolationMatrix(matrix);
				log_richTextBox.Text += String.Format("There is percolation: {0}\n", isPercolation);
				log_richTextBox.Text += String.Format("Total time : {0}\n", DateTime.Now - startTime);
				start_button.Enabled = true;
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
			DateTime startTime = DateTime.Now;
			string str;
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
					if (bw.CancellationPending)
					{
						return;
					}
				}
				str = string.Format("Probability: {0}, Trues: {1}, Percolation probability: {2}\n", probability, count, percolationProbability);
				if (log_richTextBox.InvokeRequired) log_richTextBox.Invoke(
					new Action<string>((s) =>
				   {
					   log_richTextBox.Text += str;
				   }),
					str);
				AddPointToGraph((double)probability, (double)percolationProbability);
				SerializePoints();
			}
			str = string.Format("Time passed: {0}", DateTime.Now - startTime);
			if (log_richTextBox.InvokeRequired) log_richTextBox.Invoke(
				new Action<string>((s) =>
				{
					log_richTextBox.Text += str;
				}),
				str);
		}//Method for experimental mode to work in background

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			start_button.Enabled = true;
			pause_button.Enabled = false;
			experimentalMode_checkBox.Enabled = true;
		}

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

		private void pause_button_Click(object sender, EventArgs e)
		{
			bw.CancelAsync();
			resume_button.Enabled = true;
			pause_button.Enabled = false;
			experimentalMode_checkBox.Enabled = true;
			ini.Write("Last Experiment", "number_of_experiments", numberOfExperiments.ToString());
			ini.Write("Last Experiment", "probability_step", decProbabilityStep.ToString());
			ini.Write("Last Experiment", "probability", probability.ToString());
			ini.Write("Last Experiment", "height_of_matrix", heightOfMatrix.ToString());
			ini.Write("Last Experiment", "width_of_matrix", widthOfMatrix.ToString());
			ini.Write("Last Experiment", "experimental_mode", experimentalMode.ToString());
		}

		private void resume_button_Click(object sender, EventArgs e)
		{
			LoadSettingsToResume();
			resume_button.Enabled = false;
			decProbabilityMin = probability;
			pause_button.Enabled = true;
			bw.RunWorkerAsync();
		}
	}
}
