// BuildRsc.cs
// Written by Jeremy Neal Kelly
// Copyright ©2009 Anthemion Designs
// www.anthemion.org

using System;
using System.Resources;
using System.Drawing;

class tMain {
	static void Main() {
		try {
			using (var oWrite = new ResXResourceWriter("Main.resx")) {
				oWrite.AddResource("BackGrid", Image.FromFile("Back Grid.png"));
				
				oWrite.AddResource("Die", Image.FromFile("Die.png"));
				oWrite.AddResource("DieEmph", Image.FromFile("Die Emph.png"));
				
				oWrite.AddResource("SelDie", Image.FromFile("Sel Die.png"));
				oWrite.AddResource("CursDie", Image.FromFile("Curs Die.png"));
				
				oWrite.AddResource("LinkDieHorz", Image.FromFile(
					"Link Die Horz.png"));
				oWrite.AddResource("LinkDieVert", Image.FromFile(
					"Link Die Vert.png"));
				oWrite.AddResource("LinkDieDown", Image.FromFile(
					"Link Die Down.png"));
				oWrite.AddResource("LinkDieUp", Image.FromFile("Link Die Up.png"));
				
				oWrite.AddResource("UnderDieRight", Image.FromFile(
					"Under Die Right.png"));
				oWrite.AddResource("UnderDieBtm", Image.FromFile(
					"Under Die Btm.png"));
				oWrite.AddResource("UnderDieLeft", Image.FromFile(
					"Under Die Left.png"));
				oWrite.AddResource("UnderDieTop", Image.FromFile(
					"Under Die Top.png"));
			}
		}
		catch (Exception aExc) {
			Console.WriteLine(aExc.ToString());
		}
	}
}
