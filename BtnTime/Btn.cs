// BtnTime.cs
// ----------
// Copyright ©2022 Jeremy Kelly
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
using System.Resources;
using System.Reflection;
using System.Drawing.Drawing2D;

namespace nBtnTime {
	/// <summary>
	/// Implements a button that draws zero or more horizontal bars within its
	/// body, useful for representing some quantity associated with the button.
	/// </summary>
	public partial class tqBtn: Button {
		public tqBtn() {
			HgtBar = 4;
			PadBar = 2;
			
			eBrGrad_Upd();
			eTimerBlink.Tick += eBars_Flip;
			
			InitializeComponent();
		}
		
		/// <summary>
		/// Updates the quantity bar gradient to fit the new size, and redraws the
		/// button.
		/// </summary>
		protected override void OnResize(EventArgs aqArgs) {
			base.OnResize(aqArgs);
			eBrGrad_Upd();
			Invalidate();
		}
		
		/// <summary>
		/// Draws the quantity bars atop the button.
		/// </summary>
		protected override void OnPaint(PaintEventArgs aqArgs) {
			base.OnPaint(aqArgs);
			if (!eVisBars) return;
			
			Graphics oqGr = aqArgs.Graphics;
			Int32 oTop = ClientRectangle.Height - esPosBars.Y + PadBar;
			for (Int32 o = 0; o < CtBar; ++o) {
				oTop -= (HgtBar + PadBar);
				oqGr.FillRectangle(eBrGrad, esPosBars.X, oTop, eWthBar, HgtBar);
				
				if (oTop < 0) break;
			}
		}
		
		// Bar layout
		// ----------
		
		/// <summary>
		/// The bottom-left corner of the bottom-most quantity bar.
		/// </summary>
		static readonly Point esPosBars = new Point(6, 6);
		
		/// <summary>
		/// The width of a single quantity bar.
		/// </summary>
		Int32 eWthBar {
			get {
				Int32 o = Width - (esPosBars.X * 2);
				if (o < 1) return 1;
				return o;
			}
		}
		
		/// <summary>
		/// Set to 'true' if the quantity bars are visible.
		/// </summary>
		bool eVisBars = true;
		
		/// <summary>
		/// Toggles the quantity bars' visibility and invalidates the control.
		/// </summary>
		void eBars_Flip(object aqSend, EventArgs aqArgs) {
			eVisBars = !eVisBars;
			Invalidate();
		}
		
		/// <summary>
		/// The maximum number of quantity bars to be displayed. Excess bars are
		/// ignored; they are not sized to fit.
		/// </summary>
		Int32 eCtBar;
		/// <summary>
		/// The maximum number of quantity bars to be displayed. Excess bars are
		/// ignored; they are not sized to fit.
		/// </summary>
		[Description("The maximum number of bars to display.")]
		public Int32 CtBar {
			get { return eCtBar; }
			
			set {
				if (value < 0) eCtBar = 0;
				else eCtBar = value;
				Invalidate();
			}
		}
		
		/// <summary>
		/// The height of a single quantity bar.
		/// </summary>
		Int32 eHgtBar;
		/// <summary>
		/// The height of a single quantity bar.
		/// </summary>
		[Description("The height of a single bar.")]
		public Int32 HgtBar {
			get { return eHgtBar; }
			
			set {
				if (value < 1) eHgtBar = 1;
				else eHgtBar = value;
				Invalidate();
			}
		}
		
		/// <summary>
		/// The vertical space between each quantity bar.
		/// </summary>
		Int32 ePadBar;
		/// <summary>
		/// The vertical space between each quantity bar.
		/// </summary>
		[Description("The vertical padding between bars.")]
		public Int32 PadBar {
			get { return ePadBar; }
			
			set {
				if (value < 0) ePadBar = 0;
				else ePadBar = value;
				Invalidate();
			}
		}
		
		// Bar colors
		// ----------
		
		/// <summary>
		/// The color on the left side of the bar gradient.
		/// </summary>
		Color eClrBarLeft = Color.FromArgb(146, 156, 111);
		/// <summary>
		/// The color on the left side of the bar gradient.
		/// </summary>
		[Description("The color on the left side of the bar gradient.")]
		public Color ClrBarLeft {
			get { return eClrBarLeft; }
			
			set {
				eClrBarLeft = value;
				eBrGrad_Upd();
				Invalidate();
			}
		}
		
		/// <summary>
		/// The color on the right side of the bar gradient.
		/// </summary>
		Color eClrBarRight = Color.FromArgb(204, 197, 156);
		/// <summary>
		/// The color on the right side of the bar gradient.
		/// </summary>
		[Description("The color on the right side of the bar gradient.")]
		public Color ClrBarRight {
			get { return eClrBarRight; }
			
			set {
				eClrBarRight = value;
				eBrGrad_Upd();
				Invalidate();
			}
		}
		
		/// <summary>
		/// The quantity bar gradient.
		/// </summary>
		LinearGradientBrush eBrGrad;
		
		/// <summary>
		/// Creates and assigns a new gradient to eBrGrad.
		/// </summary>
		void eBrGrad_Upd() {
			// Gradients are defined relative to the client rectangle, not the
			// fill rectangle, so the bar position is relevant:
			var oRect = new Rectangle(esPosBars.X + 1, 0, eWthBar, 1);
			eBrGrad = new LinearGradientBrush(oRect, ClrBarLeft, ClrBarRight, 0F);
		}
		
		// Bar animation
		// -------------
		
		/// <summary>
		/// The quantity bar blink timer.
		/// </summary>
		Timer eTimerBlink = new Timer();
		
		/// <summary>
		/// The quantity bar blink timeout, in milliseconds. Set to zero to
		/// disable blinking.
		/// </summary>
		Int32 eDurBlink = 0;
		/// <summary>
		/// The quantity bar blink timeout, in milliseconds. Set to zero to
		/// disable blinking.
		/// </summary>
		[Description("The number of milliseconds during which bars are visible "
			+ "or hidden when blinking. Set to zero to disable blinking.")]
		public Int32 DurBlink {
			get { return eDurBlink; }
			
			set {
				if (value < 1) {
					eDurBlink = 0;
					eTimerBlink.Enabled = false;
					eVisBars = true;
				}
				else {
					eDurBlink = value;
					eVisBars = false;
					eTimerBlink.Interval = eDurBlink;
					eTimerBlink.Enabled = true;
				}
				Invalidate();
			}
		}
	}
}
