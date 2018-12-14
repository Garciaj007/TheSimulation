using UnityEngine;

public class LocalSceneManager : MonoBehaviour {

    public static LocalSceneManager Instance { get; private set; }

    protected virtual void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    protected virtual void Start () {
        SetupMasterSpellLibrary();
    }
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}

    void SetupMasterSpellLibrary()
    {
        SpellLibrary.AddSpell(new Force_MoveObject());
        SpellLibrary.AddSpell(new Force_NeutraliseGravity());
        SpellLibrary.AddSpell(new Force_FlipGravity());
        SpellLibrary.AddSpell(new Ice_Freeze());
        SpellLibrary.AddSpell(new Fire_Burn());
    }
}
