// Ctl.cs
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
using System.Drawing;
using System.Windows.Forms;
using nMisc;

namespace nCtl {
	/// <summary>
	/// Implements various methods used to work with controls.
	/// </summary>
	public static class tCtl {
		/// <summary>
		/// Returns the specified text truncated on the right to fit the specified
		/// width when rendered with aqFont. An ellipsis is added to the end if
		/// the string is truncated.
		/// </summary>
		public static string sTextFitRight(string aqText, Font aqFont, Int32
			aWthMax) {
			
			aqText.sThrowNull("aqText");
			aqFont.sThrowNull("aqFont");
			
			if (aWthMax < 1) return "";
			
			string oqText = aqText;
			while (true) {
				Size oSize = TextRenderer.MeasureText(oqText, aqFont);
				if (oSize.Width <= aWthMax) break;
				
				if (oqText.Length <= 1) return "";
				
				if (oqText[oqText.Length - 1] == '…')
					oqText = oqText.Substring(0, oqText.Length - 2) + '…';
				else oqText = oqText.Substring(0, oqText.Length - 1) + '…';
			}
			return oqText;
		}
		
		/// <summary>
		/// Returns the specified text truncated on the left to fit the specified
		/// width when rendered with aqFont. An ellipsis is added to the beginning
		/// if the string is truncated.
		/// </summary>
		public static string sTextFitLeft(string aqText, Font aqFont, Int32
			aWthMax) {
			
			aqText.sThrowNull("aqText");
			aqFont.sThrowNull("aqFont");
			
			if (aWthMax < 1) return "";
			
			string oqText = aqText;
			while (true) {
				Size oSize = TextRenderer.MeasureText(oqText, aqFont);
				if (oSize.Width <= aWthMax) break;
				
				if (oqText.Length <= 1) return "";
				
				if (oqText[0] == '…') oqText = '…' + oqText.Remove(0, 2);
				else oqText = '…' + oqText.Remove(0, 1);
			}
			return oqText;
		}
	}
}
