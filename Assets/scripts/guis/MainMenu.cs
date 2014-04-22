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

		GUI.Label(ExitRect, "EXIT", ExitStyle);
		Utils.DrawRectangle(ExitRect, 50, Colors.ButtonOutline);
		if(Main.Clicked && ExitRect.Contains(Main.TouchGuiLocation)){
			Application.Quit();
		}
		
		Utils.DrawOutline(TitleRect, "LETTERALS", TitleStyle, 2);

		GUI.Label(EasyRect, "EASY", MenuDifficultyStyle);
		Utils.DrawRectangle(EasyRect, 50, Colors.ButtonOutline);

		if(Main.Clicked && EasyRect.Contains(Main.TouchGuiLocation)){
			Main.SetGui(new GameScreen(WordOptions.Difficulty.Easy));
		}

		GUI.Label(MediumRect, "MEDIUM", MenuDifficultyStyle);
		Utils.DrawRectangle(MediumRect, 50, Colors.ButtonOutline);

		if(Main.Clicked && MediumRect.Contains(Main.TouchGuiLocation)){
			Main.SetGui(new GameScreen(WordOptions.Difficulty.Medium));
		}

		GUI.Label(HardRect, "HARD", MenuDifficultyStyle);
		Utils.DrawRectangle(HardRect, 50, Colors.ButtonOutline);

		if(Main.Clicked && HardRect.Contains(Main.TouchGuiLocation)){
			Main.SetGui(new GameScreen(WordOptions.Difficulty.Hard));
		}

		GUI.Label(ScoresRect, "HIGH SCORES", ScoresStyle);
		Utils.DrawRectangle(ScoresRect, 50, Colors.ButtonOutline);

		if(Main.Clicked && ScoresRect.Contains(Main.TouchGuiLocation)){
			Main.SetGui(new HighScores());
		}
	}

}