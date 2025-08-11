using UnityEngine;
using TMPro;
using System.Collections;

public class LanguageManagerV2 : MonoBehaviour
{
    public GameObject LoadingPanel;
    public LanguageManagerV1[] languages;
    public TMP_Text languageText;
    public TMP_Text soundText;
    public TMP_Text creditsText;
    public TMP_Text badgesText;
    public TMP_Text loadingText;

    public void SetLanguage(int index)
    {
        StartCoroutine(SetLanguageWithDelay(index));
    }

    private IEnumerator SetLanguageWithDelay(int index)
    {
        LanguageManagerV1 lang = languages[index];
        LoadingPanel.SetActive(true);

        float timer = 0f;
        int dotCount = 0;
        while (timer < 1f)
        {
            loadingText.text = lang.Loading + new string('.', dotCount % 4);
            dotCount++;
            timer += 0.25f;
            yield return new WaitForSeconds(0.25f);
        }

        languageText.text = lang.Language;
        soundText.text = lang.Sound;
        creditsText.text = lang.Credits;
        badgesText.text = lang.Badges;
        loadingText.text = lang.Done;

        yield return new WaitForSeconds(0.5f);
        LoadingPanel.SetActive(false);
    }

}
