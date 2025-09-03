using UnityEngine;

public class AutoScrollCredits : MonoBehaviour
{
    public GameObject settingPanel;
    public GameObject CreditPanel;
    public float speed = 20f;
    public RectTransform viewport;
    public RectTransform content;

    void OnEnable()
    {
        content.anchoredPosition = new Vector2(0, 0);
    }

    void Update()
    {
        content.anchoredPosition += new Vector2(0, speed * Time.deltaTime);

        if (content.anchoredPosition.y >= content.sizeDelta.y - viewport.rect.height)
        {
            if (CreditPanel.activeSelf)
            {
                settingPanel.SetActive(true);
                CreditPanel.SetActive(false);
            }
        }
    }
}
