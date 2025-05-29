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

    // All of these "playVoiceLine" functions work basically the same, so I'll walk you through the first one.
    public void playKillVoiceLine(AudioClip[] enemyKillLines)
    {
        // Different Enemies have kill voice lines specific to their type
        // to make use of those, this function takes an array of lines from the enemy who initiated it.

        // We then take those lines and the general use kill lines and put them both into one array.
        UsableKillVoiceLines = killVoiceLines.Concat<AudioClip>(enemyKillLines).ToArray();
        // If the Player is speaking, return the function early.
        if (voice.isPlaying) { return; }
        else
        {
            // Otherwise, give the line a 50% chance to play or return the function early
            if (UnityEngine.Random.Range(0, 1) != 0) { return; }
            // Pick a random voice line, assign it to the player's voice and play it.
            if (killVoiceLines.Length > 0)
            {
                int index = UnityEngine.Random.Range(0, UsableKillVoiceLines.Length);
                voice.clip = UsableKillVoiceLines[index];
                voice.Play();
            }
        }
    }

    public void playReloadVoiceLine()
    {
        if (voice.isPlaying) { return; }
        else
        {
            // This one is a 25% because it plays a lot more often
            if (UnityEngine.Random.Range(0, 3) == 0) { return; }

            if (reloadVoiceLines.Length > 0)
            {
                int index = UnityEngine.Random.Range(0, reloadVoiceLines.Length);
                voice.clip = reloadVoiceLines[index];
                voice.Play();
            }
        }
    }

    public void playDamageVoiceLine()
    {
        if (voice.isPlaying) { return; }
        else
        {
            if (UnityEngine.Random.Range(0, 1) != 0) { return; }

            if (damageVoiceLines.Length > 0)
            {
                int index = UnityEngine.Random.Range(0, damageVoiceLines.Length);
                voice.clip = damageVoiceLines[index];
                voice.Play();
            }
        }
    }

}
