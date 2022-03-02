// FrmScore.cs
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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using nMisc;

namespace nOgle {
	/// <summary>
	/// Implements the Score form.
	/// </summary>
	public partial class tqFrmScore: tqFrmOgle {
		/// <summary>
		/// The round just played.
		/// </summary>
		readonly tqRound eqRound;
		/// <summary>
		/// High scores by percent.
		/// </summary>
		readonly IEnumerable<tqScores.tEl> eqScoresPer;
		/// <summary>
		/// High scores by point total.
		/// </summary>
		readonly IEnumerable<tqScores.tEl> eqScoresPts;
		
		/// <summary>
		/// All entry pairs in the last game.
		/// </summary>
		readonly List<tPair> eqPairs;
		
		/// <summary>
		/// The pixel height of a line in the entry or high score lists.
		/// </summary>
		const Int32 eHgtLineList = 22;
		/// <summary>
		/// The Entry List renderer.
		/// </summary>
		readonly tqRendTbl eqRendList;
		/// <summary>
		/// The Percent High Score List renderer.
		/// </summary>
		readonly tqRendTbl eqRendPers;
		/// <summary>
		/// The Point Total High Score List renderer.
		/// </summary>
		readonly tqRendTbl eqRendPts;
		
		/// <param name="aqRound">
		/// The round to be documented.
		/// </param>
		/// <param name="aqScoresPer">
		/// The high score entries by percent.
		/// </param>
		/// <param name="aqScoresPts">
		/// The high score entries by point total.
		/// </param>
		public tqFrmScore(tqRound aqRound, IEnumerable<tqScores.tEl> aqScoresPer,
			IEnumerable<tqScores.tEl> aqScoresPts): base("score.html") {
			
			aqRound.sThrowNull("aqRound");
			aqScoresPer.sThrowNull("aqScoresPer");
			aqScoresPts.sThrowNull("aqScoresPts");
			aqRound.qCardPlay.sThrowNull("aqRound.qCardPlay");
			aqRound.qCardComp.sThrowNull("aqRound.qCardComp");
			
			eqRound = aqRound;
			eqScoresPer = aqScoresPer;
			eqScoresPts = aqScoresPts;
			
			eqPairs = new List<tPair>();
			ePairs_Tally();
			
			InitializeComponent();
			
			// This might not contrast well with some Windows themes: [incomplete]
			Color oClrBarLt = Color.FromArgb(250, 250, 250);
			
			var oRectCli = new Rectangle(new Point(), eqPanPairs.Size);
			eqRendList = new tqRendTbl(oRectCli, eHgtLineList, oClrBarLt, BackColor,
				ForeColor, Color.Peru);
			
			oRectCli = new Rectangle(14, 54, eqBoxScores.Width - 29, eHgtLineList
				* 5);
			eqRendPers = new tqRendTbl(oRectCli, eHgtLineList, oClrBarLt, BackColor,
				ForeColor, Color.Peru);
			
			oRectCli = new Rectangle(14, 216, eqBoxScores.Width - 29, eHgtLineList
				* 5);
			eqRendPts = new tqRendTbl(oRectCli, eHgtLineList, oClrBarLt, BackColor,
				ForeColor, Color.Peru);
			
			eqPanScore.PtsPlay = aqRound.qCardPlay.Score();
			eqPanScore.PtsComp = aqRound.qCardComp.Score();
		}
		
