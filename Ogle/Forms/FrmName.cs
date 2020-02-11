// FrmName.cs
// ----------
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
using nMisc;

namespace nOgle {
	/// <summary>
	/// Implements the Name form, used to gather the player's name when a high
	/// score is achieved.
	/// </summary>
	public partial class tqFrmName: tqFrmOgle {
		/// <summary>
		/// The player's point total.
		/// </summary>
		readonly Int32 ePtsPlay;
		/// <summary>
		/// The computer's point total.
		/// </summary>
		readonly Int32 ePtsComp;
		/// <summary>
		/// Text to be displayed in the edit control when the form first appears.
		/// </summary>
		readonly string eqTextDef;
		
		/// <summary>
		/// The text in the edit control, trimmed of whitespace.
		/// </summary>
		public string qText {
			get { return eqEd.Text.Trim(); }
		}
		
		/// <param name="aPtsPlay">
		/// The player's point total.
		/// </param>
		/// <param name="aPtsComp">
		/// The computer's point total.
		/// </param>
		/// <param name="aqTextDef">
		/// Text to be displayed in the edit control when the form first appears.
		/// </param>
		public tqFrmName(Int32 aPtsPlay, Int32 aPtsComp, string aqTextDef) {
			ePtsPlay = aPtsPlay;
			ePtsComp = aPtsComp;
			eqTextDef = aqTextDef;
			InitializeComponent();
		}
		
		// Events
		// ------
		
		/// <summary>
		/// Updates the score panel and the edit control.
		/// </summary>
		void eFrmName_Load(object aqSend, EventArgs aqArgs) {
			eqPanScore.PtsPlay = ePtsPlay;
			eqPanScore.PtsComp = ePtsComp;
			if (eqTextDef != null) eqEd.Text = eqTextDef;
			
			// The form is not sized correctly, for some reason, if this value is
			// set in the designer and PosCtr is set before the form is shown:
			ControlBox = false;
		}
		
		/// <summary>
		/// Plays the keypress sound, and closes the form when a carriage return
		/// is entered.
		/// </summary>
		void eEd_KeyPress(object aqSend, KeyPressEventArgs aqArgs) {
			aqArgs.sThrowNull("aqArgs");
			
			if (aqArgs.KeyChar == '\r') {
				// This prevents the 'asterisk' sound from being played:
				aqArgs.Handled = true;
				Close();
			}
			else cqMgrSound.SelDie_Play();
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
		/// Closes the form.
		/// </summary>
		void eBtnOK_Click(object aqSend, EventArgs aqArgs) {
			Close();
		}
	}
}
