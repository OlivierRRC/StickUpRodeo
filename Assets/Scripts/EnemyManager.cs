using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    GameObject[] enemies;
    
    public Animator sceneTransition;
    public TMP_Text enemyText;
    public Light princessSpotlight;

    // Used to enable or disable the triggers in the Win Zone collider
    private bool noEnemiesLeft = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyText.text = "ENEMIES REMAINING: " + enemies.Length;
    }

    // Update is called once per frame
    void Update()
    {
        // Count the enemies and update the "ENEMIES REMAINING" UI element
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null)
            {
                enemies = GameObject.FindGameObjectsWithTag("Enemy");
                enemyText.text = "ENEMIES REMAINING: " + enemies.Length;
            }
        }

        // If no enemies remain, enable the Win Zone Collider and update the "ENEMIES REMAINING" UI element
        if (enemies.Length == 0)
        {
            noEnemiesLeft = true;
            princessSpotlight.color = Color.white;
            enemyText.text = "THREAT CLEAR, RECLAIM PRINCESS.";
            enemyText.color = Color.white;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If any enemies remain, the collider will do nothing
        if (!noEnemiesLeft)
        {
            return;
        }
        // Otherwise, it takes you to the win screen!
        else
        {
            StartCoroutine(TransitionToWin());
        }

    }


    IEnumerator TransitionToWin()
    {
        // Fades to black and then switches over to the win screen
        sceneTransition.SetTrigger("SceneExit");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Win");
    }
}
