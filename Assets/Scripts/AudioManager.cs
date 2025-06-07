using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgAudio;
    [SerializeField] private AudioSource effAudio;
    [SerializeField] private AudioClip bgAudioClip;
    [SerializeField] private AudioClip BossClip;
    [SerializeField] private AudioClip jumpAudioClip;
    [SerializeField] private AudioClip coinClip;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip reloadClip;
   

    //[SerializeField] private TextMeshProUGUI TextNhac;
   
    public void playShootClip()
    {
        effAudio.PlayOneShot(shootClip);
    }
    public void playReloadClip()
    {
        effAudio.PlayOneShot(reloadClip);
    }
    public void playBackgroundmusic()
    {
        bgAudio.clip = bgAudioClip;
        bgAudio.Play();
    }
    public void playCoinSound()
    {
        effAudio.PlayOneShot(coinClip);

    }
    public void playJumpSound()
    {
        effAudio.PlayOneShot(jumpAudioClip);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {          
                playBossdmusic();           
        }
        else
        {         
                playBackgroundmusic();          
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
            bgAudio.enabled = true;

            bgAudio.UnPause();
        }

    }
    public void playBossdmusic()
    {
        bgAudio.clip = BossClip;
        bgAudio.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
