using UnityEngine;

public class MainMenu : Gui {

	private Rect ExitRect;
	private Rect TitleRect;
	private Rect EasyRect;
	private Rect MediumRect;
	private Rect HardRect;
	private Rect ScoresRect;

	private GUIStyle TitleStyle;
	private GUIStyle ExitStyle;
	private GUIStyle MenuDifficultyStyle;
	private GUIStyle ScoresStyle;

	public MainMenu(){

		TitleRect = new Rect(0, 0, Main.NativeWidth, (Main.NativeHeight / 8) * 2);
		ExitRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - (((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) + (Main.NativeWidth * 0.05f)), (Main.NativeWidth / 3) - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		EasyRect = new Rect(0 + (Main.NativeWidth * 0.05f), ((Main.NativeHeight / 8f) * 2) + (Main.NativeWidth * 0.025f), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 8f) - (Main.NativeWidth * 0.05f));
		MediumRect = new Rect(0 + (Main.NativeWidth * 0.05f), ((Main.NativeHeight / 8f) * 3) + (Main.NativeWidth * 0.025f), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 8f) - (Main.NativeWidth * 0.05f));
		HardRect = new Rect(0 + (Main.NativeWidth * 0.05f), ((Main.NativeHeight / 8f) * 4) + (Main.NativeWidth * 0.025f), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 8f) - (Main.NativeWidth * 0.05f));
		ScoresRect = new Rect(0 + (Main.NativeWidth * 0.05f), ((Main.NativeHeight / 8f) * 6) + (Main.NativeWidth * 0.025f), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 8f) - (Main.NativeWidth * 0.05f));
		
		TitleStyle = new GUIStyle();
		TitleStyle.fontSize = Main.FontLargest * 2;
		TitleStyle.normal.textColor = Colors.ReadableText;
		TitleStyle.alignment = TextAnchor.MiddleCenter;

		ExitStyle = new GUIStyle();
		ExitStyle.fontSize = Main.FontLarge;
		ExitStyle.normal.textColor = Colors.ClickableText;
		ExitStyle.alignment = TextAnchor.MiddleCenter;

		MenuDifficultyStyle = new GUIStyle();
		MenuDifficultyStyle.fontSize = Main.FontLargest;
		MenuDifficultyStyle.normal.textColor = Colors.ClickableText;
		MenuDifficultyStyle.alignment = TextAnchor.MiddleCenter;

		ScoresStyle = new GUIStyle();
		ScoresStyle.fontSize = Main.FontLarge;
		ScoresStyle.normal.textColor = Colors.ClickableText;
		ScoresStyle.alignment = TextAnchor.MiddleCenter;

	}

	public override void OnGUI(){

		// Utils.DrawRectangle(ExitRect, 50, Colors.ButtonOutline);
		Utils.FillRectangle(ExitRect, Colors.ButtonBackground);
		GUI.Label(ExitRect, "EXIT", ExitStyle);
		if(Main.Clicked && ExitRect.Contains(Main.TouchGuiLocation)){
			Application.Quit();
		}
		
		Utils.DrawOutline(TitleRect, "LETTERALS", TitleStyle, 2);

		// Utils.DrawRectangle(EasyRect, 50, Colors.ButtonOutline);
		Utils.FillRectangle(EasyRect, Colors.ButtonBackground);
		GUI.Label(EasyRect, "EASY", MenuDifficultyStyle);

		if(Main.Clicked && EasyRect.Contains(Main.TouchGuiLocation)){
			Main.SetGui(new Instructions(WordOptions.Difficulty.Easy));
		}

		// Utils.DrawRectangle(MediumRect, 50, Colors.ButtonOutline);
		Utils.FillRectangle(MediumRect, Colors.ButtonBackground);
		GUI.Label(MediumRect, "MEDIUM", MenuDifficultyStyle);

		if(Main.Clicked && MediumRect.Contains(Main.TouchGuiLocation)){
			Main.SetGui(new Instructions(WordOptions.Difficulty.Medium));
		}

		// Utils.DrawRectangle(HardRect, 50, Colors.ButtonOutline);
		Utils.FillRectangle(HardRect, Colors.ButtonBackground);
		GUI.Label(HardRect, "HARD", MenuDifficultyStyle);

		if(Main.Clicked && HardRect.Contains(Main.TouchGuiLocation)){
			Main.SetGui(new Instructions(WordOptions.Difficulty.Hard));
		}

		// Utils.DrawRectangle(ScoresRect, 50, Colors.ButtonOutline);
		Utils.FillRectangle(ScoresRect, Colors.ButtonBackground);
		GUI.Label(ScoresRect, "HIGH SCORES", ScoresStyle);

		if(Main.Clicked && ScoresRect.Contains(Main.TouchGuiLocation)){
			Main.SetGui(new HighScores());
		}
	}

}