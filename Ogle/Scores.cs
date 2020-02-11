// Scores.cs
// ---------
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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using nMisc;

namespace nOgle {
	/// <summary>
	/// Manages the point total and percent high score lists for all Yield /
	/// Pace combinations.
	/// </summary>
	public class tqScores {
		/// <summary>
		/// Point total lists by Yield and Pace:
		/// </summary>
		readonly tqList[,] eqListsPts;
		/// <summary>
		/// Percent lists by Yield and Pace:
		/// </summary>
		readonly tqList[,] eqListsPer;
		/// <summary>
		/// An empty list, returned in place of unset elements.
		/// </summary>
		readonly tqList eqListEmpty;
		
		/// <summary>
		/// Creates an instance with no score data. Not to be used except by
		/// <see cref="tqScores.sReadOrDef"/>.
		/// </summary>
		tqScores() {
			Int32 oCtYld = tSetup.sYldMax + 1;
			Int32 oCtPace = tSetup.sPaceMax + 1;
			eqListsPts = new tqList[oCtYld, oCtPace];
			eqListsPer = new tqList[oCtYld, oCtPace];
			eqListEmpty = new tqList();
		}
		
		/// <summary>
		/// Returns an enumerator for the point total elements matching the
		/// specified Yield and Pace.
		/// </summary>
		public IEnumerable<tEl> Pts(Int32 aYld, Int32 aPace) {
			tqList oq = eqListsPts[aYld, aPace];
			if (oq == null) return eqListEmpty;
			return oq;
		}
		
		/// <summary>
		/// Returns an enumerator for the point total elements matching the Yield
		/// and Pace of the specified settings.
		/// </summary>
		public IEnumerable<tEl> Pts(tSetup aSetup) {
			return Pts(aSetup.Yld, aSetup.Pace);
		}
		
		/// <summary>
		/// Returns an enumerator for the percent elements matching the specified
		/// Yield and Pace.
		/// </summary>
		public IEnumerable<tEl> Per(Int32 aYld, Int32 aPace) {
			tqList oq = eqListsPer[aYld, aPace];
			if (oq == null) return eqListEmpty;
			return oq;
		}
		
		/// <summary>
		/// Returns an enumerator for the percent elements matching the Yield and
		/// Pace of the specified settings.
		/// </summary>
		public IEnumerable<tEl> Per(tSetup aSetup) {
			return Per(aSetup.Yld, aSetup.Pace);
		}
		
		/// <summary>
		/// Returns 'true' if the specified point total qualifies as a high score
		/// at the specified Yield and Pace.
		/// </summary>
		/// <param name="aTime">
		/// The creation time of the round in which the score was earned.
		/// </param>
		public bool CkHighPts(Int32 aYld, Int32 aPace, Int32 aPts, DateTime aTime)
		{
			tqList oq = eqListsPts[aYld, aPace];
			if (oq == null) return true;
			return oq.CkHigh(aPts, aTime);
		}
		
		/// <summary>
		/// Returns 'true' if the specified percent qualifies as a high score at
		/// the specified Yield and Pace.
		/// </summary>
		/// <param name="aTime">
		/// The creation time of the round in which the score was earned.
		/// </param>
		public bool CkHighPer(Int32 aYld, Int32 aPace, Int32 aPer, DateTime aTime)
		{
			tqList oq = eqListsPer[aYld, aPace];
			if (oq == null) return true;
			return oq.CkHigh(aPer, aTime);
		}
		
		/// <summary>
		/// Adds the specified element to the point total high score list for the
		/// specified Yield and Pace, assuming it qualifies.
		/// </summary>
		public void Pts_Add(Int32 aYld, Int32 aPace, tEl aEl) {
			tqList oq = eqListsPts[aYld, aPace];
			if (oq == null) {
				oq = new tqList();
				eqListsPts[aYld, aPace] = oq;
			}
			oq.Add(aEl);
		}
		
		/// <summary>
		/// Adds the specified element to the percent high score list for the
		/// specified Yield and Pace, assuming it qualifies.
		/// </summary>
		public void Per_Add(Int32 aYld, Int32 aPace, tEl aEl) {
			tqList oq = eqListsPer[aYld, aPace];
			if (oq == null) {
				oq = new tqList();
				eqListsPer[aYld, aPace] = oq;
			}
			oq.Add(aEl);
		}
		
