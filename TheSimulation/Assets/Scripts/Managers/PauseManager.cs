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
            Paused = false;
            Cursor.visible = false;
        }

        DisplayPanel(pauseAnim, Paused);
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
            Debug.Log("Exiting to Menu");
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
