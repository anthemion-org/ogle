// Main.cs
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

// Define mTestProd to test board productivity:
#define mTestProdNo

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;
using System.Threading;
using nMisc;

namespace nOgle {
	/// <summary>
	/// Implements the Main method, and stores various application-global data.
	/// </summary>
	static class tqMain {
		/// <summary>
		/// A random number generator for the application.
		/// </summary>
		public static readonly Random sqRnd = new Random();
		/// <summary>
		/// XML write settings for the application.
		/// </summary>
		public static readonly XmlWriterSettings sqSetsWriteXML;
		
		static tqMain() {
			sqSetsWriteXML = new XmlWriterSettings();
			sqSetsWriteXML.Indent = true;
			sqSetsWriteXML.IndentChars = "\t";
			sqSetsWriteXML.CheckCharacters = true;
		}
		
		// Play settings
		// -------------
		
		/// <summary>
		/// The minimum length for entered words.
		/// </summary>
		public static readonly Int32 sLenEntMin = 4;
		
		/// <summary>
		/// The computer's erudition. This was meant to filter words by frequency of 
		/// usage, but that is no longer configurable.
		/// </summary>
		public static readonly Int32 sErudComp = 2;
		
		/// <summary>
		/// The current game settings.
		/// </summary>
		public static tSetup sSetup;
		/// <summary>
		/// The current advanced settings.
		/// </summary>
		public static tSetupAdv sSetupAdv;
		
		// Grid metrics
		// ------------
		
		/// <summary>
		/// The letter grid width.
		/// </summary>
		public static Int32 sWthGrid {
			get { return 5; }
		}
		/// <summary>
		/// The letter grid height.
		/// </summary>
		public static Int32 sHgtGrid {
			get { return 5; }
		}
		/// <summary>
		/// The letter grid dimensions.
		/// </summary>
		public static Size sSizeGrid {
			get { return new Size(sWthGrid, sHgtGrid); }
		}
		/// <summary>
		/// The letter grid rectangle.
		/// </summary>
		public static Rectangle sRectGrid {
			get { return new Rectangle(0, 0, sWthGrid, sHgtGrid); }
		}
		/// <summary>
		/// The number of elements in the letter grid.
		/// </summary>
		public static Int32 sCtDie {
			get {
				return sWthGrid * sSizeGrid.Height;
			}
		}
		
		/// <summary>
		/// Returns 'true' if the specified square is within the letter grid.
		/// </summary>
		public static bool sCkGrid(Int32 aX, Int32 aY) {
			return ((aX >= 0) && (aX < tqMain.sWthGrid)
				&& (aY >= 0) && (aY < tqMain.sHgtGrid));
		}
		/// <summary>
		/// Returns 'true' if the specified square is within the letter grid.
		/// </summary>
		public static bool sCkGrid(Point aPt) {
			return ((aPt.X >= 0) && (aPt.X < tqMain.sWthGrid)
				&& (aPt.Y >= 0) && (aPt.Y < tqMain.sHgtGrid));
		}
		
		/// <summary>
		/// Returns all squares in the letter grid.
		/// </summary>
		public static IEnumerable<Point> sSqsGrid() {
			for (Int32 oX = 0; oX < sWthGrid; ++oX)
				for (Int32 oY = 0; oY < sSizeGrid.Height; ++oY)
					yield return new Point(oX, oY);
		}
		
		/// <summary>
		/// Returns a random square in the letter grid.
		/// </summary>
		public static Point sSqRnd() {
			return new Point(sqRnd.Next(sWthGrid), sqRnd.Next(sHgtGrid));
		}
		
		// Form display
		// ------------
		
		/// <summary>
		/// The center point of the last form displayed with esFrm_Show. Used to
		/// center each form over its predecessor.
		/// </summary>
		static Point esPosCtrFrm;
		
		/// <summary>
		/// Displays a form, centering it over the same position as the last form
		/// displayed by esFrm_Show.
		/// </summary>
		static tqFrmOgle.tNext esFrm_Show(tqFrmOgle aqFrm) {
			aqFrm.sThrowNull("aqFrm");
			
			aqFrm.PosCtr = esPosCtrFrm;
			Application.Run(aqFrm);
			esPosCtrFrm = aqFrm.PosCtr;
			return aqFrm.Next;
		}
		
