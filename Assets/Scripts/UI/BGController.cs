using UnityEngine;
using System.Collections;

public class BGController : MonoBehaviour {

	public UISprite mySprite;

	public void ChangeBG(string spriteName)
	{
		mySprite.spriteName = spriteName;
	}
}
