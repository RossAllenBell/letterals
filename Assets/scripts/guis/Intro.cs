using UnityEngine;

public class Intro : Gui {

	public static readonly Color NavyBlue = new Color(0, 34f/255, 171f/255);

	private Rect NextRect;
	private Rect IntroductionRect;

	private GUIStyle NextStyle;
	private GUIStyle IntroductionStyle;

	public Intro(){

		NextRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - (((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		IntroductionRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeWidth * 0.05f, Main.NativeWidth - (Main.NativeWidth * 0.1f), Main.NativeHeight - (Main.NativeWidth * 0.1f));

		NextStyle = new GUIStyle();
		NextStyle.fontSize = Main.FontLarge;
		NextStyle.normal.textColor = NavyBlue;
		NextStyle.alignment = TextAnchor.MiddleCenter;

		IntroductionStyle = new GUIStyle();
		IntroductionStyle.fontSize = Main.FontLarge;
		IntroductionStyle.normal.textColor = Color.black;
		IntroductionStyle.alignment = TextAnchor.MiddleCenter;
		IntroductionStyle.wordWrap = true;

	}

	public override void OnGUI(){

		GUI.Label(NextRect, "ACKNOWLEDGE", NextStyle);
		Utils.DrawRectangle(NextRect, 50, Color.black);
		if(Main.Clicked && NextRect.Contains(Main.TouchGuiLocation)){
			Main.SetGui(new MainMenu());
		}
		
		GUI.Label(IntroductionRect, "I've made an attempt to remove offensive words from the list of words used in this game. However, you should not play this game if you find any words offensive.", IntroductionStyle);
	}

}