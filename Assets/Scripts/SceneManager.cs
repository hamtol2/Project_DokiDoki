using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{
	private static SceneManager _instance;
	public static SceneManager Instance
	{
		get { return _instance; }
	}

	public enum SceneType
	{
		Theater = 0,
		Karaoke = 1,
		Cafe = 2,
		Shopping = 3,
		Restraunt = 4,
		Park = 5,
		GirlsHome = 6,

		Max
	}

	public SpriteRenderer fadeSprite;
	public bool isOnMovingScene = false;

	void Awake()
	{
		if (_instance == null)
		{
			_instance = this;

			if (fadeSprite == null)
				fadeSprite = GetComponentInChildren<SpriteRenderer>();

			DontDestroyOnLoad(gameObject);
		}

		else
		{
			Destroy(gameObject);
		}
	}

	// Scene load with Fade effect with scene name
	public IEnumerator SceneFader(string sceneNameToLoad)
	{
		yield return StartCoroutine("FadeEffect");
//		Application.LoadLevel(sceneNameToLoad);
	}

	// scene load with fade effect with scen index
	public IEnumerator SceneFader(int sceneIndexToLoad)
	{
		yield return StartCoroutine("FadeEffect");
		Application.LoadLevel(sceneIndexToLoad);
	}

	// fade effector
	IEnumerator FadeEffect()
	{
		Debug.Log("alpha value: " + fadeSprite.color.a);
		Color color = fadeSprite.color;
		while (fadeSprite.color.a < 1f)
		{
			//Debug.Log("here");
			color.a += 0.2f * Time.deltaTime;
			//color = fadeSprite.color;
			fadeSprite.color = color;
			
			yield return null;
		}

		color.a = 0f;
		fadeSprite.color = color;
	}
}