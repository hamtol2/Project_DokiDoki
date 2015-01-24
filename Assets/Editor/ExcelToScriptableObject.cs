using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

public class ExcelToScriptableObject : MonoBehaviour
{
    static readonly string filePath = "Assets/Editor/Data/ExcelData.xlsx";
    static readonly string chatDataPath = "Assets/Resources/Data/ChatDB.asset";

    [MenuItem("Excel Data Import/Create Scriptable Object")]
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
		for(int scene_index = 0; scene_index < inputBook.NumberOfSheets; scene_index++)
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
		for (int ix = 1; ix <= inputSheet.LastRowNum; )
		{
			IRow row = inputSheet.GetRow(ix);
			const int col_time = 0;
			const int col_type = 1;
			const int col_speaker = 2;
			const int col_question = 3;
			const int col_fadein = 4;
			//const int col_sound = 5;
			//const int col_heroin_img = 6;
			//const int col_reserved = 7;
			const int col_answer_num = 8;
			const int col_answer_first = 9;
			
			//this is row define relate to answer info
			const int row_answer_content = 0;
			const int row_answer_success_react = 1;
			const int row_answer_fail_react = 2;
			const int row_answer_success_next_scene = 3;
			const int row_answer_fail_next_scene = 4;
			if(!row.GetCell(col_type).Equals(""))
			{
				string speechtype = row.GetCell(col_type).StringCellValue;
				if(speechtype.Equals("S"))
				{
					ChatData.SceneScript.Speech speech = new ChatData.SceneScript.Speech();
					speech.speech_type = ChatData.SceneScript.Speech.TYPE.S;
					speech.question = row.GetCell(col_question).StringCellValue;
					string speaker_gender = row.GetCell(col_type).StringCellValue;
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
					speech.speech_type = ChatData.SceneScript.Speech.TYPE.QN;
					speech.question = row.GetCell(col_question).StringCellValue;
					int answer_num = (int)row.GetCell(col_answer_num).NumericCellValue;
					speech.answerlist = new List<ChatData.SceneScript.Speech.Answer>();
					for(int i = 0; i < answer_num; i++)
					{
						ChatData.SceneScript.Speech.Answer answer = new ChatData.SceneScript.Speech.Answer();
						answer.contents = row.GetCell(col_answer_first + i).StringCellValue;
						answer.success_reaction = inputSheet.GetRow(ix + row_answer_success_react).GetCell(col_answer_first + i).StringCellValue; //same col and next row line has success reaction string 
						speech.answerlist.Add(answer); 
					}
					inputScreneScript.speech_list.Add(speech);
					ix += 2;
				}
				else if(speechtype.Equals("QR"))
				{
					ChatData.SceneScript.Speech speech = new ChatData.SceneScript.Speech();
					speech.speech_type = ChatData.SceneScript.Speech.TYPE.QR;
					speech.question = row.GetCell(col_question).StringCellValue;
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