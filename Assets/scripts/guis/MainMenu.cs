using UnityEngine;

public class MainMenu : Gui {

	public static readonly Color NavyBlue = new Color(0, 34f/255, 171f/255);

	private Rect ExitRect;
	private Rect TitleRect;
	private Rect EasyRect;
	private Rect MediumRect;
	private Rect HardRect;

	private GUIStyle TitleStyle;
	private GUIStyle ExitStyle;
	private GUIStyle MenuDifficultyStyle;

	public MainMenu(){

		TitleRect = new Rect(0, 0, Main.NativeWidth, (Main.NativeHeight / 6) * 2);
		ExitRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - (((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) + (Main.NativeWidth * 0.05f)), (Main.NativeWidth / 3) - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		EasyRect = new Rect(0 + (Main.NativeWidth * 0.05f), ((Main.NativeHeight / 6f) * 2) + (Main.NativeWidth * 0.025f), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 6f) - (Main.NativeWidth * 0.05f));
		MediumRect = new Rect(0 + (Main.NativeWidth * 0.05f), ((Main.NativeHeight / 6f) * 3) + (Main.NativeWidth * 0.025f), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 6f) - (Main.NativeWidth * 0.05f));
		HardRect = new Rect(0 + (Main.NativeWidth * 0.05f), ((Main.NativeHeight / 6f) * 4) + (Main.NativeWidth * 0.025f), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 6f) - (Main.NativeWidth * 0.05f));

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
		if(Main.Clicked && EasyRect.Contains(Main.TouchGuiLocation)){
			Main.SetGui(new GameScreen(WordOptions.Difficulty.Easy));
		}

		GUI.Label(MediumRect, "MEDIUM", MenuDifficultyStyle);
		Utils.DrawRectangle(MediumRect, 50, Color.black);
		if(Main.Clicked && MediumRect.Contains(Main.TouchGuiLocation)){
			Main.SetGui(new GameScreen(WordOptions.Difficulty.Medium));
		}

		GUI.Label(HardRect, "HARD", MenuDifficultyStyle);
		Utils.DrawRectangle(HardRect, 50, Color.black);
		if(Main.Clicked && HardRect.Contains(Main.TouchGuiLocation)){
			Main.SetGui(new GameScreen(WordOptions.Difficulty.Hard));
		}
	}

}