using UnityEngine;

public class HighScores : Gui {

	public static readonly Color NavyBlue = new Color(0, 34f/255, 171f/255);

	private Rect BackRect;

	private Rect EasyLabelRect;
	private Rect MediumLabelRect;
	private Rect HighLabelRect;

	private Rect EasyScoreRect;
	private Rect MediumScoreRect;
	private Rect HardScoreRect;

	private GUIStyle BackStyle;
	private GUIStyle DifficultyLabelStyle;
	private GUIStyle DifficultyScoreStyle;
	private GUIStyle DifficultyAverageStyle;

	private Scores scores;

	public HighScores(){

		BackRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - (((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) + (Main.NativeWidth * 0.05f)), (Main.NativeWidth / 3) - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		
		EasyLabelRect = new Rect(0, Main.NativeWidth * 0.05f, Main.NativeWidth, Main.NativeHeight / 3.5f);
		MediumLabelRect = new Rect(EasyLabelRect.x, EasyLabelRect.y + EasyLabelRect.height, EasyLabelRect.width, EasyLabelRect.height);
		HighLabelRect = new Rect(EasyLabelRect.x, MediumLabelRect.y + EasyLabelRect.height, EasyLabelRect.width, EasyLabelRect.height);

		BackStyle = new GUIStyle();
		BackStyle.fontSize = Main.FontLarge;
		BackStyle.normal.textColor = Color.black;
		BackStyle.alignment = TextAnchor.MiddleCenter;

		DifficultyLabelStyle = new GUIStyle();
		DifficultyLabelStyle.fontSize = Main.FontLargest;
		DifficultyLabelStyle.normal.textColor = Color.black;
		DifficultyLabelStyle.alignment = TextAnchor.UpperCenter;

		DifficultyScoreStyle = new GUIStyle();
		DifficultyScoreStyle.fontSize = Main.FontLargest;
		DifficultyScoreStyle.normal.textColor = Color.black;
		DifficultyScoreStyle.alignment = TextAnchor.UpperLeft;

		DifficultyAverageStyle = new GUIStyle();
		DifficultyAverageStyle.fontSize = Main.FontLargest;
		DifficultyAverageStyle.normal.textColor = Color.black;
		DifficultyAverageStyle.alignment = TextAnchor.UpperRight;

		scores = new Scores(WordOptions.Difficulty.Easy);

	}

	public override void OnGUI(){

		GUI.Label(BackRect, "BACK", BackStyle);
		Utils.DrawRectangle(BackRect, 50, Color.black);
		if(Main.Clicked && BackRect.Contains(Main.TouchGuiLocation)){
			Main.SetGui(new MainMenu());
		}

		GUI.Label(EasyLabelRect, "EASY", DifficultyLabelStyle);
		GUI.Label(MediumLabelRect, "MEDIUM", DifficultyLabelStyle);
		GUI.Label(HighLabelRect, "HIGH", DifficultyLabelStyle);

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
		GUI.Label(HighLabelRect, displayString, DifficultyScoreStyle);

		displayString = "";
		foreach(float average in scores.LifetimeAverages(WordOptions.Difficulty.Easy)){
			displayString += "\n" + average.ToString("0.0");
		}
		GUI.Label(EasyLabelRect, displayString, DifficultyAverageStyle);
		displayString = "";
		foreach(float average in scores.LifetimeAverages(WordOptions.Difficulty.Medium)){
			displayString += "\n" + average.ToString("0");
		}
		GUI.Label(MediumLabelRect, displayString, DifficultyAverageStyle);
		displayString = "";
		foreach(float average in scores.LifetimeAverages(WordOptions.Difficulty.Hard)){
			displayString += "\n" + average.ToString("0.0");
		}
		GUI.Label(HighLabelRect, displayString, DifficultyAverageStyle);
		
	}

}