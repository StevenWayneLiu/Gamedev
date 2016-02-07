using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance = null;
    public AudioSource bgmSource;//audio source attached to the camera controlls bgm

	// Use this for initialization
	void Awake () {
        if (instance == null)
            instance = this;
        else if(instance != this)
            Destroy(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
