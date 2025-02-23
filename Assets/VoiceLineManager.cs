using UnityEngine;
using UnityEngine.InputSystem;

public class VoiceLineManager : MonoBehaviour
{

    public AudioClip[] killVoiceLines;
    public AudioClip[] reloadVoiceLines;

    private void Start()
    {

    }

    public void PlayKillVoiceLine()
    {
        if(Random.Range(0, 2) != 0)
        {
            return;
        }

        if (killVoiceLines.Length > 0)
        {
            int index = Random.Range(0, killVoiceLines.Length);
            AudioSource.PlayClipAtPoint(killVoiceLines[index], Camera.main.transform.position,100);
        }
    }

    public void playReloadVoiceLine()
    {
        if (Random.Range(0, 1) != 0)
        {
            return;
        }

        if (reloadVoiceLines.Length > 0)
        {
            int index = Random.Range(0, reloadVoiceLines.Length);
            AudioSource.PlayClipAtPoint(reloadVoiceLines[index], Camera.main.transform.position);
        }
    }
}
