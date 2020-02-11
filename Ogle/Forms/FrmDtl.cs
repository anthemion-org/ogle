// FrmDtl.cs
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
using nCtl;
using nMisc;

namespace nOgle {
	/// <summary>
	/// Implements the Word detail form, used to review an entry pair found on
	/// the score form.
	/// </summary>
	public partial class tqFrmDtl: tqFrmOgle {
		/// <summary>
		/// The entry pair being displayed.
		/// </summary>
		readonly tPair ePair;
		
		/// <summary>
		/// The font used to display entry states.
		/// </summary>
		readonly Font eqFontSt;
		/// <summary>
		/// The brush used to display entry states.
		/// </summary>
		readonly Brush eqBrSt;
		
		/// <summary>
		/// Creates an instance to display the specified entry pair.
		/// </summary>
		public tqFrmDtl(tPair aPair): base("detail.html") {
			ePair = aPair;
			eqFontSt = new Font("Georgia", 30, FontStyle.Bold, GraphicsUnit.Pixel);
			eqBrSt = new SolidBrush(this.ForeColor);
			
			InitializeComponent();
		}
		
		/// <summary>
		/// Updates the specified score label.
		/// </summary>
		static void esLblsScore_Upd(tqSel aqSel, bool aPlay, Label aqLbl, Label
			aqLblLeg) {
			
			aqLbl.sThrowNull("aqLbl");
			aqLblLeg.sThrowNull("aqLblLeg");
			
			if (aqSel == null) aqLbl.Text = "0";
			else {
				string oqText = aqSel.Score(aPlay).ToString();
				aqLbl.Text = oqText;
				if (oqText == "1") aqLblLeg.Text = "Point";
			}
		}
		
		/// <summary>
		/// Draws entry state text at the specified position, rotated to the
		/// specified orientation.
		/// </summary>
		void eBoxScore_Draw(Graphics aqGr, tqSel aqSel, PointF aPos, float aAng) {
			aqGr.sThrowNull("aqGr");
			
			string oqText;
			if (aqSel == null) oqText = "missed";
			else switch (aqSel.St) {
				case tqSel.tSt.Follow:
					oqText = "followed";
					break;
				case tqSel.tSt.Dup:
					oqText = "duplicate";
					break;
				default:
					oqText = "valid";
					break;
			}
			
			StringFormat oqFmt = new StringFormat();
			oqFmt.Alignment = StringAlignment.Center;
			oqFmt.LineAlignment = StringAlignment.Center;
			
			aqGr.TranslateTransform(aPos.X, aPos.Y);
			aqGr.RotateTransform(aAng);
			aqGr.DrawString(oqText, eqFontSt, eqBrSt, 0, 0, oqFmt);
			aqGr.ResetTransform();
		}
		
		// Events
		// ------
		
		/// <summary>
		/// Prepares the form for display, readying graphics resources and
		/// updating labels.
		/// </summary>
		void eFrmDtl_Load(object aqSend, EventArgs aqArgs) {
			tqSel oqSel = ePair.SelPref();
			eqGrid.qSel = oqSel;
			
			string oqText = tCtl.sTextFitRight(ePair.qText, eqBtnSel.Font, eqBtnSel
				.Width - 32);
			eqBtnSel.Text = oqText;
			
			eqLblLen.Text = ePair.qText.Length.ToString();
			esLblsScore_Upd(ePair.qSelPlay, true, eqLblScorePlay,
				eqLblLegScorePlay);
			esLblsScore_Upd(ePair.qSelComp, false, eqLblScoreComp,
				eqLblLegScoreComp);
		}
		
		/// <summary>
		/// Draws state text for the player's entry.
		/// </summary>
		void eBoxPlay_Paint(object aqSend, PaintEventArgs aqArgs) {
			aqArgs.sThrowNull("aqArgs");
			
			eBoxScore_Draw(aqArgs.Graphics, ePair.qSelPlay, new PointF(36, 180),
				270);
		}
		
		/// <summary>
		/// Draws state text for the computer's entry.
		/// </summary>
		void eBoxComp_Paint(object aqSend, PaintEventArgs aqArgs) {
			aqArgs.sThrowNull("aqArgs");
			
			eBoxScore_Draw(aqArgs.Graphics, ePair.qSelComp, new PointF(32, 116),
				90);
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
		/// Visits the Wiktionary page for the entered word.
		/// </summary>
		void eBtnSel_Click(object aqSend, EventArgs aqArgs) {
			try {
				string oqAddr = "http://en.wiktionary.org/wiki/" + ePair.qText;
				// This sometimes generates a 'file not found' exception:
				Process.Start(oqAddr);
			}
			catch {}
		}
		
		/// <summary>
		/// Closes the form.
		/// </summary>
		void eBtnOK_Click(object aqSend, EventArgs aqArgs) {
			Close();
		}
	}
}