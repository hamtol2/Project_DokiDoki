using UnityEngine;
using System.Collections;

public class AnswerListItem : MonoBehaviour
{
	public UILabel _answerLabel;
	public int responseIndex = -1;
	public ChatData.SceneScript.Speech speechData;
	public UIButton questionButton;

    void Awake()
    {
        _answerLabel = GetComponent<UILabel>();
    }

	public void OnAnswerClicked()
	{
		//string success = ChatDataManager.Instance.GetSpeech().answerlist[responseIndex].success_reaction;
		//string failed = ChatDataManager.Instance.GetSpeech().answerlist[responseIndex].fail_reaction;
		//Debug.Log(success + " : " + failed);

//		ChatScrollView chatScrollView = ChatDataManager.Instance.chatScrollView.GetComponent<ChatScrollView>();
//
//		if (speechData.speech_type == ChatData.SceneScript.Speech.TYPE.QN)
//		{
//			chatScrollView.UpdateQuestionLabel(ChatData.SceneScript.Speech.SPEAKER.WOMAN, speechData.answerlist[responseIndex].success_reaction);
//
////			chatScrollView.questionLabel.text = speechData.answerlist[responseIndex].success_reaction;
////			chatScrollView.questionLabel.GetComponent<TypewriterEffect>().ResetToBeginning();
//			//chatScrollView.ShowStoryOnly();
//			//chatScrollView.SetBoxStyle();
//		}
//		else if (speechData.speech_type == ChatData.SceneScript.Speech.TYPE.QR)
//		{
//			// success / fail.
//
//			//chatScrollView.ShowStoryOnly();
//		}

		ChatDataManager.Instance.OnClick_Answer(responseIndex);
	}

	public void SetSpeechData(int responseIndex, ChatData.SceneScript.Speech speedData)
	{
		this.responseIndex = responseIndex;
		this.speechData = speedData;
	}

	public void SetRefQuestionButton(UIButton qButton)
	{
		questionButton = qButton;
	}
}