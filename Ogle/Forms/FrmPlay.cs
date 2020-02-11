// FrmPlay.cs
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
using System.Text;
using System.Windows.Forms;
using nCtl;
using nMisc;

namespace nOgle {
	/// <summary>
	/// Implements the Play form.
	/// </summary>
	public partial class tqFrmPlay: tqFrmOgle {
		/// <summary>
		/// The lexicon used to verify player entries.
		/// </summary>
		readonly tqLex eqLex;
		/// <summary>
		/// The board being played.
		/// </summary>
		readonly tqBoard eqBoard;
		/// <summary>
		/// The player's score card.
		/// </summary>
		readonly tqCard eqCard;
		
		/// <summary>
		/// Set to 'true' when the play timer has expired.
		/// </summary>
		bool eExp = false;
		/// <summary>
		/// The play timer expiration point, when not paused.
		/// </summary>
		DateTime ePtExp;
		/// <summary>
		/// The remaining time span at which the low time tick will be used.
		/// </summary>
		TimeSpan eSpanLow;
		/// <summary>
		/// The remaining time span at which the last time tick will be used.
		/// </summary>
		TimeSpan eSpanLast;
		/// <summary>
		/// The number of twentieths remaining when a tick last sounded.
		/// </summary>
		Int32 eTwentsTickLast;
		
		/// <summary>
		/// Set to 'true' when the game is paused.
		/// </summary>
		bool ePaused = false;
		/// <summary>
		/// The time left to play, when paused.
		/// </summary>
		TimeSpan eSpanAfter;
		
		/// <param name="aqLex">
		/// The lexicon to be used when verifying player entries.
		/// <param>
		/// <param name="aqBoard">
		/// The board to be played.
		/// <param>
		/// <param name="aqCard">
		/// The player's score card.
		/// <param>
		public tqFrmPlay(tqLex aqLex, tqBoard aqBoard, tqCard aqCard):
			base("play.html") {
			
			aqLex.sThrowNull("aqLex");
			aqBoard.sThrowNull("aqBoard");
			aqCard.sThrowNull("aqCard");
			
			eqLex = aqLex;
			eqBoard = aqBoard;
			eqCard = aqCard;
			InitializeComponent();
		}
		
		/// <summary>
		/// Updates the entry label.
		/// </summary>
		void eLblEnt_Upd() {
			if ((eqGrid.qSel == null) || !eqGrid.qSel.Ck()) {
				eqLblEnt.Visible = false;
				eqLblDir.Visible = true;
			}
			else {
				eqLblDir.Visible = false;
				
				string oqText = eqGrid.qSel.qText + "_";
				eqLblEnt.Text = tCtl.sTextFitLeft(oqText, eqLblEnt.Font, eqGrid
					.Width);
				eqLblEnt.Visible = true;
			}
		}
		
		/// <summary>
		/// Updates the score label.
		/// </summary>
		void eLblScore_Upd() {
			eqLblScore.Text = eqCard.Score().ToString();
		}
		
		/// <summary>
		/// Returns the time left to play, in seconds.
		/// </summary>
		Int32 eSecsRemain() {
			return (Int32)Math.Ceiling((ePtExp - DateTime.Now).TotalSeconds);
		}
		
		/// <summary>
		/// Updates the time labels.
		/// </summary>
		void eLblsTime_Upd(Int32 aSecs) {
			// There is room in the label only for three digits:
			if (aSecs < 1000) {
				if (aSecs == 1) eqLblUnitTime.Text = "Second";
				else eqLblUnitTime.Text = "Seconds";
			}
			else {
				// There is no need to check for aSecs equal to one, as that value is
				// within the seconds range. It seems unlikely that any value greater
				// than 999 minutes could be encountered:
				aSecs /= 60;
				eqLblUnitTime.Text = "Minutes";
			}
			eqLblTime.Text = aSecs.ToString();
		}
		
		/// <summary>
		/// Updates the pause button bar count.
		/// </summary>
		void eBarsBtnPause_Upd(Int32 aSecs) {
			// Thirty-six bars fills the button:
			const Int32 oCtBarMax = 36;
			Int32 oCtBar = (Int32)Math.Ceiling((float)oCtBarMax * aSecs / 120);
			if (oCtBar > oCtBarMax) oCtBar = oCtBarMax;
			eqBtnPause.CtBar = oCtBar;
		}
		
		/// <summary>
		/// Updates the time labels and the pause button bar count.
		/// </summary>
		void eCtlsTime_Upd(Int32 aSecs) {
			eLblsTime_Upd(aSecs);
			eBarsBtnPause_Upd(aSecs);
		}
		
