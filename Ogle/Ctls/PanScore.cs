// PanScore.cs
// -----------
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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Resources;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using nMisc;

namespace nOgle {
	/// <summary>
	/// Implements a panel control displaying the player's point total, the
	/// computer's point total, and the player's percent score.
	/// </summary>
	public class tqPanScore: Panel {
		/// <summary>
		/// The player's point total.
		/// </summary>
		Int32 ePtsPlay;
		/// <summary>
		/// The player's point total.
		/// </summary>
		public Int32 PtsPlay {
			get {
				return ePtsPlay;
			}
			set {
				ePtsPlay = value;
				Invalidate();
			}
		}
		
		/// <summary>
		/// The computer's point total.
		/// </summary>
		Int32 ePtsComp;
		/// <summary>
		/// The computer's point total.
		/// </summary>
		public Int32 PtsComp {
			get {
				return ePtsComp;
			}
			set {
				ePtsComp = value;
				Invalidate();
			}
		}
		
		/// <summary>
		/// The brush used to render the name text.
		/// </summary>
		readonly Brush eqBrText;
		/// <summary>
		/// The brush used to label the score text.
		/// </summary>
		readonly Brush eqBrVal;
		
		public tqPanScore() {
			eqBrText = new SolidBrush(Color.Black);
			eqBrVal = new SolidBrush(Color.Peru);
		}
		
		protected override void OnPaint(PaintEventArgs aqArgs) {
			aqArgs.sThrowNull("aqArgs");
			
			base.OnPaint(aqArgs);
			
			Graphics oqGr = aqArgs.Graphics;
			oqGr.Clear(BackColor);
			
			const Int32 oPad = -4;
			
			// Display player score
			// --------------------
			
			string oqText = PtsPlay.ToString();
			var oPt = new Point();
			oqGr.DrawString(oqText, Font, eqBrVal, oPt);
			
			Size oSize = TextRenderer.MeasureText(oqText, Font);
			oPt = new Point(oSize.Width + oPad, 0);
			oqText = "Player";
			oqGr.DrawString(oqText, Font, eqBrText, oPt);
			
			oSize = TextRenderer.MeasureText(oqText, Font);
			Int32 oRightPlay = oPt.X + oSize.Width - 1;
			
			// Display computer score
			// ----------------------
			
			oqText = PtsComp.ToString();
			oSize = TextRenderer.MeasureText(oqText, Font);
			oPt = new Point(Width - oSize.Width, 0);
			oqGr.DrawString(oqText, Font, eqBrVal, oPt);
			
			oqText = "Ogle";
			oSize = TextRenderer.MeasureText(oqText, Font);
			oPt = new Point(oPt.X - oSize.Width - oPad, 0);
			oqGr.DrawString(oqText, Font, eqBrText, oPt);
			
			Int32 oLeftComp = oPt.X;
			
			// Display percent
			// ---------------
			// Center the percent text between the player labels, not necessarily in
			// the middle of the panel.
			
			if (PtsComp == 0) oqText = "\u221E%";
			else {
				Int32 oPer = (Int32)Math.Round((float)PtsPlay / (float)PtsComp * 100);
				oqText = string.Format("{0}%", oPer);
			}
			oSize = TextRenderer.MeasureText(oqText, Font);
			oPt = new Point(oRightPlay + (oLeftComp - oRightPlay - oSize.Width) / 2,
				0);
			oqGr.DrawString(oqText, Font, eqBrVal, oPt);
		}
	}
}
