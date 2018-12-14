using UnityEngine;

public class UISoundManager : MonoBehaviour {

    public static UISoundManager Instance { get; private set; }

    [Header("UIClips")]
    public AudioClip mouseOverClip;
    public AudioClip mouseDownClip;

    private AudioSource source;

    private void Awake()
    {
        Instance = this;
    }

    void Start () {
        source = GetComponent<AudioSource>();
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            source.PlayOneShot(mouseDownClip);
        }
	}
}
