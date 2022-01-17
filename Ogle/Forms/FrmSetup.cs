// FrmSetup.cs
// -----------
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
using System.Diagnostics;
using nText;

namespace nOgle {
	/// <summary>
	/// Implements the Setup form.
	/// </summary>
	public partial class tqFrmSetup: tqFrmOgle {
		public tqFrmSetup(): base("setup.html") {
			Next = tNext.QuitOgle;
			
			InitializeComponent();
		}
		
		/// <summary>
		/// Updates the Yield labels to fit the stored Yield value.
		/// </summary>
		void eLblsYld_Upd() {
			eqLblYld.Text = tqMain.sSetup.TextYld();
			
			string oqText;
			Int32? oCtMin = tqMain.sSetup.CtWordMin();
			Int32? oCtMax = tqMain.sSetup.CtWordMax();
			if (oCtMin.HasValue) {
				if (oCtMax.HasValue)
					oqText = string.Format("at least {0} and at most {1} words", oCtMin,
						oCtMax);
				else oqText = string.Format("at least {0} words", oCtMin);
			}
			else {
				if (oCtMax.HasValue)
					oqText = string.Format("at most {0} words", oCtMax);
				else oqText = "any number of words";
			}
			eqLblDtlYld.Text = "The board contains " + oqText;
		}
		
		/// <summary>
		/// Updates the Pace labels to fit the stored Pace value.
		/// </summary>
		void eLblsPace_Upd() {
			eqLblPace.Text = tqMain.sSetup.TextPace();
			
			Int32 oAdd = tqMain.sSetup.TimeAdd().Seconds;
			string oqStart = tqText.sText(tqMain.sSetup.TimeStart().Seconds);
			string oqAdd = tqText.sText(oAdd);
			string oqSuffPlur = ((oAdd > 1) ? "s" : "");
			string oqText = string.Format("The player starts with {0} seconds, and "
				+ "gains {1} second{2} for each letter over three in every entered "
				+ "word", oqStart, oqAdd, oqSuffPlur);
			eqLblDtlPace.Text = oqText;
		}
		
		// Events
		// ------
		
		/// <summary>
		/// Readies the form for display, updating all controls to fit the stored
		/// settings.
		/// </summary>
		void eFrmSetup_Shown(object aqSend, EventArgs aqArgs) {
			eqBarPace.Value = tqMain.sSetup.Pace;
			eqLblPace.Text = tqMain.sSetup.TextPace();
			eqBarYld.Value = tqMain.sSetup.Yld;
			eqLblYld.Text = tqMain.sSetup.TextYld();
			
			eLblsYld_Upd();
			eLblsPace_Upd();
		}
		
		/// <summary>
		/// Plays a sound when a slider is moved.
		/// </summary>
		//
		// The ValueChanged event is fired even when the value is set
		// programmatically, so the change sound must be played here.
		//
		// I tried defining this in tqFrmOgle, but the designer complained:
		void eCtl_ChgVal(object sender, EventArgs e) {
			cqMgrSound.SelDie_Play();
		}
		
		/// <summary>
		/// Stores the value specified by the Yield slider and updates the Yield
		/// labels.
		/// </summary>
		void eBarYld_ValueChanged(object aqSend, EventArgs aqArgs) {
			tqMain.sSetup.Yld = eqBarYld.Value;
			eLblsYld_Upd();
		}
		
		/// <summary>
		/// Stores the value specified by the Pace slider and updates the Pace
		/// labels.
		/// </summary>
		void eBarPace_ValueChanged(object aqSend, EventArgs aqArgs) {
			tqMain.sSetup.Pace = eqBarPace.Value;
			eLblsPace_Upd();
		}
		
		/// <summary>
		/// Plays a sound when a control is moused-over.
		/// </summary>
		//
		// I tried defining this in tqFrmOgle, but the designer complained:
		void eCtl_MouseEnter(object aqSend, EventArgs aqArgs) {
			cqMgrSound.MouseOver_Play();
		}
		
		/// <summary>
		/// Shows the About form.
		/// </summary>
		void eBtnAbout_Click(object aqSend, EventArgs aqArgs) {
			using (var oqFrm = new tqFrmAbout())
				oqFrm.ShowDialog();
		}
		
		/// <summary>
		/// Opens the main help page.
		/// </summary>
		void eBtnHelp_Click(object aqSend, EventArgs aqArgs) {
			string oqPath = "https://www.anthemion.org/ogle_help/index.html";
			Process.Start(oqPath);
		}
		
		/// <summary>
		/// Displays the Advanced Setup form, then stores the values specified
		/// there.
		/// </summary>
		void eBtnAdv_Click(object aqSend, EventArgs aqArgs) {
			using (var oqFrm = new tqFrmSetupAdv()) {
				oqFrm.ShowDialog();
				cqMgrSound.PerVol = tqMain.sSetupAdv.PerVol;
				cqMgrSound.Mute = tqMain.sSetupAdv.Mute;
			}
		}
		
		/// <summary>
		/// Starts a game.
		/// </summary>
		void eBtnPlay_Click(object aqSend, EventArgs aqArgs) {
			Next = tNext.Play;
			Close();
		}
	}
}
