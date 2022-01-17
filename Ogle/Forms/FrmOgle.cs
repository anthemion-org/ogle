// FrmOgle.cs
// ----------
// Copyright ©2007-2022 Jeremy Kelly
// Distributed under the terms of the GNU General Public License
// www.anthemion.org
// -----------------
// This file is part of Ogle.
//
// Ogle is free software: you can redistribute it and/or modify it under the
// terms of the GNU General Public License as published by the Free Software
// Foundation, either version 3 of the License, or (at your option) any later
// version.
//
// Ogle is distributed in the hope that it will be useful, but WITHOUT ANY
// WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
// FOR A PARTICULAR PURPOSE. See the GNU General Public License for more
// details.
//
// You should have received a copy of the GNU General Public License along
// with Ogle. If not, see <http://www.gnu.org/licenses/>.
// -----------------

using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using nMisc;

namespace nOgle {
	/// <summary>
	/// Implements features common to most forms in the application, including
	/// help display, and sound management.
	/// </summary>
	public class tqFrmOgle: tqFrmCtr, IDisposable {
		/// <summary>
		/// The sound manager.
		/// </summary>
		protected tqMgrSound cqMgrSound;
		
		/// <summary>
		/// The path to the help page to be displayed when the user presses F1.
		/// </summary>
		readonly string eqPathHelp;
		
		/// <summary>
		/// Creates an instance with no associated help page.
		/// </summary>
		public tqFrmOgle(): base() {
			Next = tNext.Play;
		}
		
		/// <summary>
		/// Creates an instance associated with the specified help page.
		/// </summary>
		public tqFrmOgle(string aqFileHelp): this() {
			aqFileHelp.sThrowNull("aqFileHelp");
			
			if (aqFileHelp.Length > 0) {
				string oqFold = Path.GetDirectoryName(Application.ExecutablePath);
			// Use 'Path.Join' or 'Path.Combine' here: [refactor]
				eqPathHelp = oqFold + "\\Help\\" + aqFileHelp;
			}
			KeyPreview = true;
		}
		
		// Events
		// ------
		
		/// <summary>
		/// Instantiates the sound manager.
		/// </summary>
		protected override void OnLoad(EventArgs aqArgs) {
			// DesignMode cannot be relied upon in the constructor, so the sound
			// manager cannot be instantiated there:
			cqMgrSound = new tqMgrSound(this, DesignMode, tqMain.sSetupAdv.PerVol,
				tqMain.sSetupAdv.Mute);
			base.OnLoad(aqArgs);
		}
		
		/// <summary>
		/// Plays the form show sound.
		/// </summary>
		protected override void OnShown(EventArgs aqArgs) {
			cqMgrSound.EntChall_Play();
			base.OnShown(aqArgs);
		}
		
		/// <summary>
		/// Opens the help page when F1 is pressed, if one is associated.
		/// </summary>
		protected override void OnKeyDown(KeyEventArgs aqArgs) {
			aqArgs.sThrowNull("aqArgs");
			
			if ((aqArgs.KeyCode == Keys.F1) && (eqPathHelp.Length > 0)) {
				if (File.Exists(eqPathHelp)) Process.Start(eqPathHelp);
				else MessageBox.Show("Help file not found", "Ogle", MessageBoxButtons
					.OK, MessageBoxIcon.Error);
			}
			base.OnKeyDown(aqArgs);
		}
		
		/// <summary>
		/// Gets the player's choice for the next action.
		/// </summary>
		//
		// Isn't there a better way to distinguish different close triggers in the
		// form closing event? FormClosingEventArgs.CloseReason is set to
		// UserClosing even when the form is closed programmatically: [design]
		[Browsable(false)]
		public tNext Next {
			get;
			protected set;
		}
		
		// IDisposable
		// -----------
		
		/// <summary>
		/// Disposes of the sound manager.
		/// </summary>
		public new void Dispose() {
			if (cqMgrSound != null) {
				cqMgrSound.Dispose();
				cqMgrSound = null;
			}
			base.Dispose();
		}
		
		/// <summary>
		/// Represents the player's choice for the next action.
		/// </summary>
		public enum tNext {
			/// <summary>
			/// Start a new game.
			/// </summary>
			Play = 0,
			/// <summary>
			/// End the current game without quitting Ogle.
			/// </summary>
			EndRound,
			/// <summary>
			/// Show the Setup form.
			/// </summary>
			Setup,
			/// <summary>
			/// Quit Ogle altogether.
			/// </summary>
			QuitOgle
		}

		private void InitializeComponent() {
			this.SuspendLayout();
			// 
			// tqFrmOgle
			// 
			this.ClientSize = new System.Drawing.Size(282, 255);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Name = "tqFrmOgle";
			this.PosCtr = new System.Drawing.Point(165, 165);
			this.ResumeLayout(false);

		}
	}
}
