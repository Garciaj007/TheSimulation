using UnityEngine;
using TMPro;

public class PauseManager : MenuManager {

    [Header("Animators")]
    public Animator pauseAnim;
    public Animator settingsAnim;
    public Animator confirmAnimMainMenu;

    public bool Paused { get; private set; }
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape) && !Paused)
        {
            DefaultMenu();
            Cursor.visible = true;
            Paused = true;
        } else if (Input.GetKeyDown(KeyCode.Escape))
        {
            DefaultMenu();
            Paused = false;
            Cursor.visible = false;
        }

        DisplayPanel(pauseAnim, Paused);

        //if (Paused)
        //    Time.timeScale = 0;
        //else
        //    Time.timeScale = 1;
    }

    public void UnPause()
    {
        Paused = false;
        Cursor.visible = false;
    }

    public void QuitToMenu()
    {
        HidePanels();
        DisplayPanel(confirmAnimMainMenu, true);
    }

    public override void ConfirmYes(int option)
    {
        if (option == 0)
            Application.Quit();
        else
            GameManager.Instance.MainMenu();
    }

    public override void DefaultMenu()
    {
        HidePanels();
    }

    public override void HidePanels()
    {
        DisplayPanel(confirmAnim, false);
        DisplayPanel(settingsAnim, false);
        DisplayPanel(confirmAnimMainMenu, false);
    }

    public void DisplaySettings()
    {
        DisplayPanel(settingsAnim, true);
    }
}
