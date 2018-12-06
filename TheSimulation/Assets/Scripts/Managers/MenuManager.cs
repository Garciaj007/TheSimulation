using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour {

    [Header("Animators")]
    public Animator mainAnim;
    public Animator confirmAnim;
    public Animator playAnim;

    [Header("Objects")]
    public GameObject contentPanel;
    public GameObject overlay;
    public TextMeshProUGUI subtitle;
    [Space]
    public GameObject videoPanel;
    public GameObject soundPanel;
    public GameObject controlsPanel;
    public GameObject networkPanel;
    public GameObject assistPanel;
    public GameObject creditsPanel;

    [Header("Prefabs")]
    public GameObject sliderPanel;
    public GameObject selectionPanel;
    public GameObject imagePanel;
    public GameObject headerPanel;
    public GameObject divider;

    //Singleton
    public static MenuManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
	
	//private void Selection () {
 //       switch (e_MainMenu)
 //       {
 //           case MainMenuEvents.Null: break;
 //           case MainMenuEvents.Play: //Do Something
 //               break;
 //           case MainMenuEvents.Settings: //Do Something
 //               break;
 //           case MainMenuEvents.Quit: //Do Something
 //               break;
 //       }
	//}

    //------------------------- Default State ---------------------------------

    public void MainMenu()
    {
        DisplayPanel(playAnim, false);
        DisplayPanel(confirmAnim, false);

        mainAnim.SetInteger("State", 0);
    }

    //------------------------ Main Menu Methods ------------------------------

    public void Play()
    {
        DisplayPanel(playAnim, true);
    }

    public void Settings()
    {
        mainAnim.SetInteger("State", 1);
        Video(overlay.transform);
    }

    public void Quit()
    {
        DisplayPanel(confirmAnim, true);
    }

    // -------------------------- Confirm Menu Methods ---------------------

    public void ConfirmYes()
    {
        Application.Quit();
    } 

    public void ConfirmNo()
    {
        MainMenu();
    }

    //------------------------- Play Menu Methods --------------------------

    public void Resume()
    {
        Debug.Log("Resumes Saved Game......");
    }

    public void NewGame()
    {
        mainAnim.SetInteger("State", 2);
        Debug.Log("Not Implemented Yet...");
    }

    public void Online()
    {
        mainAnim.SetInteger("State", 3);
    }

    // ---------------------------- Online Menu Methods -------------------------

    public void Coop()
    {
        Debug.Log("Begins Co-op Game");
    }

    public void Versus()
    {
        Debug.Log("Begins Versus Game");
    }

    // ---------------------------- Settings Menu --------------------------------

    public void Video(Transform t)
    {
        ClearContentPanel();

        overlay.transform.position = t.position;

        videoPanel.SetActive(true);

        subtitle.text = "Video & Graphical Settings";
   
    }

    public void Sound(Transform t)
    {
        ClearContentPanel();

        overlay.transform.position = t.position;

        soundPanel.SetActive(true);

        subtitle.text = "Sound Settings";
    }

    public void Controls(Transform t)
    {
        ClearContentPanel();

        overlay.transform.position = t.position;

        controlsPanel.SetActive(true);

        subtitle.text = "Control Settings";

        Debug.Log("You are asking for too much......");
    }

    public void Network(Transform t)
    {
        ClearContentPanel();

        overlay.transform.position = t.position;

        networkPanel.SetActive(true);

        subtitle.text = "Network Settings";

        Debug.Log("Not Implemented Yet, GUD LUCK");
    } 

    public void AssistMode(Transform t)
    {
        ClearContentPanel();

        overlay.transform.position = t.position;

        assistPanel.SetActive(true);

        subtitle.text = "Difficulty & Accessability Settings";
    }

    public void Credits(Transform t)
    {
        ClearContentPanel();

        overlay.transform.position = t.position;

        creditsPanel.SetActive(true);

        subtitle.text = " ";
    }

    // --------------------- Utility functions -------------------------------

    public void ClearContentPanel()
    {
        foreach(Transform child in contentPanel.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void DisplayPanel(Animator anim, bool b)
    {
        anim.SetBool("Display", b);
    }
}
