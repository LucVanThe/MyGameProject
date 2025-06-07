using UnityEngine;
using UnityEngine.SceneManagement;
public class BackGroundMusic : MonoBehaviour
{
    public static BackGroundMusic instance; 

    [SerializeField] private AudioSource bgAudio;
    [SerializeField] private AudioClip bgAudioClip;
    [SerializeField] private AudioClip BossClip;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(bgAudio); 
            return;
        }
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            if (bgAudio.clip != BossClip)
            {
                playBossdmusic();
            }
        }
        else
        {
            if (bgAudio.clip != bgAudioClip)
            {
                PlayBackgroundMusic();
            }
        }
    }
    public void playBossdmusic()
    {
        bgAudio.clip = BossClip;
        bgAudio.Play();
    }
    public void PlayBackgroundMusic()
    {
        if (bgAudio.clip != bgAudioClip)
        { 
            bgAudio.clip = bgAudioClip;
            bgAudio.loop = true;
            bgAudio.Play();
        }
    }
    public void BatTatNhacNen()
    {
        if (bgAudio.isPlaying)
        {


            bgAudio.Pause();


        }
        else
        {
           // bgAudio.enabled = true;

            bgAudio.UnPause();
        }

    }
}
