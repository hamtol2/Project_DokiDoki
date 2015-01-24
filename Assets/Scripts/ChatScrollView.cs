using UnityEngine;
using System.Collections;

public class ChatScrollView : MonoBehaviour
{
    public AnswerListItem prefab;
    public UIGrid grid;
    public UILabel questionLabel;

    void Start()
    {
        questionLabel.text = ChatDataManager.Instance.chatData.list[0].question;

        for (int ix = 0; ix < ChatDataManager.Instance.chatData.list[0].answers.Count; ++ix)
        {
            AnswerListItem newItem = Instantiate(prefab) as AnswerListItem;
            newItem._answerLabel.text = ChatDataManager.Instance.chatData.list[0].answers[ix];
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