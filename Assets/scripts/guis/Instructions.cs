using UnityEngine;

public class Instructions : Gui {

	private GUIStyle BackStyle;
	private GUIStyle NextWordStyle;
	private GUIStyle SessionScoreStyle;
	private GUIStyle SessionScoreLabelStyle;
	private GUIStyle InstructionsStyle;

	private Rect BackRect;
	private Rect BeginRect;
	private Rect SessionScoreRect;
	private Rect SessionAverageRect;
	private Rect PhaseScoreImpactRect;
	private Rect SessionScoreLabelRect;
	private Rect SessionAverageLabelRect;
	private Rect PhaseScoreImpactLabelRect;
	private Rect InstructionsRect;

	public const string InstructionString = "Click on the word that matches the slowly forming Letterals to score points and extra time. An incorrect guess will deduct points and time.";

	private WordOptions.Difficulty difficulty;

	private float lastScoreImpact;
	private float sessionScore;
	private float sessionAverage;

	public Instructions(WordOptions.Difficulty difficulty) : this(difficulty, 0f, 0f, 0f) {
	}

	public Instructions(WordOptions.Difficulty difficulty, float lastScoreImpact, float sessionScore, float sessionAverage) {

		BackStyle = new GUIStyle();
		BackStyle.fontSize = Main.FontLarge;
		BackStyle.normal.textColor = Colors.ClickableText;
		BackStyle.alignment = TextAnchor.MiddleCenter;

		NextWordStyle = new GUIStyle();
		NextWordStyle.fontSize = Main.FontLargest;
		NextWordStyle.normal.textColor = Colors.ClickableText;
		NextWordStyle.alignment = TextAnchor.MiddleCenter;

		SessionScoreStyle = new GUIStyle();
		SessionScoreStyle.fontSize = Main.FontLarge;
		SessionScoreStyle.normal.textColor = Colors.ReadableText;
		SessionScoreStyle.alignment = TextAnchor.MiddleRight;

		SessionScoreLabelStyle = new GUIStyle();
		SessionScoreLabelStyle.fontSize = Main.FontLarge;
		SessionScoreLabelStyle.normal.textColor = Colors.ReadableText;
		SessionScoreLabelStyle.alignment = TextAnchor.MiddleLeft;

		InstructionsStyle = new GUIStyle();
		InstructionsStyle.fontSize = Main.FontLarge;
		InstructionsStyle.normal.textColor = Colors.ReadableText;
		InstructionsStyle.alignment = TextAnchor.MiddleCenter;
		InstructionsStyle.wordWrap = true;

		BackRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - (((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) + (Main.NativeWidth * 0.05f)), (Main.NativeWidth / 3) - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		BeginRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeWidth * 0.05f, Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 8f) - (Main.NativeWidth * 0.05f));
		InstructionsRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeWidth * 0.05f, Main.NativeWidth - (Main.NativeWidth * 0.1f), Main.NativeHeight - (Main.NativeWidth * 0.1f));
		SessionScoreRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - (((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		SessionAverageRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - ((((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) * 2) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		PhaseScoreImpactRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - ((((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) * 3) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		SessionScoreLabelRect = new Rect(Main.NativeWidth * 0.5f, Main.NativeHeight - (((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		SessionAverageLabelRect = new Rect(Main.NativeWidth * 0.5f, Main.NativeHeight - ((((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) * 2) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		PhaseScoreImpactLabelRect = new Rect(Main.NativeWidth * 0.5f, Main.NativeHeight - ((((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) * 3) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));

		this.difficulty = difficulty;
		this.lastScoreImpact = lastScoreImpact;
		this.sessionScore = sessionScore;
		this.sessionAverage = sessionAverage;

	}

	public override void OnGUI(){

		// Utils.DrawRectangle(BeginRect, 50, Colors.ButtonOutline);
		Utils.FillRoundedRectangle(BeginRect, Colors.ButtonBackground);
		GUI.Label(BeginRect, "BEGIN", NextWordStyle);

		GUI.Label(InstructionsRect, InstructionString, InstructionsStyle);

		if(Main.Clicked && BeginRect.Contains(Main.TouchGuiLocation)) {
			Main.SetGui(new GameScreen(difficulty));
		}
		

		GUI.Label(SessionScoreLabelRect, "total", SessionScoreLabelStyle);
		GUI.Label(SessionAverageLabelRect, "average", SessionScoreLabelStyle);
		GUI.Label(PhaseScoreImpactLabelRect, "last word", SessionScoreLabelStyle);

		GUI.Label(SessionScoreRect, sessionScore.ToString("0"), SessionScoreStyle);
		GUI.Label(SessionAverageRect, sessionAverage.ToString("0.0"), SessionScoreStyle);
		GUI.Label(PhaseScoreImpactRect, lastScoreImpact.ToString("0"), SessionScoreStyle);

		// Utils.DrawRectangle(BackRect, 50, Colors.ButtonOutline);
		Utils.FillRoundedRectangle(BackRect, Colors.ButtonBackground);
		GUI.Label(BackRect, "BACK", BackStyle);
		if(Main.Clicked && BackRect.Contains(Main.TouchGuiLocation)){
			Main.SetGui(new MainMenu());
		}

	}

}