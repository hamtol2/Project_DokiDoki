using UnityEngine;
using System.Collections;

public class FaceLookTest : MonoBehaviour
{
	public UISprite faceSprite;

	string[] sprites = new string[]
	{
		"Face1", "Face2", "Face3", "Face4", "Face5", "Face6", "Face7", "Face8"
	};

	int spriteIndex = 0;

	IEnumerator Start()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.5f);

			if (spriteIndex >= sprites.Length)
				spriteIndex = 0;

			faceSprite.spriteName = sprites[spriteIndex++];
		}
	}
}