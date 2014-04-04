using UnityEngine;

public class MainMenu : Gui {

	public static readonly Color NavyBlue = new Color(0, 34f/255, 171f/255);
	public static readonly Color LabelColor = new Color(150/255, 150/255, 150/255, 200f/255);

	private Rect ExitRect;
	private Rect TitleRect;
	private Rect EasyRect;
	private Rect MediumRect;
	private Rect HardRect;
	private Rect EasyScoreRect;
	private Rect MediumScoreRect;
	private Rect HardScoreRect;
	private Rect EasyAverageRect;
	private Rect MediumAverageRect;
	private Rect HardAverageRect;

	private GUIStyle TitleStyle;
	private GUIStyle ExitStyle;
	private GUIStyle MenuDifficultyStyle;
	private GUIStyle StatsStyle;
	private GUIStyle StatsLabelStyle;

	public MainMenu(){

		TitleRect = new Rect(0, 0, Main.NativeWidth, (Main.NativeHeight / 6) * 2);
		ExitRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - (((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) + (Main.NativeWidth * 0.05f)), (Main.NativeWidth / 3) - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		EasyRect = new Rect(0 + (Main.NativeWidth * 0.05f), ((Main.NativeHeight / 6f) * 2) + (Main.NativeWidth * 0.025f), (Main.NativeWidth - (Main.NativeWidth * 0.1f)) / 2, (Main.NativeHeight / 6f) - (Main.NativeWidth * 0.05f));
		MediumRect = new Rect(0 + (Main.NativeWidth * 0.05f), ((Main.NativeHeight / 6f) * 3) + (Main.NativeWidth * 0.025f), (Main.NativeWidth - (Main.NativeWidth * 0.1f)) / 2, (Main.NativeHeight / 6f) - (Main.NativeWidth * 0.05f));
		HardRect = new Rect(0 + (Main.NativeWidth * 0.05f), ((Main.NativeHeight / 6f) * 4) + (Main.NativeWidth * 0.025f), (Main.NativeWidth - (Main.NativeWidth * 0.1f)) / 2, (Main.NativeHeight / 6f) - (Main.NativeWidth * 0.05f));
		
		EasyScoreRect = new Rect(((Main.NativeWidth - (Main.NativeWidth * 0.1f)) / 2) + (Main.NativeWidth * 0.05f), ((Main.NativeHeight / 6f) * 2) + (Main.NativeWidth * 0.025f), (Main.NativeWidth - (Main.NativeWidth * 0.1f)) / 2, ((Main.NativeHeight / 6f) - (Main.NativeWidth * 0.05f)) / 2);
		MediumScoreRect = new Rect(((Main.NativeWidth - (Main.NativeWidth * 0.1f)) / 2) + (Main.NativeWidth * 0.05f), ((Main.NativeHeight / 6f) * 3) + (Main.NativeWidth * 0.025f), (Main.NativeWidth - (Main.NativeWidth * 0.1f)) / 2, ((Main.NativeHeight / 6f) - (Main.NativeWidth * 0.05f)) / 2);
		HardScoreRect = new Rect(((Main.NativeWidth - (Main.NativeWidth * 0.1f)) / 2) + (Main.NativeWidth * 0.05f), ((Main.NativeHeight / 6f) * 4) + (Main.NativeWidth * 0.025f), (Main.NativeWidth - (Main.NativeWidth * 0.1f)) / 2, ((Main.NativeHeight / 6f) - (Main.NativeWidth * 0.05f)) / 2);

		EasyAverageRect = new Rect(((Main.NativeWidth - (Main.NativeWidth * 0.1f)) / 2) + (Main.NativeWidth * 0.05f), (((Main.NativeHeight / 6f) - (Main.NativeWidth * 0.05f)) / 2) + ((Main.NativeHeight / 6f) * 2) + (Main.NativeWidth * 0.025f), (Main.NativeWidth - (Main.NativeWidth * 0.1f)) / 2, ((Main.NativeHeight / 6f) - (Main.NativeWidth * 0.05f)) / 2);
		MediumAverageRect = new Rect(((Main.NativeWidth - (Main.NativeWidth * 0.1f)) / 2) + (Main.NativeWidth * 0.05f), (((Main.NativeHeight / 6f) - (Main.NativeWidth * 0.05f)) / 2) + ((Main.NativeHeight / 6f) * 3) + (Main.NativeWidth * 0.025f), (Main.NativeWidth - (Main.NativeWidth * 0.1f)) / 2, ((Main.NativeHeight / 6f) - (Main.NativeWidth * 0.05f)) / 2);
		HardAverageRect = new Rect(((Main.NativeWidth - (Main.NativeWidth * 0.1f)) / 2) + (Main.NativeWidth * 0.05f), (((Main.NativeHeight / 6f) - (Main.NativeWidth * 0.05f)) / 2) + ((Main.NativeHeight / 6f) * 4) + (Main.NativeWidth * 0.025f), (Main.NativeWidth - (Main.NativeWidth * 0.1f)) / 2, ((Main.NativeHeight / 6f) - (Main.NativeWidth * 0.05f)) / 2);

		TitleStyle = new GUIStyle();
		TitleStyle.fontSize = Main.FontLargest * 2;
		TitleStyle.normal.textColor = Color.black;
		TitleStyle.alignment = TextAnchor.MiddleCenter;

		ExitStyle = new GUIStyle();
		ExitStyle.fontSize = Main.FontLarge;
		ExitStyle.normal.textColor = Color.black;
		ExitStyle.alignment = TextAnchor.MiddleCenter;

		MenuDifficultyStyle = new GUIStyle();
		MenuDifficultyStyle.fontSize = Main.FontLargest;
		MenuDifficultyStyle.normal.textColor = NavyBlue;
		MenuDifficultyStyle.alignment = TextAnchor.MiddleCenter;

		StatsStyle = new GUIStyle();
		StatsStyle.fontSize = Main.FontLarge;
		StatsStyle.normal.textColor = Color.black;
		StatsStyle.alignment = TextAnchor.MiddleRight;

		StatsLabelStyle = new GUIStyle();
		StatsLabelStyle.fontSize = Main.FontMedium;
		StatsLabelStyle.normal.textColor = LabelColor;
		StatsLabelStyle.alignment = TextAnchor.MiddleLeft;

	}

	public override void OnGUI(){

		GUI.Label(ExitRect, "EXIT", ExitStyle);
		Utils.DrawRectangle(ExitRect, 50, Color.black);
		if(Main.Clicked && ExitRect.Contains(Main.TouchGuiLocation)){
			Application.Quit();
		}
		
		GUI.Label(TitleRect, "LETTERALS", TitleStyle);

		GUI.Label(EasyRect, "EASY", MenuDifficultyStyle);
		Utils.DrawRectangle(EasyRect, 50, Color.black);
		if(Main.LifetimeScores.ContainsKey(WordOptions.Difficulty.Easy)){
			GUI.Label(EasyScoreRect, "Best Total", StatsLabelStyle);
			GUI.Label(EasyAverageRect, "Best Average", StatsLabelStyle);

			GUI.Label(EasyScoreRect, Main.LifetimeScores[WordOptions.Difficulty.Easy].ToString(), StatsStyle);
			GUI.Label(EasyAverageRect, Main.LifetimeAverages[WordOptions.Difficulty.Easy].ToString("0.0"), StatsStyle);
		}

		if(Main.Clicked && EasyRect.Contains(Main.TouchGuiLocation)){
			Main.SetGui(new GameScreen(WordOptions.Difficulty.Easy));
		}

		GUI.Label(MediumRect, "MEDIUM", MenuDifficultyStyle);
		Utils.DrawRectangle(MediumRect, 50, Color.black);
		if(Main.LifetimeScores.ContainsKey(WordOptions.Difficulty.Medium)){
			GUI.Label(MediumScoreRect, "Best Total", StatsLabelStyle);
			GUI.Label(MediumAverageRect, "Best Average", StatsLabelStyle);

			GUI.Label(MediumScoreRect, Main.LifetimeScores[WordOptions.Difficulty.Medium].ToString(), StatsStyle);
			GUI.Label(MediumAverageRect, Main.LifetimeAverages[WordOptions.Difficulty.Medium].ToString("0.0"), StatsStyle);
		}

		if(Main.Clicked && MediumRect.Contains(Main.TouchGuiLocation)){
			Main.SetGui(new GameScreen(WordOptions.Difficulty.Medium));
		}

		GUI.Label(HardRect, "HARD", MenuDifficultyStyle);
		Utils.DrawRectangle(HardRect, 50, Color.black);
		if(Main.LifetimeScores.ContainsKey(WordOptions.Difficulty.Hard)){
			GUI.Label(HardScoreRect, "Best Total", StatsLabelStyle);
			GUI.Label(HardAverageRect, "Best Average", StatsLabelStyle);

			GUI.Label(HardScoreRect, Main.LifetimeScores[WordOptions.Difficulty.Hard].ToString(), StatsStyle);
			GUI.Label(HardAverageRect, Main.LifetimeAverages[WordOptions.Difficulty.Hard].ToString("0.0"), StatsStyle);
		}

		if(Main.Clicked && HardRect.Contains(Main.TouchGuiLocation)){
			Main.SetGui(new GameScreen(WordOptions.Difficulty.Hard));
		}
	}

}