using UnityEngine;
using System.Collections;

public class FadeTest : MonoBehaviour
{
	void Start()
	{
		StartCoroutine(SceneManager.Instance.SceneFader(1));
	}
}