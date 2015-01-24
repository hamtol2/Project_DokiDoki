using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChatScrollView : MonoBehaviour
{
    public AnswerListItem prefab;
    public UIGrid grid;
    public UILabel questionLabel;
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
		questionLabel.text = speech.question;
		
		for (int ix = 0; ix < speech.answerlist.Count; ++ix)
		{
			AnswerListItem newItem = Instantiate(prefab) as AnswerListItem;
			newItem._answerLabel.text = speech.answerlist[ix].contents;
			grid.AddChild(newItem.transform);
			newItem.transform.localScale = Vector3.one;
			newItem.GetComponent<UIButton>().onClick.Add(
			new EventDelegate(()=> 
		    {
				int responseIndex = ix;
				Debug.Log("responseIndex: " + responseIndex);
				Debug.Log("ix: " + ix + "count: " + speech.answerlist.Count + " : " + speech.answerlist[ix].success_reaction + " : " + speech.answerlist[ix].fail_reaction);
			}));
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

	public void AnswerItemClicked()
	{

		//Debug.Log("here");
	}
}