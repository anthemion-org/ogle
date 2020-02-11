// Line.cs
// -------
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
using System.Windows.Forms;
using System.Drawing;

namespace nLine {
	/// <summary>
	/// Implements a control that draws a single horizontal or vertical line
	/// through the middle of its area.
	/// </summary>
	public partial class tqLine: Control {
		public tqLine() {
			eHorz = true;
			TabStop = false;
			InitializeComponent();
		}
		
		/// <summary>
		/// Set to 'true' if a horizontal line is to be drawn, 'false' if a
		/// vertical line is.
		/// </summary>
		bool eHorz;
		/// <summary>
		/// Set to 'true' if a horizontal line is to be drawn, 'false' if a
		/// vertical line is.
		/// </summary>
		public bool Horizontal {
			get { return eHorz; }
			set {
				eHorz = value;
				Invalidate();
			}
		}
		
		// Events
		// ------
		
		protected override void OnPaint(PaintEventArgs aqArgs) {
			base.OnPaint(aqArgs);
			
			Graphics oqGr = aqArgs.Graphics;
			using (Brush oqBr = new SolidBrush(ForeColor))
				using (Pen oqPen = new Pen(oqBr)) {
					Point oPtStart;
					Point oPtEnd;
					if (eHorz) {
						oPtStart = new Point(0, (Height / 2));
						oPtEnd = new Point(Width, (Height / 2));
					}
					else {
						oPtStart = new Point((Width / 2), 0);
						oPtEnd = new Point((Width / 2), Height);
					}
					oqGr.DrawLine(oqPen, oPtStart, oPtEnd);
				}
		}
	}
}
