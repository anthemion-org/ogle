// SetupAdv.cs
// -----------
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
	/// Represents advanced settings, including sound settings and dialect.
	/// </summary>
	public struct tSetupAdv {
		/// <summary>
		/// The sound volume, as a percent of maximum volume.
		/// </summary>
		Int32 ePerVol;
		/// <summary>
		/// Set to 'true' if sound playback is to be muted.
		/// </summary>
		bool eMute;
		/// <summary>
		/// The English dialect to be used during play.
		/// </summary>
		tDial eDial;
		
		public tSetupAdv(Int32 aPerVol, bool aMute, tDial aDial) {
			ePerVol = aPerVol;
			eMute = aMute;
			eDial = aDial;
		}
		
		/// <summary>
		/// The sound volume, as a percent of maximum volume.
		/// </summary>
		public Int32 PerVol {
			get { return ePerVol; }
			set { ePerVol = value; }
		}
		
		/// <summary>
		/// Set to 'true' if sound playback is to be muted.
		/// </summary>
		public bool Mute {
			get { return eMute; }
			set { eMute = value; }
		}
		
		/// <summary>
		/// The English dialect to be used during play.
		/// </summary>
		public tDial Dial {
			get { return eDial; }
			set { eDial = value; }
		}
		
		/// <summary>
		/// Stores an XML representation of the advanced settings in the specified
		/// file, its path relative to the executable path.
		/// </summary>
		public void Store(string aqNameFile) {
			aqNameFile.sThrowNull("aqNameFile");
			
			try {
				var oqWrite = XmlWriter.Create(aqNameFile, tqMain.sqSetsWriteXML);
				oqWrite.WriteStartElement(eqNameRoot);
				oqWrite.WriteAttributeString(eqNameAttrPerVol, PerVol.ToString());
				oqWrite.WriteAttributeString(eqNameAttrMute, Mute.ToString());
				oqWrite.WriteAttributeString(eqNameAttrDial, ((Int32)Dial)
					.ToString());
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
		const string eqNameRoot = "SetupAdv";
		/// <summary>
		/// The XML Volume attribute name.
		/// </summary>
		const string eqNameAttrPerVol = "PerVol";
		/// <summary>
		/// The XML Mute attribute name.
		/// </summary>
		const string eqNameAttrMute = "Mute";
		/// <summary>
		/// The XML Dialect attribute name.
		/// </summary>
		const string eqNameAttrDial = "Dial";
		
		/// <summary>
		/// Creates and returns a default instance.
		/// </summary>
		public static tSetupAdv sDef() {
			return new tSetupAdv(88, false, tDial.American);
		}
		
		/// <summary>
		/// Creates and returns an instance read from the specified XML file, its
		/// path relative to the executable path. A default instance is returned
		/// if the file cannot be read.
		/// </summary>
		public static tSetupAdv sReadOrDef(string aqNameFile) {
			aqNameFile.sThrowNull("aqNameFile");
			
			try {
				if (!File.Exists(aqNameFile)) return tSetupAdv.sDef();
				
				Int32 oPerVol = 88;
				bool oMute = false;
				tDial oDial = tDial.American;
				using (var oqRead = XmlReader.Create(aqNameFile))
					while (!oqRead.EOF) {
						if ((oqRead.MoveToContent() != XmlNodeType.Element)
							|| (oqRead.Name != eqNameRoot)) {
							oqRead.Read();
							continue;
						}
						
						string oq = oqRead.GetAttribute(eqNameAttrPerVol);
						oPerVol = Int32.Parse(oq);
						
						oq = oqRead.GetAttribute(eqNameAttrMute);
						oMute = bool.Parse(oq);
						
						oq = oqRead.GetAttribute(eqNameAttrDial);
						oDial = (tDial)Int32.Parse(oq);
						
						break;
					}
				return new tSetupAdv(oPerVol, oMute, oDial);
			}
			catch {
				return tSetupAdv.sDef();
			}
		}
		
		/// <summary>
		/// Represents an English dialect to be used during play.
		/// </summary>
		public enum tDial {
			// These enumeration values are stored in the advanced settings file.
			
			/// <summary>
			/// American English
			/// </summary>
			American = 0,
			/// <summary>
			/// British English
			/// </summary>
			British,
			/// <summary>
			/// Canadian English
			/// </summary>
			Canadian
		}
	}
}
