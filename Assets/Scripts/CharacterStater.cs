using UnityEngine;
using System.Collections;

public class CharacterStater : MonoBehaviour
{
	public UISprite[] sprites;

	void Awake()
	{
		sprites = GetComponentsInChildren<UISprite>();
	}

	void Start()
	{
		StartCoroutine("FadeInCharacter");
	}

	IEnumerator FadeInCharacter()
	{
		yield return new WaitForSeconds(0.5f);

		Color color = sprites[0].color;
		while (sprites[0].color.a < 1f)
		{
			color.a += 0.4f * Time.deltaTime;
			foreach (UISprite item in sprites)
			{
				item.color = color;
			}

			yield return null;
		}

		ChatDataManager.Instance.chatScrollView.UpdateScreen();
	}
}