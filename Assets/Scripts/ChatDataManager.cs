using UnityEngine;
using System.Collections;

public class ChatDataManager : MonoBehaviour
{
    private static ChatDataManager _instance;
    public static ChatDataManager Instance
    {
        get { return _instance; }
    }

    public ChatData chatData;

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
}