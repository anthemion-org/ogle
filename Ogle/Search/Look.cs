// Look.cs
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
using nMisc;

namespace nOgle {
	/// <summary>
	/// Stores the state of a binary search within some lexicon, allowing that
	/// state to be extended and shared as enumerators traverse the board.
	/// </summary>
	public struct tLook {
		/// <summary>
		/// The lexicon being searched.
		/// </summary>
		readonly tqLex eqLex;
		
		/// <summary>
		/// The text being sought by this instance.
		/// </summary>
		readonly string eqText;
		/// <summary>
		/// The maximum allowable erudition level. Lexicon entries with greater
		/// values are ignored.
		/// </summary>
		readonly Int32 eErud;
		
		/// <summary>
		/// The last lexicon index before the search window.
		/// </summary>
		Int32 ejFore;
		/// <summary>
		/// The first lexicon index after the search window.
		/// </summary>
		Int32 ejAft;
		
		/// <summary>
		/// Creates a new instance without borrowing search state from another
		/// instance.
		/// </summary>
		/// <param name="aqLex">
		/// The lexicon to be searched.
		/// </param>
		/// <param name="aqText">
		/// The text to be sought.
		/// </param>
		/// <param name="aErud">
		/// The maximum allowable erudition level. Lexicon entries with greater
		/// values will be ignored.
		/// </param>
		public tLook(tqLex aqLex, Int32 aErud, string aqText) {
			aqLex.sThrowNull("aqLex");
			aqText.sThrowNull("aqText");
			
			eqLex = aqLex;
			eqText = aqText;
			eErud = aErud;
			
			ejFore = -1;
			ejAft = eqLex.Ct();
		}
		
		/// <summary>
		/// Creates a new instance that borrows search state from the specified
		/// instance. This allows many binary search interations to be skipped as
		/// the die sequence grows.
		/// </summary>
		/// <param name="aqLex">
		/// The lexicon to be searched.
		/// </param>
		/// <param name="aqText">
		/// The text to be sought.
		/// </param>
		/// <param name="aLast">
		/// The instance from which state is to be borrowed.
		/// </param>
		public tLook(tLook aLast, string aqText) {
			aqText.sThrowNull("aqText");
			
			eqLex = aLast.eqLex;
			eErud = aLast.eErud;
			eqText = aqText;
			
			ejFore = aLast.ejFore;
			ejAft = aLast.ejAft;
		}
		
		/// <summary>
		/// Compares eqText with the specified word, returning a negative number
		/// if eqText sorts before, a positive number if sorts after, and zero if
		/// it matches the beginning or the entirety of the word.
		/// </summary>
		Int32 eComp(string aqTextLex) {
			aqTextLex.sThrowNull("aqTextLex");
			
			// eqText represents the current die sequence. Because the sequence
			// grows as the board is enumerated, it would be a mistake to require a
			// full match here; it is necessary instead to stop when the search
			// identifies a set of possible future matches. Going further would
			// narrow and focus the window on the start of the word set beginning
			// with eqText, which could cause words near the end of that set to be
			// missed when the sequence grows:
			if (eqText.Length < aqTextLex.Length)
				return String.Compare(eqText, 0, aqTextLex, 0, eqText.Length);
			// When eqText is longer than aqTextLex, it is desireable to require a
			// full match, as aqTextLex is necessarily too short to match the
			// sequences following eqText:
			return String.Compare(eqText, aqTextLex);
		}
		
		/// <summary>
		/// Searches eqLex for eqText, narrowing the window until a match or a
		/// miss is identified. If aStopFrag is set to 'true', the search will
		/// also stop when a fragment is identified. Returns the reason the search
		/// stopped.
		/// </summary>
		public tOut Exec(bool aStopFrag) {
			if (eqLex.Ct() < 1) return tOut.Miss;
			
			while (true) {
				Int32 oMid = (ejFore + ejAft) / 2;
				tqLex.tEl oEl = eqLex[oMid];
				
				Int32 oComp = eComp(oEl.qText);
				if (oComp == 0) {
					if (oEl.qText == eqText) {
						if (oEl.Erud <= eErud) return tOut.Match;
						return tOut.Miss;
					}
					if (aStopFrag) return tOut.Frag;
					// oEl begins with but is longer than eqText, so the match, if any,
					// must precede oMid:
					ejAft = oMid;
				}
				// oEl sorts after eqText:
				else if (oComp < 0) ejAft = oMid;
				// oEl sorts before eqText:
				else ejFore = oMid;
				
				// The word in the middle of the window has just been checked. If the
				// window contracts in size to one word or less, no match is left to
				// be found:
				if ((ejAft - ejFore) <= 1) return tOut.Miss;
			}
		}
		
		/// <summary>
		/// Represents the outcome of a text search.
		/// </summary>
		public enum tOut {
			/// <summary>
			/// The text was not found.
			/// </summary>
			Miss = 0,
			/// <summary>
			/// A word was found that matches the sought word with one or more
			/// letters added to its end.
			/// </summary>
			Frag,
			/// <summary>
			/// An exact match was found.
			/// </summary>
			Match
		}
	}
}
