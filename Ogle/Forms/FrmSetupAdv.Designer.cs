namespace nOgle {
	partial class tqFrmSetupAdv {
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
			this.eqBoxSound = new System.Windows.Forms.GroupBox();
			this.eqCkMute = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.eqBarVol = new System.Windows.Forms.TrackBar();
			this.tqLine2 = new nLine.tqLine();
			this.eqBoxDialect = new System.Windows.Forms.GroupBox();
			this.eqEdDial = new System.Windows.Forms.ComboBox();
			this.eqBtnOK = new System.Windows.Forms.Button();
			this.eqBoxSound.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.eqBarVol)).BeginInit();
			this.eqBoxDialect.SuspendLayout();
			this.SuspendLayout();
			// 
			// eqBoxSound
			// 
			this.eqBoxSound.Controls.Add(this.eqCkMute);
			this.eqBoxSound.Controls.Add(this.label3);
			this.eqBoxSound.Controls.Add(this.eqBarVol);
			this.eqBoxSound.Controls.Add(this.tqLine2);
			this.eqBoxSound.ForeColor = System.Drawing.Color.Peru;
			this.eqBoxSound.Location = new System.Drawing.Point(14, 10);
			this.eqBoxSound.Margin = new System.Windows.Forms.Padding(4);
			this.eqBoxSound.Name = "eqBoxSound";
			this.eqBoxSound.Padding = new System.Windows.Forms.Padding(4);
			this.eqBoxSound.Size = new System.Drawing.Size(134, 238);
			this.eqBoxSound.TabIndex = 1;
			this.eqBoxSound.TabStop = false;
			this.eqBoxSound.Text = "Sound";
			// 
			// eqCkMute
			// 
			this.eqCkMute.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
			this.eqCkMute.ForeColor = System.Drawing.SystemColors.ControlText;
			this.eqCkMute.Location = new System.Drawing.Point(4, 182);
			this.eqCkMute.Margin = new System.Windows.Forms.Padding(4);
			this.eqCkMute.Name = "eqCkMute";
			this.eqCkMute.Size = new System.Drawing.Size(126, 52);
			this.eqCkMute.TabIndex = 1;
			this.eqCkMute.Text = "Mute";
			this.eqCkMute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.eqCkMute.MouseEnter += new System.EventHandler(this.eCtl_MouseEnter);
			this.eqCkMute.Click += new System.EventHandler(this.eCkMute_Click);
			// 
			// label3
			// 
			this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label3.Location = new System.Drawing.Point(4, 139);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(126, 18);
			this.label3.TabIndex = 30;
			this.label3.Text = "Volume";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// eqBarVol
			// 
			this.eqBarVol.Location = new System.Drawing.Point(43, 18);
			this.eqBarVol.Margin = new System.Windows.Forms.Padding(4);
			this.eqBarVol.Maximum = 100;
			this.eqBarVol.Minimum = 60;
			this.eqBarVol.Name = "eqBarVol";
			this.eqBarVol.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.eqBarVol.Size = new System.Drawing.Size(56, 128);
			this.eqBarVol.SmallChange = 5;
			this.eqBarVol.TabIndex = 0;
			this.eqBarVol.TickFrequency = 5;
			this.eqBarVol.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.eqBarVol.Value = 75;
			this.eqBarVol.ValueChanged += new System.EventHandler(this.eBarVol_ValueChanged);
			this.eqBarVol.Scroll += new System.EventHandler(this.eCtl_ChgVal);
			this.eqBarVol.MouseEnter += new System.EventHandler(this.eCtl_MouseEnter);
			// 
			// tqLine2
			// 
			this.tqLine2.ForeColor = System.Drawing.SystemColors.ActiveBorder;
			this.tqLine2.Horizontal = true;
			this.tqLine2.Location = new System.Drawing.Point(13, 158);
			this.tqLine2.Margin = new System.Windows.Forms.Padding(4);
			this.tqLine2.Name = "tqLine2";
			this.tqLine2.Size = new System.Drawing.Size(108, 23);
			this.tqLine2.TabIndex = 28;
			this.tqLine2.TabStop = false;
			this.tqLine2.Text = "tqLine2";
			// 
			// eqBoxDialect
			// 
			this.eqBoxDialect.Controls.Add(this.eqEdDial);
			this.eqBoxDialect.ForeColor = System.Drawing.Color.Peru;
			this.eqBoxDialect.Location = new System.Drawing.Point(14, 254);
			this.eqBoxDialect.Margin = new System.Windows.Forms.Padding(4);
			this.eqBoxDialect.Name = "eqBoxDialect";
			this.eqBoxDialect.Padding = new System.Windows.Forms.Padding(4);
			this.eqBoxDialect.Size = new System.Drawing.Size(134, 64);
			this.eqBoxDialect.TabIndex = 2;
			this.eqBoxDialect.TabStop = false;
			this.eqBoxDialect.Text = "Dialect";
			// 
			// eqEdDial
			// 
			this.eqEdDial.DisplayMember = "American";
			this.eqEdDial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.eqEdDial.FormattingEnabled = true;
			this.eqEdDial.Items.AddRange(new object[] {
            "American",
            "British",
            "Canadian"});
			this.eqEdDial.Location = new System.Drawing.Point(15, 24);
			this.eqEdDial.Margin = new System.Windows.Forms.Padding(4);
			this.eqEdDial.Name = "eqEdDial";
			this.eqEdDial.Size = new System.Drawing.Size(104, 24);
			this.eqEdDial.TabIndex = 0;
			this.eqEdDial.SelectionChangeCommitted += new System.EventHandler(this.eEdDial_SelectionChangeCommitted);
			this.eqEdDial.SelectedIndexChanged += new System.EventHandler(this.eEdDial_SelectedIndexChanged);
			this.eqEdDial.MouseEnter += new System.EventHandler(this.eCtl_MouseEnter);
			// 
			// eqBtnOK
			// 
			this.eqBtnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.eqBtnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.eqBtnOK.Location = new System.Drawing.Point(14, 328);
			this.eqBtnOK.Margin = new System.Windows.Forms.Padding(4);
			this.eqBtnOK.Name = "eqBtnOK";
			this.eqBtnOK.Size = new System.Drawing.Size(134, 28);
			this.eqBtnOK.TabIndex = 0;
			this.eqBtnOK.Text = "&OK";
			this.eqBtnOK.UseVisualStyleBackColor = true;
			this.eqBtnOK.Click += new System.EventHandler(this.eBtnOK_Click);
			this.eqBtnOK.MouseEnter += new System.EventHandler(this.eCtl_MouseEnter);
			// 
			// tqFrmSetupAdv
			// 
			this.AcceptButton = this.eqBtnOK;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(164, 368);
			this.ControlBox = false;
			this.Controls.Add(this.eqBtnOK);
			this.Controls.Add(this.eqBoxSound);
			this.Controls.Add(this.eqBoxDialect);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "tqFrmSetupAdv";
			this.PosCtr = new System.Drawing.Point(132, 438);
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Ogle Setup Advanced";
			this.Load += new System.EventHandler(this.eFrmSetupAdv_Load);
			this.eqBoxSound.ResumeLayout(false);
			this.eqBoxSound.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.eqBarVol)).EndInit();
			this.eqBoxDialect.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox eqBoxSound;
		private System.Windows.Forms.CheckBox eqCkMute;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TrackBar eqBarVol;
		private nLine.tqLine tqLine2;
		private System.Windows.Forms.GroupBox eqBoxDialect;
		private System.Windows.Forms.ComboBox eqEdDial;
		private System.Windows.Forms.Button eqBtnOK;
	}
}