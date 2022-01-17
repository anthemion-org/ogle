// Grid.cs
// -------
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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Resources;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using nDir;
using nMisc;

namespace nOgle {
	/// <summary>
	/// Signals a change to the letter grid selection.
	/// </summary>
	public class tqArgsChg: EventArgs {
		/// <summary>
		/// The type of change made to the selection.
		/// </summary>
		public readonly tType Type;
		
		public tqArgsChg(tType aType): base() {
			Type = aType;
		}
		
		/// <summary>
		/// Describes a change to the letter grid selection.
		/// </summary>
		public enum tType {
			/// <summary>
			/// An element was selected.
			/// </summary>
			Sel,
			/// <summary>
			/// An element was unselected.
			/// </summary>
			Unsel,
			/// <summary>
			/// The selection was cleared.
			/// </summary>
			Cl
		}
	}
	
	/// <summary>
	/// Responds to a change to the letter grid selection.
	/// </summary>
	public delegate void tdHandChg(object aqSend, tqArgsChg aqArgs);
	
	/// <summary>
	/// Stores a letter grid selection.
	/// </summary>
	public class tqArgsSel: EventArgs {
		/// <summary>
		/// The entered selection.
		/// </summary>
		public readonly tqSel qSel;
		
		public tqArgsSel(tqSel aqSel): base() {
			qSel = aqSel;
		}
	}
	
	/// <summary>
	/// Responds to the entry of a letter grid selection.
	/// </summary>
	public delegate void tdHandEnt(object aqSend, tqArgsSel aqArgs);
	
	/// <summary>
	/// Implements the letter grid, used to display the board and accept or
	/// display word entries.
	/// </summary>
	public partial class tqGrid: Control {
		/// <summary>
		/// The dimensions of the grid, in squares.
		/// </summary>
		static readonly Size esSize = tqMain.sSizeGrid;
		
		/// <summary>
		/// The board to be displayed, or 'null' if no display is desired.
		/// </summary>
		tqBoard eqBoard;
		/// <summary>
		/// The board overlay, or 'null' if no overlay is needed.
		/// </summary>
		tqOver eqOver;
		/// <summary>
		/// The current selection, or 'null' if no selection is to be accepted or
		/// displayed.
		/// </summary>
		tqSel eqSel;
		
		/// <summary>
		/// The last moused-over square, or 'null' if no square has been moused-
		/// over.
		/// </summary>
		Point? eSqNextLast = null;
		
		/// <summary>
		/// Creates an instance, loading various images from the Main resource.
		/// </summary>
		public tqGrid() {
			Assembly oqAsm = Assembly.GetExecutingAssembly();
			var oqRes = new ResourceManager("Ogle.Main", oqAsm);
			
			eqImgBack = (Image)oqRes.GetObject("BackGrid");
			
			eqImgDie = (Image)oqRes.GetObject("Die");
			eqImgDieEmph = (Image)oqRes.GetObject("DieEmph");
			
			eqImgSel = (Image)oqRes.GetObject("SelDie");
			eqImgCurs = (Image)oqRes.GetObject("CursDie");
			
			eqImgUnderLeft = (Image)oqRes.GetObject("UnderDieLeft");
			eqImgUnderTop = (Image)oqRes.GetObject("UnderDieTop");
			eqImgUnderRight = (Image)oqRes.GetObject("UnderDieRight");
			eqImgUnderBtm = (Image)oqRes.GetObject("UnderDieBtm");
			
			eqImgLinkHorz = (Image)oqRes.GetObject("LinkDieHorz");
			eqImgLinkVert = (Image)oqRes.GetObject("LinkDieVert");
			eqImgLinkDown = (Image)oqRes.GetObject("LinkDieDown");
			eqImgLinkUp = (Image)oqRes.GetObject("LinkDieUp");
			
			eqFmtTextDie = new StringFormat();
			eqFmtTextDie.Alignment = StringAlignment.Center;
			eqFmtTextDie.LineAlignment = StringAlignment.Center;
			
			SetStyle(ControlStyles.UserPaint
				| ControlStyles.AllPaintingInWmPaint
				| ControlStyles.OptimizedDoubleBuffer, true);
			InitializeComponent();
		}
		
