namespace nOgle {
	partial class tqFrmStat {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tqFrmStat));
			this.eqImg = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.eqImg)).BeginInit();
			this.SuspendLayout();
			// 
			// eqImg
			// 
			this.eqImg.BackColor = System.Drawing.SystemColors.Control;
			this.eqImg.Image = ((System.Drawing.Image)(resources.GetObject("eqImg.Image")));
			this.eqImg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.eqImg.InitialImage = null;
			this.eqImg.Location = new System.Drawing.Point(8, 8);
			this.eqImg.Name = "eqImg";
			this.eqImg.Size = new System.Drawing.Size(97, 97);
			this.eqImg.TabIndex = 1;
			this.eqImg.TabStop = false;
			// 
			// tqFrmStat
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(113, 113);
			this.Controls.Add(this.eqImg);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "tqFrmStat";
			this.PosCtr = new System.Drawing.Point(71, 71);
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Ogle";
			this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			((System.ComponentModel.ISupportInitialize)(this.eqImg)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox eqImg;
	}
}