		/// <summary>
		/// Populates the entry pair list with one set of elements reflecting both
		/// players' work.
		/// </summary>
		void ePairs_Tally() {
			// Duplicate and redundant entries were marked when the entries were
			// added to the cards:
			IEnumerator<tqSel> oiqPlay = eqRound.qCardPlay.OrderBy(o => o.qText)
				.GetEnumerator();
			IEnumerator<tqSel> oiqComp = eqRound.qCardComp.OrderBy(o => o.qText)
				.GetEnumerator();
			
			// Create entry pairs
			// ------------------
			
			bool oCkPlay = oiqPlay.MoveNext();
			bool oCkComp = oiqComp.MoveNext();
			
			// Iterate both entry lists in parallel:
			while (oCkPlay && oCkComp) {
				// The computer finds many followed words, and it would be distracting
				// to list them all. The player's entries should all be listed,
				// however, so they understand why they weren't counted:
				if (oiqComp.Current.St != tqSel.tSt.Valid) {
					oCkComp = oiqComp.MoveNext();
					continue;
				}
				
				Int32 oComp = oiqPlay.Current.qText.CompareTo(oiqComp.Current.qText);
				if (oComp < 0) {
					var oPair = new tPair(oiqPlay.Current, null);
					eqPairs.Add(oPair);
					oCkPlay = oiqPlay.MoveNext();
				}
				else if (oComp > 0) {
					var oPair = new tPair(null, oiqComp.Current);
					eqPairs.Add(oPair);
					oCkComp = oiqComp.MoveNext();
				}
				else {
					var oPair = new tPair(oiqPlay.Current, oiqComp.Current);
					eqPairs.Add(oPair);
					oCkPlay = oiqPlay.MoveNext();
					oCkComp = oiqComp.MoveNext();
				}
			}
			
			// Add remaining player entries:
			while (oCkPlay) {
				if (oiqPlay.Current.St == tqSel.tSt.Valid) {
					var oPair = new tPair(oiqPlay.Current, null);
					eqPairs.Add(oPair);
				}
				oCkPlay = oiqPlay.MoveNext();
			}
			
			// Add remaining computer entries:
			while (oCkComp) {
				if (oiqComp.Current.St == tqSel.tSt.Valid) {
					var oPair = new tPair(null, oiqComp.Current);
					eqPairs.Add(oPair);
				}
				oCkComp = oiqComp.MoveNext();
			}
			
			eqPairs.Sort();
		}
		
		/// <summary>
		/// Render a word entry or high score table.
		///
		/// The position, size, and colors of the table are established when the
		/// instance is created. Its content is rendered with a call to <see
		/// cref="tqRendTbl.Pairs_Draw"/> or <see cref="tqRendTbl.Scores_Draw"/>.
		/// </summary>
		class tqRendTbl {
			/// <summary>
			/// The rectangle within which the table is rendered.
			/// </summary>
			readonly Rectangle eRectCli;
			/// <summary>
			/// The height of each line in the table.
			/// </summary>
			readonly Int32 eHgtLine;
			
			/// <summary>
			/// The vertical text position within each line.
			/// </summary>
			readonly Int32 eTopText;
			
			/// <summary>
			/// The background color for light lines.
			/// </summary>
			readonly Brush eqBrBackLt;
			/// <summary>
			/// The background color for dark lines.
			/// </summary>
			readonly Brush eqBrBackDk;
			
			/// <summary>
			/// The brush used to render words and names.
			/// </summary>
			readonly Brush eqBrText;
			/// <summary>
			/// The brush used to render scores.
			/// </summary>
			readonly Brush eqBrVal;
			
			/// <summary>
			/// The font used to render words and names.
			/// </summary>
			readonly Font eqFontText;
			/// <summary>
			/// The brush used to render percents and point totals.
			/// </summary>
			readonly Font eqFontVal;
			
			/// <param name="aRectCli">
			/// The rectangle within which the table is to be rendered.
			/// </param>
			/// <param name="aHgtLine">
			/// The height of each line in the table.
			/// </param>
			/// <param name="aClrBackLt">
			/// The background color for light lines.
			/// </param>
			/// <param name="aClrBackDk">
			/// The background color for dark lines.
			/// </param>
			/// <param name="aClrText">
			/// The font used to render words and names.
			/// </param>
			/// <param name="aClrVal">
			/// The brush used to render percents and point totals.
			/// </param>
			public tqRendTbl(Rectangle aRectCli, Int32 aHgtLine, Color aClrBackLt,
				Color aClrBackDk, Color aClrText, Color aClrVal) {
				
				eRectCli = aRectCli;
				eHgtLine = aHgtLine;
				
				eqBrBackLt = new SolidBrush(aClrBackLt);
				eqBrBackDk = new SolidBrush(aClrBackDk);
				
				eqBrText = new SolidBrush(aClrText);
				eqBrVal = new SolidBrush(aClrVal);
				
				eqFontText = new Font("Microsoft Sans Serif", 16, FontStyle.Regular,
					GraphicsUnit.Pixel, 0);
				eqFontVal = new Font("Georgia", 16, FontStyle.Regular, GraphicsUnit
					.Pixel, 0);
				
				// Actual text may contain descenders, and it is not desirable that
				// such be considered when centering:
				Size oSize = TextRenderer.MeasureText("X", eqFontText);
				eTopText = (eHgtLine - oSize.Height) / 2;
			}
			
