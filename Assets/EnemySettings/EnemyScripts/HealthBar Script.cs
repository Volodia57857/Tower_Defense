using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Vector3 offset = new Vector3(0, 2f, 0);

    private Transform target;
    private Camera cam;

    public void Init(Transform followTarget, int HP)
    {
        target = followTarget;
        cam = Camera.main;

        slider.maxValue = HP;
        slider.value = HP;
    }

    public void SetHealth(int hp)
    {
        slider.value = hp;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // позиція над головою
        transform.position = target.position + offset;

        // завжди повертається до камери
        if (cam != null)
            transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }
}