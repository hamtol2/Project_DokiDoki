using UnityEngine;
using System.Collections;

public class TouchScreen : MonoBehaviour {

	void Start()
	{
		iTween.FadeTo(gameObject, iTween.Hash("alpha", 1f, "time", 0.5f));
	}
	// Update is called once per frame
	public void OnClickMe () {
		Application.LoadLevel(3);
	}
}
