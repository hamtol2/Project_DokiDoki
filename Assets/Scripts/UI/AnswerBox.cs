using UnityEngine;
using System.Collections;

public class AnswerBox : MonoBehaviour {

	public enum STATE 
	{
		STORY,
		QUESTION
	}
	
	public void ChangeState(STATE state)
	{
		if(state == STATE.STORY)
		{
			gameObject.SetActive(false);
		}
		else
		{
			gameObject.SetActive(true);
		}
	}
}