		/// <summary>
		/// The Status form instance, or 'null' if the Status form is hidden.
		/// </summary>
		static tqFrmStat esqFrmStat;
		
		/// <summary>
		/// Instantiates and shows the Status form, if it is not already visible.
		/// </summary>
		public static void sStat_Show() {
			if (esqFrmStat != null) return;
			
			esqFrmStat = new tqFrmStat();
			esqFrmStat.PosCtr = esPosCtrFrm;
			esqFrmStat.Show();
			Application.DoEvents();
		}
		
		/// <summary>
		/// Hides the Status form and clears its reference, if it is not already
		/// hidden.
		/// </summary>
		public static void sStat_Hide() {
			if (esqFrmStat == null) return;
			
			esqFrmStat.Hide();
			// It would be better to encapsulate the disposal process to ensure this
			// is called:
			esqFrmStat.Dispose();
			esqFrmStat = null;
		}
		
		// Miscellanea
		// -----------
		
		/// <summary>
		/// Returns the signature of the topmost method in the specified stack
		/// that is not one of the throw methods, like <see cref="tMisc
		/// .sThrowNull"/>.
		/// </summary>
		/// <remarks>
		/// Decreasing the amount of text in the exception report increases the 
		/// likelihood that the text will be reported.
		/// </remarks>
		static string eMethLast(string aqTrace) {
			if (aqTrace == null) return "";
			
			string oqMeth = "";
			char[] oqDelims = { '\r' };
			string[] oqLines = aqTrace.Split(oqDelims, 3);
			switch (oqLines.Length) {
				case 0:
					break;
				case 1:
					oqMeth = oqLines[0];
					break;
				default:
					if (oqLines[0].Contains("sThrowNull")) oqMeth = oqLines[1];
					else oqMeth = oqLines[0];
					break;
			}
			const string oqPref = " at ";
			Int32 oIdx = oqMeth.IndexOf(oqPref);
			if (oIdx < 0) oIdx = 0;
			else oIdx += oqPref.Length;
			
			const string oqSuff = " in ";
			Int32 oLen = oqMeth.LastIndexOf(oqSuff);
			if (oLen < 0) oLen = oqMeth.Length - oIdx;
			else oLen -= oIdx;
			
			oqMeth = oqMeth.Substring(oIdx, oLen);
			return oqMeth;
		}
		
		/// <summary>
		/// Displays the text of the specified exception to the user.
		/// </summary>
		public static void sExcept_Rep(Exception aqExcept) {
			// Store full stack trace in log file? [incomplete]
			
			// Do not throw an exception while another is being handled:
			if (aqExcept == null) return;
			
			string oqText = "Sorry; Ogle has run into a problem!\n\n" + aqExcept
				.Message + "\n\n" + eMethLast(aqExcept.StackTrace);
			MessageBox.Show(oqText, "Ogle exception", MessageBoxButtons.OK,
				MessageBoxIcon.Error);
		}
		
		/// <summary>
		/// The name of the Game Settings XML file.
		/// </summary>
		const string esqNameFileSetup = "Setup.xml";
		/// <summary>
		/// The name of the Advanced Settings XML file.
		/// </summary>
		const string esqNameFileSetupAdv = "SetupAdv.xml";
		/// <summary>
		/// The name of the high scores XML file.
		/// </summary>
		const string esqNameFileScores = "Scores.xml";
		
		/// <summary>
		/// Displays the point totals and percent from the last game, and queries
		/// the player for their name.
		/// </summary>
		static string esName_Ask(Int32 aPtsPlay, Int32 aPtsComp, string
			aqNameLast) {
			
			using (var oqFrm = new tqFrmName(aPtsPlay, aPtsComp, aqNameLast)) {
				oqFrm.PosCtr = esPosCtrFrm;
				oqFrm.ShowDialog();
				return oqFrm.qText;
			}
		}
		