		/// <summary>
		/// Updates the pause button bar color and blink state.
		/// </summary>
		void eBlinkBtnPause_Upd(tStPause aSt) {
			if (aSt == tStPause.PauseBlink) eqBtnPause.DurBlink = 200;
			else eqBtnPause.DurBlink = 0;
		}
		
		/// <summary>
		/// Pauses the game.
		/// </summary>
		/// <param name="aBlink">
		/// Set to 'true' if the pause button should blink.
		///
		/// The button blinks to remind the user to unpause the game; if the game
		/// was not paused by the user, the button should not blink.
		/// </param>
		void ePause(bool aBlink) {
			eqTimer.Enabled = false;
			eSpanAfter = ePtExp - DateTime.Now;
			ePaused = true;
			
			eqGrid.VisMask = true;
			if (aBlink) eBlinkBtnPause_Upd(tStPause.PauseBlink);
			else eBlinkBtnPause_Upd(tStPause.PauseNoBlink);
			
			// Dim all labels:
			eqLblEnt.ForeColor = Color.FromKnownColor(KnownColor.GrayText);
			eqLblDir.ForeColor = Color.FromKnownColor(KnownColor.GrayText);
			eqLblScore.ForeColor = Color.FromKnownColor(KnownColor.GrayText);
			eqLblScoreLeg.ForeColor = Color.FromKnownColor(KnownColor.GrayText);
			eqLblTime.ForeColor = Color.FromKnownColor(KnownColor.GrayText);
			eqLblUnitTime.ForeColor = Color.FromKnownColor(KnownColor.GrayText);
			eqLblKeys.ForeColor = Color.FromKnownColor(KnownColor.GrayText);
			
			eqLblPause.Text = "Click to unpause";
		}
		
		/// <summary>
		/// Unpauses the game.
		/// </summary>
		void cUnpause() {
			eqGrid.VisMask = false;
			eBlinkBtnPause_Upd(tStPause.Active);
			
			// Un-dim all labels:
			eqLblEnt.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
			eqLblDir.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
			eqLblScore.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
			eqLblScoreLeg.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
			eqLblTime.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
			eqLblUnitTime.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
			eqLblKeys.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
			
			eqLblPause.Text = "Click to pause";
			
			ePtExp = DateTime.Now + eSpanAfter;
			eqTimer.Enabled = true;
			ePaused = false;
		}
		
		// Events
		// ------
		
		/// <summary>
		/// Readies the form for display, updating various controls, setting the
		/// time variables, and starting the timer.
		/// </summary>
		void eFrmPlay_Load(object aqSend, EventArgs aqArgs) {
			eBlinkBtnPause_Upd(tStPause.Active);
			
			eqGrid.qBoard = eqBoard;
			eLblEnt_Upd();
			eLblScore_Upd();
			
			ePtExp = DateTime.Now + tqMain.sSetup.TimeStart();
			eSpanLow = tqMain.sSetup.TimeStart();
			eSpanLast = TimeSpan.FromMilliseconds(tqMain.sSetup.TimeStart()
				.TotalMilliseconds / 2);
			eTwentsTickLast = esTwents(tqMain.sSetup.TimeStart());
			eqTimer.Enabled = true;
			eCtlsTime_Upd(eSecsRemain());
		}
		
		/// <summary>
		/// Pauses the round if it is not already paused.
		/// </summary>
		void eFrmPlay_Deactivate(object aqSend, EventArgs aqArgs) {
			if (!ePaused) {
				ePause(true);
				cqMgrSound.SelDie_Play();
			}
		}
		
		/// <summary>
		/// Returns the number of twentieths of a second in the specified span.
		/// </summary>
		static Int32 esTwents(TimeSpan aSpan) {
			return (Int32)Math.Floor(aSpan.TotalMilliseconds / 50);
		}
		
		/// <summary>
		/// Updates the time label, the pause button bar count, plays the tick
		/// sound, and closes the form when time expires.
		/// </summary>
		void eTimer_Tick(object aqSend, EventArgs aqArgs) {
			TimeSpan oSpan = ePtExp - DateTime.Now;
			if (ePtExp < DateTime.Now) {
				eExp = true;
				Close();
			}
			
			Int32 oSecs = eSecsRemain();
			eCtlsTime_Upd(oSecs);
			
			Int32 oTwents = esTwents(oSpan);
			if (oSpan <= eSpanLast) {
				if ((eTwentsTickLast - oTwents) >= 5) {
					cqMgrSound.TickLast_Play();
					eTwentsTickLast = oTwents;
				}
			}
			else if (oSpan <= eSpanLow) {
				if ((eTwentsTickLast - oTwents) >= 10) {
					cqMgrSound.TickLow_Play();
					eTwentsTickLast = oTwents;
				}
			}
			else if ((eTwentsTickLast - oTwents) >= 20) {
				cqMgrSound.Tick_Play();
				eTwentsTickLast = oTwents;
			}
		}
		