			/// <summary>
			/// Returns the index of the entry at the specified vertical position.
			/// </summary>
			public Int32 IdxFromY(Int32 aY) {
				return aY / eHgtLine;
			}
			
			/// <summary>
			/// Renders an entry pair within the list.
			/// </summary>
			/// <param name="aTop">
			/// The vertical position of the list entry.
			/// </param>
			/// <param name="aLt">
			/// Set to 'true' if the entry should be drawn with a light background.
			/// </param>
			void ePair_Draw(Graphics aqGr, Int32 aTop, bool aLt, tPair aPair) {
				aqGr.sThrowNull("aqGr");
				
				var oRect = new Rectangle(eRectCli.Left, aTop, eRectCli.Width,
					eHgtLine);
				
				Brush oqBr;
				if (aLt) oqBr = eqBrBackLt;
				else oqBr = eqBrBackDk;
				
				aqGr.FillRectangle(oqBr, oRect);
				
				string oqText = aPair.qText;
				SizeF oSizeText = aqGr.MeasureString(oqText, eqFontText);
				Point oPt = new Point(eRectCli.Left + (oRect.Width - (Int32)oSizeText
					.Width) / 2, aTop + eTopText);
				aqGr.DrawString(oqText, eqFontText, eqBrText, oPt);
				
				const Int32 oPadScore = 4;
				
				if (aPair.qSelPlay != null) {
					oqText = aPair.qSelPlay.Score().ToString();
					oSizeText = aqGr.MeasureString(oqText, eqFontVal);
					oPt = new Point(eRectCli.Left + oPadScore, aTop + eTopText);
					aqGr.DrawString(oqText, eqFontVal, eqBrVal, oPt);
				}
				
				if (aPair.qSelComp != null) {
					oqText = aPair.qSelComp.Score().ToString();
					oSizeText = aqGr.MeasureString(oqText, eqFontVal);
					oPt = new Point(eRectCli.Right - oPadScore - (Int32)oSizeText.Width,
						aTop + eTopText);
					aqGr.DrawString(oqText, eqFontVal, eqBrVal, oPt);
				}
			}
			
			/// <summary>
			/// Renders a set of entry pairs in the list.
			/// </summary>
			/// <param name="aTopLog">
			/// The vertical position of the view window within the logical extent
			/// of the list, which may surpass the view window in height.
			/// </param>
			public void Pairs_Draw(Graphics aqGr, IList<tPair> aqPairs, Int32
				aTopLog) {
				
				aqGr.sThrowNull("aqGr");
				aqPairs.sThrowNull("aqPairs");
				
				Int32 oSh = aTopLog % eHgtLine;
				// Draw one extra line to ensure partial lines are rendered:
				Int32 oOffMax = eRectCli.Height + eHgtLine;
				for (Int32 oOff = 0; oOff < oOffMax; oOff += eHgtLine) {
					Int32 oIdx = (aTopLog + oOff) / eHgtLine;
					if (oIdx < 0) continue;
					if (oIdx >= aqPairs.Count) break;
					
					bool oLt = ((oIdx % 2) == 0);
					ePair_Draw(aqGr, eRectCli.Top + oOff - oSh, oLt, aqPairs[oIdx]);
				}
			}
			
