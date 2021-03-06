﻿namespace PercolationGUI
{
	partial class Percolation
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Percolation));
			this.options_groupBox = new System.Windows.Forms.GroupBox();
			this.finished_label = new System.Windows.Forms.Label();
			this.resume_button = new System.Windows.Forms.Button();
			this.pause_button = new System.Windows.Forms.Button();
			this.numberOfExperiments_label = new System.Windows.Forms.Label();
			this.numberOfExperiments_numericUpDown = new System.Windows.Forms.NumericUpDown();
			this.probabilityStep_label = new System.Windows.Forms.Label();
			this.probability_numericUpDown = new System.Windows.Forms.NumericUpDown();
			this.probabilityStep_numericUpDown = new System.Windows.Forms.NumericUpDown();
			this.start_button = new System.Windows.Forms.Button();
			this.probability_label = new System.Windows.Forms.Label();
			this.widthOfMatrix_numericUpDown = new System.Windows.Forms.NumericUpDown();
			this.widthOfMatrix_label = new System.Windows.Forms.Label();
			this.heightOfMatrix_label = new System.Windows.Forms.Label();
			this.heightOfMatrix_numericUpDown = new System.Windows.Forms.NumericUpDown();
			this.experimentalMode_checkBox = new System.Windows.Forms.CheckBox();
			this.pages_tabControl = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.graph_zedGraphControl = new ZedGraph.ZedGraphControl();
			this.log_tabpage = new System.Windows.Forms.TabPage();
			this.log_richTextBox = new System.Windows.Forms.RichTextBox();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.options_groupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numberOfExperiments_numericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.probability_numericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.probabilityStep_numericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.widthOfMatrix_numericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.heightOfMatrix_numericUpDown)).BeginInit();
			this.pages_tabControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.log_tabpage.SuspendLayout();
			this.SuspendLayout();
			// 
			// options_groupBox
			// 
			this.options_groupBox.Controls.Add(this.finished_label);
			this.options_groupBox.Controls.Add(this.resume_button);
			this.options_groupBox.Controls.Add(this.pause_button);
			this.options_groupBox.Controls.Add(this.numberOfExperiments_label);
			this.options_groupBox.Controls.Add(this.numberOfExperiments_numericUpDown);
			this.options_groupBox.Controls.Add(this.probabilityStep_label);
			this.options_groupBox.Controls.Add(this.probability_numericUpDown);
			this.options_groupBox.Controls.Add(this.probabilityStep_numericUpDown);
			this.options_groupBox.Controls.Add(this.start_button);
			this.options_groupBox.Controls.Add(this.probability_label);
			this.options_groupBox.Controls.Add(this.widthOfMatrix_numericUpDown);
			this.options_groupBox.Controls.Add(this.widthOfMatrix_label);
			this.options_groupBox.Controls.Add(this.heightOfMatrix_label);
			this.options_groupBox.Controls.Add(this.heightOfMatrix_numericUpDown);
			this.options_groupBox.Controls.Add(this.experimentalMode_checkBox);
			this.options_groupBox.Location = new System.Drawing.Point(17, 12);
			this.options_groupBox.Name = "options_groupBox";
			this.options_groupBox.Size = new System.Drawing.Size(230, 361);
			this.options_groupBox.TabIndex = 0;
			this.options_groupBox.TabStop = false;
			this.options_groupBox.Text = "Options";
			// 
			// finished_label
			// 
			this.finished_label.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.finished_label.AutoSize = true;
			this.finished_label.Location = new System.Drawing.Point(57, 118);
			this.finished_label.Name = "finished_label";
			this.finished_label.Size = new System.Drawing.Size(54, 13);
			this.finished_label.TabIndex = 3;
			this.finished_label.Text = "% finished";
			this.finished_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// resume_button
			// 
			this.resume_button.Location = new System.Drawing.Point(20, 75);
			this.resume_button.Name = "resume_button";
			this.resume_button.Size = new System.Drawing.Size(128, 23);
			this.resume_button.TabIndex = 14;
			this.resume_button.Text = "Resume";
			this.resume_button.UseVisualStyleBackColor = true;
			this.resume_button.Click += new System.EventHandler(this.resume_button_Click);
			// 
			// pause_button
			// 
			this.pause_button.Enabled = false;
			this.pause_button.Location = new System.Drawing.Point(20, 46);
			this.pause_button.Name = "pause_button";
			this.pause_button.Size = new System.Drawing.Size(128, 23);
			this.pause_button.TabIndex = 13;
			this.pause_button.Text = "Pause";
			this.pause_button.UseVisualStyleBackColor = true;
			this.pause_button.Click += new System.EventHandler(this.pause_button_Click);
			// 
			// numberOfExperiments_label
			// 
			this.numberOfExperiments_label.AutoSize = true;
			this.numberOfExperiments_label.Location = new System.Drawing.Point(6, 279);
			this.numberOfExperiments_label.Name = "numberOfExperiments_label";
			this.numberOfExperiments_label.Size = new System.Drawing.Size(78, 26);
			this.numberOfExperiments_label.TabIndex = 12;
			this.numberOfExperiments_label.Text = "Number\nof experiments:";
			this.numberOfExperiments_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// numberOfExperiments_numericUpDown
			// 
			this.numberOfExperiments_numericUpDown.Location = new System.Drawing.Point(98, 285);
			this.numberOfExperiments_numericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numberOfExperiments_numericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numberOfExperiments_numericUpDown.Name = "numberOfExperiments_numericUpDown";
			this.numberOfExperiments_numericUpDown.Size = new System.Drawing.Size(120, 20);
			this.numberOfExperiments_numericUpDown.TabIndex = 11;
			this.numberOfExperiments_numericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numberOfExperiments_numericUpDown.ValueChanged += new System.EventHandler(this.numberOfExperiments_numericUpDown_ValueChanged);
			// 
			// probabilityStep_label
			// 
			this.probabilityStep_label.AutoSize = true;
			this.probabilityStep_label.Location = new System.Drawing.Point(6, 243);
			this.probabilityStep_label.Name = "probabilityStep_label";
			this.probabilityStep_label.Size = new System.Drawing.Size(81, 13);
			this.probabilityStep_label.TabIndex = 10;
			this.probabilityStep_label.Text = "Probability step:";
			// 
			// probability_numericUpDown
			// 
			this.probability_numericUpDown.DecimalPlaces = 3;
			this.probability_numericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
			this.probability_numericUpDown.Location = new System.Drawing.Point(98, 157);
			this.probability_numericUpDown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.probability_numericUpDown.Name = "probability_numericUpDown";
			this.probability_numericUpDown.Size = new System.Drawing.Size(120, 20);
			this.probability_numericUpDown.TabIndex = 9;
			this.probability_numericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.probability_numericUpDown.ValueChanged += new System.EventHandler(this.probability_numericUpDown_ValueChanged);
			// 
			// probabilityStep_numericUpDown
			// 
			this.probabilityStep_numericUpDown.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.probabilityStep_numericUpDown.DecimalPlaces = 3;
			this.probabilityStep_numericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
			this.probabilityStep_numericUpDown.Location = new System.Drawing.Point(98, 241);
			this.probabilityStep_numericUpDown.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            131072});
			this.probabilityStep_numericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
			this.probabilityStep_numericUpDown.Name = "probabilityStep_numericUpDown";
			this.probabilityStep_numericUpDown.Size = new System.Drawing.Size(120, 20);
			this.probabilityStep_numericUpDown.TabIndex = 8;
			this.probabilityStep_numericUpDown.Value = new decimal(new int[] {
            25,
            0,
            0,
            131072});
			this.probabilityStep_numericUpDown.ValueChanged += new System.EventHandler(this.probabilityStep_numericUpDown_ValueChanged);
			// 
			// start_button
			// 
			this.start_button.Location = new System.Drawing.Point(20, 19);
			this.start_button.Name = "start_button";
			this.start_button.Size = new System.Drawing.Size(128, 21);
			this.start_button.TabIndex = 7;
			this.start_button.Text = "New experiment!";
			this.start_button.UseVisualStyleBackColor = true;
			this.start_button.Click += new System.EventHandler(this.start_button_Click);
			// 
			// probability_label
			// 
			this.probability_label.AutoSize = true;
			this.probability_label.Location = new System.Drawing.Point(6, 159);
			this.probability_label.Name = "probability_label";
			this.probability_label.Size = new System.Drawing.Size(58, 13);
			this.probability_label.TabIndex = 5;
			this.probability_label.Text = "Probability:";
			// 
			// widthOfMatrix_numericUpDown
			// 
			this.widthOfMatrix_numericUpDown.Location = new System.Drawing.Point(112, 209);
			this.widthOfMatrix_numericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.widthOfMatrix_numericUpDown.MaximumSize = new System.Drawing.Size(50, 0);
			this.widthOfMatrix_numericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.widthOfMatrix_numericUpDown.MinimumSize = new System.Drawing.Size(50, 0);
			this.widthOfMatrix_numericUpDown.Name = "widthOfMatrix_numericUpDown";
			this.widthOfMatrix_numericUpDown.Size = new System.Drawing.Size(50, 20);
			this.widthOfMatrix_numericUpDown.TabIndex = 4;
			this.widthOfMatrix_numericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.widthOfMatrix_numericUpDown.ValueChanged += new System.EventHandler(this.widthOfMatrix_numericUpDown_ValueChanged);
			// 
			// widthOfMatrix_label
			// 
			this.widthOfMatrix_label.AutoSize = true;
			this.widthOfMatrix_label.Location = new System.Drawing.Point(95, 193);
			this.widthOfMatrix_label.Name = "widthOfMatrix_label";
			this.widthOfMatrix_label.Size = new System.Drawing.Size(80, 13);
			this.widthOfMatrix_label.TabIndex = 3;
			this.widthOfMatrix_label.Text = "Width of matrix:";
			// 
			// heightOfMatrix_label
			// 
			this.heightOfMatrix_label.AutoSize = true;
			this.heightOfMatrix_label.Location = new System.Drawing.Point(6, 193);
			this.heightOfMatrix_label.Name = "heightOfMatrix_label";
			this.heightOfMatrix_label.Size = new System.Drawing.Size(83, 13);
			this.heightOfMatrix_label.TabIndex = 2;
			this.heightOfMatrix_label.Text = "Height of matrix:";
			// 
			// heightOfMatrix_numericUpDown
			// 
			this.heightOfMatrix_numericUpDown.Location = new System.Drawing.Point(20, 209);
			this.heightOfMatrix_numericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.heightOfMatrix_numericUpDown.MaximumSize = new System.Drawing.Size(50, 0);
			this.heightOfMatrix_numericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.heightOfMatrix_numericUpDown.MinimumSize = new System.Drawing.Size(50, 0);
			this.heightOfMatrix_numericUpDown.Name = "heightOfMatrix_numericUpDown";
			this.heightOfMatrix_numericUpDown.Size = new System.Drawing.Size(50, 20);
			this.heightOfMatrix_numericUpDown.TabIndex = 1;
			this.heightOfMatrix_numericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.heightOfMatrix_numericUpDown.ValueChanged += new System.EventHandler(this.heightOfMatrix_numericUpDown_ValueChanged);
			// 
			// experimentalMode_checkBox
			// 
			this.experimentalMode_checkBox.AutoSize = true;
			this.experimentalMode_checkBox.Checked = true;
			this.experimentalMode_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.experimentalMode_checkBox.Location = new System.Drawing.Point(6, 321);
			this.experimentalMode_checkBox.Name = "experimentalMode_checkBox";
			this.experimentalMode_checkBox.Size = new System.Drawing.Size(116, 17);
			this.experimentalMode_checkBox.TabIndex = 0;
			this.experimentalMode_checkBox.Text = "Experimental Mode";
			this.experimentalMode_checkBox.UseVisualStyleBackColor = true;
			this.experimentalMode_checkBox.CheckedChanged += new System.EventHandler(this.experimantalMode_checkBox_CheckedChanged);
			// 
			// pages_tabControl
			// 
			this.pages_tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pages_tabControl.Controls.Add(this.tabPage1);
			this.pages_tabControl.Controls.Add(this.log_tabpage);
			this.pages_tabControl.Location = new System.Drawing.Point(253, 12);
			this.pages_tabControl.Name = "pages_tabControl";
			this.pages_tabControl.SelectedIndex = 0;
			this.pages_tabControl.Size = new System.Drawing.Size(409, 362);
			this.pages_tabControl.TabIndex = 1;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.graph_zedGraphControl);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(401, 336);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Graph";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// graph_zedGraphControl
			// 
			this.graph_zedGraphControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.graph_zedGraphControl.IsShowContextMenu = false;
			this.graph_zedGraphControl.Location = new System.Drawing.Point(3, 3);
			this.graph_zedGraphControl.Name = "graph_zedGraphControl";
			this.graph_zedGraphControl.ScrollGrace = 0D;
			this.graph_zedGraphControl.ScrollMaxX = 1D;
			this.graph_zedGraphControl.ScrollMaxY = 1D;
			this.graph_zedGraphControl.ScrollMaxY2 = 0D;
			this.graph_zedGraphControl.ScrollMinX = 0D;
			this.graph_zedGraphControl.ScrollMinY = 0D;
			this.graph_zedGraphControl.ScrollMinY2 = 0D;
			this.graph_zedGraphControl.Size = new System.Drawing.Size(395, 330);
			this.graph_zedGraphControl.TabIndex = 2;
			this.graph_zedGraphControl.ZoomEvent += new ZedGraph.ZedGraphControl.ZoomEventHandler(this.graph_zedGraphControl_ZoomEvent);
			// 
			// log_tabpage
			// 
			this.log_tabpage.Controls.Add(this.log_richTextBox);
			this.log_tabpage.Location = new System.Drawing.Point(4, 22);
			this.log_tabpage.Name = "log_tabpage";
			this.log_tabpage.Padding = new System.Windows.Forms.Padding(3);
			this.log_tabpage.Size = new System.Drawing.Size(404, 335);
			this.log_tabpage.TabIndex = 1;
			this.log_tabpage.Text = "Log";
			this.log_tabpage.UseVisualStyleBackColor = true;
			// 
			// log_richTextBox
			// 
			this.log_richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.log_richTextBox.Location = new System.Drawing.Point(6, 6);
			this.log_richTextBox.Name = "log_richTextBox";
			this.log_richTextBox.ReadOnly = true;
			this.log_richTextBox.Size = new System.Drawing.Size(392, 323);
			this.log_richTextBox.TabIndex = 0;
			this.log_richTextBox.Text = "";
			this.log_richTextBox.WordWrap = false;
			this.log_richTextBox.TextChanged += new System.EventHandler(this.log_richTextBox_TextChanged);
			// 
			// timer
			// 
			this.timer.Interval = 500;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// Percolation
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(674, 386);
			this.Controls.Add(this.options_groupBox);
			this.Controls.Add(this.pages_tabControl);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(690, 425);
			this.Name = "Percolation";
			this.Text = "Percolation";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Percolation_FormClosing);
			this.options_groupBox.ResumeLayout(false);
			this.options_groupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numberOfExperiments_numericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.probability_numericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.probabilityStep_numericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.widthOfMatrix_numericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.heightOfMatrix_numericUpDown)).EndInit();
			this.pages_tabControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.log_tabpage.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox options_groupBox;
		private System.Windows.Forms.CheckBox experimentalMode_checkBox;
		private System.Windows.Forms.NumericUpDown heightOfMatrix_numericUpDown;
		private System.Windows.Forms.Label heightOfMatrix_label;
		private System.Windows.Forms.Label widthOfMatrix_label;
		private System.Windows.Forms.NumericUpDown widthOfMatrix_numericUpDown;
		private System.Windows.Forms.Label probability_label;
		private System.Windows.Forms.TabControl pages_tabControl;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage log_tabpage;
		private System.Windows.Forms.RichTextBox log_richTextBox;
		private System.Windows.Forms.Button start_button;
		private System.Windows.Forms.NumericUpDown probabilityStep_numericUpDown;
		private System.Windows.Forms.NumericUpDown probability_numericUpDown;
		private System.Windows.Forms.Label probabilityStep_label;
		private System.Windows.Forms.Label numberOfExperiments_label;
		private System.Windows.Forms.NumericUpDown numberOfExperiments_numericUpDown;
		private ZedGraph.ZedGraphControl graph_zedGraphControl;
		private System.Windows.Forms.Button resume_button;
		private System.Windows.Forms.Button pause_button;
		private System.Windows.Forms.Label finished_label;
		private System.Windows.Forms.Timer timer;
	}
}

