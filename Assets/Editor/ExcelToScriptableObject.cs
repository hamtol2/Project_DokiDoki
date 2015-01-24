using UnityEngine;
using UnityEditor;
using System.Collections;
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
        data.list.Clear();

        using (FileStream stream = File.Open(filePath, FileMode.Open))
        {
            IWorkbook book = new XSSFWorkbook(stream);
            ISheet sheet = book.GetSheetAt(0);

            for (int ix = 1; ix <= sheet.LastRowNum; ++ix)
            {
                IRow row = sheet.GetRow(ix);

                ChatData.ChatProperty property = new ChatData.ChatProperty();
                property.question = row.GetCell(2).StringCellValue;
                
                for (int jx = 4; jx < 4 + (int)row.GetCell(3).NumericCellValue; ++jx)
                {
                    property.answers.Add(row.GetCell(jx).StringCellValue);
                }

                data.list.Add(property);
            }

            stream.Close();
        }

        ScriptableObject obj = AssetDatabase.LoadAssetAtPath(chatDataPath, typeof(ScriptableObject)) as ScriptableObject;
        EditorUtility.SetDirty(obj);
    }
}