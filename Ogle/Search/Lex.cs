// Lex.cs
// ------
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
using System.IO;
using nMisc;

namespace nOgle {

	/// <summary>
	/// Stores the words known to the computer.
	/// </summary>
	public class tqLex {
		/// <summary>
		/// The lexicon folder.
		/// </summary>
		readonly string eqFold;
		
		/// <summary>
		/// The English lexicon filename. Words common to all dialects are stored
		/// within.
		/// </summary>
		const string eqNameFileEng = "Eng.txt";
		/// <summary>
		/// The American lexicon filename. Words specific to American English are
		/// stored within.
		/// </summary>
		const string eqNameFileAm = "Am.txt";
		/// <summary>
		/// The British lexicon filename. Words specific to British English are
		/// stored within.
		/// </summary>
		const string eqNameFileBrit = "Brit.txt";
		/// <summary>
		/// The Canadian lexicon filename. Words specific to Canadian English are
		/// stored within.
		/// </summary>
		const string eqNameFileCan = "Can.txt";
		/// <summary>
		/// The user lexicon filename. Words passed by the Word Verification form
		/// are stored within.
		/// </summary>
		const string eqNameFileUser = "User.txt";
		
		/// <summary>
		/// The maximum erudition level to be loaded by this instance.
		/// </summary>
		const Int32 eErudMax = 2;
		
		/// <summary>
		/// The word list.
		/// </summary>
		readonly List<tEl> eqEls;
		/// <summary>
		/// User entries that have yet to be added to the main list.
		/// </summary>
		readonly List<tEl> eqElsUser;
		
		/// <summary>
		/// Creates an instance with elements equal to or less than the specified
		/// erudition.
		/// </summary>
		public tqLex(tSetupAdv.tDial aDial) {
			eqFold = Environment.GetFolderPath(Environment.SpecialFolder
				.ApplicationData) + "\\Ogle\\Lexicon\\";
			
			eqEls = new List<tEl>();
			eqElsUser = new List<tEl>();
			
			eAdd(eqFold + eqNameFileEng);
			eAdd(eqFold + eqNameFileUser);
			switch (aDial) {
				case tSetupAdv.tDial.British:
					eAdd(eqFold + eqNameFileBrit);
					break;
				case tSetupAdv.tDial.Canadian:
					eAdd(eqFold + eqNameFileCan);
					break;
				default:
					eAdd(eqFold + eqNameFileAm);
					break;
			}
			
			eqEls.Sort();
		}
		
		/// <summary>
		/// Returns the number of words in the lexicon.
		/// </summary>
		public Int32 Ct() {
			return eqEls.Count;
		}
		
		/// <summary>
		/// Returns the specified word.
		/// </summary>
		public tEl this[Int32 aIdx] {
			get {
				return eqEls[aIdx];
			}
		}
		
		/// <summary>
		/// Performs a binary search for the specified word within the main list
		/// and the user list, returning 'true' if it is found.
		/// </summary>
		public bool Ck(string aqText) {
			// tEl does not compare erudition levels:
			var oEl = new tEl(aqText, 0);
			return (eqEls.BinarySearch(oEl) >= 0) 
				|| (eqElsUser.BinarySearch(oEl) >= 0);
		}
		
		/// <summary>
		/// Returns 'true' if one or more user entries are waiting to be merged.
		/// </summary>
		public bool CkMerge() {
			return (eqElsUser.Count > 0);
		}
		
		/// <summary>
		/// Adds a word to the user list. The word is not added to the main list
		/// until <see cref="tqLex.WordsUser_Merge"/> is called.
		/// </summary>
		public void WordUser_Add(string aqText) {
			eqElsUser.Add(new tEl(aqText, 0));
			eqElsUser.Sort();
		}
		
		/// <summary>
		/// Moves entries from the user list into the main list.
		/// </summary>
		public void WordsUser_Merge() {
			if (eqElsUser.Count < 1) return;
			
			eqEls.AddRange(eqElsUser);
			eWordsUser_Store();
			eqElsUser.Clear();
			
			eqEls.Sort();
		}
		
		/// <summary>
		/// Adds entries from the specified file to the word list, ignoring those
		/// with erudition levels greater than <see cref="tqLex.eErudMax"/>.
		/// </summary>
		void eAdd(string aqNameFile) {
			aqNameFile.sThrowNull("aqNameFile");
			
			if (!File.Exists(aqNameFile)) return;
			
			FileStream oqFile = new FileStream(aqNameFile, FileMode.OpenOrCreate,
				FileAccess.Read, FileShare.ReadWrite);
			using (StreamReader oqRead = new StreamReader(oqFile))
				while (!oqRead.EndOfStream) {
					string[] oqFlds = oqRead.ReadLine().Split(null, 2);
					if (oqFlds.Length != 2) continue;
					
					if (oqFlds[0].Length < 1) continue;
					
					if (oqFlds[1].Length < 1) continue;
					Int32 oErud;
					if (!Int32.TryParse(oqFlds[1], out oErud)) continue;
					if ((oErud < 0) || (oErud > eErudMax)) continue;
					
					eqEls.Add(new tEl(oqFlds[0].ToLower(), oErud));
				}
		}
		
		/// <summary>
		/// Appends the user list to the user lexicon file.
		/// </summary>
		void eWordsUser_Store() {
			string oqPathFullFile = eqFold + eqNameFileUser;
			using (StreamWriter oqWrite = File.AppendText(oqPathFullFile))
				foreach (tEl o in eqElsUser)
					oqWrite.WriteLine(o.qText + ' ' + o.Erud.ToString());
		}
		
		/// <summary>
		/// Represents a single lexicon entry.
		/// </summary>
		public struct tEl: IComparable<tEl> {
			/// <summary>
			/// The entry text.
			/// </summary>
			readonly string eqText;
			/// <summary>
			/// The entry text.
			/// </summary>
			public string qText {
				get { return eqText; }
			}
			
			/// <summary>
			/// The entry erudition level.
			/// </summary>
			readonly Int32 eErud;
			/// <summary>
			/// The entry erudition level.
			/// </summary>
			public Int32 Erud {
				get { return eErud; }
			}
			
			public tEl(string aqText, Int32 aErud) {
				aqText.sThrowNull("aqText");
				
				eqText = aqText;
				eErud = aErud;
			}
			
			// IComparable<tEl>
			// ----------------
			
			/// <summary>
			/// Sorts entries by their text only; erudition levels are ignored.
			/// </summary>
			public int CompareTo(tEl a) {
				return qText.CompareTo(a.qText);
			}
		}
	}
}
