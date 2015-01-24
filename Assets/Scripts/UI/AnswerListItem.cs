using UnityEngine;
using System.Collections;

public class AnswerListItem : MonoBehaviour
{
    public UILabel _answerLabel;
	public int responseIndex = -1;
	public ChatData.SceneScript.Speech speechData;

    void Awake()
    {
        _answerLabel = GetComponent<UILabel>();
    }

	public void OnAnswerClicked()
	{
		//Debug.Log("responseindex : " + responseIndex);
		string success = ChatDataManager.Instance.GetSpeech().answerlist[responseIndex].success_reaction;
		string failed = ChatDataManager.Instance.GetSpeech().answerlist[responseIndex].fail_reaction;
		Debug.Log(success + " : " + failed);
	}

	public void SetSpeechData(int responseIndex, ChatData.SceneScript.Speech speedData)
	{
		this.responseIndex = responseIndex;
		this.speechData = speedData;
	}
}