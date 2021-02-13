using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip eatAudio, gameAudio, titleAudio;
    public float volume = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        eatAudio = Resources.Load<AudioClip>("eatFood");
        gameAudio = Resources.Load<AudioClip>("gameAudio");
        titleAudio = Resources.Load<AudioClip>("titleAudio");//fill in me.
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void soundPlayer(string clip)
    {
        switch (clip)
        {
            case "eatFood":
                audioSource.PlayOneShot(eatAudio);
                Debug.Log("called eat");
                break;
        }
    }
}
