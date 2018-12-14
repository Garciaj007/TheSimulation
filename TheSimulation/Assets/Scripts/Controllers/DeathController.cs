using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathController : MonoBehaviour {

    public Animator anim;
    public int currentScene;
    private EntityController player;
    private Timer respawnTime;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityController>();
        player.Death += Die;

        respawnTime = gameObject.AddComponent<Timer>();
        respawnTime.Duration = 6.0f;
        respawnTime.TimerDone += Respawn;
    }

    public void Die()
    {
        anim.SetBool("Display", true);
        player.CanDamage = false;
        player.Heal(player.EntityProperties.maxHealth);
        respawnTime.Begin();
    }

    public void Respawn()
    {
        if(anim)
        anim.SetBool("Display", false);
        //SceneManager.LoadScene(currentScene);
        respawnTime.Restart();
        respawnTime.Pause();
    }

}