		/// <summary>
		/// The board to be displayed, or 'null' if no display is desired. Setting
		/// the board clears the selection.
		/// </summary>
		[Browsable(false)]
		public tqBoard qBoard {
			get { return eqBoard; }
			set {
				eqBoard = value;
				eqSel = (value == null) ? null : new tqSel(value);
				Invalidate();
			}
		}
		
		/// <summary>
		/// The board overlay, or 'null' if no overlay is needed.
		/// </summary>
		[Browsable(false)]
		public tqOver qOver {
			get { return eqOver; }
			set {
				eqOver = value;
				Invalidate();
			}
		}
		
		/// <summary>
		/// The visible selection, or 'null' if nothing is selected.
		/// </summary>
		[Browsable(false)]
		public tqSel qSel {
			get { return eqSel; }
			set {
				eqSel = value;
				eqBoard = (value == null) ? null : value.qBoard;
				Invalidate();
			}
		}
		
		/// <summary>
		/// The die text color.
		/// </summary>
		Color eClrText = Color.Black;
		[Description("The die text color.")]
		public Color ClrText {
			get { return eClrText; }
			set {
				eClrText = value;
				eBrsText = new tBrs(ClrText, ClrTextAccent);
				Invalidate();
			}
		}
		
		/// <summary>
		/// The die text accent color.
		/// </summary>
		Color eClrTextAccent = Color.DarkGray;
		[Description("The die text accent color.")]
		public Color ClrTextAccent {
			get { return eClrTextAccent; }
			set {
				eClrTextAccent = value;
				eBrsText = new tBrs(ClrText, ClrTextAccent);
				Invalidate();
			}
		}
		
		/// <summary>
		/// The die mask color.
		/// </summary>
		Color eClrMask = Color.Gray;
		[Description("The die mask color.")]
		public Color ClrMask {
			get { return eClrMask; }
			set {
				eClrMask = value;
				eBrsMask = new tBrs(ClrMask, ClrMaskAccent);
				Invalidate();
			}
		}
		
		/// <summary>
		/// The die mask accent color.
		/// </summary>
		Color eClrMaskAccent = Color.LightGray;
		[Description("The die mask accent color.")]
		public Color ClrMaskAccent {
			get { return eClrMaskAccent; }
			set {
				eClrMaskAccent = value;
				eBrsMask = new tBrs(ClrMask, ClrMaskAccent);
				Invalidate();
			}
		}
		
		/// <summary>
		/// Set to 'true' if the mask is visible.
		/// </summary>
		bool eVisMask;
		[Description("Set to 'true' if the mask is visible.")]
		public bool VisMask {
			get { return eVisMask; }
			set {
				eVisMask = value;
				if (!value) SqCurs = null;
				Invalidate();
			}
		}
		
		/// <summary>
		/// The cursor square, or 'null' if no cursor should be displayed.
		/// </summary>
		Point? eSqCurs;
		[Description("The cursor square, or 'null' if no cursor should be "
			+ "displayed.")]
		public Point? SqCurs {
			get { return eSqCurs; }
			set {
				eSqCurs = value;
				Invalidate();
			}
		}
		
		/// <summary>
		/// Set to 'true' if the user should be allowed to edit the entry.
		/// </summary>
		[Description("Set to 'true' if the user should be allowed to edit the "
			+ "entry.")]
		public bool Editable {
			get;
			set;
		}
		
		/// <summary>
		/// Occurs when the user moves the mouse to a selectable die.
		/// </summary>
		[Description("Occurs when the user moves the mouse to a selectable die.")]
		public event EventHandler OverNext;
		
		/// <summary>
		/// Occurs when the user changes the word selection.
		/// </summary>
		[Description("Occurs when the user changes the word selection.")]
		public event tdHandChg ChgSel;
		
		/// <summary>
		/// Occurs when the user enters the word selection.
		/// </summary>
		[Description("Occurs when the user enters the word selection.")]
		public event tdHandEnt EntSel;
		
		// Display
		// -------
		
		/// <summary>
		/// The size of the background image.
		/// </summary>
		const Int32 eSizeBack = 298;
		/// <summary>
		/// The size of the die image.
		/// </summary>
		const Int32 eSizeDie = 60;
		/// <summary>
		/// The position of the top-left die relative to the background.
		/// </summary>
		const Int32 eOffDie = 1;
		/// <summary>
		/// The size of the border within the die image.
		/// </summary>
		const Int32 eSizeBordDie = 1;
		