		/// <summary>
		/// The XML root element name.
		/// </summary>
		const string eqNameRoot = "Scores";
		/// <summary>
		/// The XML point totals array element name.
		/// </summary>
		const string eqNamePts = "Pts";
		/// <summary>
		/// The XML percents array element name.
		/// </summary>
		const string eqNamePer = "Per";
		
		/// <summary>
		/// The XML list element name.
		/// </summary>
		const string eqNameList = "List";
		/// <summary>
		/// The XML Yield attribute name.
		/// </summary>
		const string eqNameAttrYld = "Yld";
		/// <summary>
		/// The XML Pace attribute name.
		/// </summary>
		const string eqNameAttrPace = "Pace";
		
		/// <summary>
		/// The XML score element name.
		/// </summary>
		const string eqNameEl = "El";
		/// <summary>
		/// The XML player name attribute name.
		/// </summary>
		const string eqNameAttrName = "Name";
		/// <summary>
		/// The XML score value attribute name.
		/// </summary>
		const string eqNameAttrVal = "Val";
		/// <summary>
		/// The XML round creation time attribute name.
		/// </summary>
		const string eqNameAttrTime = "Time";
		
		/// <summary>
		/// Appends a score value set to an XML stream.
		/// </summary>
		void eLists_Store(XmlWriter aqWrite, tqList[,] aqLists) {
			aqWrite.sThrowNull("aqWrite");
			aqLists.sThrowNull("aqLists");
			
			for (Int32 oYld = 0; oYld <= tSetup.sYldMax; ++oYld)
				for (Int32 oPace = 0; oPace <= tSetup.sPaceMax; ++oPace) {
					tqList oqList = aqLists[oYld, oPace];
					if (oqList != null) {
						aqWrite.WriteStartElement(eqNameList);
						aqWrite.WriteAttributeString(eqNameAttrYld, oYld.ToString());
						aqWrite.WriteAttributeString(eqNameAttrPace, oPace.ToString());
						
						foreach (tEl oEl in oqList) {
							aqWrite.WriteStartElement(eqNameEl);
							aqWrite.WriteAttributeString(eqNameAttrName, oEl.qName);
							aqWrite.WriteAttributeString(eqNameAttrVal, oEl.Val.ToString());
							aqWrite.WriteAttributeString(eqNameAttrTime, oEl.Time
								.ToString());
							aqWrite.WriteEndElement();
						}
						
						aqWrite.WriteEndElement();
					}
				}
		}
		
		/// <summary>
		/// Stores the scores represented by this instance as XML in the specified
		/// file, its path relative to the executable path.
		/// </summary>
		public void Store(string aqNameFile) {
			aqNameFile.sThrowNull("aqNameFile");
			
			try {
				using (var oqWrite = XmlWriter.Create(aqNameFile, tqMain
					.sqSetsWriteXML)) {
					
					oqWrite.WriteStartElement(eqNameRoot);
					
					oqWrite.WriteStartElement(eqNamePts);
					eLists_Store(oqWrite, eqListsPts);
					oqWrite.WriteEndElement();
					
					oqWrite.WriteStartElement(eqNamePer);
					eLists_Store(oqWrite, eqListsPer);
					oqWrite.WriteEndElement();
					
					oqWrite.WriteEndElement();
				}
			}
			catch (Exception aq) {
				tqMain.sExcept_Rep(aq);
			}
		}
		
		// Static
		// ------
		
