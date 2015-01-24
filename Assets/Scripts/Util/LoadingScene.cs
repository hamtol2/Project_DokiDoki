using UnityEngine;
using System.Collections;

public class LoadingScene : MonoBehaviour
{
	public int loadingSceneIndex = 2;
	public float heartImageChangeTime = 1f;
	public float sceneLoadDealyTime = 2f;
	public UISprite bgSprite;
	public string secondSprite;
	public UISprite fadeoutSprite;

	// Change start logo after 2 sec and load new scene.
	IEnumerator Start()
	{
		yield return new WaitForSeconds(heartImageChangeTime);
		bgSprite.spriteName = secondSprite;

		yield return new WaitForSeconds(sceneLoadDealyTime);
		while (fadeoutSprite.color.a < 1f)
		{
			Color c = fadeoutSprite.color;
			c.a += 0.4f * Time.deltaTime;

			fadeoutSprite.color = c;

			yield return null;
		}

		Application.LoadLevel(loadingSceneIndex);
	}
}