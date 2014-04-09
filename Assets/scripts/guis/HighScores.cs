using UnityEngine;

public class HighScores : Gui {

	public static readonly Color NavyBlue = new Color(0, 34f/255, 171f/255);

	private Rect BackRect;
	private Rect EasyLabelRect;
	private Rect MediumLabelRect;
	private Rect HighLabelRect;

	private GUIStyle BackStyle;
	private GUIStyle DifficultyLabelStyle;

	public HighScores(){

		BackRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - (((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) + (Main.NativeWidth * 0.05f)), (Main.NativeWidth / 3) - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		EasyLabelRect = new Rect(0, Main.NativeWidth * 0.05f, Main.NativeWidth, Main.NativeHeight / 4f);
		MediumLabelRect = new Rect(EasyLabelRect.x, EasyLabelRect.y + (Main.NativeHeight / 4f), EasyLabelRect.width, EasyLabelRect.height);
		HighLabelRect = new Rect(EasyLabelRect.x, EasyLabelRect.y + (Main.NativeHeight / 2f), EasyLabelRect.width, EasyLabelRect.height);

		BackStyle = new GUIStyle();
		BackStyle.fontSize = Main.FontLarge;
		BackStyle.normal.textColor = Color.black;
		BackStyle.alignment = TextAnchor.MiddleCenter;

		DifficultyLabelStyle = new GUIStyle();
		DifficultyLabelStyle.fontSize = Main.FontLargest;
		DifficultyLabelStyle.normal.textColor = Color.black;
		DifficultyLabelStyle.alignment = TextAnchor.UpperCenter;

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
		
	}

}