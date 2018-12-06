using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler {

    private AudioSource source;

    void Start() {
        source = GameObject.Find("SoundManager").GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        source.PlayOneShot(SoundManager.Instance.mouseOverClip);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        source.PlayOneShot(SoundManager.Instance.mouseDownClip);
    }
}
