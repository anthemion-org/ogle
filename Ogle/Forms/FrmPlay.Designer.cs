namespace nOgle {
	partial class tqFrmPlay {
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tqFrmPlay));
			this.eqLblTime = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.eqLblDir = new System.Windows.Forms.Label();
			this.eqLblEnt = new System.Windows.Forms.Label();
			this.eqGrid = new nOgle.tqGrid();
			this.eqLblKeys = new System.Windows.Forms.Label();
			this.eqLblUnitTime = new System.Windows.Forms.Label();
			this.eqBoxTime = new System.Windows.Forms.GroupBox();
			this.eqBtnPause = new nBtnTime.tqBtn();
			this.eqLblPause = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.eqLblScoreLeg = new System.Windows.Forms.Label();
			this.eqLblScore = new System.Windows.Forms.Label();
			this.eqTimer = new System.Windows.Forms.Timer(this.components);
			this.groupBox1.SuspendLayout();
			this.eqBoxTime.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// eqLblTime
			// 
			resources.ApplyResources(this.eqLblTime, "eqLblTime");
			this.eqLblTime.Name = "eqLblTime";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.eqLblDir);
			this.groupBox1.Controls.Add(this.eqLblEnt);
			this.groupBox1.Controls.Add(this.eqGrid);
			this.groupBox1.Controls.Add(this.eqLblKeys);
			resources.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			// 
			// eqLblDir
			// 
			resources.ApplyResources(this.eqLblDir, "eqLblDir");
			this.eqLblDir.Name = "eqLblDir";
			// 
			// eqLblEnt
			// 
			resources.ApplyResources(this.eqLblEnt, "eqLblEnt");
			this.eqLblEnt.Name = "eqLblEnt";
			// 
			// eqGrid
			// 
			this.eqGrid.ClrMask = System.Drawing.Color.Gray;
			this.eqGrid.ClrMaskAccent = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(170)))), ((int)(((byte)(151)))));
			this.eqGrid.ClrText = System.Drawing.Color.Black;
			this.eqGrid.ClrTextAccent = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
			this.eqGrid.Cursor = System.Windows.Forms.Cursors.Hand;
			this.eqGrid.Editable = true;
			resources.ApplyResources(this.eqGrid, "eqGrid");
			this.eqGrid.Name = "eqGrid";
			this.eqGrid.qBoard = null;
			this.eqGrid.qOver = null;
			this.eqGrid.qSel = null;
			this.eqGrid.SqCurs = null;
			this.eqGrid.VisMask = false;
			this.eqGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.eGrid_MouseClick);
			this.eqGrid.EntSel += new nOgle.tdHandEnt(this.eGrid_EntSel);
			this.eqGrid.OverNext += new System.EventHandler(this.eGrid_OverNext);
			this.eqGrid.ChgSel += new nOgle.tdHandChg(this.eGrid_ChgSel);
			// 
			// eqLblKeys
			// 
			resources.ApplyResources(this.eqLblKeys, "eqLblKeys");
			this.eqLblKeys.Name = "eqLblKeys";
			// 
			// eqLblUnitTime
			// 
			resources.ApplyResources(this.eqLblUnitTime, "eqLblUnitTime");
			this.eqLblUnitTime.Name = "eqLblUnitTime";
			// 
			// eqBoxTime
			// 
			this.eqBoxTime.Controls.Add(this.eqBtnPause);
			this.eqBoxTime.Controls.Add(this.eqLblPause);
			this.eqBoxTime.Controls.Add(this.eqLblTime);
			this.eqBoxTime.Controls.Add(this.eqLblUnitTime);
			resources.ApplyResources(this.eqBoxTime, "eqBoxTime");
			this.eqBoxTime.Name = "eqBoxTime";
			this.eqBoxTime.TabStop = false;
			// 
			// eqBtnPause
			// 
			this.eqBtnPause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(239)))), ((int)(((byte)(233)))));
			this.eqBtnPause.ClrBarLeft = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(179)))), ((int)(((byte)(140)))));
			this.eqBtnPause.ClrBarRight = System.Drawing.Color.Peru;
			this.eqBtnPause.CtBar = 18;
			this.eqBtnPause.DurBlink = 0;
			this.eqBtnPause.HgtBar = 4;
			resources.ApplyResources(this.eqBtnPause, "eqBtnPause");
			this.eqBtnPause.Name = "eqBtnPause";
			this.eqBtnPause.PadBar = 2;
			this.eqBtnPause.UseVisualStyleBackColor = false;
			this.eqBtnPause.Click += new System.EventHandler(this.eBtnTime_Click);
			// 
			// eqLblPause
			// 
			resources.ApplyResources(this.eqLblPause, "eqLblPause");
			this.eqLblPause.Name = "eqLblPause";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.eqLblScoreLeg);
			this.groupBox3.Controls.Add(this.eqLblScore);
			resources.ApplyResources(this.groupBox3, "groupBox3");
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.TabStop = false;
			// 
			// eqLblScoreLeg
			// 
			resources.ApplyResources(this.eqLblScoreLeg, "eqLblScoreLeg");
			this.eqLblScoreLeg.Name = "eqLblScoreLeg";
			// 
			// eqLblScore
			// 
			resources.ApplyResources(this.eqLblScore, "eqLblScore");
			this.eqLblScore.Name = "eqLblScore";
			// 
			// eqTimer
			// 
			this.eqTimer.Interval = 50;
			this.eqTimer.Tick += new System.EventHandler(this.eTimer_Tick);
			// 
			// tqFrmPlay
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.eqBoxTime);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "tqFrmPlay";
			this.PosCtr = new System.Drawing.Point(245, 242);
			this.Deactivate += new System.EventHandler(this.eFrmPlay_Deactivate);
			this.Load += new System.EventHandler(this.eFrmPlay_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.eFrmPlay_FormClosing);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.eqBoxTime.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label eqLblTime;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label eqLblUnitTime;
		private System.Windows.Forms.GroupBox eqBoxTime;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label eqLblScore;
		private System.Windows.Forms.Label eqLblScoreLeg;
		private System.Windows.Forms.Label eqLblKeys;
		private System.Windows.Forms.Label eqLblPause;
		private tqGrid eqGrid;
		private System.Windows.Forms.Timer eqTimer;
		private System.Windows.Forms.Label eqLblEnt;
		private System.Windows.Forms.Label eqLblDir;
		private nBtnTime.tqBtn eqBtnPause;
	}
}