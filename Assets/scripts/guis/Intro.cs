using UnityEngine;

public class Intro : Gui {

	private Rect NextRect;
	private Rect IntroductionRect;

	private GUIStyle NextStyle;
	private GUIStyle IntroductionStyle;

	public Intro(){

		NextRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - (((Main.NativeHeight / 8f) - (Main.NativeWidth * 0.05f)) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 8f) - (Main.NativeWidth * 0.05f));
		IntroductionRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeWidth * 0.05f, Main.NativeWidth - (Main.NativeWidth * 0.1f), Main.NativeHeight - (Main.NativeWidth * 0.1f));

		NextStyle = new GUIStyle();
		NextStyle.fontSize = Main.FontLargest;
		NextStyle.normal.textColor = Colors.ClickableText;
		NextStyle.alignment = TextAnchor.MiddleCenter;

		IntroductionStyle = new GUIStyle();
		IntroductionStyle.fontSize = Main.FontLarge;
		IntroductionStyle.normal.textColor = Colors.ReadableText;
		IntroductionStyle.alignment = TextAnchor.MiddleCenter;
		IntroductionStyle.wordWrap = true;

	}

	public override void OnGUI(){

		// Utils.DrawRectangle(NextRect, 50, Colors.ButtonOutline);
		Utils.FillRoundedRectangle(NextRect, Colors.Gold);
		GUI.Label(NextRect, "ACKNOWLEDGE", NextStyle);
		if(Main.Clicked && NextRect.Contains(Main.TouchGuiLocation)){
			Main.SetGui(new MainMenu());
		}
		
		GUI.Label(IntroductionRect, "Don't play this game if you find any words offensive.", IntroductionStyle);
	}

}