using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    GameObject[] enemies;
    public Animator sceneTransition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null)
            {
                enemies = GameObject.FindGameObjectsWithTag("Enemy");
            }
        }

        if (enemies.Length == 0)
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
