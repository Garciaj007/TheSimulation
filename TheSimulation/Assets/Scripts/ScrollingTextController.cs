using UnityEngine;
using TMPro;
using System.Collections;
public class ScrollingTextController : MonoBehaviour {

    public TextMeshProUGUI text;
    [Range(0f, 10f)]
    public float speed;
    public string ID;

    private TextMeshProUGUI clone;
    private float width;

    private void Awake()
    {
        clone = Instantiate(text) as TextMeshProUGUI;
        clone.rectTransform.SetParent(text.rectTransform);
        clone.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        clone.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        clone.rectTransform.localScale = Vector3.one;
        clone.rectTransform.position = new Vector3(text.rectTransform.position.x + text.preferredWidth, text.rectTransform.position.y, 0);
    }

    IEnumerator Start()
    {
        width = text.preferredWidth - text.preferredWidth / 2;
        Vector3 startPosition = new Vector3(text.rectTransform.position.x - width, text.rectTransform.position.y, 0);

        float scrollPosition = 0;

        while (true)
        {
            text.rectTransform.position = new Vector3(-scrollPosition % width + startPosition.x, startPosition.y, 0f);

            clone.color = text.color;

            scrollPosition += speed * 10 * Time.deltaTime;

            yield return null;
        }
    }

    public void UpdateText()
    {
        width = text.preferredWidth - text.preferredWidth / 4;
        clone.text = text.text;
    }
}
