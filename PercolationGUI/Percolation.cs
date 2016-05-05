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
	public partial class Percolation : Form
	{
		IniSettings ini = new IniSettings("config.ini");
		string pathToPoints = "points of graph.xml";
		string mainConfigSection = "Configuration";
		string experimentConfigSection = "Last Experiment";

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
		TimeSpan timePassed = TimeSpan.Zero;
		ulong finishedExperiments = 0;

		GraphPane pane;
		LineItem pointsCurve;

		static BackgroundWorker bw = new BackgroundWorker();
		public Percolation()
		{
			bw.WorkerSupportsCancellation = true;
			bw.DoWork += Experiment;
			bw.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;

			InitializeComponent();
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
			pointsCurve.Label.IsVisible = false;
			//Code for graph's visualization
		}//Form1

		private void LoadSettings()
		{
			log_richTextBox.Text += "Loading settings...\n";
			try//Settings
			{
				if (ini.KeyExists("probability", mainConfigSection))
					probability_numericUpDown.Value = decimal.Parse(ini.ReadINI(mainConfigSection, "probability"));
				else Default(false, false, true);
				if (ini.KeyExists("number_of_experiments", mainConfigSection))
					numberOfExperiments_numericUpDown.Value = decimal.Parse(ini.ReadINI(mainConfigSection, "number_of_experiments"));
				else Default(false, true);
				if (ini.KeyExists("height_of_matrix", mainConfigSection))
					heightOfMatrix_numericUpDown.Value = decimal.Parse(ini.ReadINI(mainConfigSection, "height_of_matrix"));
				else Default(false, false, false, false, true);
				if (ini.KeyExists("width_of_matrix", mainConfigSection))
					widthOfMatrix_numericUpDown.Value = decimal.Parse(ini.ReadINI(mainConfigSection, "width_of_matrix"));
				else Default(false, false, false, false, false, true);
				if (ini.KeyExists("probability_step", mainConfigSection))
					probabilityStep_numericUpDown.Value = decimal.Parse(ini.ReadINI(mainConfigSection, "probability_step"));
				else Default(false, false, false, true);
				if (ini.KeyExists("experimental_mode", mainConfigSection))
					experimentalMode_checkBox.Checked = bool.Parse(ini.ReadINI(mainConfigSection, "experimental_mode"));
				else Default(false, false, false, false, false, false, true);
				if (ini.KeyExists("finished", experimentConfigSection))
					finished_label.Text = ini.ReadINI(experimentConfigSection, "finished");
				else RefreshGUI(false, true);
				if (ini.KeyExists("fully_finished", experimentConfigSection))
					if (bool.Parse(ini.ReadINI(experimentConfigSection, "fully_finished")))
						resume_button.Enabled = false;
				RefreshData(true, true, true, true, true);
				RefreshGUI(true);
			}
			catch (Exception)
			{
				MessageBox.Show("config.ini is corrupted!\nAll values is set to default");
				log_richTextBox.Text += "config.ini is corrupted!\nAll values is set to default\n";
				Default(false, true, true, true, true, true, true);
				RefreshData(true, true, true, true, true);
				RefreshGUI(true);
			}
			finally
			{
				log_richTextBox.Text += "config.ini loaded correctly.\n";
			}

			try//Deserialization
			{
				if (File.Exists(pathToPoints))
					points = Serialization.Deserialize<PointPairList>(pathToPoints);
			}
			catch (Exception)
			{
				MessageBox.Show("point of graph.xml is corrupted!\nList of points is set to empty");
				log_richTextBox.Text += "point of graph.xml is corrupted!\nList of points is set to empty\n";
			}
			finally
			{
				log_richTextBox.Text += " point of graph.xml loaded corectly\n";
			}
		}//Loads settings

		private void LoadSettingsToResume()
		{
			try//SettingsToResume
			{
				if (ini.KeyExists("probability", experimentConfigSection))
					probability_numericUpDown.Value = decimal.Parse(ini.ReadINI(experimentConfigSection, "probability"));
				else throw new FileLoadException("There is no info to resume in config.ini");
				if (ini.KeyExists("number_of_experiments", experimentConfigSection))
					numberOfExperiments_numericUpDown.Value = decimal.Parse(ini.ReadINI(experimentConfigSection, "number_of_experiments"));
				else throw new FileLoadException("There is no info to resume in config.ini");
				if (ini.KeyExists("height_of_matrix", experimentConfigSection))
					heightOfMatrix_numericUpDown.Value = decimal.Parse(ini.ReadINI(experimentConfigSection, "height_of_matrix"));
				else throw new FileLoadException("There is no info to resume in config.ini");
				if (ini.KeyExists("width_of_matrix", experimentConfigSection))
					widthOfMatrix_numericUpDown.Value = decimal.Parse(ini.ReadINI(experimentConfigSection, "width_of_matrix"));
				else throw new FileLoadException("There is no info to resume in config.ini");
				if (ini.KeyExists("probability_step", experimentConfigSection))
					probabilityStep_numericUpDown.Value = decimal.Parse(ini.ReadINI(experimentConfigSection, "probability_step"));
				else throw new FileLoadException("There is no info to resume in config.ini");
				if (ini.KeyExists("experimental_mode", experimentConfigSection))
					experimentalMode_checkBox.Checked = bool.Parse(ini.ReadINI(experimentConfigSection, "experimental_mode"));
				else throw new FileLoadException("There is no info to resume in config.ini");
				if (ini.KeyExists("time_passed", experimentConfigSection))
					timePassed = TimeSpan.Parse(ini.ReadINI(experimentConfigSection, "time_passed"));
				else throw new FileLoadException("There is no info to resume in config.ini.");
				RefreshData(true, true, true, true, true);
				RefreshGUI(true);
			}
			catch (FileLoadException e)
			{
				Default(false, true, true, true, true, true, false, true);
				RefreshData(true, true, true, true, true);
				RefreshGUI(true);
				throw new Exception(e.Message);
			}
			catch (Exception)
			{
				Default(false, true, true, true, true, true, false, true);
				RefreshData(true, true, true, true, true);
				RefreshGUI(true);
				throw new Exception("config.ini is corrupted.");
			}

		}//Loads config to resume

		private void SerializePoints()
		{
			Serialization.Serialize(points, pathToPoints);
		}

		private void Default(bool points = false, bool numberOfExperiments = false,
			bool probability = false, bool probabilityStep = false,
			bool heightOfMatrix = false, bool widthOfMatrix = false,
			bool experimentalMode = false, bool timePassed = false)
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
			if (timePassed)
				this.timePassed = TimeSpan.Zero;
			RefreshData(true, true, true, true, true);
			RefreshGUI(true);
		}

		private void RefreshGUI(bool elements = false, bool procents = false)
		{
			if (elements)
			{
				if (experimentalMode_checkBox.Checked)
				{
					probability_numericUpDown.Hide();
					probability_label.Hide();
					probabilityStep_numericUpDown.Show();
					probabilityStep_label.Show();
					numberOfExperiments_label.Show();
					numberOfExperiments_numericUpDown.Show();
					resume_button.Show();
					pause_button.Show();
					heightOfMatrix_numericUpDown.Maximum = 1000;
					widthOfMatrix_numericUpDown.Maximum = 1000;
					finished_label.Show();
				}
				else
				{
					probability_numericUpDown.Show();
					probability_label.Show();
					probabilityStep_numericUpDown.Hide();
					probabilityStep_label.Hide();
					numberOfExperiments_label.Hide();
					numberOfExperiments_numericUpDown.Hide();
					resume_button.Hide();
					pause_button.Hide();
					heightOfMatrix_numericUpDown.Maximum = 100;
					widthOfMatrix_numericUpDown.Maximum = 100;
					finished_label.Hide();
				}
				graph_zedGraphControl.Invalidate();
			}
			if (procents)
			{
				finished_label.Text = String.Format("{0:0.##}% finished",
					(finishedExperiments * 100) / (Math.Truncate(1.0 / (double)decProbabilityStep + 1) * numberOfExperiments));
			}
		}//Refreshes GUI elements

		/// <summary>
		/// Enables or disables elements, which tunes probability step, size of matrix,
		/// experimental mode, number of experiments per probability
		/// </summary>
		/// <param name="key">True to enable and false to disable.</param>
		private void ShowExperimentalElements(bool key)
		{
			probabilityStep_numericUpDown.Enabled = key;
			heightOfMatrix_numericUpDown.Enabled = key;
			widthOfMatrix_numericUpDown.Enabled = key;
			numberOfExperiments_numericUpDown.Enabled = key;
			experimentalMode_checkBox.Enabled = key;
		}

		private void RefreshData(bool experiment = false, bool probability = false,
			bool size = false, bool probabilityStep = false,
			bool numberOfExperiments = false)
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
			RefreshGUI(true);
			ini.Write(mainConfigSection, "experimental_mode", experimentalMode.ToString());
		}//Changing experimental/single-matrix mode in GUI

		private void heightOfMatrix_numericUpDown_ValueChanged(object sender, EventArgs e)
		{
			RefreshData(false, false, true);
			ini.Write(mainConfigSection, "height_of_matrix", heightOfMatrix.ToString());
		}//Changing height in GUI

		private void widthOfMatrix_numericUpDown_ValueChanged(object sender, EventArgs e)
		{
			RefreshData(false, false, true);
			ini.Write(mainConfigSection, "width_of_matrix", widthOfMatrix.ToString());
		}//Changing width of matrix in GUI

		private void start_button_Click(object sender, EventArgs e)
		{
			if (experimentalMode)
			{
				start_button.Enabled = false;
				pause_button.Enabled = true;
				resume_button.Enabled = false;
				ShowExperimentalElements(false);
				Default(true);
				decProbabilityMin = 0;
				finishedExperiments = 0;
				timePassed = TimeSpan.Zero;
				timer.Enabled = true;
				log_richTextBox.Text += "New experiment started...\n";
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
			ini.Write(mainConfigSection, "probability", probability.ToString());
		}//Changing probability in GUI

		private void probabilityStep_numericUpDown_ValueChanged(object sender, EventArgs e)
		{
			RefreshData(false, false, false, true);
			ini.Write(mainConfigSection, "probability_step", decProbabilityStep.ToString());
		}//Changing probability step in GUI

		private void log_richTextBox_TextChanged(object sender, EventArgs e)
		{
			log_richTextBox.SelectionStart = log_richTextBox.TextLength;
			log_richTextBox.ScrollToCaret();
		}

		private void numberOfExperiments_numericUpDown_ValueChanged(object sender, EventArgs e)
		{
			RefreshData(false, false, false, false, true);
			ini.Write(mainConfigSection, "number_of_experiments", numberOfExperiments.ToString());
		}//Changing number of experiments per probability in GUI

		private void Experiment(object sender, DoWorkEventArgs e)
		{
			DateTime startTime = DateTime.Now - timePassed;
			string str;
			for (probability = decProbabilityMin; probability <= decProbabilityMax; probability += decProbabilityStep)
			{
				uint percolationProbability = 0;
				int count = 0;
				for (int i = 0; i < numberOfExperiments; i++)
				{
					PercolationCell[,] matrix = Methods.CreatePercolationMatrix(heightOfMatrix, widthOfMatrix, (double)probability);
					bool isPercolation = Methods.CheckPercolation(matrix);
					if (isPercolation) percolationProbability++;
					if (isPercolation) count++;
					timePassed = DateTime.Now - startTime;
					finishedExperiments += 1;
					if (bw.CancellationPending)
					{
						return;
					}
				}
				str = string.Format("Probability: {0}, Trues: {1}, Percolation probability: {2}\n", probability, count, (double)percolationProbability / numberOfExperiments);
				if (log_richTextBox.InvokeRequired) log_richTextBox.Invoke(
					new Action<string>((s) =>
				   {
					   log_richTextBox.Text += str;
				   }),
					str);
				AddPointToGraph((double)probability, (double)percolationProbability / numberOfExperiments);
				try
				{
					SerializePoints();
				}
				catch (Exception)
				{
					MessageBox.Show("Something is wrong with point of graph.xml\nExperiment will be paused");
					if (log_richTextBox.InvokeRequired)
					{
						log_richTextBox.Invoke(
							new Action(() => { pause_button.PerformClick(); })
							);
					}
					else
						pause_button.PerformClick();
				}
			}
			str = string.Format("Finished!\nTime passed: {0}\n", timePassed);
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
			ShowExperimentalElements(true);
			timer.Enabled = false;
			RefreshGUI(false, true);
			if (finishedExperiments / (Math.Truncate(1.0 / (double)decProbabilityStep + 1) * numberOfExperiments) == 1)
			{
				ini.Write(experimentConfigSection, "fully_finished", true.ToString());
			}
			ini.Write(experimentConfigSection, "finished", finished_label.Text);
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
			log_richTextBox.Text += string.Format("Paused.\tTime passed: {0}\n", timePassed);
			timer.Enabled = false;
			resume_button.Enabled = true;
			pause_button.Enabled = false;
			ini.Write(experimentConfigSection, "number_of_experiments", numberOfExperiments.ToString());
			ini.Write(experimentConfigSection, "probability_step", decProbabilityStep.ToString());
			ini.Write(experimentConfigSection, "probability", probability.ToString());
			ini.Write(experimentConfigSection, "height_of_matrix", heightOfMatrix.ToString());
			ini.Write(experimentConfigSection, "width_of_matrix", widthOfMatrix.ToString());
			ini.Write(experimentConfigSection, "experimental_mode", experimentalMode.ToString());
			ini.Write(experimentConfigSection, "time_passed", timePassed.ToString());
			ini.Write(experimentConfigSection, "finished", finished_label.Text);
			ini.Write(experimentConfigSection, "fully_finished", false.ToString());
			RefreshGUI(false, true);
			RefreshData(false, true);
		}

		private void resume_button_Click(object sender, EventArgs e)
		{
			try
			{
				LoadSettingsToResume();
			}
			catch (Exception exception)
			{
				MessageBox.Show(String.Format("{0}\nResuming is unavailable.\nAll settings is set to default.", exception.Message));
				log_richTextBox.Text += String.Format("{0}\nResuming is unavailable.\nAll settings is set to default.", exception.Message);
				return;
			}
			resume_button.Enabled = false;
			start_button.Enabled = false;
			ShowExperimentalElements(false);
			pause_button.Enabled = true;
			decProbabilityMin = probability;
			finishedExperiments = (ulong)(probability / decProbabilityStep * numberOfExperiments);
			RefreshGUI(false, true);
			timer.Enabled = true;
			log_richTextBox.Text += "Resumed.\n";
			bw.RunWorkerAsync();
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			RefreshGUI(false, true);
		}

		private void Percolation_FormClosing(object sender, FormClosingEventArgs e)
		{
			pause_button.PerformClick();
		}
	}
}
