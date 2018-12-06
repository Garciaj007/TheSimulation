using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public GameManager Instance { get; private set; }
    public List<GameObject> players;
    public List<GameObject> enemies;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
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
