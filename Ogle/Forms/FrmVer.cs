// FrmVer.cs
// ---------
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
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using nMisc;

namespace nOgle {
	/// <summary>
	/// Implements the Word Verification form.
	/// </summary>
	public partial class tqFrmVer: tqFrmOgle {
		/// <summary>
		/// The word being verified.
		/// </summary>
		readonly string eqWord;
		
		/// <summary>
		/// Set to 'true' if the word is accepted.
		/// </summary>
		public bool CkAdd { get; private set; }
		
		public tqFrmVer(string aqWord): base("ver.html") {
			aqWord.sThrowNull("aqWord");
			
			eqWord = aqWord;
			CkAdd = false;
			InitializeComponent();
		}
		
		// Events
		// ------
		
		/// <summary>
		/// Updates the Word button, adjusting its font size and trimming the word
		/// if necessary to make it fit.
		/// </summary>
		void eFrmVer_Shown(object aqSend, EventArgs aqArgs) {
			// Diminish the font size, if necessary, until the word fits:
			Int32 oWthMax = eqBtnWord.Width - 32;
			Font oqFont = eqBtnWord.Font;
			while (true) {
				Size oSize = TextRenderer.MeasureText(eqWord, oqFont);
				if (oSize.Width < oWthMax) break;
				oqFont = new Font(oqFont.FontFamily, oqFont.Size * 4 / 5, oqFont
					.Style, oqFont.Unit);
			}
			if (oqFont != eqBtnWord.Font) eqBtnWord.Font = oqFont;
			
			eqBtnWord.Text = eqWord;
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
		/// Visits the Wiktionary page for the word being verified.
		/// </summary>
		void eBtnWord_Click(object aqSend, EventArgs aqArgs) {
			string oqAddr = "http://en.wiktionary.org/wiki/" + eqWord;
			Process.Start(oqAddr);
		}
		
		/// <summary>
		/// Closes the form and signals that the word should be accepted.
		/// </summary>
		void eBtnAdd_Click(object aqSend, EventArgs aqArgs) {
			CkAdd = true;
			Close();
		}
		
		/// <summary>
		/// Closes the form and signals that the word should not be accepted.
		/// </summary>
		void eBtnCancel_Click(object aqSend, EventArgs aqArgs) {
			Close();
		}
	}
}
