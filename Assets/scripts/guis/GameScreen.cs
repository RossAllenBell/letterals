using UnityEngine;
using System.Collections.Generic;

public class GameScreen : Gui {

	public const float PreviewSeconds = 0f;
	public const float ShiftSeconds = 3f;
	public const float FullScore = 300;
	public const float NoGuessPenalty = -50;

	public const float BeginningSeconds = 15;
	public const float PointsToTimeConversion = ShiftSeconds / FullScore;

	public static readonly Color PhaseProgressBarColor = new Color(150f/255, 150f/255, 150f/255, 150f/255);
	public static readonly Color LabelColor = new Color(150/255, 150/255, 150/255, 150f/255);
	public static readonly Color NavyBlue = new Color(0, 34f/255, 171f/255);
	public static readonly Color SessionHealthPercentageColor = new Color(1f, 0f, 0f, 100f/255);

	private GUIStyle BackStyle;
	private GUIStyle OptionStyle;
	private GUIStyle CorrectOptionStyle;
	private GUIStyle WrongOptionStyle;
	private GUIStyle ObsoleteOptionStyle;
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

	public const string Instructions = "Letters will appear and slowly form a word. You will be presented with three options. Click on the option that matches the slowly forming word.\n\nChoosing the correct word will add more time to the round. Choosing an incorrect word will remove time. In either case, the faster you act, the greater the affect on time.";

	private WordOptions.Difficulty difficulty;
	private string currentWord;
	private List<string> currentOptions;
	private float wordStartTime = -ShiftSeconds;
	private List<Letteral> letterals;
	private string guessedOption;

	private float sessionLowerBoundsTime;
	private float sessionStartTime;

	private Scores score;

