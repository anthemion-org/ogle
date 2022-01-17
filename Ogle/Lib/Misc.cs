// Misc.cs
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

namespace nMisc {
	/// <summary>
	/// Implements miscellaneous library methods.
	/// </summary>
	public static class tMisc {
		/// <summary>
		/// Throws a NullReferenceException attributed to the specified variable
		/// if the variable is null.
		/// </summary>
		//
		// This technique adapted from:
		//
		//   http://stackoverflow.com/questions/665454/c-should-i-bother-checking-for-null-in-this-situation:
		//
		public static void sThrowNull<xq>(this xq aq, string aqName)
			where xq: class {
			
			if (aq == null) {
				if (aqName == null) throw new NullReferenceException("Null value");
				throw new NullReferenceException("Null value '" + aqName + "'");
			}
		}
	}
}
