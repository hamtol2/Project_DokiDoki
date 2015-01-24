using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AudioController : MonoBehaviour {
	public List<AudioClip> myAudioClip;
	// Use this for initialization
	void Start () {
	
	}
	
	public void ChangeSound(string filename)
	{
		audio.Stop();
		for(int i = 0; i < myAudioClip.Count; i++)
		{
			if(myAudioClip[i].name.Equals(filename))
			{
				audio.clip = myAudioClip[i];
				break;
			}
		}
		audio.Play();
	}
}
