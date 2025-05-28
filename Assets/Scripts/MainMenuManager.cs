using System.Collections;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class MainMenuManager : MonoBehaviour
{
    AudioSource audioClip;
    public Animator sceneTransition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        audioClip = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        audioClip.clip = clip;
        audioClip.Play();
    }

    public void StartGame(string scene)
    {
        StartCoroutine(delayedStart(scene));
    }

    IEnumerator delayedStart(string scene)
    {
        sceneTransition.SetTrigger("SceneExit");
        yield return new WaitForSeconds(1);
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        StartCoroutine(delayedEnd());
    }

    IEnumerator delayedEnd()
    {
        sceneTransition.SetTrigger("SceneExit");
        yield return new WaitForSeconds(1);
        Application.Quit();
    }

}
