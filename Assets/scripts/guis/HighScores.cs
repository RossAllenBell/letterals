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

		// EasyScoreRect = new Rect(EasyLabelRect.x, EasyLabelRect.y + (Main.NativeHeight / 20f), EasyLabelRect.width, EasyLabelRect.height - (Main.NativeHeight / 10f));
		// MediumScoreRect = new Rect(EasyLabelRect.x, EasyScoreRect.y + EasyLabelRect.height, EasyLabelRect.width, EasyScoreRect.height);
		// HardScoreRect = new Rect(EasyLabelRect.x, MediumScoreRect.y + EasyLabelRect.height, EasyLabelRect.width, EasyScoreRect.height);

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

		GUI.Label(EasyLabelRect, "\n" + scores.LifetimeScore(WordOptions.Difficulty.Easy).ToString("0"), DifficultyScoreStyle);
		GUI.Label(MediumLabelRect, "\n" + scores.LifetimeScore(WordOptions.Difficulty.Medium).ToString("0"), DifficultyScoreStyle);
		GUI.Label(HighLabelRect, "\n" + scores.LifetimeScore(WordOptions.Difficulty.Hard).ToString("0"), DifficultyScoreStyle);

		GUI.Label(EasyLabelRect, "\n" + scores.LifetimeAverage(WordOptions.Difficulty.Easy).ToString("0.0"), DifficultyAverageStyle);
		GUI.Label(MediumLabelRect, "\n" + scores.LifetimeAverage(WordOptions.Difficulty.Medium).ToString("0.0"), DifficultyAverageStyle);
		GUI.Label(HighLabelRect, "\n" + scores.LifetimeAverage(WordOptions.Difficulty.Hard).ToString("0.0"), DifficultyAverageStyle);
		
	}

}