using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    static public AudioSource sfxSource;
    public AudioSource musicSource;

    [SerializeField]
    AudioClip MenuMusic;
    [SerializeField]
    AudioClip LevelMusic;
    [SerializeField]
    AudioClip WinMusic;

    [SerializeField]
    static AudioClip ButtonSFX;
    [SerializeField]
    static AudioClip EatSFX;


    private void Awake()
    {

        sfxSource = GetComponentInChildren<AudioSource>();
        DontDestroyOnLoad(transform.gameObject);
        SceneManager.activeSceneChanged += ChangedActiveScene;

        EatSFX = Resources.Load<AudioClip>("Sounds/eat_sfx");
        ButtonSFX = Resources.Load<AudioClip>("Sounds/button_sfx");
    }

    static public void PlayBiteSFX()
    {
        sfxSource.PlayOneShot(EatSFX);
    }

    static public void PlayButtonSFX()
    {
        sfxSource.PlayOneShot(ButtonSFX);
    }


    private void ChangedActiveScene(Scene current, Scene next)
    {

        switch (next.buildIndex)
        {
            case 0:
            case 1:
            case 2:
            case 8: // 8 is the alt main menu to prevent dupliting the audio manager
                if (!(musicSource.isPlaying && musicSource.clip == MenuMusic))
                {
                    musicSource.clip = MenuMusic;
                    musicSource.Play();
                }
                break;
            case 3:
            case 4:
            case 5:
            case 6:
                if (!(musicSource.isPlaying && musicSource.clip == LevelMusic))
                {
                    musicSource.clip = LevelMusic;
                    musicSource.Play();
                }
                break;
            case 7:
                musicSource.clip = WinMusic;
                musicSource.Play();
                break;
        };

    }
}
