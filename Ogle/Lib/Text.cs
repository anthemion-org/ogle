// Text.cs
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

namespace nText {
	/// <summary>
	/// Implements various methods used to work with text.
	/// </summary>
	public static class tqText {
		/// <summary>
		/// Returns the English equivalent of the specified value if the value is
		/// between zero and twelve; returns the numeric text equivalent
		/// otherwise.
		/// </summary>
		/// <example>
		///	Examples: '-1', 'zero', 'ten', '15', '1000'.
		/// </example>
		public static string sText(Int32 aVal) {
			if ((aVal < 0) || (aVal > 12)) return aVal.ToString();
			switch (aVal) {
				case 0: return "zero";
				case 1: return "one";
				case 2: return "two";
				case 3: return "three";
				case 4: return "four";
				case 5: return "five";
				case 6: return "six";
				case 7: return "seven";
				case 8: return "eight";
				case 9: return "nine";
				case 10: return "ten";
				case 11: return "eleven";
				default: return "twelve";
			}
		}
	}
}