		/// <summary>
		/// The grid background image.
		/// </summary>
		readonly Image eqImgBack;
		
		/// <summary>
		/// The die image.
		/// </summary>
		readonly Image eqImgDie;
		/// <summary>
		/// The emphasized die image.
		/// </summary>
		readonly Image eqImgDieEmph;
		
		/// <summary>
		/// The die selection image.
		/// </summary>
		readonly Image eqImgSel;
		/// <summary>
		/// The die cursor image.
		/// </summary>
		readonly Image eqImgCurs;
		
		/// <summary>
		/// The left-side text underline image.
		/// </summary>
		readonly Image eqImgUnderLeft;
		/// <summary>
		/// The top-side text underline image.
		/// </summary>
		readonly Image eqImgUnderTop;
		/// <summary>
		/// The right-side text underline image.
		/// </summary>
		readonly Image eqImgUnderRight;
		/// <summary>
		/// The bottom-side text underline image.
		/// </summary>
		readonly Image eqImgUnderBtm;
		
		/// <summary>
		/// The horizontal selection link image.
		/// </summary>
		readonly Image eqImgLinkHorz;
		/// <summary>
		/// The vertical selection link image.
		/// </summary>
		readonly Image eqImgLinkVert;
		/// <summary>
		/// The top-left to bottom-right diagonal selection link image.
		/// </summary>
		readonly Image eqImgLinkDown;
		/// <summary>
		/// The bottom-left to top-right diagonal selection link image.
		/// </summary>
		readonly Image eqImgLinkUp;
		
		/// <summary>
		/// The die text format.
		/// </summary>
		readonly StringFormat eqFmtTextDie;
		
		/// <summary>
		/// The multi-letter die font.
		/// </summary>
		Font eqFontTextSm;
		/// <summary>
		/// The masked die font.
		/// </summary>
		Font eqFontMask;
		
		/// <summary>
		/// The die text color.
		/// </summary>
		tBrs eBrsText;
		/// <summary>
		/// The masked die text color.
		/// </summary>
		tBrs eBrsMask;
		
		/// <summary>
		/// Displays the grid background.
		/// </summary>
		void eBack_Draw(Graphics aqGr) {
			aqGr.sThrowNull("aqGr");
			
			var oRect = new Rectangle(0, 0, eSizeBack, eSizeBack);
			aqGr.DrawImage(eqImgBack, oRect);
		}
		
		/// <summary>
		/// Displays all die faces without displaying their text.
		/// </summary>
		void eFaces_Draw(Graphics aqGr) {
			aqGr.sThrowNull("aqGr");
			
			var oSize = new Size(eSizeDie, eSizeDie);
			for (Int32 oY = 0; oY < esSize.Height; ++oY) {
				for (Int32 oX = 0; oX < esSize.Width; ++oX) {
					Image oqImg;
					if (eqOver == null) oqImg = eqImgDie;
					else
						switch (eqOver[oX, oY]) {
							case tqOver.tSt.Emph:
								oqImg = eqImgDieEmph;
								break;
							default:
								oqImg = eqImgDie;
								break;
						}
					var oRect = new Rectangle(eCliFromSq(new Point(oX, oY)), oSize);
					aqGr.DrawImage(oqImg, oRect);
				}
			}
		}
		
		/// <summary>
		/// Displays the die cursor.
		/// </summary>
		void eCurs_Draw(Graphics aqGr) {
			aqGr.sThrowNull("aqGr");
			
			if (SqCurs == null) return;
			
			Point oPos = eCliFromSq(SqCurs.Value);
			var oRect = new Rectangle(oPos.X, oPos.Y, eSizeDie, eSizeDie);
			aqGr.DrawImage(eqImgCurs, oRect);
		}
		
