using UnityEngine;
using TMPro;
using System.Collections;

public class LanguageManagerV2 : MonoBehaviour
{
    public LanguageManagerV1[] languages;
    public TMP_Text languageText;
    public TMP_Text soundText;
    public TMP_Text creditsText;
    public TMP_Text badgesText;
    public void SetLanguageWithDelay(int index)
    {
        LanguageManagerV1 lang = languages[index];

        languageText.text = lang.Language;
        soundText.text = lang.Sound;
        creditsText.text = lang.Credits;
        badgesText.text = lang.Badges;
    }
}
