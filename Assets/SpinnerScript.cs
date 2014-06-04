using UnityEngine;

public class SpinnerScript : MonoBehaviour {

	const float NormalRotation = 10f;
	const float Variance = 2f;

	private float rotation;

	void Start () {
		rotation = (NormalRotation - Variance) + (Variance * 2f * Random.value);
	}
	
	void Update () {
		transform.Rotate(Vector3.forward, rotation * Time.deltaTime);
	}
}
