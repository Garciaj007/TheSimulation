using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEffectSoundController : MonoBehaviour {

    public AudioSource source;
    public AudioClip clip;
    public Vector2 minMaxPitch;
    [Range(0.0f, 1.0f)]
    public float volume;

    private AudioSource clone;
    private Timer pauseTimer;

    private void Start()
    {
        clone = gameObject.AddComponent<AudioSource>();
        clone.volume = volume;
        clone.spatialBlend = 1.0f;
        clone.spread = 360.0f;
        clone.playOnAwake = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        float p = Random.Range(minMaxPitch.x, minMaxPitch.y);
        clone.pitch = p;
        clone.PlayOneShot(clip);
    }
}
