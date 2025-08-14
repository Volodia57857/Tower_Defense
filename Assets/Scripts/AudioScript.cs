using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GlobalMusicManager : MonoBehaviour
{
    public static GlobalMusicManager instance;

    public AudioSource audioSource;
    public AudioClip menuMusic;
    public AudioClip[] levelMusic; 
    public Slider volumeSlider;    

    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        audioSource.volume = savedVolume;
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       
        if (scene.name == "MainMenu")
        {
            PlayMusic(menuMusic);
        }
        else
        {
            
            for (int i = 0; i < levelMusic.Length; i++)
            {
                if (scene.name == "Level" + (i + 1))
                {
                    PlayMusic(levelMusic[i]);
                    break;
                }
            }
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (clip != null && audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void SetVolume(float value)
    {
        audioSource.volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
    }
}