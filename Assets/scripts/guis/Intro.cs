using UnityEngine;

public class Intro : Gui {

	private Rect NextRect;
	private Rect IntroductionRect;

	private GUIStyle NextStyle;
	private GUIStyle IntroductionStyle;

	private float startTime;
	private float endTime;

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

		startTime = Time.time;
	}

	public override void OnGUI(){

		if(Time.time - startTime < Gui.FadeIn) {
			GUI.color = new Color(1f, 1f, 1f, (Time.time - startTime) / Gui.FadeIn);
		}
		if(endTime != 0){
			if(Time.time - endTime > Gui.FadeOut){
				Main.SetGui(new MainMenu());
			} else {
				GUI.color = new Color(1f, 1f, 1f, 1f - ((Time.time - endTime) / Gui.FadeOut));
			}
		}

		// Utils.DrawRectangle(NextRect, 50, Colors.ButtonOutline);
		Utils.FillRectangle(NextRect, Colors.ButtonBackground);
		GUI.Label(NextRect, "ACKNOWLEDGE", NextStyle);
		if(Main.Clicked && NextRect.Contains(Main.TouchGuiLocation)){
			endTime = Time.time;
		}
		
		GUI.Label(IntroductionRect, "Don't play this game if you find any words offensive.", IntroductionStyle);
	}

}