			/// <summary>
			/// Renders a score entry within the list.
			/// </summary>
			/// <param name="aTop">
			/// The vertical position of the list entry.
			/// </param>
			/// <param name="aLt">
			/// Set to 'true' if the entry should be drawn with a light background.
			/// </param>
			/// <param name="aPer">
			/// Set to 'true' if score should be rendered as a percent.
			/// </param>
			/// <param name="aEmph">
			/// Set to 'true' is the entry should be emphasized.
			/// </param>
			void eScore_Draw(Graphics aqGr, Int32 aTop, bool aLt, tqScores.tEl aEl,
				bool aPer, bool aEmph) {
				
				aqGr.sThrowNull("aqGr");
				aEl.qName.sThrowNull("aEl.qName");
				
				var oRect = new Rectangle(eRectCli.Left, aTop, eRectCli.Width,
					eHgtLine);
				
				Brush oqBr;
				if (aLt) oqBr = eqBrBackLt;
				else oqBr = eqBrBackDk;
				
				aqGr.FillRectangle(oqBr, oRect);
				
				string oqText = aEl.qName;
				if (oqText.Length < 1) oqText = "(anonymous)";
				
				const Int32 oPadText = 4;
				Point oPt = new Point(eRectCli.Left + oPadText, aTop + eTopText);
				oqBr = aEmph ? eqBrVal : eqBrText;
				aqGr.DrawString(oqText, eqFontText, oqBr, oPt);
				
				oqText = aEl.Val.ToString();
				if (aPer) oqText += '%';
				SizeF oSizeText = aqGr.MeasureString(oqText, eqFontVal);
				oPt = new Point(eRectCli.Right - (Int32)oSizeText.Width - oPadText,
					aTop + eTopText);
				// The percent sign disturbs the string measurement, somehow:
				if (aPer) oPt.X += 2;
				aqGr.DrawString(oqText, eqFontVal, eqBrVal, oPt);
			}
			
			/// <summary>
			/// Renders a set of scores in the list.
			/// </summary>
			/// <param name="aPer">
			/// Set to 'true' if the scores should be rendered as percents.
			/// </param>
			/// <param name="aTimeLast">
			/// The start time of the last game, used to emphasize that entry if it
			/// appears in the table.
			/// </param>
			public void Scores_Draw(Graphics aqGr, IEnumerable<tqScores.tEl> eqEls,
				bool aPer, DateTime aTimeLast) {
				
				aqGr.sThrowNull("aqGr");
				eqEls.sThrowNull("eqEls");
				
				Int32 oTop = eRectCli.Top;
				bool oLt = true;
				foreach (tqScores.tEl oEl in eqEls) {
					if (oTop > eRectCli.Bottom) break;
					
					bool oEmph = (oEl.Time == aTimeLast);
					eScore_Draw(aqGr, oTop, oLt, oEl, aPer, oEmph);
					
					oLt = !oLt;
					oTop += eHgtLine;
				}
			}
		}
		
		// Events
		// ------
		
		/// <summary>
		/// Updates the word entry scroll box.
		/// </summary>
		void eFrmScore_Load(object aqSend, EventArgs aqArgs) {
			// From the ScrollBar.Maximum help:
			//
			//   "The maximum value can only be reached programmatically. The value
			//   of a scroll bar cannot reach its maximum value through user
			//   interaction at run time. The maximum value that can be reached
			//   through user interaction is equal to 1 plus the Maximum property
			//   value minus the LargeChange property value."
			//
			eqScroll.Maximum = ((eqPairs.Count - 1) * eHgtLineList) - 1;
			eqScroll.LargeChange = eqPanPairs.Height - eHgtLineList;
			
			// For whatever reason, the MouseWheel event is hidden from the form
			// designer. Moreover, the event does not fire when I assign the
			// instance to the ePanPairs MouseWheel event. Assigning it to the form
			// event causes the list to scroll even when the mouse is not over the
			// panel, but this seems acceptable:
			MouseWheel += new MouseEventHandler(ePanPairs_MouseWheel);
		}
		
		/// <summary>
		/// Updates the play settings labels.
		/// </summary>
		void eFrmScore_Shown(object aqSend, EventArgs aqArgs) {
			eqLblPace.Text = tqMain.sSetup.TextPace();
			eqLblYld.Text = tqMain.sSetup.TextYld();
			Next = tNext.QuitOgle;
		}
		
