using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip eatAudio, gameAudio, titleAudio;
    public float volume = 0.0f;

    public enum Sounds
    {
        eatFood,
    };

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void soundPlayer(Sounds clip)
    {
        switch (clip)
        {
            case Sounds.eatFood:
                audioSource.PlayOneShot(eatAudio);
                Debug.Log("called eat");
                break;
        }
    }
}
