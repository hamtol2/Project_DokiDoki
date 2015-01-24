using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChatData : ScriptableObject
{
	public List<SceneScript> scene_script_list = new List<SceneScript>();

	[System.Serializable]
    public class SceneScript
    {
		public int scene_num;
		public List<Speech> speech_list = new List<Speech>();

		[System.Serializable]
		public class Speech
		{
			public enum TYPE
			{
				S,  //Stroy
				QN, //normal sccuess question
				QR  //Random sccuess question
				
			}
			public enum SPEAKER
			{
				MAN,
				WOMAN
			}

			[System.Serializable]
			public class Answer
			{
				public string contents;
				[SerializeField]
				private string _success_reasction;
				public string success_reaction
				{
					get
					{
						return this._success_reasction;
					}

					set
					{
						this._success_reasction = value;
						if(this._success_reasction == "-")
						{
							asolutely_not = true;
						}
						else
						{
							asolutely_not = false;
						}
						//next_scene_id_if_success = -1;
					}
				}
				public string fail_reaction;
				public int    next_scene_id_if_success;
				public int    next_scene_id_if_fail;
				public string success_facelook_filename;
				public string fail_facelook_filename;
				public bool   asolutely_not;
				
			};
			public TYPE       speech_type;
			public SPEAKER    speaker;
			public int        time;
			public int        fadein_time;
			public int        fadeout_time;
			public string     bgm_filename;
			public string     facelook_filename;
			public string     question;
			public List<Answer> answerlist;
		};

    }
}