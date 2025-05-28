using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class VoiceLineManager : MonoBehaviour
{
    public AudioSource voice;

    public AudioClip[] killVoiceLines;
    private AudioClip[] UsableKillVoiceLines;
    public AudioClip[] reloadVoiceLines;
    public AudioClip[] damageVoiceLines;

    private void Start()
    {

    }

    public void playKillVoiceLine(AudioClip[] enemyKillLines)
    {
        UsableKillVoiceLines = killVoiceLines.Concat<AudioClip>(enemyKillLines).ToArray();
        if (!voice.isPlaying)
        {
            if (UnityEngine.Random.Range(0, 1) != 0)
            {
                return;
            }
            if (killVoiceLines.Length > 0)
            {
                int index = UnityEngine.Random.Range(0, UsableKillVoiceLines.Length);
                // AudioSource.PlayClipAtPoint(killVoiceLines[index], Camera.main.transform.position);

                voice.clip = UsableKillVoiceLines[index];
                voice.Play();
            }
        }
    }

    public void playReloadVoiceLine()
    {
        if (!voice.isPlaying)
        {
            if (UnityEngine.Random.Range(0, 2) != 0)
            {
                return;
            }

            if (reloadVoiceLines.Length > 0)
            {
                int index = UnityEngine.Random.Range(0, reloadVoiceLines.Length);
                // AudioSource.PlayClipAtPoint(reloadVoiceLines[index], Camera.main.transform.position);

                voice.clip = reloadVoiceLines[index];
                voice.Play();
            }
        }
    }

    public void playDamageVoiceLine()
    {
        if (!voice.isPlaying)
        {
            if (UnityEngine.Random.Range(0, 1) != 0)
            {
                return;
            }

            if (damageVoiceLines.Length > 0)
            {
                int index = UnityEngine.Random.Range(0, damageVoiceLines.Length);
                // AudioSource.PlayClipAtPoint(damageVoiceLines[index], Camera.main.transform.position);

                voice.clip = damageVoiceLines[index];
                voice.Play();
            }
        }
    }

}