		/// <summary>
		/// Plays a sound when a letter grid element is moused-over.
		/// </summary>
		void eGrid_OverNext(object aqSend, EventArgs aqArgs) {
			cqMgrSound.MouseOver_Play();
		}
		
		/// <summary>
		/// Plays a sound appropriate to the specified entry event, and updates
		/// the entry label.
		/// </summary>
		void eGrid_ChgSel(object aqSend, tqArgsChg aqArgs) {
			aqArgs.sThrowNull("aqArgs");
			
			switch (aqArgs.Type) {
				case tqArgsChg.tType.Sel:
					cqMgrSound.SelDie_Play();
					break;
				case tqArgsChg.tType.Unsel:
				case tqArgsChg.tType.Cl:
					cqMgrSound.UnselDie_Play();
					break;
			}
			eLblEnt_Upd();
		}
		
		/// <summary>
		/// Processes the specified entry, updating the entry label, verifying the
		/// word, storing the entry and incrementing the time, if appropriate, and
		/// playing the relevant sound.
		/// </summary>
		void eGrid_EntSel(object aqSend, tqArgsSel aqArgs) {
			aqArgs.sThrowNull("aqArgs");
			aqArgs.qSel.sThrowNull("aqArgs.qSel");
			aqArgs.qSel.qText.sThrowNull("aqArgs.qSel.qText");
			
			eLblEnt_Upd();
			
			// Check for validity:
			if (!eqLex.Ck(aqArgs.qSel.qText)) {
				// Pause the timer, if it is not already paused:
				bool oPausedOrig = ePaused;
				if (ePaused) eBlinkBtnPause_Upd(tStPause.PauseNoBlink);
				else ePause(false);
				
				// Display the verification form:
				using (var oqFrmVer = new tqFrmVer(aqArgs.qSel.qText)) {
					oqFrmVer.ShowDialog();
					
					// Unpause the timer as appropriate:
					if (oPausedOrig) eBlinkBtnPause_Upd(tStPause.PauseBlink);
					else cUnpause();
					
					if (!oqFrmVer.CkAdd) return;
				}
				
				eqLex.WordUser_Add(aqArgs.qSel.qText);
			}
			
			Int32 oCtAdd = eqCard.Add(aqArgs.qSel);
			TimeSpan oSpan = tqMain.sSetup.TimeAdd(aqArgs.qSel.qText.Length,
				oCtAdd);
			ePtExp += oSpan;
			eTwentsTickLast += (Int32)(oSpan.TotalMilliseconds / 50);
			eLblScore_Upd();
			
			if (oCtAdd == 0) cqMgrSound.EntRedund_Play();
			else cqMgrSound.EntVal_Play();
		}
		
		/// <summary>
		/// Unpauses the game, if it is paused.
		/// </summary>
		void eGrid_MouseClick(object aqSend, MouseEventArgs aqArgs) {
			if (ePaused) {
				cUnpause();
				cqMgrSound.SelDie_Play();
			}
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
		/// Toggles the pause state.
		/// </summary>
		void eBtnTime_Click(object aqSend, EventArgs aqArgs) {
			if (ePaused) cUnpause();
			else ePause(true);
			cqMgrSound.SelDie_Play();
		}
		
		/// <summary>
		/// Allows the form to close if time has expired; otherwise, queries the
		/// player's intentions with the Quit play form.
		/// </summary>
		void eFrmPlay_FormClosing(object aqSend, FormClosingEventArgs aqArgs) {
			aqArgs.sThrowNull("aqArgs");
			
			// If the round timed-out, close immediately:
			if (eExp) return;
			
			// Pause the timer, if it is not already paused:
			bool oPausedOrig = ePaused;
			if (ePaused) eBlinkBtnPause_Upd(tStPause.PauseNoBlink);
			else ePause(false);
			
			// Confirm the player's intent, restoring the pause state if they wish
			// to continue:
			using (tqFrmQuitPlay oqFrm = new tqFrmQuitPlay()) {
				oqFrm.ShowDialog(this);
				if (oqFrm.Next == tNext.Play) {
					aqArgs.Cancel = true;
					if (oPausedOrig) eBlinkBtnPause_Upd(tStPause.PauseBlink);
					else cUnpause();
					return;
				}
				Next = oqFrm.Next;
			}
		}
		
		/// <summary>
		/// Represents the game pause state.
		/// </summary>
		enum tStPause {
			/// <summary>
			/// The game is not paused.
			/// </summary>
			Active = 0,
			/// <summary>
			/// The game is paused; the Pause button should blink.
			/// </summary>
			PauseBlink,
			/// <summary>
			/// The game is paused; the Pause button should not blink.
			/// </summary>
			PauseNoBlink
		}
	}
}
