namespace nOgle {
	partial class tqFrmDtl {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tqFrmDtl));
			this.eqBtnOK = new System.Windows.Forms.Button();
			this.eqBoxComp = new System.Windows.Forms.GroupBox();
			this.eqLblComp = new System.Windows.Forms.Label();
			this.eqLblScoreComp = new System.Windows.Forms.Label();
			this.eqLblLegScoreComp = new System.Windows.Forms.Label();
			this.eqBoxPlay = new System.Windows.Forms.GroupBox();
			this.eqLblPlay = new System.Windows.Forms.Label();
			this.eqLblScorePlay = new System.Windows.Forms.Label();
			this.eqLblLegScorePlay = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.eqBtnSel = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.eqLblLen = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.eqGrid = new nOgle.tqGrid();
			this.eqBoxComp.SuspendLayout();
			this.eqBoxPlay.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// eqBtnOK
			// 
			this.eqBtnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(this.eqBtnOK, "eqBtnOK");
			this.eqBtnOK.Name = "eqBtnOK";
			this.eqBtnOK.UseVisualStyleBackColor = true;
			this.eqBtnOK.Click += new System.EventHandler(this.eBtnOK_Click);
			this.eqBtnOK.MouseEnter += new System.EventHandler(this.eCtl_MouseEnter);
			// 
			// eqBoxComp
			// 
			this.eqBoxComp.Controls.Add(this.eqLblLegScoreComp);
			this.eqBoxComp.Controls.Add(this.eqLblComp);
			this.eqBoxComp.Controls.Add(this.eqLblScoreComp);
			resources.ApplyResources(this.eqBoxComp, "eqBoxComp");
			this.eqBoxComp.Name = "eqBoxComp";
			this.eqBoxComp.TabStop = false;
			this.eqBoxComp.Paint += new System.Windows.Forms.PaintEventHandler(this.eBoxComp_Paint);
			// 
			// eqLblComp
			// 
			resources.ApplyResources(this.eqLblComp, "eqLblComp");
			this.eqLblComp.Name = "eqLblComp";
			// 
			// eqLblScoreComp
			// 
			resources.ApplyResources(this.eqLblScoreComp, "eqLblScoreComp");
			this.eqLblScoreComp.Name = "eqLblScoreComp";
			// 
			// eqLblLegScoreComp
			// 
			resources.ApplyResources(this.eqLblLegScoreComp, "eqLblLegScoreComp");
			this.eqLblLegScoreComp.Name = "eqLblLegScoreComp";
			// 
			// eqBoxPlay
			// 
			this.eqBoxPlay.Controls.Add(this.eqLblLegScorePlay);
			this.eqBoxPlay.Controls.Add(this.eqLblPlay);
			this.eqBoxPlay.Controls.Add(this.eqLblScorePlay);
			resources.ApplyResources(this.eqBoxPlay, "eqBoxPlay");
			this.eqBoxPlay.Name = "eqBoxPlay";
			this.eqBoxPlay.TabStop = false;
			this.eqBoxPlay.Paint += new System.Windows.Forms.PaintEventHandler(this.eBoxPlay_Paint);
			// 
			// eqLblPlay
			// 
			resources.ApplyResources(this.eqLblPlay, "eqLblPlay");
			this.eqLblPlay.Name = "eqLblPlay";
			// 
			// eqLblScorePlay
			// 
			resources.ApplyResources(this.eqLblScorePlay, "eqLblScorePlay");
			this.eqLblScorePlay.Name = "eqLblScorePlay";
			// 
			// eqLblLegScorePlay
			// 
			resources.ApplyResources(this.eqLblLegScorePlay, "eqLblLegScorePlay");
			this.eqLblLegScorePlay.Name = "eqLblLegScorePlay";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.label1);
			this.groupBox4.Controls.Add(this.eqBtnSel);
			resources.ApplyResources(this.groupBox4, "groupBox4");
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.TabStop = false;
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// eqBtnSel
			// 
			resources.ApplyResources(this.eqBtnSel, "eqBtnSel");
			this.eqBtnSel.Name = "eqBtnSel";
			this.eqBtnSel.UseVisualStyleBackColor = true;
			this.eqBtnSel.Click += new System.EventHandler(this.eBtnSel_Click);
			this.eqBtnSel.MouseEnter += new System.EventHandler(this.eCtl_MouseEnter);
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// eqLblLen
			// 
			resources.ApplyResources(this.eqLblLen, "eqLblLen");
			this.eqLblLen.Name = "eqLblLen";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.eqLblLen);
			this.groupBox3.Controls.Add(this.label3);
			resources.ApplyResources(this.groupBox3, "groupBox3");
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.TabStop = false;
			// 
			// eqGrid
			// 
			this.eqGrid.ClrMask = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(156)))), ((int)(((byte)(111)))));
			this.eqGrid.ClrMaskAccent = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(197)))), ((int)(((byte)(156)))));
			this.eqGrid.ClrText = System.Drawing.Color.Black;
			this.eqGrid.ClrTextAccent = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.eqGrid.Cursor = System.Windows.Forms.Cursors.Default;
			this.eqGrid.Editable = false;
			resources.ApplyResources(this.eqGrid, "eqGrid");
			this.eqGrid.Name = "eqGrid";
			this.eqGrid.qBoard = null;
			this.eqGrid.qOver = null;
			this.eqGrid.qSel = null;
			this.eqGrid.SqCurs = null;
			this.eqGrid.VisMask = false;
			// 
			// tqFrmDtl
			// 
			this.AcceptButton = this.eqBtnOK;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.eqBtnOK;
			resources.ApplyResources(this, "$this");
			this.ControlBox = false;
			this.Controls.Add(this.eqGrid);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.eqBoxPlay);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.eqBoxComp);
			this.Controls.Add(this.eqBtnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "tqFrmDtl";
			this.PosCtr = new System.Drawing.Point(258, 225);
			this.ShowInTaskbar = false;
			this.Load += new System.EventHandler(this.eFrmDtl_Load);
			this.eqBoxComp.ResumeLayout(false);
			this.eqBoxPlay.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button eqBtnOK;
		private System.Windows.Forms.GroupBox eqBoxComp;
		private System.Windows.Forms.Label eqLblScoreComp;
		private System.Windows.Forms.Label eqLblLegScoreComp;
		private tqGrid eqGrid;
		private System.Windows.Forms.GroupBox eqBoxPlay;
		private System.Windows.Forms.Label eqLblScorePlay;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button eqBtnSel;
		private System.Windows.Forms.Label eqLblComp;
		private System.Windows.Forms.Label eqLblPlay;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label eqLblLen;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label eqLblLegScorePlay;
	}
}