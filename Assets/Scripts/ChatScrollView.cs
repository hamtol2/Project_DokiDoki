using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChatScrollView : MonoBehaviour
{
    public AnswerListItem prefab;
    public UIGrid grid;
    public UILabel questionLabel;
	public QuestionBox questionBox;
	public AnswerBox answerBox;
	public int scene_index;

    void Start()
    {
		Update_screen();
    }

	void Update()
	{
		//Update_screen();
	}

	public void Update_screen()
	{
		ChatData.SceneScript.Speech speech = ChatDataManager.Instance.GetSpeech();
		//select Box style
		if(speech.speech_type == ChatData.SceneScript.Speech.TYPE.S)
		{
			questionBox.ChangeState(QuestionBox.STATE.STORY);
			answerBox.ChangeState(AnswerBox.STATE.STORY);
		}
		else
		{
			questionBox.ChangeState(QuestionBox.STATE.QUESTION);
			answerBox.ChangeState(AnswerBox.STATE.QUESTION);
		}
		questionLabel.text = speech.question;
		
		for (int ix = 0; ix < speech.answerlist.Count; ++ix)
		{
			AnswerListItem newItem = Instantiate(prefab) as AnswerListItem;
			newItem._answerLabel.text = speech.answerlist[ix].contents;
			grid.AddChild(newItem.transform);
			newItem.transform.localScale = Vector3.one;

			newItem.responseIndex = ix;
			newItem.GetComponent<UIButton>().onClick.Add(new EventDelegate(newItem.OnAnswerClicked));
		}
		
		grid.onReposition = ScrollUpdate;
		grid.Reposition();
	}

	void ScrollUpdate()
	{
		UIScrollView scrollView = grid.transform.parent.GetComponent<UIScrollView>();

		scrollView.UpdatePosition();
		scrollView.UpdateScrollbars();
		scrollView.verticalScrollBar.value = 0f;
	}
}