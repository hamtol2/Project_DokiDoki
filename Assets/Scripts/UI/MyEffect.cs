using UnityEngine;
using System.Collections;

public class MyEffect : MonoBehaviour {

//	iTween.ShakePosition(Camera.main.gameObject, iTween.Hash("x", 0.2f, "y", 0.2f, "time", 1f));
//	iTween.FadeTo(whiteScreen, iTween.Hash("alpha", 0.3, "time", 0.2f));
//	iTween.FadeTo(whiteScreen, iTween.Hash("alpha", 0, "time", 0.2f, "delay", 0.2f));
//	FinalScore.text = Score.ToString();
//	int bestScore = PlayerPrefs.GetInt("BestScore");
//	if(Score > bestScore)
//	{
//		bestScore = Score;
//		PlayerPrefs.SetInt("BestScore", bestScore);
//		N.SetActive(true);
//	}
//	ScoreText.gameObject.SetActive(false);
//	BestScore.text = bestScore.ToString();
//	iTween.MoveTo(Final, iTween.Hash("y", 0f, "time", 0.5f));
//	CancelInvoke("MakePipe");
	public void ShakePosition()
	{
		iTween.ShakePosition(gameObject, iTween.Hash("x", 0.02f, "y", 0.02f, "time", 1f));
		Handheld.Vibrate();
	}
}
