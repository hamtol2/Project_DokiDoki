using UnityEngine;
using System.Collections;

public class GameQuit : MonoBehaviour
{
	private static GameQuit _instance;

	public static GameQuit Instance
	{
		get
		{
			return _instance;
		}
	}

	void Start()
	{
		if (_instance == null)
		{
			_instance = this;

			DontDestroyOnLoad(this.gameObject);
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}