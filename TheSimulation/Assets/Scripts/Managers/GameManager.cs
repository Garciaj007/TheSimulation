using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void TutorialLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void ForestLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void HarbourLevel()
    {
        SceneManager.LoadScene(3);
    }
}
