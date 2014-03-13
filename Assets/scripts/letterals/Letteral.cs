using UnityEngine;

public class Letteral {

	public static GUIStyle LetteralStyle;

	private string character;
	private Vector2 startPos;
	private Vector2 endPos;
	private float startTime;
	private float width;

	public Letteral(string character, float width, Vector2 startPos, Vector2 endPos) {
		this.character = character;
		this.width = width;
		this.startPos = startPos;
		this.endPos = endPos;
		this.startTime = Time.time;
	}

	public void Update() {
		float shiftDur = Mathf.Max(0, Mathf.Min((Time.time - startTime) - Main.PreviewSeconds, Main.ShiftSeconds));
		Vector2 currentPos = startPos + ((endPos - startPos) * Mathf.Pow(shiftDur / Main.ShiftSeconds, 0.6f));
		Utils.DrawOutline(new Rect(currentPos.x, currentPos.y, width, 100), character, Main.LetteralStyle, 2, Color.blue);
	}

}