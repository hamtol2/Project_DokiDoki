using UnityEngine;
using System.Collections;

public class ChatDataManager : MonoBehaviour
{
    private static ChatDataManager _instance;
    public static ChatDataManager Instance
    {
        get { return _instance; }
    }

	public enum TextState
	{
		Processing, Finished,
	}

	public TextState textState;
	public ChatScrollView chatScrollView;
	//public BGController heroinReaction;
	public QuestionBox questionBox;
	public AnswerBox answerBox;
	public AudioController bmgController;
	public UISprite characterFace;

    public ChatData chatData;
	private int curr_scene_script_index = 0;
	private int curr_speech_index = 0;

	private string fileName = "SpeechDB_Test";
	//private string fileName = "ChatDB";

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
//			chatData = Resources.Load("Data/ChatDB") as ChatData;
			chatData = Resources.Load("Data/" + fileName) as ChatData;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

	void Start()
	{
		chatScrollView.Update_screen();
		//change heroin face
		//if(!GetSpeech().facelook_filename.Equals(""))
		//	heroinReaction.ChangeFacelook(GetSpeech().facelook_filename);
		//change box style
		SetBoxStyle();
	}

	public ChatData.SceneScript.Speech GetSpeech()
	{
		return chatData.scene_script_list[curr_scene_script_index].speech_list[curr_speech_index];
	}

	private bool isAnswered = false;
	public void OnClick_Question()
	{
		if (textState == TextState.Processing)
		{
			chatScrollView.questionLabel.GetComponent<TypewriterEffect>().Finish();

			return;
		}

		if ((HasToBeAnswered && isAnswered) || !HasToBeAnswered)
		{
			GetNextSpeech();
		}
	}

	public bool HasToBeAnswered
	{
		get
		{
			bool hasToBeAnswered = false;

			if (GetSpeech().speech_type == ChatData.SceneScript.Speech.TYPE.QN) hasToBeAnswered = true;
			if (GetSpeech ().speech_type == ChatData.SceneScript.Speech.TYPE.QR) hasToBeAnswered = true;
			if (GetSpeech().speech_type == ChatData.SceneScript.Speech.TYPE.S) hasToBeAnswered = false;

			return hasToBeAnswered;
		}
	}

	void GetNextSpeech()
	{
		if(curr_speech_index < chatData.scene_script_list[curr_scene_script_index].speech_list.Count - 1)
		{
			// Testing..
			textState = TextState.Processing;

			curr_speech_index++;
			chatScrollView.Update_screen();
			//change heroin face
			if (!string.IsNullOrEmpty(GetSpeech().facelook_filename))
			{
				characterFace.spriteName = GetSpeech().facelook_filename;
			}
			//if(!GetSpeech().facelook_filename.Equals(""))
			//	heroinReaction.ChangeFacelook(GetSpeech().facelook_filename);
			//change box style
			SetBoxStyle();
			//Change sound
			if(GetSpeech().bgm_filename != null && !GetSpeech().bgm_filename.Equals(""))
				bmgController.ChangeSound(GetSpeech().bgm_filename);

		}
	}

	public void OnClick_Answer(int selectedItemIndex)
	{
		bool isSuccessAnswer = true;
		//judge success or fail


		//change heroin face
		string heroin_facelook_filename = "";
		if(isSuccessAnswer)
		{
			heroin_facelook_filename = GetSpeech().answerlist[selectedItemIndex].success_facelook_filename;
		}
		else
		{
			heroin_facelook_filename = GetSpeech().answerlist[selectedItemIndex].fail_facelook_filename;
		}

		//heroinReaction.ChangeFacelook(heroin_facelook_filename);

		//change box style
		ShowStoryOnly();

		isAnswered = true;
	}

	void SetBoxStyle()
	{
		if(GetSpeech().speech_type == ChatData.SceneScript.Speech.TYPE.S)
		{
			ShowStoryOnly();
		}
		else
		{
			ShowChat();
		}
	}
	void ShowStoryOnly()
	{
		questionBox.ChangeState(QuestionBox.STATE.STORY);
		answerBox.ChangeState(AnswerBox.STATE.STORY);
	}

	void ShowChat()
	{
		questionBox.ChangeState(QuestionBox.STATE.QUESTION);
		answerBox.ChangeState(AnswerBox.STATE.QUESTION);
	}


}