		/// <summary>
		/// Plays a long series of games using the current settings (without input
		/// from the player), then displays statistics describing the productivity
		/// of the boards, plus the search time.
		/// </summary>
		static void esTestProd(tqLex aqLex) {
			const Int32 oCt = 100;
			Int32 oWordsMin = 99999;
			Int32 oWordsMax = 0;
			Int32 oWordsTtl = 0;
			long oTimeTtl = 0;
			for (Int32 o = 0; o < oCt; ++o) {
				var oqRound = new tqRound(1.0F);
				var oqMgrSearch = new tqMgrSearch(oqRound.qBoard, aqLex, sErudComp);
				
				oqMgrSearch.Exec();
				while (!oqMgrSearch.CkDone) Thread.Sleep(200);
				
				if (oqMgrSearch.CtRaw() < oWordsMin) oWordsMin = oqMgrSearch.CtRaw();
				if (oqMgrSearch.CtRaw() > oWordsMax) oWordsMax = oqMgrSearch.CtRaw();
				oWordsTtl += oqMgrSearch.CtRaw();

				oTimeTtl += oqMgrSearch.Time;
			}
			float oWordsAvg = (float)oWordsTtl / oCt;
			float oTimeAvg = (float)oTimeTtl / oCt;
			string oqText = string.Format(
				"Words min: {0}\nWords max: {1}\nWords avg: {2}\nTime avg: {3}",
				oWordsMin, oWordsMax, oWordsAvg, oTimeAvg);
			MessageBox.Show(oqText);
		}
		
