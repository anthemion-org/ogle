namespace nOgle {
	partial class tqFrmScore {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tqFrmScore));
			this.eqBtnPlay = new System.Windows.Forms.Button();
			this.eqBoxSet = new System.Windows.Forms.GroupBox();
			this.eqLblYld = new System.Windows.Forms.Label();
			this.eqLblPace = new System.Windows.Forms.Label();
			this.label78 = new System.Windows.Forms.Label();
			this.label77 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.eqPanScore = new nOgle.tqPanScore();
			this.eqScroll = new System.Windows.Forms.VScrollBar();
			this.eqLineScores = new nLine.tqLine();
			this.eqPanPairs = new nOgle.tqPanBuffDbl();
			this.eqBtnSetup = new System.Windows.Forms.Button();
			this.eqBoxScores = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tqLine1 = new nLine.tqLine();
			this.label2 = new System.Windows.Forms.Label();
			this.eqBoxSet.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.eqBoxScores.SuspendLayout();
			this.SuspendLayout();
			// 
			// eqBtnPlay
			// 
			this.eqBtnPlay.Location = new System.Drawing.Point(435, 513);
			this.eqBtnPlay.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.eqBtnPlay.Name = "eqBtnPlay";
			this.eqBtnPlay.Size = new System.Drawing.Size(100, 30);
			this.eqBtnPlay.TabIndex = 0;
			this.eqBtnPlay.Text = "&Play Again";
			this.eqBtnPlay.UseVisualStyleBackColor = true;
			this.eqBtnPlay.Click += new System.EventHandler(this.eBtnPlay_Click);
			this.eqBtnPlay.MouseEnter += new System.EventHandler(this.eCtl_MouseEnter);
			// 
			// eqBoxSet
			// 
			this.eqBoxSet.Controls.Add(this.eqLblYld);
			this.eqBoxSet.Controls.Add(this.eqLblPace);
			this.eqBoxSet.Controls.Add(this.label78);
			this.eqBoxSet.Controls.Add(this.label77);
			this.eqBoxSet.ForeColor = System.Drawing.Color.Peru;
			this.eqBoxSet.Location = new System.Drawing.Point(331, 10);
			this.eqBoxSet.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.eqBoxSet.Name = "eqBoxSet";
			this.eqBoxSet.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.eqBoxSet.Size = new System.Drawing.Size(204, 147);
			this.eqBoxSet.TabIndex = 155;
			this.eqBoxSet.TabStop = false;
			this.eqBoxSet.Text = "Settings";
			// 
			// eqLblYld
			// 
			this.eqLblYld.Font = new System.Drawing.Font("Georgia", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.eqLblYld.ForeColor = System.Drawing.SystemColors.ControlText;
			this.eqLblYld.Location = new System.Drawing.Point(8, 27);
			this.eqLblYld.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.eqLblYld.Name = "eqLblYld";
			this.eqLblYld.Size = new System.Drawing.Size(188, 36);
			this.eqLblYld.TabIndex = 141;
			this.eqLblYld.Text = "Adequate";
			this.eqLblYld.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// eqLblPace
			// 
			this.eqLblPace.Font = new System.Drawing.Font("Georgia", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.eqLblPace.ForeColor = System.Drawing.SystemColors.ControlText;
			this.eqLblPace.Location = new System.Drawing.Point(8, 77);
			this.eqLblPace.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.eqLblPace.Name = "eqLblPace";
			this.eqLblPace.Size = new System.Drawing.Size(188, 36);
			this.eqLblPace.TabIndex = 139;
			this.eqLblPace.Text = "Dizzying";
			this.eqLblPace.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label78
			// 
			this.label78.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label78.Location = new System.Drawing.Point(8, 61);
			this.label78.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label78.Name = "label78";
			this.label78.Size = new System.Drawing.Size(188, 16);
			this.label78.TabIndex = 143;
			this.label78.Text = "Yield";
			this.label78.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label77
			// 
			this.label77.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label77.Location = new System.Drawing.Point(8, 111);
			this.label77.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label77.Name = "label77";
			this.label77.Size = new System.Drawing.Size(188, 16);
			this.label77.TabIndex = 144;
			this.label77.Text = "Pace";
			this.label77.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.eqPanScore);
			this.groupBox1.Controls.Add(this.eqScroll);
			this.groupBox1.Controls.Add(this.eqLineScores);
			this.groupBox1.Controls.Add(this.eqPanPairs);
			this.groupBox1.ForeColor = System.Drawing.Color.Peru;
			this.groupBox1.Location = new System.Drawing.Point(14, 10);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.groupBox1.Size = new System.Drawing.Size(310, 493);
			this.groupBox1.TabIndex = 169;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Result";
			// 
			// eqPanScore
			// 
			this.eqPanScore.Font = new System.Drawing.Font("Georgia", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.eqPanScore.Location = new System.Drawing.Point(14, 22);
			this.eqPanScore.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.eqPanScore.Name = "eqPanScore";
			this.eqPanScore.PtsComp = 999;
			this.eqPanScore.PtsPlay = 999;
			this.eqPanScore.Size = new System.Drawing.Size(264, 26);
			this.eqPanScore.TabIndex = 173;
			// 
			// eqScroll
			// 
			this.eqScroll.Cursor = System.Windows.Forms.Cursors.Default;
			this.eqScroll.Location = new System.Drawing.Point(274, 60);
			this.eqScroll.Name = "eqScroll";
			this.eqScroll.Size = new System.Drawing.Size(22, 419);
			this.eqScroll.TabIndex = 145;
			this.eqScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.eScroll_Scroll);
			// 
			// eqLineScores
			// 
			this.eqLineScores.ForeColor = System.Drawing.SystemColors.ActiveBorder;
			this.eqLineScores.Horizontal = true;
			this.eqLineScores.Location = new System.Drawing.Point(14, 46);
			this.eqLineScores.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.eqLineScores.Name = "eqLineScores";
			this.eqLineScores.Size = new System.Drawing.Size(260, 9);
			this.eqLineScores.TabIndex = 174;
			this.eqLineScores.TabStop = false;
			// 
			// eqPanPairs
			// 
			this.eqPanPairs.Location = new System.Drawing.Point(14, 61);
			this.eqPanPairs.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.eqPanPairs.Name = "eqPanPairs";
			this.eqPanPairs.Size = new System.Drawing.Size(260, 418);
			this.eqPanPairs.TabIndex = 169;
			this.eqPanPairs.Paint += new System.Windows.Forms.PaintEventHandler(this.ePanPairs_Paint);
			this.eqPanPairs.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ePanPairs_MouseMove);
			this.eqPanPairs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ePanPairs_MouseClick);
			// 
			// eqBtnSetup
			// 
			this.eqBtnSetup.Location = new System.Drawing.Point(14, 514);
			this.eqBtnSetup.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.eqBtnSetup.Name = "eqBtnSetup";
			this.eqBtnSetup.Size = new System.Drawing.Size(100, 30);
			this.eqBtnSetup.TabIndex = 1;
			this.eqBtnSetup.Text = "&Setup";
			this.eqBtnSetup.UseVisualStyleBackColor = true;
			this.eqBtnSetup.Click += new System.EventHandler(this.eBtnSetup_Click);
			this.eqBtnSetup.MouseEnter += new System.EventHandler(this.eCtl_MouseEnter);
			// 
			// eqBoxScores
			// 
			this.eqBoxScores.Controls.Add(this.label1);
			this.eqBoxScores.Controls.Add(this.tqLine1);
			this.eqBoxScores.Controls.Add(this.label2);
			this.eqBoxScores.ForeColor = System.Drawing.Color.Peru;
			this.eqBoxScores.Location = new System.Drawing.Point(331, 163);
			this.eqBoxScores.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.eqBoxScores.Name = "eqBoxScores";
			this.eqBoxScores.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.eqBoxScores.Size = new System.Drawing.Size(204, 340);
			this.eqBoxScores.TabIndex = 172;
			this.eqBoxScores.TabStop = false;
			this.eqBoxScores.Text = "High scores";
			this.eqBoxScores.Paint += new System.Windows.Forms.PaintEventHandler(this.eqBoxScores_Paint);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Georgia", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label1.Location = new System.Drawing.Point(8, 22);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(188, 24);
			this.label1.TabIndex = 29;
			this.label1.Text = "by Percent";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// tqLine1
			// 
			this.tqLine1.ForeColor = System.Drawing.SystemColors.ActiveBorder;
			this.tqLine1.Horizontal = true;
			this.tqLine1.Location = new System.Drawing.Point(14, 166);
			this.tqLine1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.tqLine1.Name = "tqLine1";
			this.tqLine1.Size = new System.Drawing.Size(175, 19);
			this.tqLine1.TabIndex = 28;
			this.tqLine1.TabStop = false;
			this.tqLine1.Text = "tqLine1";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Georgia", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label2.Location = new System.Drawing.Point(8, 184);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(188, 24);
			this.label2.TabIndex = 7;
			this.label2.Text = "by Points";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// tqFrmScore
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(551, 556);
			this.Controls.Add(this.eqBoxScores);
			this.Controls.Add(this.eqBtnSetup);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.eqBoxSet);
			this.Controls.Add(this.eqBtnPlay);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "tqFrmScore";
			this.PosCtr = new System.Drawing.Point(293, 307);
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Ogle score";
			this.Load += new System.EventHandler(this.eFrmScore_Load);
			this.Shown += new System.EventHandler(this.eFrmScore_Shown);
			this.eqBoxSet.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.eqBoxScores.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button eqBtnPlay;
		private System.Windows.Forms.GroupBox eqBoxSet;
		private System.Windows.Forms.Label label78;
		private System.Windows.Forms.Label label77;
		private System.Windows.Forms.Label eqLblYld;
		private System.Windows.Forms.GroupBox groupBox1;
		private nLine.tqLine eqLineScores;
		private nOgle.tqPanBuffDbl eqPanPairs;
		private System.Windows.Forms.Button eqBtnSetup;
		private System.Windows.Forms.GroupBox eqBoxScores;
		private nLine.tqLine tqLine1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.VScrollBar eqScroll;
		private System.Windows.Forms.Label label1;
		private tqPanScore eqPanScore;
		private System.Windows.Forms.Label eqLblPace;
	}
}