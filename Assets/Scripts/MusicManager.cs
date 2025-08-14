using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GlobalMusicManager : MonoBehaviour
{
    public static GlobalMusicManager instance;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip menuMusic;
    public AudioClip[] levelPlaylist;
    public float fadeDuration = 1f;

    [Header("UI Sliders")]
    public Slider menuVolumeSlider;
    public Slider pauseVolumeSlider;

    private int currentTrackIndex = 0;
    private bool inLevelPlaylist = false;

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
        audioSource.loop = true;
        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);

        SetupSlider(menuVolumeSlider);
        SetupSlider(pauseVolumeSlider);

        PlayMenuMusic();
    }

    private void SetupSlider(Slider slider)
    {
        if (slider != null)
        {
            slider.value = audioSource.volume;
            slider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float value)
    {
        audioSource.volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);

        if (menuVolumeSlider != null && menuVolumeSlider.value != value)
            menuVolumeSlider.value = value;
        if (pauseVolumeSlider != null && pauseVolumeSlider.value != value)
            pauseVolumeSlider.value = value;
    }

    public void PlayMenuMusic()
    {
        StopAllCoroutines();
        inLevelPlaylist = false;
        currentTrackIndex = 0;
        StartCoroutine(FadeTo(menuMusic, loop: true));
    }

    public void PlayLevelPlaylist()
    {
        if (levelPlaylist.Length == 0) return;

        StopAllCoroutines();
        inLevelPlaylist = true;
        currentTrackIndex = 0;
        StartCoroutine(FadeTo(levelPlaylist[currentTrackIndex], loop: false, nextTrack: true));
    }

    private IEnumerator FadeTo(AudioClip newClip, bool loop = false, bool nextTrack = false)
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.unscaledDeltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }
        audioSource.volume = 0;

        audioSource.clip = newClip;
        audioSource.loop = loop;
        audioSource.Play();

        float targetVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        for (float t = 0; t < fadeDuration; t += Time.unscaledDeltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, targetVolume, t / fadeDuration);
            yield return null;
        }
        audioSource.volume = targetVolume;

        if (nextTrack && inLevelPlaylist)
            StartCoroutine(PlayNextTrackSmooth());
    }

    private IEnumerator PlayNextTrackSmooth()
    {
        while (inLevelPlaylist && levelPlaylist.Length > 0)
        {
            yield return new WaitUntil(() => audioSource.time >= audioSource.clip.length - fadeDuration);

            float startVolume = audioSource.volume;
            for (float t = 0; t < fadeDuration; t += Time.unscaledDeltaTime)
            {
                audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
                yield return null;
            }

            currentTrackIndex++;
            if (currentTrackIndex >= levelPlaylist.Length)
                currentTrackIndex = 0;

            audioSource.clip = levelPlaylist[currentTrackIndex];
            audioSource.Play();

            float targetVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            for (float t = 0; t < fadeDuration; t += Time.unscaledDeltaTime)
            {
                audioSource.volume = Mathf.Lerp(0, targetVolume, t / fadeDuration);
                yield return null;
            }
            audioSource.volume = targetVolume;
        }
    }
}