using UnityEngine;
using System.Collections.Generic;

public class GameScreen : Gui {

	public const float WordFadeOut = 0.3f;
	public const float WordFadeIn = 0.6f;

	public const float PreviewSeconds = 0f;
	public const float ShiftSeconds = 3f + (WordFadeOut + WordFadeIn);
	public const float FullScore = 300;
	public const float NoGuessPenalty = -50;

	public const float BeginningSeconds = 15;
	public const float PointsToTimeConversion = ShiftSeconds / FullScore;

	public static readonly Color SessionHealthPercentageColor = new Color(1f, 0f, 0f, 75f/255);
	public static readonly Color SessionHealthPercentageLineColor = new Color(1f, 0f, 0f, 1f);

	private GUIStyle BackStyle;
	private GUIStyle OptionStyle;
	private GUIStyle CorrectOptionStyle;
	private GUIStyle WrongOptionStyle;
	private GUIStyle ObsoleteOptionStyle;
	private GUIStyle SessionScoreStyle;
	private GUIStyle SessionScoreLabelStyle;
	private GUIStyle ScoreCorrectSpriteStyle;
	private GUIStyle ScoreIncorrectSpriteStyle;

	private Rect BackRect;
	private Rect SessionScoreRect;
	private Rect SessionAverageRect;
	// private Rect WordScoreImpactRect;
	private Rect SessionScoreLabelRect;
	private Rect SessionAverageLabelRect;
	// private Rect WordScoreImpactLabelRect;

	private Vector2 scoreSpriteStartLocation;
	private Vector2 ScoreSpriteEndLocation;

	private WordOptions.Difficulty difficulty;
	private string currentWord;
	private List<string> currentOptions;
	private float wordStartTime;
	private float wordEndTime;
	private List<Letteral> letterals;
	private string guessedOption;

	private float sessionLowerBoundsTime;

	private Scores score;

	private bool active;

