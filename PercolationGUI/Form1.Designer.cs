namespace PercolationGUI
{
	partial class Form1
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
			this.options_groupBox = new System.Windows.Forms.GroupBox();
			this.probability_trackBar = new System.Windows.Forms.TrackBar();
			this.probability_label = new System.Windows.Forms.Label();
			this.widthOfMatrix_numericUpDown = new System.Windows.Forms.NumericUpDown();
			this.widthOfMatrix_label = new System.Windows.Forms.Label();
			this.heightOfMatrix_label = new System.Windows.Forms.Label();
			this.heightOfMatrix_numericUpDown = new System.Windows.Forms.NumericUpDown();
			this.experimantalMode_checkBox = new System.Windows.Forms.CheckBox();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.options_groupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.probability_trackBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.widthOfMatrix_numericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.heightOfMatrix_numericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// options_groupBox
			// 
			this.options_groupBox.Controls.Add(this.probability_trackBar);
			this.options_groupBox.Controls.Add(this.probability_label);
			this.options_groupBox.Controls.Add(this.widthOfMatrix_numericUpDown);
			this.options_groupBox.Controls.Add(this.widthOfMatrix_label);
			this.options_groupBox.Controls.Add(this.heightOfMatrix_label);
			this.options_groupBox.Controls.Add(this.heightOfMatrix_numericUpDown);
			this.options_groupBox.Controls.Add(this.experimantalMode_checkBox);
			this.options_groupBox.Location = new System.Drawing.Point(17, 20);
			this.options_groupBox.Name = "options_groupBox";
			this.options_groupBox.Size = new System.Drawing.Size(230, 353);
			this.options_groupBox.TabIndex = 0;
			this.options_groupBox.TabStop = false;
			this.options_groupBox.Text = "Options";
			// 
			// probability_trackBar
			// 
			this.probability_trackBar.Location = new System.Drawing.Point(0, 173);
			this.probability_trackBar.Maximum = 100;
			this.probability_trackBar.Name = "probability_trackBar";
			this.probability_trackBar.Size = new System.Drawing.Size(122, 45);
			this.probability_trackBar.TabIndex = 6;
			this.probability_trackBar.Scroll += new System.EventHandler(this.probability_trackBar_Scroll);
			// 
			// probability_label
			// 
			this.probability_label.AutoSize = true;
			this.probability_label.Location = new System.Drawing.Point(6, 157);
			this.probability_label.Name = "probability_label";
			this.probability_label.Size = new System.Drawing.Size(58, 13);
			this.probability_label.TabIndex = 5;
			this.probability_label.Text = "Probability:";
			// 
			// widthOfMatrix_numericUpDown
			// 
			this.widthOfMatrix_numericUpDown.Location = new System.Drawing.Point(109, 237);
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
            10,
            0,
            0,
            0});
			this.widthOfMatrix_numericUpDown.ValueChanged += new System.EventHandler(this.widthOfMatrix_numericUpDown_ValueChanged);
			// 
			// widthOfMatrix_label
			// 
			this.widthOfMatrix_label.AutoSize = true;
			this.widthOfMatrix_label.Location = new System.Drawing.Point(95, 221);
			this.widthOfMatrix_label.Name = "widthOfMatrix_label";
			this.widthOfMatrix_label.Size = new System.Drawing.Size(80, 13);
			this.widthOfMatrix_label.TabIndex = 3;
			this.widthOfMatrix_label.Text = "Width of matrix:";
			// 
			// heightOfMatrix_label
			// 
			this.heightOfMatrix_label.AutoSize = true;
			this.heightOfMatrix_label.Location = new System.Drawing.Point(6, 221);
			this.heightOfMatrix_label.Name = "heightOfMatrix_label";
			this.heightOfMatrix_label.Size = new System.Drawing.Size(83, 13);
			this.heightOfMatrix_label.TabIndex = 2;
			this.heightOfMatrix_label.Text = "Height of matrix:";
			// 
			// heightOfMatrix_numericUpDown
			// 
			this.heightOfMatrix_numericUpDown.Location = new System.Drawing.Point(20, 237);
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
            10,
            0,
            0,
            0});
			this.heightOfMatrix_numericUpDown.ValueChanged += new System.EventHandler(this.heightOfMatrix_numericUpDown_ValueChanged);
			// 
			// experimantalMode_checkBox
			// 
			this.experimantalMode_checkBox.AutoSize = true;
			this.experimantalMode_checkBox.Location = new System.Drawing.Point(6, 321);
			this.experimantalMode_checkBox.Name = "experimantalMode_checkBox";
			this.experimantalMode_checkBox.Size = new System.Drawing.Size(116, 17);
			this.experimantalMode_checkBox.TabIndex = 0;
			this.experimantalMode_checkBox.Text = "Experimental Mode";
			this.experimantalMode_checkBox.UseVisualStyleBackColor = true;
			this.experimantalMode_checkBox.CheckedChanged += new System.EventHandler(this.experimantalMode_checkBox_CheckedChanged);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(263, 20);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(402, 353);
			this.richTextBox1.TabIndex = 1;
			this.richTextBox1.Text = "";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(677, 385);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.options_groupBox);
			this.Name = "Form1";
			this.Text = "Form1";
			this.options_groupBox.ResumeLayout(false);
			this.options_groupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.probability_trackBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.widthOfMatrix_numericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.heightOfMatrix_numericUpDown)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox options_groupBox;
		private System.Windows.Forms.CheckBox experimantalMode_checkBox;
		private System.Windows.Forms.NumericUpDown heightOfMatrix_numericUpDown;
		private System.Windows.Forms.Label heightOfMatrix_label;
		private System.Windows.Forms.Label widthOfMatrix_label;
		private System.Windows.Forms.NumericUpDown widthOfMatrix_numericUpDown;
		private System.Windows.Forms.TrackBar probability_trackBar;
		private System.Windows.Forms.Label probability_label;
		private System.Windows.Forms.RichTextBox richTextBox1;
	}
}