		/// <summary>
		/// The form management loop, which cycles the player through the Setup,
		/// Play, and Score forms.
		/// </summary>
		static void esExec() {
			Rectangle oDesk = Screen.PrimaryScreen.WorkingArea;
			esPosCtrFrm = new Point(oDesk.Width / 2, oDesk.Height / 2);
			
			sStat_Show();
			
			bool oShowSetup = true;
			
			// Use 'Path.Join' or 'Path.Combine' here: [refactor]
			string oqFoldSetup = Environment.GetFolderPath(Environment.SpecialFolder
				.ApplicationData) + "\\Ogle\\";
			sSetup = tSetup.sReadOrDef(oqFoldSetup + esqNameFileSetup);
			sSetupAdv = tSetupAdv.sReadOrDef(oqFoldSetup + esqNameFileSetupAdv);
			tqScores oqScores = tqScores.sReadOrDef(oqFoldSetup + esqNameFileScores);
			var oqLex = new tqLex(sSetupAdv.Dial);
			
			sStat_Hide();
			
			string oqNamePlayLast = "";
			while (true) {
				// Some of these blocks should be moved to subroutines. [refactor]

				tqFrmOgle.tNext oNext;
				
				// Show setup and create round
				// ---------------------------
				
				if (oShowSetup) {
					tSetupAdv.tDial oDialOrig = sSetupAdv.Dial;
					
					using (var oqFrmSetup = new tqFrmSetup())
						oNext = esFrm_Show(oqFrmSetup);
					if (oNext == tqFrmOgle.tNext.QuitOgle) break;
					oShowSetup = false;
					
					if (sSetupAdv.Dial != oDialOrig) {
						sStat_Show();
						oqLex = new tqLex(sSetupAdv.Dial);
					}
				}
				
				#if mTestProd
					esTestProd(oqLex);
					return;
				#endif
				
				sStat_Show();
				
				// Select and search board
				// -----------------------
				// Board selection is sometimes slower than it might be. Future versions 
				// might run a background thread to generate and store boards for future 
				// use.
				
				Int32 oCtWordMin = sSetup.CtWordMin().GetValueOrDefault(0);
				Int32 oCtWordMax = sSetup.CtWordMax().GetValueOrDefault(99999);
				
				Int32 oCtTry = 0;
				tqRound oqRound;
				tqMgrSearch oqMgrSearch;
				while (true) {
					++oCtTry;
					if (oCtTry > 50)
						throw new Exception("Cannot create specified board");
					
					oqRound = new tqRound(sSetup.CeilPool());
					oqMgrSearch = new tqMgrSearch(oqRound.qBoard, oqLex, sErudComp);
					oqMgrSearch.Exec();
					while (!oqMgrSearch.CkDone) Thread.Sleep(200);
					
					// The redundancy check is relatively slow, so it makes sense to
					// skip boards that are unlikely to fit the constraints.
					if (((oqMgrSearch.CtRaw() / 2) < oCtWordMin)
						|| ((oqMgrSearch.CtRaw() * 2 / 3) > oCtWordMax)) continue;
					
					// Add marks duplicate and redundant entries automatically.
					//
					// There must be a faster way to do this: [optimize]
					foreach (tqSel oq in oqMgrSearch) oqRound.qCardComp.Add(oq);
					
					if ((oqRound.qCardComp.CtVal() >= oCtWordMin)
						&& (oqRound.qCardComp.CtVal() <= oCtWordMax)) break;
				}
				
				// Show play form
				// --------------
				
				sStat_Hide();
				using (var oqFrmPlay = new tqFrmPlay(oqLex, oqRound.qBoard, oqRound
					.qCardPlay))
					oNext = esFrm_Show(oqFrmPlay);
				sStat_Show();
				
				if (oqLex.CkMerge()) oqLex.WordsUser_Merge();
				
				if (oNext == tqFrmOgle.tNext.QuitOgle) break;
				
				// Add to high scores
				// ------------------
				
				Int32 oPtsPlay = oqRound.qCardPlay.Score();
				Int32 oPtsComp = oqRound.qCardComp.Score();
				Int32 oPerPlay = (Int32)Math.Round((float)oPtsPlay / (float)oPtsComp
					* 100);
				
				string oqNamePlay = null;
				
				if ((oPtsPlay > 0) && oqScores.CkHighPts(sSetup.Yld, sSetup.Pace,
					oPtsPlay, oqRound.Time)) {
					
					sStat_Hide();
					oqNamePlay = esName_Ask(oPtsPlay, oPtsComp, oqNamePlayLast);
					oqNamePlayLast = oqNamePlay;
					
					var oEl = new tqScores.tEl(oqNamePlay, oPtsPlay, oqRound.Time);
					oqScores.Pts_Add(sSetup.Yld, sSetup.Pace, oEl);
				}
				
				if ((oPerPlay > 0) && oqScores.CkHighPer(sSetup.Yld, sSetup.Pace, oPerPlay,
					oqRound.Time)) {
					
					if (oqNamePlay == null) {
						sStat_Hide();
						oqNamePlay = esName_Ask(oPtsPlay, oPtsComp, oqNamePlayLast);
						oqNamePlayLast = oqNamePlay;
					}
					var oEl = new tqScores.tEl(oqNamePlay, oPerPlay, oqRound.Time);
					oqScores.Per_Add(sSetup.Yld, sSetup.Pace, oEl);
				}
				
				if (oqNamePlay != null) oqScores.Store(oqFoldSetup + esqNameFileScores);
				
				// Show score form
				// ---------------
				
				sStat_Hide();
				using (var oqFrm = new tqFrmScore(oqRound, oqScores.Per(sSetup),
					oqScores.Pts(sSetup))) oNext = esFrm_Show(oqFrm);
				if (oNext == tqFrmOgle.tNext.Setup) {
					oShowSetup = true;
					continue;
				}
				if (oNext == tqFrmOgle.tNext.QuitOgle) break;
			}
			
			sStat_Show();
			sSetup.Store(oqFoldSetup + esqNameFileSetup);
			sSetupAdv.Store(oqFoldSetup + esqNameFileSetupAdv);
			sStat_Hide();
		}
		
		/// <summary>
		/// The Main method.
		/// </summary>
		[STAThread]
		static void Main() {
			try {
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				
				string oqGUID = "Global\\BADECE12-C556-421b-84B0-D0C7AE9902B4";
				using (Mutex oqMutex = new Mutex(false, oqGUID)) {
					try {
						if (!oqMutex.WaitOne(0, false)) {
							MessageBox.Show("Ogle is already running", "Ogle", MessageBoxButtons
								.OK, MessageBoxIcon.Warning);
							return;
						}
					}
					catch (AbandonedMutexException) {
					}
					
					esExec();
				}
			}
			catch (Exception aq) {
				sExcept_Rep(aq);
			}
		}
	}
}