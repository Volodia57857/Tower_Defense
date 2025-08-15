using UnityEngine;

public class AutoScrollCredits : MonoBehaviour
{
    public GameObject settingPanel;
    public GameObject CreditPanel;
    public float speed = 20f;
    public RectTransform viewport;

    private RectTransform content;

    void Start()
    {
        content = GetComponent<RectTransform>();
    }

    void Update()
    {
        content.anchoredPosition += new Vector2(0, speed * Time.deltaTime);

        if (content.anchoredPosition.y >= content.sizeDelta.y - viewport.rect.height)
        {
            settingPanel.SetActive(true);
            CreditPanel.SetActive(false);
        }
    }
}
