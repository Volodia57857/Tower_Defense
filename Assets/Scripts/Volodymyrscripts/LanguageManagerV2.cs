using UnityEngine;
using TMPro;
using System.Collections;

public class LanguageManagerV2 : MonoBehaviour
{
    public LanguageManagerV1[] languages;
    public TMP_Text languageText;
    public TMP_Text soundText;
    public TMP_Text creditsText;
    public TMP_Text TheCreditText;
    public TMP_Text Level1Text;
    public TMP_Text Level2Text;
    public TMP_Text Level3Text;
    public TMP_Text Level4Text;
    public TMP_Text Level5Text;
    public void SetLanguageWithDelay(int index)
    {
        LanguageManagerV1 lang = languages[index];

        languageText.text = lang.Language;
        soundText.text = lang.Sound;
        creditsText.text = lang.Credits;
        TheCreditText.text = lang.TheCredits;
        Level1Text.text = lang.Level + " 1";
        Level2Text.text = lang.Level + " 2";
        Level3Text.text = lang.Level + " 3";
        Level4Text.text = lang.Level + " 4";
        Level5Text.text = lang.Level + " 5";
    }
}