		/// <summary>
		/// Displays the die text.
		/// </summary>
		void eText_Draw(Graphics aqGr) {
			aqGr.sThrowNull("aqGr");
			
			if (qBoard == null) return;
			
			var oSize = new Size(eSizeDie, eSizeDie);
			for (Int32 oY = 0; oY < esSize.Height; ++oY)
				for (Int32 oX = 0; oX < esSize.Width; ++oX) {
					tDie oDie = qBoard[oX, oY];
					
					oDie.qText.sThrowNull("oDie.qText");
					
					Font oqFont;
					// For some reason, the text doesn't align correctly in Windows 7:
					int oAdj;
					if (oDie.qText.Length > 1) {
						oqFont = eqFontTextSm;
						oAdj = -1;
					}
					else {
						oqFont = Font;
						oAdj = 2;
					}
					
					// Set the coordinate system origin to the center of the die:
					Point oPos = eCliFromSq(new Point(oX, oY));
					Int32 oOffCtr = eSizeDie / 2;
					aqGr.TranslateTransform(oPos.X + oOffCtr, oPos.Y + oOffCtr);
					
					// Rotate the coordinate system around the origin:
					float oAng = (tDir.sDeg(oDie.Orient) + 90) % 360;
					aqGr.RotateTransform(oAng);
					
					LinearGradientBrush oqBr = eBrsText[oDie.Orient];
					aqGr.DrawString(oDie.qText, oqFont, oqBr, 0, oAdj, eqFmtTextDie);
					
					aqGr.ResetTransform();
					
					if (oDie.Under) {
						Image oImg;
						// For some reason, certain orientations don't align correctly on
						// their own. I don't believe there's anything wrong with the
						// images:
						switch (oDie.Orient) {
							case tDir4.E:
								oImg = eqImgUnderLeft;
								oPos.X -= oAdj;
								break;
							case tDir4.S:
								oImg = eqImgUnderTop;
								oPos.Y += (1 - oAdj);
								break;
							case tDir4.W:
								oImg = eqImgUnderRight;
								oPos.X += oAdj;
								break;
							// tDir4.N:
							default:
								oImg = eqImgUnderBtm;
								oPos.Y += oAdj;
								break;
						}
						
						var oRect = new Rectangle(oPos, oSize);
						aqGr.DrawImage(oImg, oRect);
					}
				}
		}
		
		/// <summary>
		/// Displays the die mask text.
		/// </summary>
		void eMasks_Draw(Graphics aqGr) {
			aqGr.sThrowNull("aqGr");
			
			if (qBoard == null) return;
			
			for (Int32 oY = 0; oY < esSize.Height; ++oY)
				for (Int32 oX = 0; oX < esSize.Width; ++oX) {
					tDie oDie = qBoard[oX, oY];
					float oAng = (tDir.sDeg(oDie.Orient) + 90) % 360;
					
					Point oPos = eCliFromSq(new Point(oX, oY));
					aqGr.TranslateTransform(oPos.X + (eSizeDie / 2), oPos.Y + (eSizeDie
						/ 2));
					aqGr.RotateTransform(oAng);
					
					aqGr.DrawString("?", eqFontMask, eBrsMask[oDie.Orient], 0, 3,
						eqFmtTextDie);
					aqGr.ResetTransform();
				}
		}
		
		/// <summary>
		/// Displays the selection images.
		/// </summary>
		void eSel_Draw(Graphics aqGr) {
			aqGr.sThrowNull("aqGr");
			
			if (qSel == null) return;
			
			var oSizeDie = new Size(eSizeDie, eSizeDie);
			foreach (tqSel.tqNode oqNode in eqSel.qNodes()) {
				oqNode.sThrowNull("oqNode");
				
				Point oSq = oqNode.Sq;
				
				// Selection image
				// ---------------
				
				Point oPos = eCliFromSq(oSq);
				var oRect = new Rectangle(oPos, oSizeDie);
				aqGr.DrawImage(eqImgSel, oRect);
				
				if (oqNode.DirPrev == null) continue;
				
				// Link image
				// ----------
				
				Image oImg;
				var oSizeLink = new Size(eSizeDie, eSizeDie);
				const Int32 oLenDbl = (eSizeDie * 2) - eSizeBordDie;
				switch (oqNode.DirPrev.Value) {
					case tDir8.E:
						oImg = eqImgLinkHorz;
						oSizeLink.Width = oLenDbl;
						break;
					case tDir8.SE:
						oImg = eqImgLinkDown;
						oSizeLink.Width = oLenDbl;
						oSizeLink.Height = oLenDbl;
						break;
					case tDir8.S:
						oImg = eqImgLinkVert;
						oSizeLink.Height = oLenDbl;
						break;
					case tDir8.SW:
						oImg = eqImgLinkUp;
						oSq.X -= 1;
						oSizeLink.Width = oLenDbl;
						oSizeLink.Height = oLenDbl;
						break;
					case tDir8.W:
						oImg = eqImgLinkHorz;
						oSq.X -= 1;
						oSizeLink.Width = oLenDbl;
						break;
					case tDir8.NW:
						oImg = eqImgLinkDown;
						oSq.X -= 1;
						oSq.Y -= 1;
						oSizeLink.Width = oLenDbl;
						oSizeLink.Height = oLenDbl;
						break;
					case tDir8.N:
						oImg = eqImgLinkVert;
						oSq.Y -= 1;
						oSizeLink.Height = oLenDbl;
						break;
					// tDir8.NE:
					default:
						oImg = eqImgLinkUp;
						oSq.Y -= 1;
						oSizeLink.Width = oLenDbl;
						oSizeLink.Height = oLenDbl;
						break;
				}
				
				oPos = eCliFromSq(oSq);
				oRect = new Rectangle(oPos, oSizeLink);
				aqGr.DrawImage(oImg, oRect);
			}
		}
		
