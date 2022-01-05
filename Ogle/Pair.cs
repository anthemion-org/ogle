// Pair.cs
// -------
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

namespace nOgle {
	/// <summary>
	/// Represents an entry pair, associating a word with the selections made by
	/// one or both players when entering it.
	/// </summary>
	/// <remarks>
	/// Keep in mind that the players could select the same word in different 
	/// ways.
	/// </remarks>
	public struct tPair: IComparable<tPair> {
		/// <summary>
		/// The entered word.
		/// </summary>
		readonly string eqText;
		/// <summary>
		/// The entered word.
		/// </summary>
		public string qText {
			get { return eqText; }
		}
		
		/// <summary>
		/// The player's selection, or 'null' if the player did not enter the
		/// word.
		/// </summary>
		readonly tqSel eqSelPlay;
		/// <summary>
		/// The player's selection, or 'null' if the player did not enter the
		/// word.
		/// </summary>
		public tqSel qSelPlay {
			get { return eqSelPlay; }
		}
		
		/// <summary>
		/// The computer's selection, or 'null' if the computer did not enter the
		/// word.
		/// </summary>
		readonly tqSel eqSelComp;
		/// <summary>
		/// The computer's selection, or 'null' if the computer did not enter the
		/// word.
		/// </summary>
		public tqSel qSelComp {
			get { return eqSelComp; }
		}
		
		/// <summary>
		/// Creates an instance representing the specified selection. At least one
		/// selection must be non-null. If both are non-null, they should spell
		/// the same word, though they need not spell it with the same grid
		/// elements.
		/// </summary>
		public tPair(tqSel aqSelPlay, tqSel aqSelComp) {
			if ((aqSelPlay == null) && (aqSelComp == null))
				throw new Exception("Both entries null");
			
			if (aqSelPlay != null) eqText = aqSelPlay.qText;
			else eqText = aqSelComp.qText;
			
			eqSelPlay = aqSelPlay;
			eqSelComp = aqSelComp;
		}
		
		/// <summary>
		/// Returns the player's selection, if that is set, or the computer's
		/// selection, if it is not.
		/// </summary>
		public tqSel SelPref() {
			if (eqSelPlay != null) return eqSelPlay;
			if (eqSelComp != null) return eqSelComp;
			throw new Exception("No entry set");
		}
		
		// IComparable<tPair>
		// ------------------
		
		/// <summary>
		/// Sorts by length, from longest to shortest, then alphabetically.
		/// </summary>
		public Int32 CompareTo(tPair aPair) {
			if (eqText.Length > aPair.eqText.Length) return -1;
			if (eqText.Length < aPair.eqText.Length) return 1;
			return eqText.CompareTo(aPair.eqText);
		}
	}
}
