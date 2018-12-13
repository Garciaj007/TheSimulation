using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler {

    private AudioSource source;

    void Start() {
        source = GameObject.Find("UISound").GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        source.PlayOneShot(UISoundManager.Instance.mouseOverClip);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        source.PlayOneShot(UISoundManager.Instance.mouseDownClip);
    }
}
