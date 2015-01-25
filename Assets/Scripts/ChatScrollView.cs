﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChatScrollView : MonoBehaviour
{
    public AnswerListItem prefab;
    public UIGrid grid;
    public UILabel questionLabel;
	public UILabel speakerLabel;
	//public QuestionBox questionBox;
	//public AnswerBox answerBox;
	public int scene_index;
	public string playerName;
	public string girlFriendName;

    void Start()
    {
		//Update_screen();
    }

	void Update()
	{
//		Update_screen();
	}

	public void UpdateScreen()
	{
		ChatData.SceneScript.Speech speech = ChatDataManager.Instance.GetSpeech();

		//SetBoxStyle();
//		questionLabel.text = speech.question;
//		questionLabel.GetComponent<TypewriterEffect>().ResetToBeginning();
		UpdateQuestionLabel(speech.speaker, speech.question);

		// Clear grid first.
		foreach (Transform item in grid.GetChildList())
		{
			Destroy(item.gameObject);
		}
		
		for (int ix = 0; ix < speech.answerlist.Count; ++ix)
		{
			AnswerListItem newItem = Instantiate(prefab) as AnswerListItem;
			newItem._answerLabel.text = speech.answerlist[ix].contents;
			grid.AddChild(newItem.transform);
			newItem.transform.localScale = Vector3.one;

			newItem.SetSpeechData(ix, speech);
			newItem.GetComponent<UIButton>().onClick.Add(new EventDelegate(newItem.OnAnswerClicked));
		}
		
		grid.onReposition = ScrollUpdate;
		grid.Reposition();
	}

	bool isFishedAdded = false;
	public void UpdateQuestionLabel(ChatData.SceneScript.Speech.SPEAKER speaker, string chat)
	{
		ChatDataManager.Instance.textState = ChatDataManager.TextState.Processing;
		if(speaker == ChatData.SceneScript.Speech.SPEAKER.MAN)
		{
			speakerLabel.text = playerName;
		}
		else
		{
			speakerLabel.text = girlFriendName;
		}
		questionLabel.text = chat;
		if(questionLabel.GetComponent<TypewriterEffect>())
		{
			questionLabel.GetComponent<TypewriterEffect>().ResetToBeginning();
			if (!isFishedAdded)
			{
				questionLabel.GetComponent<TypewriterEffect>().onFinished.Add(new EventDelegate(()=> { ChatDataManager.Instance.textState = ChatDataManager.TextState.Finished; } ));
				isFishedAdded = true;
			}
		}
	}

//	public void SetBoxStyle()
//	{
//		//select Box style
//		if(ChatDataManager.Instance.GetSpeech().speech_type == ChatData.SceneScript.Speech.TYPE.S)
//		{
//			ShowStoryOnly();
//		}
//		else
//		{
//			ShowChat();
//		}
//	}

//	public void ShowStoryOnly()
//	{
//		questionBox.ChangeState(QuestionBox.STATE.STORY);
//		answerBox.ChangeState(AnswerBox.STATE.STORY);
//	}
//
//	public void ShowChat()
//	{
//		questionBox.ChangeState(QuestionBox.STATE.QUESTION);
//		answerBox.ChangeState(AnswerBox.STATE.QUESTION);
//	}

	void ScrollUpdate()
	{
		UIScrollView scrollView = grid.transform.parent.GetComponent<UIScrollView>();

		scrollView.UpdatePosition();
		scrollView.UpdateScrollbars();
		scrollView.verticalScrollBar.value = 0f;
	}
}