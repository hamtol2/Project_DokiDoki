using UnityEngine;
using System.Collections;

public class MyFader : MonoBehaviour {

	public UISprite mySprite;
	public ChatDataManager manager;
	private int myParam;

	void Start()
	{
		Color c = mySprite.color;
		c.a = 0f;
		mySprite.color = c;
	}
	public void StartFade(int param)
	{
		myParam = param;
		StartCoroutine("Fadein");
	}

	IEnumerator Fadein()
	{	
		while (mySprite.color.a < 1f)
		{
			Color c = mySprite.color;
			c.a += 0.1f;
			
			mySprite.color = c;
			
			yield return new WaitForSeconds(0.1f);
		}
		manager.FaderCallback(myParam);
	}
}
