using UnityEngine;

public class TitleLetteral {

	public const float ShiftSeconds = 1f;

	private string character;
	private Vector2 startPos;
	private Vector2 endPos;
	private float width;
	private float startTime;
	private Vector2 truePos;

	public TitleLetteral(string character, float width, Vector2 endPos) {
		this.character = character;
		this.width = width;
		this.truePos = endPos;
		this.startPos = endPos;
		this.endPos = endPos;
		this.startTime = Time.time;
	}

	public void Update() {
		float shiftDur = Mathf.Max(0, Mathf.Min(Time.time - startTime, ShiftSeconds));
		Vector2 currentPos = startPos + ((endPos - startPos) * Mathf.Pow(shiftDur / ShiftSeconds, 0.9f));
		Utils.DrawOutline(new Rect(currentPos.x, currentPos.y, width, 100), character, Main.TitleLetteralStyle, 1);

		if(Vector2.Distance(currentPos, endPos) - Utils.BasicallyZero <= 0f){
			startPos = endPos;
			endPos = truePos + (new Vector2(Random.value * 2 - 1, Random.value * 2 - 1).normalized * (width / 2f));
			startTime = Time.time;
		}
	}

}