	public GameScreen(WordOptions.Difficulty difficulty){

		BackStyle = new GUIStyle();
		BackStyle.fontSize = Main.FontLarge;
		BackStyle.normal.textColor = Color.black;
		BackStyle.alignment = TextAnchor.MiddleCenter;

		CorrectOptionStyle = new GUIStyle();
		CorrectOptionStyle.fontSize = Main.FontLargest;
		CorrectOptionStyle.normal.textColor = Color.black;
		CorrectOptionStyle.alignment = TextAnchor.MiddleCenter;

		OptionStyle = new GUIStyle();
		OptionStyle.fontSize = Main.FontLargest;
		OptionStyle.normal.textColor = NavyBlue;
		OptionStyle.alignment = TextAnchor.MiddleCenter;

		WrongOptionStyle = new GUIStyle();
		WrongOptionStyle.fontSize = Main.FontLargest;
		WrongOptionStyle.normal.textColor = Color.red;
		WrongOptionStyle.alignment = TextAnchor.MiddleCenter;

		ObsoleteOptionStyle = new GUIStyle();
		ObsoleteOptionStyle.fontSize = Main.FontLargest;
		ObsoleteOptionStyle.normal.textColor = PhaseProgressBarColor;
		ObsoleteOptionStyle.alignment = TextAnchor.MiddleCenter;

		NextWordStyle = new GUIStyle();
		NextWordStyle.fontSize = Main.FontLarge;
		NextWordStyle.normal.textColor = NavyBlue;
		NextWordStyle.alignment = TextAnchor.MiddleCenter;

		SessionScoreStyle = new GUIStyle();
		SessionScoreStyle.fontSize = Main.FontLarge;
		SessionScoreStyle.normal.textColor = Color.black;
		SessionScoreStyle.alignment = TextAnchor.MiddleRight;

		SessionScoreLabelStyle = new GUIStyle();
		SessionScoreLabelStyle.fontSize = Main.FontLarge;
		SessionScoreLabelStyle.normal.textColor = LabelColor;
		SessionScoreLabelStyle.alignment = TextAnchor.MiddleLeft;

		InstructionsStyle = new GUIStyle();
		InstructionsStyle.fontSize = Main.FontLarge;
		InstructionsStyle.normal.textColor = Color.black;
		InstructionsStyle.alignment = TextAnchor.MiddleCenter;
		InstructionsStyle.wordWrap = true;

		BackRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - (((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) + (Main.NativeWidth * 0.05f)), (Main.NativeWidth / 3) - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		BeginRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeWidth * 0.05f, Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f));
		InstructionsRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeWidth * 0.05f, Main.NativeWidth - (Main.NativeWidth * 0.1f), Main.NativeHeight - (Main.NativeWidth * 0.1f));
		SessionScoreRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - (((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		SessionAverageRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - ((((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) * 2) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		PhaseScoreImpactRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - ((((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) * 3) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		SessionScoreLabelRect = new Rect(Main.NativeWidth * 0.5f, Main.NativeHeight - (((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		SessionAverageLabelRect = new Rect(Main.NativeWidth * 0.5f, Main.NativeHeight - ((((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) * 2) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		PhaseScoreImpactLabelRect = new Rect(Main.NativeWidth * 0.5f, Main.NativeHeight - ((((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) * 3) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));

		this.difficulty = difficulty;

		score = new Scores(this.difficulty);

		resetSession();
	}

	public override void OnGUI(){

		if (sessionStartTime == 0) {
			GUI.Label(BeginRect, "begin...", NextWordStyle);
			Utils.DrawRectangle(BeginRect, 50, Color.black);

			GUI.Label(InstructionsRect, Instructions, InstructionsStyle);

			if(Main.Clicked && BeginRect.Contains(Main.TouchGuiLocation)) {
				resetSession();
				resetWord();

				sessionLowerBoundsTime = Time.time;
				sessionStartTime = Time.time;
			}
		} else {

			float sessionHealthPercentage = (BeginningSeconds - (Time.time - sessionLowerBoundsTime)) / BeginningSeconds;

			Rect sessionHealthPercentageRect = new Rect(0f, Main.NativeHeight * (sessionHealthPercentage), Main.NativeWidth, Main.NativeHeight * (1 - sessionHealthPercentage));
			Utils.FillRectangle(sessionHealthPercentageRect, SessionHealthPercentageColor);

			float phaseProgress = (Time.time - wordStartTime) / ShiftSeconds;
			if(phaseProgress < 1f){
				Rect phaseProgressBarRect = new Rect(0f,0f,Main.NativeWidth * phaseProgress,Main.NativeWidth*0.05f);
				Utils.FillRectangle(phaseProgressBarRect, PhaseProgressBarColor);
			}

			for(int i=0; i<currentOptions.Count; i++){
				Rect rect = new Rect(0 + (Main.NativeWidth * 0.05f), ((Main.NativeHeight / 6f) * (i + 2)) + (Main.NativeWidth * 0.025f), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 6f) - (Main.NativeWidth * 0.05f));

				if(Main.Clicked && rect.Contains(Main.TouchGuiLocation)) {
					guessedOption = currentOptions[i];
					float scoreImpact = Mathf.Round(FullScore * (Mathf.Max(0, ShiftSeconds - (Time.time - wordStartTime)) / ShiftSeconds));
					scoreImpact = guessedOption == currentWord? scoreImpact : -scoreImpact;
					score.ScorePoints(scoreImpact);
					sessionLowerBoundsTime += PointsToTimeConversion * scoreImpact;
					sessionLowerBoundsTime = Mathf.Min(sessionLowerBoundsTime, Time.time + (ShiftSeconds / 2));
					resetWord();
				}

				GUI.Label(rect, currentOptions[i], OptionStyle);
				Utils.DrawRectangle(rect, 50, Color.black);
			}

			foreach(Letteral letteral in letterals){
				letteral.Update();
			}

			if(Time.time - sessionLowerBoundsTime > BeginningSeconds){
				score.CheckForLifetimeRecords();
				sessionStartTime = 0;
			}

		}

		GUI.Label(SessionScoreLabelRect, "total", SessionScoreLabelStyle);
		GUI.Label(SessionAverageLabelRect, "average", SessionScoreLabelStyle);
		GUI.Label(PhaseScoreImpactLabelRect, "last word", SessionScoreLabelStyle);

		GUI.Label(SessionScoreRect, score.SessionScore.ToString("0"), SessionScoreStyle);
		GUI.Label(SessionAverageRect, score.SessionAverage.ToString("0.0"), SessionScoreStyle);
		GUI.Label(PhaseScoreImpactRect, score.LastScoreImpact.ToString("0"), SessionScoreStyle);

		GUI.Label(BackRect, "BACK", BackStyle);
		Utils.DrawRectangle(BackRect, 50, Color.black);
		if(Main.Clicked && BackRect.Contains(Main.TouchGuiLocation)){
			Main.SetGui(new MainMenu());
		}

	}

	private void resetSession(){
		sessionStartTime = 0;
		score.ResetSession();
	}

	private void resetWord(){
		wordStartTime = Time.time;
		currentOptions = WordOptions.GetStrings(this.difficulty);
		currentWord = currentOptions[(int) (currentOptions.Count * Random.value)];
		letterals = LetteralGenerator.GenerateLetterals(currentWord);
	}

}