		/// <summary>
		/// Updates the small text and mask fonts, basing them on Control.Font.
		/// </summary>
		protected override void OnFontChanged(EventArgs aqArgs) {
			base.OnFontChanged(aqArgs);
			
			// This propagates user-selected attributes like 'bold':
			FontStyle oSt = Font.Style;
			
			float oSize = (float)(Font.Size * 0.8);
			eqFontTextSm = new Font(Font.Name, oSize, oSt, Font.Unit);
			
			oSize = (float)(Font.Size * 1.25);
			eqFontMask = new Font(Font.Name, oSize, oSt, Font.Unit);
		}
		
		protected override void OnPaint(PaintEventArgs aqArgs) {
			aqArgs.sThrowNull("aqArgs");
			
			base.OnPaint(aqArgs);
			
			Graphics oqGr = aqArgs.Graphics;
			oqGr.Clear(BackColor);
			eBack_Draw(oqGr);
			eFaces_Draw(oqGr);
			
			if (VisMask) eMasks_Draw(oqGr);
			else {
				eSel_Draw(oqGr);
				eCurs_Draw(oqGr);
				eText_Draw(oqGr);
			}
		}
		
		// Input
		// -----
		
		/// <summary>
		/// Triggers the OverNext event if a selectable die is moused-over.
		/// </summary>
		protected override void OnMouseMove(MouseEventArgs aqArgs) {
			aqArgs.sThrowNull("aqArgs");
			
			if ((qBoard == null) || (qSel == null) || !Editable || VisMask) {
				base.OnMouseMove(aqArgs);
				return;
			}
			
			Point oSq = eSqFromCli(aqArgs.X, aqArgs.Y);
			if (eqSel.CkNext(oSq)) {
				if (oSq != SqCurs) {
					if (oSq != eSqNextLast) OverNext(this, new EventArgs());
					SqCurs = oSq;
				}
			}
			else SqCurs = null;
			eSqNextLast = oSq;
			
			base.OnMouseMove(aqArgs);
		}
		
		/// <summary>
		/// Updates the selection in response to mouse clicks, invalidates the
		/// control, and triggers the ChgSel event.
		/// </summary>
		//
		// OnMouseDown works better when the user clicks elements quickly than
		// does OnMouseClick.
		protected override void OnMouseDown(MouseEventArgs aqArgs) {
			aqArgs.sThrowNull("aqArgs");
			
			if ((qBoard == null) || (qSel == null) || !Editable || VisMask) {
				base.OnMouseClick(aqArgs);
				return;
			}
			
			Point oSq = eSqFromCli(aqArgs.X, aqArgs.Y);
			if (aqArgs.Button == MouseButtons.Left) {
				// If the die is already selected, unselect it and all those that
				// follow it:
				if (eqSel.Ck(oSq)) {
					eqSel.Drop(oSq);
					ChgSel(this, new tqArgsChg(tqArgsChg.tType.Unsel));
					Invalidate();
					return;
				}
				
				if (!eqSel.CkNext(oSq)) return;
				
				eqSel.Add(oSq);
				ChgSel(this, new tqArgsChg(tqArgsChg.tType.Sel));
				Invalidate();
			}
			else if (aqArgs.Button == MouseButtons.Middle) {
				if (!eqSel.Ck()) return;
				
				eqSel.Cl();
				ChgSel(this, new tqArgsChg(tqArgsChg.tType.Cl));
				Invalidate();
			}
			else if (aqArgs.Button == MouseButtons.Right) {
				if (!eqSel.Ck() || (eqSel.qText.Length < tqMain.sLenEntMin)) return;
				
				// The entry must be copied, as this instance will be reused:
				tqSel oqSel = new tqSel(eqSel);
				eqSel.Cl();
				Invalidate();
				
				EntSel(this, new tqArgsSel(oqSel));
			}
			base.OnMouseClick(aqArgs);
		}
		
