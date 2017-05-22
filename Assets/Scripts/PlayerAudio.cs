using UnityEngine;
using System.Collections;

public class PlayerAudio : MonoBehaviour {

	AudioSource cheerAudio;
	AudioSource whistleAudio;

	// Use this for initialization
	void Start () {
		AudioSource[] audios = GetComponents<AudioSource>();
		cheerAudio = audios[0];
		whistleAudio = audios[1];
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Cheer()
	{
		whistleAudio.Play ();
	}
}
