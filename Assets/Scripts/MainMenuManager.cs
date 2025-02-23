using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class MainMenuManager : MonoBehaviour
{
    AudioSource audioClip;
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
