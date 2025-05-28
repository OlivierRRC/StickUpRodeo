using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    GameObject[] enemies;
    

    private GameObject player;
    private BoxCollider winZone;
    public Animator sceneTransition;
    public TMP_Text enemyText;
    public Light princessSpotlight;
    private bool noEnemiesLeft = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        enemyText.text = "ENEMIES REMAINING: " + enemies.Length;
        winZone = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null)
            {
                enemies = GameObject.FindGameObjectsWithTag("Enemy");
                enemyText.text = "ENEMIES REMAINING: " + enemies.Length;
            }
        }

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
        if (!noEnemiesLeft)
        {
            return;
        }
        else
        {
            StartCoroutine(TransitionToWin());
        }

    }


    IEnumerator TransitionToWin()
    {
        sceneTransition.SetTrigger("SceneExit");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Win");
    }
}
