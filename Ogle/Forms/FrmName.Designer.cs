namespace nOgle {
	partial class tqFrmName {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tqFrmName));
			this.eqEd = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.eqBtnOK = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.eqPanScore = new nOgle.tqPanScore();
			this.tqLine1 = new nLine.tqLine();
			this.SuspendLayout();
			// 
			// eqEd
			// 
			this.eqEd.Font = new System.Drawing.Font("Georgia", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.eqEd.Location = new System.Drawing.Point(14, 137);
			this.eqEd.Margin = new System.Windows.Forms.Padding(4);
			this.eqEd.MaxLength = 10;
			this.eqEd.Name = "eqEd";
			this.eqEd.Size = new System.Drawing.Size(315, 42);
			this.eqEd.TabIndex = 0;
			this.eqEd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.eqEd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.eEd_KeyPress);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Georgia", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(14, 66);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(315, 43);
			this.label1.TabIndex = 1;
			this.label1.Text = "High score!";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// eqBtnOK
			// 
			this.eqBtnOK.Location = new System.Drawing.Point(14, 192);
			this.eqBtnOK.Margin = new System.Windows.Forms.Padding(4);
			this.eqBtnOK.Name = "eqBtnOK";
			this.eqBtnOK.Size = new System.Drawing.Size(315, 30);
			this.eqBtnOK.TabIndex = 1;
			this.eqBtnOK.Text = "&OK";
			this.eqBtnOK.UseVisualStyleBackColor = true;
			this.eqBtnOK.Click += new System.EventHandler(this.eBtnOK_Click);
			this.eqBtnOK.MouseEnter += new System.EventHandler(this.eCtl_MouseEnter);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(14, 114);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(315, 21);
			this.label2.TabIndex = 3;
			this.label2.Text = "Enter name:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// eqPanScore
			// 
			this.eqPanScore.Font = new System.Drawing.Font("Georgia", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.eqPanScore.Location = new System.Drawing.Point(14, 10);
			this.eqPanScore.Margin = new System.Windows.Forms.Padding(4);
			this.eqPanScore.Name = "eqPanScore";
			this.eqPanScore.PtsComp = 999;
			this.eqPanScore.PtsPlay = 999;
			this.eqPanScore.Size = new System.Drawing.Size(315, 32);
			this.eqPanScore.TabIndex = 174;
			// 
			// tqLine1
			// 
			this.tqLine1.ForeColor = System.Drawing.SystemColors.ActiveBorder;
			this.tqLine1.Horizontal = true;
			this.tqLine1.Location = new System.Drawing.Point(14, 44);
			this.tqLine1.Margin = new System.Windows.Forms.Padding(4);
			this.tqLine1.Name = "tqLine1";
			this.tqLine1.Size = new System.Drawing.Size(315, 10);
			this.tqLine1.TabIndex = 175;
			this.tqLine1.TabStop = false;
			this.tqLine1.Text = "tqLine1";
			// 
			// tqFrmName
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(344, 237);
			this.Controls.Add(this.eqPanScore);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.eqBtnOK);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.eqEd);
			this.Controls.Add(this.tqLine1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "tqFrmName";
			this.PosCtr = new System.Drawing.Point(192, 170);
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Ogle Name Entry";
			this.Load += new System.EventHandler(this.eFrmName_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox eqEd;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button eqBtnOK;
		private System.Windows.Forms.Label label2;
		private tqPanScore eqPanScore;
		private nLine.tqLine tqLine1;
	}
}