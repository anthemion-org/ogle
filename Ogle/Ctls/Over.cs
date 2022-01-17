// Over.cs
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

namespace nOgle {
	/// <summary>
	/// Represents a letter grid overlay, used to change the appearance of
	/// specific elements within the grid.
	/// </summary>
	/// <remarks>
	/// Overlay support supported by the letter grid and the forms that use it,
	/// but at present, nothing is actually done with it.
	/// </remarks>
	public class tqOver {
		/// <summary>
		/// Stores all overlay states.
		/// </summary>
		readonly tSt[,] eqSts = new tSt[tqMain.sWthGrid, tqMain.sHgtGrid];
		/// <summary>
		/// Gets and sets the overlay state for the specified square.
		/// </summary>
		public tSt this[Int32 aX, Int32 aY] {
			get {
				if (!tqMain.sCkGrid(aX, aY))
					throw new Exception("Position out of range");
				return eqSts[aX, aY];
			}
			set {
				if (!tqMain.sCkGrid(aX, aY))
					throw new Exception("Position out of range");
				eqSts[aX, aY] = value;
			}
		}
		
		/// <summary>
		/// Represents the overlay state of a single letter grid element.
		/// </summary>
		public enum tSt {
			/// <summary>
			/// The element is displayed as normal.
			/// </summary>
			None = 0,
			/// <summary>
			/// The element is displayed with emphasis.
			/// </summary>
			Emph
		}
	}
}
