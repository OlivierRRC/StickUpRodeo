using UnityEngine;
using UnityEngine.InputSystem;

public class VoiceLineManager : MonoBehaviour
{
    public AudioSource voice;

    public AudioClip[] killVoiceLines;
    public AudioClip[] reloadVoiceLines;
    public AudioClip[] damageVoiceLines;


    


    private void Start()
    {
      voice = GetComponent<AudioSource>();
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
            // AudioSource.PlayClipAtPoint(killVoiceLines[index], Camera.main.transform.position);

            voice.clip = killVoiceLines[index];
            voice.Play();
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
            // AudioSource.PlayClipAtPoint(reloadVoiceLines[index], Camera.main.transform.position);

            voice.clip = reloadVoiceLines[index];
            voice.Play();
        }
    }

    public void playDamageVoiceLine()
    {
        if (Random.Range(0, 1) != 0)
        {
            return;
        }

        if (damageVoiceLines.Length > 0)
        {
            int index = Random.Range(0, damageVoiceLines.Length);
            // AudioSource.PlayClipAtPoint(damageVoiceLines[index], Camera.main.transform.position);

            voice.clip = damageVoiceLines[index];
            voice.Play();
        }
    }
}
