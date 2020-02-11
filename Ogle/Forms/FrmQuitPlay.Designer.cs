namespace nOgle {
	partial class tqFrmQuitPlay {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tqFrmQuitPlay));
			this.eqBtnPlay = new System.Windows.Forms.Button();
			this.line1 = new nLine.tqLine();
			this.eqBtnQuitOgle = new System.Windows.Forms.Button();
			this.eqBtnEndRound = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// eqBtnPlay
			// 
			this.eqBtnPlay.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(this.eqBtnPlay, "eqBtnPlay");
			this.eqBtnPlay.Name = "eqBtnPlay";
			this.eqBtnPlay.UseVisualStyleBackColor = true;
			this.eqBtnPlay.Click += new System.EventHandler(this.eBtnReturn_Click);
			this.eqBtnPlay.MouseEnter += new System.EventHandler(this.eCtl_MouseEnter);
			// 
			// line1
			// 
			this.line1.ForeColor = System.Drawing.SystemColors.ActiveBorder;
			this.line1.Horizontal = true;
			resources.ApplyResources(this.line1, "line1");
			this.line1.Name = "line1";
			this.line1.TabStop = false;
			// 
			// eqBtnQuitOgle
			// 
			resources.ApplyResources(this.eqBtnQuitOgle, "eqBtnQuitOgle");
			this.eqBtnQuitOgle.Name = "eqBtnQuitOgle";
			this.eqBtnQuitOgle.UseVisualStyleBackColor = true;
			this.eqBtnQuitOgle.Click += new System.EventHandler(this.eBtnQuitOgle_Click);
			this.eqBtnQuitOgle.MouseEnter += new System.EventHandler(this.eCtl_MouseEnter);
			// 
			// eqBtnEndRound
			// 
			resources.ApplyResources(this.eqBtnEndRound, "eqBtnEndRound");
			this.eqBtnEndRound.Name = "eqBtnEndRound";
			this.eqBtnEndRound.UseVisualStyleBackColor = true;
			this.eqBtnEndRound.Click += new System.EventHandler(this.eBtnEndRound_Click);
			this.eqBtnEndRound.MouseEnter += new System.EventHandler(this.eCtl_MouseEnter);
			// 
			// tqFrmQuitPlay
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.eqBtnPlay;
			resources.ApplyResources(this, "$this");
			this.ControlBox = false;
			this.Controls.Add(this.eqBtnEndRound);
			this.Controls.Add(this.eqBtnPlay);
			this.Controls.Add(this.line1);
			this.Controls.Add(this.eqBtnQuitOgle);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "tqFrmQuitPlay";
			this.PosCtr = new System.Drawing.Point(96, 82);
			this.ShowInTaskbar = false;
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button eqBtnPlay;
		private nLine.tqLine line1;
		private System.Windows.Forms.Button eqBtnQuitOgle;
		private System.Windows.Forms.Button eqBtnEndRound;
	}
}