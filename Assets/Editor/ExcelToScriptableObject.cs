using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

public class ExcelToScriptableObject : MonoBehaviour
{
 //   static readonly string filePath = "Assets/Editor/Data/ExcelData.xlsx";
	static readonly string filePath = "Assets/Editor/Data/SpeechData.xlsx";
//	static readonly string chatDataPath = "Assets/Resources/Data/ChatDB.asset";
	static readonly string chatDataPath = "Assets/Resources/Data/SpeechDB_Test.asset";

    [MenuItem("Excel Data Import/Create Scriptable Object %i")]
    static void ImportExcel()
    {
        Debug.Log("Start Excel import");
        MakeChatData();
        Debug.Log("Fisish Excel import");
    }

    static void MakeChatData()
    {
        ChatData data = ScriptableObject.CreateInstance<ChatData>();
        AssetDatabase.CreateAsset(data, chatDataPath);
        data.hideFlags = HideFlags.NotEditable;
		data.scene_script_list.Clear();

        using (FileStream stream = File.Open(filePath, FileMode.Open))
        {
            IWorkbook book = new XSSFWorkbook(stream);
			GetSceneScriptData(ref data, book);
            stream.Close();
        }

        ScriptableObject obj = AssetDatabase.LoadAssetAtPath(chatDataPath, typeof(ScriptableObject)) as ScriptableObject;
        EditorUtility.SetDirty(obj);
    }


