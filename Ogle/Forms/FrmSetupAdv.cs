// FrmSetupAdv.cs
// --------------
// Copyright ©2011 Jeremy Kelly
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace nOgle {
	/// <summary>
	/// Implements the Advanced Setup form.
	/// </summary>
	public partial class tqFrmSetupAdv: tqFrmOgle {
		public tqFrmSetupAdv(): base("setup_adv.html") {
			InitializeComponent();
		}
		
		// Events
		// ------
		
		/// <summary>
		/// Plays a sound when a control is moused-over.
		/// </summary>
		//
		// I tried defining this in tqFrmOgle, but the designer complained:
		void eCtl_MouseEnter(object aqSend, EventArgs aqArgs) {
			cqMgrSound.MouseOver_Play();
		}
		
		/// <summary>
		/// Plays a sound when a control value is changed, unmuting the Sound
		/// Manager first if that control is the Volume slider.
		/// </summary>
		//
		// I tried defining this in tqFrmOgle, but the designer complained:
		void eCtl_ChgVal(object aqSend, EventArgs aqArgs) {
			if (tqMain.sSetupAdv.Mute) {
				tqMain.sSetupAdv.Mute = false;
				cqMgrSound.Mute = false;
				eqCkMute.Checked = false;
			}
			cqMgrSound.SelDie_Play();
		}
		
		/// <summary>
		/// Sets all controls to match the stored settings values.
		/// </summary>
		void eFrmSetupAdv_Load(object aqSend, EventArgs aqArgs) {
			eqBarVol.Value = tqMain.sSetupAdv.PerVol;
			eqCkMute.Checked = tqMain.sSetupAdv.Mute;
			eqEdDial.Text = tqMain.sSetupAdv.Dial.ToString();
		}
		
		/// <summary>
		/// Stores the value specified by the Volume slider.
		/// </summary>
		void eBarVol_ValueChanged(object aqSend, EventArgs aqArgs) {
			tqMain.sSetupAdv.PerVol = eqBarVol.Value;
			cqMgrSound.PerVol = eqBarVol.Value;
		}
		
		/// <summary>
		/// Stores the value specified by the Mute checkbox.
		/// </summary>
		void eCkMute_Click(object aqSend, EventArgs aqArgs) {
			tqMain.sSetupAdv.Mute = eqCkMute.Checked;
			cqMgrSound.Mute = eqCkMute.Checked;
			cqMgrSound.SelDie_Play();
		}
		
		/// <summary>
		/// Stores the value specified by the Dialect combo box.
		/// </summary>
		private void eEdDial_SelectedIndexChanged(object aqSend, EventArgs aqArgs)
		{
			switch (eqEdDial.Text) {
				case "American":
					tqMain.sSetupAdv.Dial = tSetupAdv.tDial.American;
					break;
				case "British":
					tqMain.sSetupAdv.Dial = tSetupAdv.tDial.British;
					break;
				case "Canadian":
					tqMain.sSetupAdv.Dial = tSetupAdv.tDial.Canadian;
					break;
			}
		}
		
		/// <summary>
		/// Plays a sound when a Dialect combo box entry is selected.
		/// </summary>
		void eEdDial_SelectionChangeCommitted(object aqSend, EventArgs aqArgs) {
			cqMgrSound.SelDie_Play();
		}
		
		/// <summary>
		/// Closes the form.
		/// </summary>
		void eBtnOK_Click(object aqSend, EventArgs aqArgs) {
			Close();
		}
	}
}
