using UnityEngine;
using System.Collections;

public class QuestionBox : MonoBehaviour {

	public enum STATE 
	{
		STORY,
		QUESTION
	}

	public Vector3 posWhenStory;
	public Vector3 posWhenQuestion;
	// Use this for initialization
	void Start () {
		//posWhenQuestion = transform.localPosition;
	}

	public void ChangeState(STATE state)
	{
		if(state == STATE.STORY)
		{
			transform.localPosition = posWhenStory;
		}
		else
		{
			transform.localPosition = posWhenQuestion;
		}
	}
}
