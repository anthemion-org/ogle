// Dir.cs
// ------
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
using System.Drawing;
using System.Collections.Generic;

namespace nDir {
	// Directions start with East because the zero angle traditionally points to
	// the right. They proceed clockwise because, after accounting for the
	// reversed Y-axis, angles progress in this direction as they increase.
	
	/// <summary>
	/// Represents a direction or orientation. Values are limited to the four
	/// cardinal directions.
	/// </summary>
	public enum tDir4 {
		// It might be useful to set the enumeration values with the angles they
		// represent, but it seems better to define them as normal so they can
		// serve as array indices:
		E = 0,
		S,
		W,
		N
	}
	
	/// <summary>
	/// Represents a direction or orientation. Values are limited to the eight
	/// cardinal and intermediate directions.
	/// </summary>
	public enum tDir8 {
		// It might be useful to set the enumeration values with the angles they
		// represent, but it seems better to define them as normal so they can
		// serve as array indices:
		E = 0,
		SE,
		S,
		SW,
		W,
		NW,
		N,
		NE
	}
	
	/// <summary>
	/// Implements a number of methods used to work with directions.
	/// </summary>
	public static class tDir {
		
		// tDir4
		// -----
		
		/// <summary>
		/// Returns the degree angle from the zero angle to the specified
		/// direction.
		///
		/// Because the Y-axis is reversed, points move clockwise relative to
		/// to the origin as their angles increase relative to the positive X-
		/// axis, not counter-clockwise.
		/// </summary>
		public static Int32 sDeg(tDir4 aDir) {
			switch (aDir) {
				case tDir4.N: return 270;
				case tDir4.E: return 0;
				case tDir4.S: return 90;
				// tDir4.W:
				default: return 180;
			}
		}
		
		// tDir8
		// -----
		
		/// <summary>
		/// Returns an offset corresponding to the specified direction.
		/// </summary>
		public static Size sOff(tDir8 aDir) {
			switch (aDir) {
				case tDir8.E: return new Size(1, 0);
				case tDir8.SE: return new Size(1, 1);
				case tDir8.S: return new Size(0, 1);
				case tDir8.SW: return new Size(-1, 1);
				case tDir8.W: return new Size(-1, 0);
				case tDir8.NW: return new Size(-1, -1);
				case tDir8.N: return new Size(0, -1);
				case tDir8.NE: return new Size(1, -1);
				default: throw new Exception("Invalid tDir8 value");
			}
		}
		
		/// <summary>
		/// Returns all eight cardinal and intermediate offsets.
		/// </summary>
		public static IEnumerable<Size> sOffs8() {
			yield return new Size(1, 0);
			yield return new Size(1, 1);
			yield return new Size(0, 1);
			yield return new Size(-1, 1);
			yield return new Size(-1, 0);
			yield return new Size(-1, -1);
			yield return new Size(0, -1);
			yield return new Size(1, -1);
		}
		
		/// <summary>
		/// Returns the cardinal or intermediate direction closest to the
		/// specified offset, or null if the zero offset is specified.
		/// </summary>
		public static tDir8? sDir8(Size aOff) {
			// This seems clearer than using NaN and the infinity values:
			if (aOff.Width == 0) {
				if (aOff.Height < 0) return tDir8.N;
				if (aOff.Height > 0) return tDir8.S;
				return null;
			}
			
			float oSl = (float)aOff.Height / aOff.Width;
			
			if (aOff.Width > 0) {
				if (oSl < esSlNNE) return tDir8.N;
				else if (oSl < esSlENE) return tDir8.NE;
				else if (oSl < esSlESE) return tDir8.E;
				else if (oSl < esSlSSE) return tDir8.SE;
				return tDir8.S;
			}
			
			if (oSl > esSlNNW) return tDir8.N;
			else if (oSl > esSlWNW) return tDir8.NW;
			else if (oSl > esSlWSW) return tDir8.W;
			else if (oSl > esSlSSW) return tDir8.SW;
			return tDir8.S;
		}
		
		// Slopes
		// ------
		
		// 22.5 degrees:
		static readonly float esSlENE = -0.414214F;
		// 67.5 degrees:
		static readonly float esSlNNE = -2.41421F;
		// 112.5 degrees:
		static readonly float esSlNNW = 2.41421F;
		// 157.5 degrees:
		static readonly float esSlWNW = 0.414214F;
		// 202.5 degrees:
		static readonly float esSlWSW = esSlENE;
		// 247.5 degrees:
		static readonly float esSlSSW = esSlNNE;
		// 292.5 degrees:
		static readonly float esSlSSE = esSlNNW;
		// 337.5 degrees:
		static readonly float esSlESE = esSlWNW;
	}
}
