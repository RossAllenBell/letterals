using UnityEngine;

public class HighScores : Gui {

	private Rect BackRect;

	private Rect EasyLabelRect;
	private Rect MediumLabelRect;
	private Rect HardLabelRect;

	private Rect EasyScoreRect;
	private Rect MediumScoreRect;
	private Rect HardScoreRect;

	private GUIStyle BackStyle;
	private GUIStyle DifficultyLabelStyle;
	private GUIStyle DifficultyScoreStyle;
	// private GUIStyle DifficultyAverageStyle;

	private Scores scores;

	public HighScores(){

		BackRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - (((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) + (Main.NativeWidth * 0.05f)), (Main.NativeWidth / 3) - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		
		EasyLabelRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeWidth * 0.05f, Main.NativeWidth - (Main.NativeWidth * 0.1f), Main.NativeHeight / 3.5f);
		MediumLabelRect = new Rect(EasyLabelRect.x, EasyLabelRect.y + EasyLabelRect.height, EasyLabelRect.width, EasyLabelRect.height);
		HardLabelRect = new Rect(EasyLabelRect.x, MediumLabelRect.y + EasyLabelRect.height, EasyLabelRect.width, EasyLabelRect.height);

		BackStyle = new GUIStyle();
		BackStyle.fontSize = Main.FontLarge;
		BackStyle.normal.textColor = Colors.ClickableText;
		BackStyle.alignment = TextAnchor.MiddleCenter;

		DifficultyLabelStyle = new GUIStyle();
		DifficultyLabelStyle.fontSize = Main.FontLargest;
		DifficultyLabelStyle.normal.textColor = Colors.ReadableText;
		DifficultyLabelStyle.alignment = TextAnchor.UpperLeft;
		DifficultyLabelStyle.fontStyle = FontStyle.Bold;

		DifficultyScoreStyle = new GUIStyle();
		DifficultyScoreStyle.fontSize = Main.FontLargest;
		DifficultyScoreStyle.normal.textColor = Colors.ReadableText;
		DifficultyScoreStyle.alignment = TextAnchor.UpperCenter;

		// DifficultyAverageStyle = new GUIStyle();
		// DifficultyAverageStyle.fontSize = Main.FontLargest;
		// DifficultyAverageStyle.normal.textColor = Colors.ReadableText;
		// DifficultyAverageStyle.alignment = TextAnchor.UpperRight;

		scores = new Scores(WordOptions.Difficulty.Easy);

	}

	public override void OnGUI(){

		// Utils.DrawRectangle(BackRect, 50, Colors.ButtonOutline);
		Utils.FillRoundedRectangle(BackRect, Colors.ButtonBackground);
		GUI.Label(BackRect, "BACK", BackStyle);
		if(Main.Clicked && BackRect.Contains(Main.TouchGuiLocation)){
			Main.SetGui(new MainMenu());
		}

		GUI.Label(EasyLabelRect, "EASY", DifficultyLabelStyle);
		GUI.Label(MediumLabelRect, "MEDIUM", DifficultyLabelStyle);
		GUI.Label(HardLabelRect, "HARD", DifficultyLabelStyle);

		string displayString = "";
		foreach(int score in scores.LifetimeScores(WordOptions.Difficulty.Easy)){
			displayString += "\n" + score.ToString("0");
		}
		GUI.Label(EasyLabelRect, displayString, DifficultyScoreStyle);
		displayString = "";
		foreach(int score in scores.LifetimeScores(WordOptions.Difficulty.Medium)){
			displayString += "\n" + score.ToString("0");
		}
		GUI.Label(MediumLabelRect, displayString, DifficultyScoreStyle);
		displayString = "";
		foreach(int score in scores.LifetimeScores(WordOptions.Difficulty.Hard)){
			displayString += "\n" + score.ToString("0");
		}
		GUI.Label(HardLabelRect, displayString, DifficultyScoreStyle);

		// displayString = "";
		// foreach(float average in scores.LifetimeAverages(WordOptions.Difficulty.Easy)){
		// 	displayString += "\n" + average.ToString("0.0");
		// }
		// GUI.Label(EasyLabelRect, displayString, DifficultyAverageStyle);
		// displayString = "";
		// foreach(float average in scores.LifetimeAverages(WordOptions.Difficulty.Medium)){
		// 	displayString += "\n" + average.ToString("0");
		// }
		// GUI.Label(MediumLabelRect, displayString, DifficultyAverageStyle);
		// displayString = "";
		// foreach(float average in scores.LifetimeAverages(WordOptions.Difficulty.Hard)){
		// 	displayString += "\n" + average.ToString("0.0");
		// }
		// GUI.Label(HardLabelRect, displayString, DifficultyAverageStyle);
		
	}

}