	public GameScreen(WordOptions.Difficulty difficulty){

		BackStyle = new GUIStyle();
		BackStyle.fontSize = Main.FontLarge;
		BackStyle.normal.textColor = Colors.ClickableText;
		BackStyle.alignment = TextAnchor.MiddleCenter;

		OptionStyle = new GUIStyle();
		OptionStyle.fontSize = Main.FontLargest;
		OptionStyle.normal.textColor = Colors.ClickableText;
		OptionStyle.alignment = TextAnchor.MiddleCenter;

		SessionScoreStyle = new GUIStyle();
		SessionScoreStyle.fontSize = Main.FontLarge;
		SessionScoreStyle.normal.textColor = Colors.ReadableText;
		SessionScoreStyle.alignment = TextAnchor.MiddleRight;

		SessionScoreLabelStyle = new GUIStyle();
		SessionScoreLabelStyle.fontSize = Main.FontLarge;
		SessionScoreLabelStyle.normal.textColor = Colors.ReadableText;
		SessionScoreLabelStyle.alignment = TextAnchor.MiddleLeft;

		ScoreCorrectSpriteStyle = new GUIStyle();
		ScoreCorrectSpriteStyle.fontSize = Main.FontLargest;
		ScoreCorrectSpriteStyle.normal.textColor = Colors.BrightGreen;
		ScoreCorrectSpriteStyle.alignment = TextAnchor.MiddleCenter;

		ScoreIncorrectSpriteStyle = new GUIStyle();
		ScoreIncorrectSpriteStyle.fontSize = Main.FontLargest;
		ScoreIncorrectSpriteStyle.normal.textColor = Colors.BrightRed;
		ScoreIncorrectSpriteStyle.alignment = TextAnchor.MiddleCenter;

		BackRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - (((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) + (Main.NativeWidth * 0.05f)), (Main.NativeWidth / 3) - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		SessionScoreRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - (((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		SessionAverageRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - ((((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) * 2) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		// WordScoreImpactRect = new Rect(Main.NativeWidth * 0.05f, Main.NativeHeight - ((((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) * 3) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		SessionScoreLabelRect = new Rect(Main.NativeWidth * 0.5f, Main.NativeHeight - (((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		SessionAverageLabelRect = new Rect(Main.NativeWidth * 0.5f, Main.NativeHeight - ((((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) * 2) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));
		// WordScoreImpactLabelRect = new Rect(Main.NativeWidth * 0.5f, Main.NativeHeight - ((((Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f)) * 3) + (Main.NativeWidth * 0.05f)), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 12f) - (Main.NativeWidth * 0.05f));

		ScoreSpriteEndLocation = new Vector2(SessionScoreRect.x + (SessionScoreRect.width * 0.9f), SessionScoreRect.y + (SessionScoreRect.height / 2f));

		this.difficulty = difficulty;

		score = new Scores(this.difficulty);

		active = true;

		resetSession();
		resetWord();
		sessionLowerBoundsTime = Time.time;
	}

	public override void OnGUI(){

		if(active) {
			float sessionHealthPercentage = (BeginningSeconds - (Time.time - sessionLowerBoundsTime)) / BeginningSeconds;

			Rect sessionHealthPercentageRect = new Rect(0f, Main.NativeHeight * (sessionHealthPercentage), Main.NativeWidth, Main.NativeHeight * (1 - sessionHealthPercentage));
			Rect sessionHealthPercentageLineRect = new Rect(0f, Main.NativeHeight * (sessionHealthPercentage), Main.NativeWidth, 10f * Main.GuiRatio);
			Utils.FillRectangle(sessionHealthPercentageRect, SessionHealthPercentageColor);
			Utils.FillRectangle(sessionHealthPercentageLineRect, SessionHealthPercentageLineColor);

			Color savedGuiColor = GUI.color;
			bool changedGuiColor = false;
			if(GUI.color.a == 1f){
				if(Time.time - wordEndTime < WordFadeOut){
					showScoreSprite();
					GUI.color = new Color(1f, 1f, 1f, 1f - ((Time.time - wordEndTime) / WordFadeOut));
					changedGuiColor = true;
				} else if (wordEndTime != 0) {
					resetWord();
				}

				if (Time.time - wordStartTime < WordFadeIn) {
					showScoreSprite();
					GUI.color = new Color(1f, 1f, 1f, (Time.time - wordStartTime) / WordFadeIn);
					changedGuiColor = true;
				}
			}

			for(int i=0; i<currentOptions.Count; i++){
				Rect rect = new Rect(0 + (Main.NativeWidth * 0.05f), ((Main.NativeHeight / 6f) * (i + 2)) + (Main.NativeWidth * 0.025f), Main.NativeWidth - (Main.NativeWidth * 0.1f), (Main.NativeHeight / 6f) - (Main.NativeWidth * 0.05f));

				if(Main.Clicked && rect.Contains(Main.TouchGuiLocation)) {
					wordEndTime = Time.time;
					guessedOption = currentOptions[i];
					float scoreImpact = Mathf.Round(FullScore * (Mathf.Max(0, ShiftSeconds - (Time.time - wordStartTime)) / ShiftSeconds));
					scoreImpact = guessedOption == currentWord? scoreImpact : -scoreImpact;
					score.ScorePoints(scoreImpact);
					sessionLowerBoundsTime += PointsToTimeConversion * scoreImpact;
					sessionLowerBoundsTime = Mathf.Min(sessionLowerBoundsTime, Time.time + (ShiftSeconds / 2));

					scoreSpriteStartLocation = Main.TouchGuiLocation;
				}

				// Utils.DrawRectangle(rect, 50, Colors.ButtonOutline);
				Utils.FillRoundedRectangle(rect, Colors.ButtonBackground);
				GUI.Label(rect, currentOptions[i], OptionStyle);
			}

			foreach(Letteral letteral in letterals){
				letteral.Update();
			}

			if (changedGuiColor) {
				GUI.color = savedGuiColor;
			}
		}

		if(Time.time - sessionLowerBoundsTime > BeginningSeconds && active){
			active = false;
			score.CheckForLifetimeRecords();
			Main.SetGui(new Instructions(difficulty, score.LastScoreImpact, score.SessionScore, score.SessionAverage));
		}

		GUI.Label(SessionScoreLabelRect, "total", SessionScoreLabelStyle);
		GUI.Label(SessionAverageLabelRect, "average", SessionScoreLabelStyle);
		// GUI.Label(WordScoreImpactLabelRect, "last word", SessionScoreLabelStyle);

		GUI.Label(SessionScoreRect, score.SessionScore.ToString("0"), SessionScoreStyle);
		GUI.Label(SessionAverageRect, score.SessionAverage.ToString("0.0"), SessionScoreStyle);
		// GUI.Label(WordScoreImpactRect, score.LastScoreImpact.ToString("0"), SessionScoreStyle);

		// Utils.DrawRectangle(BackRect, 50, Colors.ButtonOutline);
		Utils.FillRoundedRectangle(BackRect, Colors.ButtonBackground);
		GUI.Label(BackRect, "BACK", BackStyle);
		if(Main.Clicked && BackRect.Contains(Main.TouchGuiLocation)){
			score.CheckForLifetimeRecords();
			Main.SetGui(new Instructions(difficulty, score.LastScoreImpact, score.SessionScore, score.SessionAverage));
		}

	}

	private void showScoreSprite(){
		if (guessedOption != null){
			float duration = (Time.time - wordEndTime < WordFadeOut) ? Time.time - wordEndTime : WordFadeOut + (Time.time - wordStartTime);

			Vector2 currentPos = scoreSpriteStartLocation + ((ScoreSpriteEndLocation - scoreSpriteStartLocation) * Mathf.Pow(duration / (WordFadeOut + WordFadeIn), 0.6f));
			// Utils.DrawOutline(new Rect(currentPos.x - 500f, currentPos.y - 500f, 1000f, 1000f), score.LastScoreImpact.ToString("0"), ScoreCorrectSpriteStyle, 1, score.LastScoreImpact > 0? Colors.BrightGreen : Colors.BrightRed);
			GUI.Label(new Rect(currentPos.x - 500f, currentPos.y - 500f, 1000f, 1000f), score.LastScoreImpact.ToString("0"), score.LastScoreImpact > 0? ScoreCorrectSpriteStyle : ScoreIncorrectSpriteStyle);
		}
	}

	private void resetSession(){
		score.ResetSession();
	}

	private void resetWord(){
		wordEndTime = 0f;
		wordStartTime = Time.time;
		currentOptions = WordOptions.GetStrings(this.difficulty);
		currentWord = currentOptions[(int) (currentOptions.Count * Random.value)];
		letterals = LetteralGenerator.GenerateLetterals(currentWord);
	}

}