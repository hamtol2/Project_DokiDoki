using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChatData : ScriptableObject
{
    public List<ChatProperty> list = new List<ChatProperty>();

	[System.Serializable]
    public class ChatProperty
    {
        public string question;
        public List<string> answers = new List<string>();
    }
}