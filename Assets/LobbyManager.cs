using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public GameObject lobbyPanel;
    public GameObject settingsPanel;
    public GameObject ShopPanel;
    public GameObject languagePanel;
    public GameObject PlayingPanel;
    public GameObject ImagePanel;
    public GameObject CreditsPanel;

    public void OpenSettings()
    {
        lobbyPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSetting()
    {
        settingsPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }
    public void CloseShop()
    {
        ShopPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }
    public void OpenShop()
    {
        lobbyPanel.SetActive(false);
        ShopPanel.SetActive(true);
    }
    public void CloseLanguages()
    {
        languagePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    public void OpenLanguages()
    {
        languagePanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
    public void ClosePlaying()
    {
        lobbyPanel.SetActive(true);
        ImagePanel.SetActive(true);
        PlayingPanel.SetActive(false);
    }
    public void OpenPlaying()
    {
        lobbyPanel.SetActive(false);
        ImagePanel.SetActive(false);
        PlayingPanel.SetActive(true);
    }
    public void CloseCredits()
    {
        CreditsPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    public void OpenCredits()
    {
        CreditsPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
}