	static void GetSceneScriptData(ref ChatData inputChatData, IWorkbook inputBook)
	{
		for(int scene_index = 1; scene_index < inputBook.NumberOfSheets; scene_index++)
		{
			//Get Scene Information
			ISheet sheet = inputBook.GetSheetAt(scene_index);
			ChatData.SceneScript screneScript = new ChatData.SceneScript();
			//Get speech Data from scene script
			screneScript.scene_num = scene_index;
			GetSpeechData(ref screneScript, sheet);
			inputChatData.scene_script_list.Add(screneScript);
		}
	}
	static void GetSpeechData(ref ChatData.SceneScript inputScreneScript, ISheet inputSheet)
	{
		Debug.Log("Sheet name: " + inputSheet.SheetName);

		for (int ix = 1; ix <= inputSheet.LastRowNum; )
		{
			IRow row = inputSheet.GetRow(ix);
			const int col_time = 0;
			const int col_type = 1;
			const int col_speaker = 2;
			const int col_question = 3;
			const int col_bg = 4;
			const int col_bgm = 5;
			const int col_sound_effect = 6;
			const int col_facelook = 7;
			//const int col_reserved = 8;
			const int col_answer_num = 9;
			const int col_answer_first = 10;
			
			//this is row define relate to answer info
			const int row_answer_content = 0;
			const int row_answer_success_react = 1;
			const int row_answer_fail_react = 2;
			const int row_answer_success_next_scene = 3;
			const int row_answer_fail_next_scene = 4;

			//if(!row.GetCell(col_type).Equals(""))
			//if (row.GetCell(col_type).CellType != CellType.Blank)
			if (row == null)
			{
				//Debug.Log("row null");
				ix++;
				continue;
			}
				
			if (row.GetCell(col_type) != null && row.GetCell(col_type).CellType != CellType.Blank)
			{
				string speechtype = row.GetCell(col_type).StringCellValue;
				if(speechtype.Equals("S"))
				{
					ChatData.SceneScript.Speech speech = new ChatData.SceneScript.Speech();
					//set type
					speech.speech_type = ChatData.SceneScript.Speech.TYPE.S;
					//set Question
					speech.question = row.GetCell(col_question).StringCellValue;
					//set BGM
					if(row.GetCell(col_bgm) != null && row.GetCell(col_bgm).CellType != CellType.Blank)
						speech.bgm_filename = row.GetCell(col_bgm).StringCellValue;
					//set sound effect
					if(row.GetCell(col_sound_effect) != null && row.GetCell(col_sound_effect).CellType != CellType.Blank)
						speech.sound_effect_filename = row.GetCell(col_sound_effect).StringCellValue;
					//set face
					if (row.GetCell(col_facelook) != null)
						speech.facelook_filename = row.GetCell(col_facelook).StringCellValue;
					//set BG
					if(row.GetCell(col_bg) != null && row.GetCell(col_bg).CellType != CellType.Blank)
						speech.bg = row.GetCell(col_bg).StringCellValue;
					//set speaker
					string speaker_gender = string.Empty;
					if (row.GetCell(col_speaker) != null && row.GetCell(col_speaker).CellType != CellType.Blank)
						speaker_gender = row.GetCell(col_speaker).StringCellValue;
					if(speaker_gender.Equals("M"))
					{
						speech.speaker = ChatData.SceneScript.Speech.SPEAKER.MAN;
					}
					else
					{
						speech.speaker = ChatData.SceneScript.Speech.SPEAKER.WOMAN;
					}
					inputScreneScript.speech_list.Add(speech);
					ix++;
				}
				else if(speechtype.Equals("QN"))
				{
					ChatData.SceneScript.Speech speech = new ChatData.SceneScript.Speech();
					//set type
					speech.speech_type = ChatData.SceneScript.Speech.TYPE.QN;
					//Set speaker
					string speaker_gender = string.Empty;
					if (row.GetCell(col_speaker) != null && row.GetCell(col_speaker).CellType != CellType.Blank)
						speaker_gender = row.GetCell(col_speaker).StringCellValue;
					if(speaker_gender.Equals("M"))
					{
						speech.speaker = ChatData.SceneScript.Speech.SPEAKER.MAN;
					}
					else
					{
						speech.speaker = ChatData.SceneScript.Speech.SPEAKER.WOMAN;
					}
					//set question
					speech.question = row.GetCell(col_question).StringCellValue;
					//set BGM
					if(row.GetCell(col_bgm) != null && row.GetCell(col_bgm).CellType != CellType.Blank)
						speech.bgm_filename = row.GetCell(col_bgm).StringCellValue;
					//set sound effect
					if(row.GetCell(col_sound_effect) != null && row.GetCell(col_sound_effect).CellType != CellType.Blank)
						speech.sound_effect_filename = row.GetCell(col_sound_effect).StringCellValue;
					//set face
					speech.facelook_filename = row.GetCell(col_facelook).StringCellValue;
					//set BG
					if(row.GetCell(col_bg) != null && row.GetCell(col_bg).CellType != CellType.Blank)
					   speech.bg = row.GetCell(col_bg).StringCellValue;
					//set answer
					int answer_num = (int)row.GetCell(col_answer_num).NumericCellValue;
					speech.answerlist = new List<ChatData.SceneScript.Speech.Answer>();
					for(int i = 0; i < answer_num; i++)
					{
						ChatData.SceneScript.Speech.Answer answer = new ChatData.SceneScript.Speech.Answer();
						answer.contents = row.GetCell(col_answer_first + i).StringCellValue;
						answer.success_reaction = inputSheet.GetRow(ix + row_answer_success_react).GetCell(col_answer_first + i).StringCellValue; //same col and next row line has success reaction string 
						answer.success_facelook_filename = inputSheet.GetRow(ix + row_answer_success_react).GetCell(col_facelook).StringCellValue; //same col and next row line has success reaction string 
						speech.answerlist.Add(answer); 
					}
					inputScreneScript.speech_list.Add(speech);
					ix += 2;
				}
				else if(speechtype.Equals("QR"))
				{
					ChatData.SceneScript.Speech speech = new ChatData.SceneScript.Speech();
					//set type
					speech.speech_type = ChatData.SceneScript.Speech.TYPE.QR;
					//Set speaker
					string speaker_gender = row.GetCell(col_speaker).StringCellValue;
					if(speaker_gender.Equals("M"))
					{
						speech.speaker = ChatData.SceneScript.Speech.SPEAKER.MAN;
					}
					else
					{
						speech.speaker = ChatData.SceneScript.Speech.SPEAKER.WOMAN;
					}
					//set question
					speech.question = row.GetCell(col_question).StringCellValue;
					//set BGM
					if(row.GetCell(col_bgm) != null && !row.GetCell(col_bgm).Equals(""))
						speech.bgm_filename = row.GetCell(col_bgm).StringCellValue;
					//set sound effect
					if(row.GetCell(col_sound_effect) != null && row.GetCell(col_sound_effect).CellType != CellType.Blank)
						speech.sound_effect_filename = row.GetCell(col_sound_effect).StringCellValue;
					//set BG
					if(row.GetCell(col_bg) != null && row.GetCell(col_bg).CellType != CellType.Blank)
						speech.bg = row.GetCell(col_bg).StringCellValue;
					//set face
					if (row.GetCell(col_facelook) != null)
						speech.facelook_filename = row.GetCell(col_facelook).StringCellValue;
					//set answer
					int answer_num = (int)row.GetCell(col_answer_num).NumericCellValue;
					speech.answerlist = new List<ChatData.SceneScript.Speech.Answer>();
					for(int i = 0; i < answer_num; i++)
					{
						ChatData.SceneScript.Speech.Answer answer = new ChatData.SceneScript.Speech.Answer();
						answer.contents = row.GetCell(col_answer_first + i).StringCellValue;
						answer.success_reaction = inputSheet.GetRow(ix + row_answer_success_react).GetCell(col_answer_first + i).StringCellValue; //same col and next row line has success reaction string 
						answer.fail_reaction = inputSheet.GetRow(ix + row_answer_fail_react).GetCell(col_answer_first + i).StringCellValue; 
						answer.next_scene_id_if_success = (int)inputSheet.GetRow(ix + row_answer_success_next_scene).GetCell(col_answer_first + i).NumericCellValue;
						answer.next_scene_id_if_success = (int)inputSheet.GetRow(ix + row_answer_fail_next_scene).GetCell(col_answer_first + i).NumericCellValue;
						if (inputSheet.GetRow(ix + row_answer_success_react).GetCell(col_facelook) != null)
							answer.success_facelook_filename = inputSheet.GetRow(ix + row_answer_success_react).GetCell(col_facelook).StringCellValue; //same col and next row line has success reaction string 
						if (inputSheet.GetRow(ix + row_answer_fail_react).GetCell(col_facelook) != null)
							answer.fail_facelook_filename = inputSheet.GetRow(ix + row_answer_fail_react).GetCell(col_facelook).StringCellValue; //same col and next row line has success reaction string 
						if (inputSheet.GetRow(ix + row_answer_success_react).GetCell(col_bgm) != null)
							answer.success_bgm_filename = inputSheet.GetRow(ix + row_answer_success_react).GetCell(col_bgm).StringCellValue; //same col and next row line has success reaction string 
						if (inputSheet.GetRow(ix + row_answer_fail_react).GetCell(col_bgm) != null)
							answer.fail_bgm_filename = inputSheet.GetRow(ix + row_answer_fail_react).GetCell(col_bgm).StringCellValue; //same col and next row line has success reaction string 



						speech.answerlist.Add(answer); 
					}
					inputScreneScript.speech_list.Add(speech);
					ix += 4;
				}
				else
				{
					break;
				}
			}
			else
			{
				break;
			}
		}
	}
}