		/// <summary>
		/// Renders the word entry list.
		/// </summary>
		void ePanPairs_Paint(object aqSend, PaintEventArgs aqArgs) {
			aqArgs.sThrowNull("aqArgs");
			
			eqRendList.Pairs_Draw(aqArgs.Graphics, eqPairs, eqScroll.Value);
		}
		
		/// <summary>
		/// Invalidates the word entry list.
		/// </summary>
		void eScroll_Scroll(object aqSend, ScrollEventArgs aqArgs) {
			eqPanPairs.Invalidate();
		}
		
		/// <summary>
		/// Changes the caret to the 'hand' image when word entries are moused-
		/// over.
		/// </summary>
		void ePanPairs_MouseMove(object aqSend, MouseEventArgs aqArgs) {
			aqArgs.sThrowNull("aqArgs");
			
			// Emphasize the moused word, play the mouse-over sound?:
			Int32 oIdx = eqRendList.IdxFromY(eqScroll.Value + aqArgs.Y);
			if ((oIdx >= 0) && (oIdx < eqPairs.Count()))
				eqPanPairs.Cursor = Cursors.Hand;
			else eqPanPairs.Cursor = Cursors.Default;
		}
		
		/// <summary>
		/// Scrolls the word entry list.
		/// </summary>
		void ePanPairs_MouseWheel(object aqSend, MouseEventArgs aqArgs) {
			aqArgs.sThrowNull("aqArgs");
			
			Int32 oInc = eHgtLineList * 2;
			if (aqArgs.Delta < 0) {
				Int32 oVal = eqScroll.Value + oInc;
				Int32 oValMax = eqScroll.Maximum - eqPanPairs.Height + eHgtLineList + 1;
				if (oVal > oValMax) oVal = oValMax;
				
				if ((oVal < eqScroll.Minimum) || (oVal > eqScroll.Maximum)) return;
				
				eqScroll.Value = oVal;
				eqPanPairs.Invalidate();
			}
			else if (aqArgs.Delta > 0) {
				Int32 oVal = eqScroll.Value - oInc;
				if (oVal < 0) oVal = 0;
				
				if ((oVal < eqScroll.Minimum) || (oVal > eqScroll.Maximum)) return;
				
				eqScroll.Value = oVal;
				eqPanPairs.Invalidate();
			}
		}
		
		/// <summary>
		/// Opens the Word Verification form when a word entry is clicked.
		/// </summary>
		void ePanPairs_MouseClick(object aqSend, MouseEventArgs aqArgs) {
			aqArgs.sThrowNull("aqArgs");
			
			if (aqArgs.Button != MouseButtons.Left) return;
			
			Int32 oIdx = eqRendList.IdxFromY(eqScroll.Value + aqArgs.Y);
			if ((oIdx >=0) && (oIdx < eqPairs.Count())) {
				tPair oPair = eqPairs[oIdx];
				using (var oqFrm = new tqFrmDtl(eqPairs[oIdx]))
					oqFrm.ShowDialog();
			}
		}
		
		/// <summary>
		/// Renders the score tables.
		/// </summary>
		private void eqBoxScores_Paint(object aqSend, PaintEventArgs aqArgs) {
			aqArgs.sThrowNull("aqArgs");
			
			eqRendPts.Scores_Draw(aqArgs.Graphics, eqScoresPts, false, eqRound
				.Time);
			eqRendPers.Scores_Draw(aqArgs.Graphics, eqScoresPer, true, eqRound
				.Time);
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
		/// Closes the form and signals that the Setup form should be displayed.
		/// </summary>
		void eBtnSetup_Click(object aqSend, EventArgs aqArgs) {
			Next = tNext.Setup;
			Close();
		}
		
		/// <summary>
		/// Closes the form and signals that a new game should be started.
		/// </summary>
		void eBtnPlay_Click(object aqSend, EventArgs aqArgs) {
			Next = tNext.Play;
			Close();
		}
	}
}
