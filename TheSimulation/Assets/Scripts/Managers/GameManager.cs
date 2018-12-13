using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public GameManager Instance { get; private set; }
    public List<GameObject> players;
    public List<GameObject> enemies;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
        DontDestroyOnLoad(this);

        SetupMasterSpellLibrary();
    }

    void SetupMasterSpellLibrary()
    {
        SpellLibrary.AddSpell(new Force_MoveObject());
        SpellLibrary.AddSpell(new Force_NeutraliseGravity());
        SpellLibrary.AddSpell(new Force_FlipGravity());
        SpellLibrary.AddSpell(new Ice_Freeze());
        SpellLibrary.AddSpell(new Fire_Burn());
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void AddPlayer(GameObject player)
    {
        players.Add(player);
    }

    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void RemovePlayer(GameObject player)
    {
        players.Remove(player);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        players.Remove(enemy);
    }
}