		// Miscellanea
		// -----------
		
		/// <summary>
		/// Returns the square at the specified client position.
		/// </summary>
		Point eSqFromCli(Int32 aX, Int32 aY) {
			aX = (aX - eOffDie) / (eSizeDie - eSizeBordDie);
			// By clicking and dragging off the control it is possible to receive
			// mouse coordinates outside the client area:
			if (aX < 0) aX = 0;
			else if (aX >= esSize.Width) aX = esSize.Width - 1;
			
			aY = (aY - eOffDie) / (eSizeDie - eSizeBordDie);
			if (aY < 0) aY = 0;
			else if (aY >= esSize.Height) aY = esSize.Height - 1;
			
			return new Point(aX, aY);
		}
		
		/// <summary>
		/// Returns the top-left client position of the specified square.
		/// </summary>
		Point eCliFromSq(Point aSq) {
			return new Point(eOffDie + (aSq.X * (eSizeDie - eSizeBordDie)),
				eOffDie + (aSq.Y * (eSizeDie - eSizeBordDie)));
		}
	}
	
	/// <summary>
	/// Stores four gradient brushes, one for each die orientation. The
	/// gradients are set such that, when rotated to match the orientation
	/// index, they proceed from the top-left toward the accent color in the
	/// lower-right.
	/// </summary>
	struct tBrs {
		/// <summary>
		/// The brushes, in tDir4 order.
		/// </summary>
		readonly List<LinearGradientBrush> eqBrs;
		
		/// <param name="aClr">
		/// The top-left color.
		/// </param>
		/// <param name="aClrAccent">
		/// The lower-right color.
		/// </param>
		public tBrs(Color aClr, Color aClrAccent) {
			eqBrs = new List<LinearGradientBrush>();
			for (tDir4 oOr = tDir4.E; oOr <= tDir4.N; ++oOr) {
				var oqBr = new LinearGradientBrush(esRect(oOr), aClr, aClrAccent,
					esAng(oOr));
				oqBr.SetSigmaBellShape(1F);
				eqBrs.Add(oqBr);
			}
		}
		
		/// <summary>
		/// Returns the specified brush.
		/// </summary>
		public LinearGradientBrush this[tDir4 aOr] {
			get { return eqBrs[(Int32)aOr]; }
		}
		
		/// <summary>
		/// Returns the rectangle used to create the specified brush.
		/// </summary>
		static Rectangle esRect(tDir4 aOr) {
			switch (aOr) {
				case tDir4.N:
				case tDir4.S:
					return new Rectangle(24, 24, 48, 48);
				default:
					return new Rectangle(0, 0, 48, 48);
			}
		}
		
		/// <summary>
		/// Returns the 'orientation angle' of the gradient for the specified
		/// orientation.
		///
		/// The gradient proceeds from the foreground color to the accent color
		/// in the direction of this angle, measured clockwise from the positive
		/// X-axis. The coordinate system must be rotated before rotated text is
		/// drawn, however. This value gives the angle that, when thus rotated,
		/// causes the gradient to proceed from the top-left to the lower-right.
		/// </summary>
		static Int32 esAng(tDir4 aOr) {
			switch (aOr) {
				case tDir4.E: return 315;
				case tDir4.S: return 225;
				case tDir4.W: return 135;
				// tDir4.N:
				default: return 45;
			}
		}
	}
}
