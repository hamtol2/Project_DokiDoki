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
		ChatData.SceneScript sceneScript = ChatDataManager.Instance.chatData.scene_script_list[scene_index];
		questionLabel.text = sceneScript.speech_list[0].question;

		for (int ix = 0; ix < sceneScript.speech_list[0].answerlist.Count; ++ix)
        {
            AnswerListItem newItem = Instantiate(prefab) as AnswerListItem;
			newItem._answerLabel.text = sceneScript.speech_list[0].answerlist[ix].contents;
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