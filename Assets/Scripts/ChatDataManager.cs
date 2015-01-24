using UnityEngine;
using System.Collections;

public class ChatDataManager : MonoBehaviour
{
    private static ChatDataManager _instance;
    public static ChatDataManager Instance
    {
        get { return _instance; }
    }

    private ChatData chatData;
	private int curr_scene_script_index = 0;
	private int curr_speech_index = 0;
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            chatData = Resources.Load("Data/ChatDB") as ChatData;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

	public ChatData.SceneScript.Speech GetSpeech()
	{
		return chatData.scene_script_list[curr_scene_script_index].speech_list[curr_speech_index];
	}

	public void OnClick_Question()
	{
		if(curr_speech_index < chatData.scene_script_list[curr_scene_script_index].speech_list.Count)
			curr_speech_index++;
	}

	public void OnClick_Answer()
	{
		curr_speech_index++;
	}
}