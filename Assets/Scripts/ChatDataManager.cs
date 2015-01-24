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
	public BGController bgController;
	public QuestionBox questionBox;
	public AnswerBox answerBox;
	public AudioController bmgController;
	public AudioController seController;
	public UISprite characterFace;
	public ChatData chatData;
	public int curr_scene_script_index = 0;
	private int curr_speech_index = 0;
	private int next_scene_index = -1;
	
	private string fileName = "SpeechDB_Test";
	//private string fileName = "ChatDB";
	
	void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			//            chatData = Resources.Load("Data/ChatDB") as ChatData;
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
		next_scene_index = -1;
		//chatScrollView.Update_screen();
		//change bg
		if(GetSpeech().bg != null && !string.IsNullOrEmpty(GetSpeech().bg))
			bgController.ChangeBG(GetSpeech().bg);
		//Change sound
		if(GetSpeech().bgm_filename != null && !string.IsNullOrEmpty(GetSpeech().bgm_filename))
			bmgController.ChangeSound(GetSpeech().bgm_filename);
		if(GetSpeech().sound_effect_filename != null && !string.IsNullOrEmpty(GetSpeech().sound_effect_filename))
			seController.ChangeSound(GetSpeech().sound_effect_filename);
		
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
			if(next_scene_index == -1)
			{
				GetNextSpeech();
			}
			else
			{
				Application.LoadLevel(next_scene_index);
			}
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
			chatScrollView.UpdateScreen();
			//change heroin face
			if (!string.IsNullOrEmpty(GetSpeech().facelook_filename))
			{
				characterFace.spriteName = GetSpeech().facelook_filename;
			}
			//change bg
			if(GetSpeech().bg != null && !string.IsNullOrEmpty(GetSpeech().bg))
				bgController.ChangeBG(GetSpeech().bg);
			//Change sound
			if(GetSpeech().bgm_filename != null && !string.IsNullOrEmpty(GetSpeech().bgm_filename))
				bmgController.ChangeSound(GetSpeech().bgm_filename);
			if(GetSpeech().sound_effect_filename != null && !string.IsNullOrEmpty(GetSpeech().sound_effect_filename))
				seController.ChangeSound(GetSpeech().sound_effect_filename);
			//change box style
			SetBoxStyle();
			
		}
	}
	
	public void OnClick_Answer(int selectedItemIndex)
	{
		bool isSuccessAnswer = true;
		//judge success or fail
		
		if (GetSpeech().speech_type == ChatData.SceneScript.Speech.TYPE.QN)
		{
			chatScrollView.UpdateQuestionLabel(ChatData.SceneScript.Speech.SPEAKER.WOMAN, GetSpeech().answerlist[selectedItemIndex].success_reaction);
		}
		else if (GetSpeech().speech_type == ChatData.SceneScript.Speech.TYPE.QR)
		{
			// success / fail.
//			isSuccessAnswer = Random.Range(0,2);
			isSuccessAnswer = Random.Range(0, 10) == 0 ? true : false;
			if(isSuccessAnswer)
			{
				chatScrollView.UpdateQuestionLabel(ChatData.SceneScript.Speech.SPEAKER.WOMAN, GetSpeech().answerlist[selectedItemIndex].success_reaction);
				next_scene_index = GetSpeech().answerlist[selectedItemIndex].next_scene_id_if_success;
			}
			else
			{
				chatScrollView.UpdateQuestionLabel(ChatData.SceneScript.Speech.SPEAKER.WOMAN, GetSpeech().answerlist[selectedItemIndex].fail_reaction);
				next_scene_index = GetSpeech().answerlist[selectedItemIndex].next_scene_id_if_fail;
			}
		}
		//change heroin face
		if (!string.IsNullOrEmpty(GetSpeech().facelook_filename))
		{
			if(isSuccessAnswer)
			{
				characterFace.spriteName = GetSpeech().answerlist[selectedItemIndex].success_facelook_filename;
			}
			else
			{
				characterFace.spriteName = GetSpeech().answerlist[selectedItemIndex].fail_facelook_filename;
			}
		}
		
		//Change sound
		string bgmFileName;
		if(isSuccessAnswer)
		{
			bgmFileName = GetSpeech().answerlist[selectedItemIndex].success_bgm_filename;
		}
		else
		{
			bgmFileName = GetSpeech().answerlist[selectedItemIndex].fail_bgm_filename;
		}
		if(bgmFileName != null && !string.IsNullOrEmpty(bgmFileName))
			bmgController.ChangeSound(bgmFileName);
		
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