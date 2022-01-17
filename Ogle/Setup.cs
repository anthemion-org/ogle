// Setup.cs
// --------
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
using System.IO;
using System.Xml;
using nMisc;

namespace nOgle {
	/// <summary>
	/// Represents game settings, including Yield and Pace.
	/// </summary>
	public struct tSetup {
		/// <summary>
		/// The maximum Yield.
		/// </summary>
		public static readonly Int32 sYldMax = 2;
		/// <summary>
		/// The default Yield.
		/// </summary>
		const Int32 eYldDef = 2;
		
		/// <summary>
		/// The stored Yield.
		/// </summary>
		Int32 eYld;
		
		/// <summary>
		/// The maximum Pace.
		/// </summary>
		public static readonly Int32 sPaceMax = 6;
		/// <summary>
		/// The default Pace.
		/// </summary>
		const Int32 ePaceDef = 2;
		
		/// <summary>
		/// The stored Pace.
		/// </summary>
		Int32 ePace;
		
		public tSetup(Int32 aYld, Int32 aPace) {
			if ((aYld < 0) || (aYld > sYldMax))
				throw new Exception("Yield out-of-range");
			eYld = aYld;
			
			if ((aPace < 0) || (aPace > sPaceMax))
				throw new Exception("Pace out-of-range");
			ePace = aPace;
		}
		
		/// <summary>
		/// The stored Yield.
		/// </summary>
		public Int32 Yld {
			get { return eYld; }
			set { eYld = value; }
		}
		
		/// <summary>
		/// Returns the name of the stored Yield.
		/// </summary>
		public string TextYld() {
			switch (eYld) {
				case 0: return "Sparse";
				case 1: return "Adequate";
				default: return "Full";
			}
		}
		
		/// <summary>
		/// Returns the letter pool frequency ceiling for the stored Yield. See
		/// <see cref="tqPool.tqPool(float)"/>
		/// </summary>
		public float CeilPool() {
			switch (eYld) {
				case 0: return 0.50F;
				case 1: return 0.75F;
				default: return 1.0F;
			}
		}
		
		/// <summary>
		/// Returns the minimum word count for the stored Yield, or 'null' if
		/// there is no minimum.
		/// </summary>
		public Int32? CtWordMin() {
			switch (eYld) {
				case 0: return null;
				case 1: return 50;
				default: return 100;
			}
		}
		
		/// <summary>
		/// Returns the maximum word count for the stored Yield, or 'null' if
		/// there is no maximum.
		/// </summary>
		public Int32? CtWordMax() {
			switch (eYld) {
				case 0: return 50;
				case 1: return 100;
				default: return null;
			}
		}
		
		/// <summary>
		/// The stored Pace.
		/// </summary>
		public Int32 Pace {
			get { return ePace; }
			set { ePace = value; }
		}
		
		/// <summary>
		/// Returns the name of the stored Pace.
		/// </summary>
		public string TextPace() {
			switch (ePace) {
				case 0: return "Plodding";
				case 1: return "Slow";
				case 2: return "Unhurried";
				case 3: return "Measured";
				case 4: return "Brisk";
				case 5: return "Fast";
				default: return "Dizzying";
			}
		}
		
		/// <summary>
		/// Returns the starting play time span for the stored Pace.
		/// </summary>
		public TimeSpan TimeStart() {
			return TimeSpan.FromSeconds(TimeAdd().Seconds * 6);
		}
		
		/// <summary>
		/// Returns the bonus time span for the stored Pace.
		/// </summary>
		public TimeSpan TimeAdd() {
			Int32 oSecs;
			switch (ePace) {
				case 0:
					oSecs = 8;
					break;
				case 1:
					oSecs = 6;
					break;
				case 2:
					oSecs = 5;
					break;
				case 3:
					oSecs = 4;
					break;
				case 4:
					oSecs = 3;
					break;
				case 5:
					oSecs = 2;
					break;
				default:
					oSecs = 1;
					break;
			}
			return TimeSpan.FromSeconds(oSecs);
		}
		
		/// <summary>
		/// Returns the bonus time to be added for the stored Pace after entering
		/// a word.
		/// </summary>
		/// <param name="aLenWord">
		/// The length of the entered word.
		/// </param>
		/// <param name="aLenAdd">
		/// The length by which the entered word exceeds the word it follows, if
		/// it follows one.
		/// </param>
		public TimeSpan TimeAdd(Int32 aLenWord, Int32 aLenAdd) {
			Int32 oCt;
			if (aLenAdd < aLenWord) oCt = aLenAdd;
			else oCt = aLenWord - tqMain.sLenEntMin + 1;
			
			if (oCt < 1) return TimeSpan.Zero;
			return TimeSpan.FromSeconds(oCt * TimeAdd().Seconds);
		}
		
		/// <summary>
		/// Stores an XML representation of the game settings in the specified
		/// file, its path relative to the executable path.
		/// </summary>
		public void Store(string aqNameFile) {
			aqNameFile.sThrowNull("aqNameFile");
			
			try {
				var oqWrite = XmlWriter.Create(aqNameFile, tqMain.sqSetsWriteXML);
				oqWrite.WriteStartElement(eqNameRoot);
				oqWrite.WriteAttributeString(eqNameAttrYld, Yld.ToString());
				oqWrite.WriteAttributeString(eqNameAttrPace, Pace.ToString());
				oqWrite.WriteEndElement();
				oqWrite.Close();
			}
			catch (Exception aq) {
				tqMain.sExcept_Rep(aq);
			}
		}
		
		// Static
		// ------
		
		/// <summary>
		/// The XML root element name.
		/// </summary>
		const string eqNameRoot = "Setup";
		/// <summary>
		/// The XML Yield attribute name.
		/// </summary>
		const string eqNameAttrYld = "Yld";
		/// <summary>
		/// The XML Pace attribute name.
		/// </summary>
		const string eqNameAttrPace = "Pace";
		
		/// <summary>
		/// Creates and returns a default instance.
		/// </summary>
		public static tSetup sDef() {
			return new tSetup(eYldDef, ePaceDef);
		}
		
		/// <summary>
		/// Creates and returns an instance read from the specified XML file, its
		/// path relative to the executable path. A default instance is returned
		/// if the file cannot be read.
		/// </summary>
		public static tSetup sReadOrDef(string aqNameFile) {
			aqNameFile.sThrowNull("aqNameFile");
			
			try {
				if (!File.Exists(aqNameFile)) return tSetup.sDef();
				
				Int32 oYld = eYldDef;
				Int32 oPace = ePaceDef;
				using (var oqRead = XmlReader.Create(aqNameFile))
					while (!oqRead.EOF) {
						if ((oqRead.MoveToContent() != XmlNodeType.Element)
							|| (oqRead.Name != eqNameRoot)) {
							oqRead.Read();
							continue;
						}
						
						string oq = oqRead.GetAttribute(eqNameAttrYld);
						oYld = Int32.Parse(oq);
						
						oq = oqRead.GetAttribute(eqNameAttrPace);
						oPace = Int32.Parse(oq);
						
						break;
					}
				return new tSetup(oYld, oPace);
			}
			catch {
				return tSetup.sDef();
			}
		}
	}
}
