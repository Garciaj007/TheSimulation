using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerEnterHandler {

    public AudioSource source;
    public AudioClip clip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        source.PlayOneShot(clip);
    }
}
