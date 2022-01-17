// Die.cs
// ------
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

using nDir;
using nMisc;

namespace nOgle {
	/// <summary>
	/// Represents the text, underlining, and orientation of a single die within
	/// the letter grid.
	/// </summary>
	public struct tDie {
		/// <summary>The die text.</summary>
		readonly string eqText;
		/// <summary>Set to 'true' if the text is underlined.</summary>
		readonly bool eUnder;
		/// <summary>The die orientation.</summary>
		readonly tDir4 eOrient;
		
		public tDie(string aqText, tDir4 aOrient) {
			aqText.sThrowNull("aqText");
			
			eqText = aqText;
			eUnder = (aqText == "L")
				|| (aqText == "T")
				|| (aqText == "N")
				|| (aqText == "Z")
				|| (aqText == "W");
			eOrient = aOrient;
		}
		
		public tDie(tDie aDie) {
			eqText = aDie.eqText;
			eUnder = aDie.eUnder;
			eOrient = aDie.eOrient;
		}
		
		/// <summary>The die text.</summary>
		public string qText {
			get {
				return eqText;
			}
		}
		
		/// <summary>Set to 'true' if the text is underlined.</summary>
		public bool Under {
			get {
				return eUnder;
			}
		}
		
		/// <summary>The die orientation.</summary>
		public tDir4 Orient {
			get {
				return eOrient;
			}
		}
	}
}
