namespace nOgle {
	partial class tqFrmSetup {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tqFrmSetup));
			this.eqLblDtlYld = new System.Windows.Forms.Label();
			this.eqBarYld = new System.Windows.Forms.TrackBar();
			this.eqLblYld = new System.Windows.Forms.Label();
			this.eqBoxYld = new System.Windows.Forms.GroupBox();
			this.eqBoxTime = new System.Windows.Forms.GroupBox();
			this.eqLblPace = new System.Windows.Forms.Label();
			this.eqLblDtlPace = new System.Windows.Forms.Label();
			this.eqBarPace = new System.Windows.Forms.TrackBar();
			this.eqBtnPlay = new System.Windows.Forms.Button();
			this.eqBtnAbout = new System.Windows.Forms.Button();
			this.eqBtnHelp = new System.Windows.Forms.Button();
			this.eqBtnAdv = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.eqBarYld)).BeginInit();
			this.eqBoxYld.SuspendLayout();
			this.eqBoxTime.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.eqBarPace)).BeginInit();
			this.SuspendLayout();
			// 
			// eqLblDtlYld
			// 
			this.eqLblDtlYld.ForeColor = System.Drawing.SystemColors.ControlText;
			this.eqLblDtlYld.Location = new System.Drawing.Point(10, 94);
			this.eqLblDtlYld.Name = "eqLblDtlYld";
			this.eqLblDtlYld.Size = new System.Drawing.Size(222, 39);
			this.eqLblDtlYld.TabIndex = 6;
			this.eqLblDtlYld.Text = "Xxxxx xxxx xxx";
			// 
			// eqBarYld
			// 
			this.eqBarYld.LargeChange = 1;
			this.eqBarYld.Location = new System.Drawing.Point(6, 53);
			this.eqBarYld.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.eqBarYld.Maximum = 2;
			this.eqBarYld.Name = "eqBarYld";
			this.eqBarYld.Size = new System.Drawing.Size(227, 56);
			this.eqBarYld.TabIndex = 0;
			this.eqBarYld.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.eqBarYld.ValueChanged += new System.EventHandler(this.eBarYld_ValueChanged);
			this.eqBarYld.Scroll += new System.EventHandler(this.eCtl_ChgVal);
			this.eqBarYld.MouseEnter += new System.EventHandler(this.eCtl_MouseEnter);
			// 
			// eqLblYld
			// 
			this.eqLblYld.AutoSize = true;
			this.eqLblYld.Font = new System.Drawing.Font("Georgia", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.eqLblYld.ForeColor = System.Drawing.SystemColors.ControlText;
			this.eqLblYld.Location = new System.Drawing.Point(6, 20);
			this.eqLblYld.Name = "eqLblYld";
			this.eqLblYld.Size = new System.Drawing.Size(156, 35);
			this.eqLblYld.TabIndex = 5;
			this.eqLblYld.Text = "Xxxxxxxxx";
			// 
			// eqBoxYld
			// 
			this.eqBoxYld.Controls.Add(this.eqLblYld);
			this.eqBoxYld.Controls.Add(this.eqLblDtlYld);
			this.eqBoxYld.Controls.Add(this.eqBarYld);
			this.eqBoxYld.ForeColor = System.Drawing.Color.Peru;
			this.eqBoxYld.Location = new System.Drawing.Point(14, 10);
			this.eqBoxYld.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.eqBoxYld.Name = "eqBoxYld";
			this.eqBoxYld.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.eqBoxYld.Size = new System.Drawing.Size(240, 142);
			this.eqBoxYld.TabIndex = 1;
			this.eqBoxYld.TabStop = false;
			this.eqBoxYld.Text = "Yield";
			// 
			// eqBoxTime
			// 
			this.eqBoxTime.Controls.Add(this.eqLblPace);
			this.eqBoxTime.Controls.Add(this.eqLblDtlPace);
			this.eqBoxTime.Controls.Add(this.eqBarPace);
			this.eqBoxTime.ForeColor = System.Drawing.Color.Peru;
			this.eqBoxTime.Location = new System.Drawing.Point(14, 157);
			this.eqBoxTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.eqBoxTime.Name = "eqBoxTime";
			this.eqBoxTime.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.eqBoxTime.Size = new System.Drawing.Size(240, 178);
			this.eqBoxTime.TabIndex = 2;
			this.eqBoxTime.TabStop = false;
			this.eqBoxTime.Text = "Pace";
			// 
			// eqLblPace
			// 
			this.eqLblPace.AutoSize = true;
			this.eqLblPace.Font = new System.Drawing.Font("Georgia", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.eqLblPace.ForeColor = System.Drawing.SystemColors.ControlText;
			this.eqLblPace.Location = new System.Drawing.Point(6, 20);
			this.eqLblPace.Name = "eqLblPace";
			this.eqLblPace.Size = new System.Drawing.Size(156, 35);
			this.eqLblPace.TabIndex = 5;
			this.eqLblPace.Text = "Xxxxxxxxx";
			// 
			// eqLblDtlPace
			// 
			this.eqLblDtlPace.ForeColor = System.Drawing.SystemColors.ControlText;
			this.eqLblDtlPace.Location = new System.Drawing.Point(10, 94);
			this.eqLblDtlPace.Name = "eqLblDtlPace";
			this.eqLblDtlPace.Size = new System.Drawing.Size(222, 71);
			this.eqLblDtlPace.TabIndex = 6;
			this.eqLblDtlPace.Text = "Xxxxx xxxx xxx";
			// 
			// eqBarPace
			// 
			this.eqBarPace.LargeChange = 1;
			this.eqBarPace.Location = new System.Drawing.Point(6, 53);
			this.eqBarPace.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.eqBarPace.Maximum = 6;
			this.eqBarPace.Name = "eqBarPace";
			this.eqBarPace.Size = new System.Drawing.Size(227, 56);
			this.eqBarPace.TabIndex = 0;
			this.eqBarPace.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.eqBarPace.ValueChanged += new System.EventHandler(this.eBarPace_ValueChanged);
			this.eqBarPace.Scroll += new System.EventHandler(this.eCtl_ChgVal);
			this.eqBarPace.MouseEnter += new System.EventHandler(this.eCtl_MouseEnter);
			// 
			// eqBtnPlay
			// 
			this.eqBtnPlay.Location = new System.Drawing.Point(138, 381);
			this.eqBtnPlay.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.eqBtnPlay.Name = "eqBtnPlay";
			this.eqBtnPlay.Size = new System.Drawing.Size(117, 30);
			this.eqBtnPlay.TabIndex = 0;
			this.eqBtnPlay.Text = "&Play";
			this.eqBtnPlay.Click += new System.EventHandler(this.eBtnPlay_Click);
			this.eqBtnPlay.MouseEnter += new System.EventHandler(this.eCtl_MouseEnter);
			// 
			// eqBtnAbout
			// 
			this.eqBtnAbout.Location = new System.Drawing.Point(14, 346);
			this.eqBtnAbout.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.eqBtnAbout.Name = "eqBtnAbout";
			this.eqBtnAbout.Size = new System.Drawing.Size(117, 30);
			this.eqBtnAbout.TabIndex = 3;
			this.eqBtnAbout.Text = "&About";
			this.eqBtnAbout.Click += new System.EventHandler(this.eBtnAbout_Click);
			this.eqBtnAbout.MouseEnter += new System.EventHandler(this.eCtl_MouseEnter);
			// 
			// eqBtnHelp
			// 
			this.eqBtnHelp.Location = new System.Drawing.Point(138, 346);
			this.eqBtnHelp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.eqBtnHelp.Name = "eqBtnHelp";
			this.eqBtnHelp.Size = new System.Drawing.Size(117, 30);
			this.eqBtnHelp.TabIndex = 5;
			this.eqBtnHelp.Text = "&Help";
			this.eqBtnHelp.Click += new System.EventHandler(this.eBtnHelp_Click);
			this.eqBtnHelp.MouseEnter += new System.EventHandler(this.eCtl_MouseEnter);
			// 
			// eqBtnAdv
			// 
			this.eqBtnAdv.Location = new System.Drawing.Point(14, 381);
			this.eqBtnAdv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.eqBtnAdv.Name = "eqBtnAdv";
			this.eqBtnAdv.Size = new System.Drawing.Size(117, 30);
			this.eqBtnAdv.TabIndex = 4;
			this.eqBtnAdv.Text = "Ad&vanced";
			this.eqBtnAdv.Click += new System.EventHandler(this.eBtnAdv_Click);
			this.eqBtnAdv.MouseEnter += new System.EventHandler(this.eCtl_MouseEnter);
			// 
			// tqFrmSetup
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(269, 423);
			this.Controls.Add(this.eqBtnAdv);
			this.Controls.Add(this.eqBtnHelp);
			this.Controls.Add(this.eqBtnAbout);
			this.Controls.Add(this.eqBoxYld);
			this.Controls.Add(this.eqBoxTime);
			this.Controls.Add(this.eqBtnPlay);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "tqFrmSetup";
			this.PosCtr = new System.Drawing.Point(152, 243);
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Ogle setup";
			this.Shown += new System.EventHandler(this.eFrmSetup_Shown);
			((System.ComponentModel.ISupportInitialize)(this.eqBarYld)).EndInit();
			this.eqBoxYld.ResumeLayout(false);
			this.eqBoxYld.PerformLayout();
			this.eqBoxTime.ResumeLayout(false);
			this.eqBoxTime.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.eqBarPace)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label eqLblDtlYld;
		private System.Windows.Forms.TrackBar eqBarYld;
		private System.Windows.Forms.Label eqLblYld;
		private System.Windows.Forms.GroupBox eqBoxYld;
		private System.Windows.Forms.GroupBox eqBoxTime;
		private System.Windows.Forms.Label eqLblDtlPace;
		private System.Windows.Forms.TrackBar eqBarPace;
		private System.Windows.Forms.Label eqLblPace;
		private System.Windows.Forms.Button eqBtnPlay;
		private System.Windows.Forms.Button eqBtnAbout;
		private System.Windows.Forms.Button eqBtnHelp;
		private System.Windows.Forms.Button eqBtnAdv;
	}
}