		/// <summary>
		/// Creates and returns an instance read from the specified XML file, its
		/// path relative to the executable path. A default instance is returned
		/// if the file cannot be read.
		/// </summary>
		public static tqScores sReadOrDef(string aqNameFile) {
			aqNameFile.sThrowNull("aqNameFile");
			
			try {
				var oqScores = new tqScores();
				if (!File.Exists(aqNameFile)) return oqScores;
				
				using (var oqRead = XmlReader.Create(aqNameFile)) {
					bool? oCkPts = null;
					Int32 oYld = 0;
					Int32 oPace = 0;
					while (!oqRead.EOF) {
						if (oqRead.MoveToContent() == XmlNodeType.Element)
							switch (oqRead.Name) {
								case eqNamePts:
									oCkPts = true;
									break;
								case eqNamePer:
									oCkPts = false;
									break;
								case eqNameList:
									string oq = oqRead.GetAttribute(eqNameAttrYld);
									oYld = Int32.Parse(oq);
									oq = oqRead.GetAttribute(eqNameAttrPace);
									oPace = Int32.Parse(oq);
									break;
								case eqNameEl:
									string oqName = oqRead.GetAttribute(eqNameAttrName);
									
									oq = oqRead.GetAttribute(eqNameAttrVal);
									Int32 oVal = Int32.Parse(oq);
									
									oq = oqRead.GetAttribute(eqNameAttrTime);
									DateTime oTime = DateTime.Parse(oq);
									
									if (!oCkPts.HasValue) throw new Exception("Type not set");
									var oEl = new tEl(oqName, oVal, oTime);
									if (oCkPts == true) oqScores.Pts_Add(oYld, oPace, oEl);
									else oqScores.Per_Add(oYld, oPace, oEl);
									break;
							}
						oqRead.Read();
					}
				}
				return oqScores;
			}
			catch (Exception aq) {
				tqMain.sExcept_Rep(aq);
				return new tqScores();
			}
		}
		
		/// <summary>
		/// Manages a single list of high scores, either point totals or percents.
		/// </summary>
		class tqList: IEnumerable<tEl> {
			const Int32 eCtMax = 5;
			List<tEl> eEls = new List<tEl>();
			
			public bool CkHigh(Int32 aVal, DateTime aTime) {
				if (eEls.Count < eCtMax) return true;
				var oEl = new tEl("", aVal, aTime);
				return !eEls.TrueForAll(o => (o.CompareTo(oEl) <= 0));
			}
			
			public void Add(tEl aEl) {
				if ((eEls.Count >= eCtMax)
					&& eEls.TrueForAll(o => (o.CompareTo(aEl) <= 0))) return;
				
				eEls.Add(aEl);
				eEls.Sort();
				if (eEls.Count > eCtMax) eEls.RemoveRange(5, eEls.Count - eCtMax);
			}
			
			// IEnumerable<tEl>
			// ----------------
			
			/// <summary>
			/// Returns an enumerator to the score elements in this instance.
			/// </summary>
			public IEnumerator<tEl> GetEnumerator() {
				return eEls.GetEnumerator();
			}
			
			/// <summary>
			/// Returns an enumerator to the score elements in this instance.
			/// </summary>
			//
			// Because IEnumerator<> inherits from IEnumerator, this must be
			// implemented as well:
			IEnumerator IEnumerable.GetEnumerator() {
				return GetEnumerator();
			}
		}
		
		/// <summary>
		/// Represents a high score element, storing the player's name, the score
		/// value, and the creation time of the board for which the score was
		/// earned.
		/// </summary>
		public struct tEl: IComparable<tEl> {
			/// <summary>
			/// The player's name.
			/// </summary>
			readonly string eqName;
			/// <summary>
			/// The player's name.
			/// </summary>
			public string qName {
				get { return eqName; }
			}
			
			/// <summary>
			/// The score value, whether point total or percent.
			/// </summary>
			readonly Int32 eVal;
			/// <summary>
			/// The score value, whether point total or percent.
			/// </summary>
			public Int32 Val {
				get { return eVal; }
			}
			
			/// <summary>
			/// The creation time of the board for which the score was earned.
			/// </summary>
			readonly DateTime eTime;
			/// <summary>
			/// The creation time of the board for which the score was earned.
			/// </summary>
			public DateTime Time {
				get { return eTime; }
			}
			
			/// <param name="aqName">
			/// The player's name.
			/// </param>
			/// <param name="aVal">
			/// The score value, whether point total or percent.
			/// </param>
			/// <param name="aTime">
			/// The creation time of the board for which the score was earned.
			/// </param>
			public tEl(string aqName, Int32 aVal, DateTime aTime) {
				aqName.sThrowNull("aqName");
				
				eqName = aqName;
				eVal = aVal;
				eTime = aTime;
			}
			
			// IComparable<tEl>
			// ----------------
			
			/// <summary>
			/// Sorts instances in descending order by score value, then in
			/// ascending order by board creation time. Player names are
			/// disregarded.
			/// <summary>
			public int CompareTo(tEl a) {
				if (Val > a.Val) return -1;
				if (Val < a.Val) return 1;
				
				if (Time < a.Time) return -1;
				if (Time > a.Time) return 1;
				
				return 0;
			}
		}
	}
}
