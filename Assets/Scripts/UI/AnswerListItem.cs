using UnityEngine;
using System.Collections;

public class AnswerListItem : MonoBehaviour
{
    public UILabel _answerLabel;

    void Awake()
    {
        _answerLabel = GetComponent<UILabel>();
    }

    void Update()
    {

    }
}