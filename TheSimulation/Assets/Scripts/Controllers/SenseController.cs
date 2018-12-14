using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SenseController : MonoBehaviour {

    public bool enableDebug = true;
    public Identifier.IdentifierTypes identity = Identifier.IdentifierTypes.ENEMY;
    public AIController controller;
    public float detectionRate = 1.0f;

    protected float elapsedTime = 0.0f;
   
    protected virtual void Initialize() { }
    protected virtual void UpdateSense() { }

	// Use this for initialization
	void Start () {
        if(!controller)
            controller = GetComponent<AIController>();
        if (!controller)
            controller = GetComponentInParent<AIController>();

        elapsedTime = 0.0f;
        Initialize();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateSense();
	}
}
