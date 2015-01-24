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
		update_screen();
    }

	void Update()
	{
		//update_screen();
	}

	private void update_screen()
	{
		ChatData.SceneScript.Speech speech = ChatDataManager.Instance.GetSpeech();
		questionLabel.text = speech.question;
		
		for (int ix = 0; ix < speech.answerlist.Count; ++ix)
		{
			AnswerListItem newItem = Instantiate(prefab) as AnswerListItem;
			newItem._answerLabel.text = speech.answerlist[ix].contents;
			grid.AddChild(newItem.transform);
			newItem.transform.localScale = Vector3.one;
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