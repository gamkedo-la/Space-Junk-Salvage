using System;
using UnityEngine;

public class EnemySoundEffects : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip alertedSound;
    public AudioClip noLongerAlertedSound;
    public AudioClip seePlayerSound;
    public AudioClip noLongerSeePlayerSound;
    public AudioClip attackSound;
    public AudioClip noLongerAttackSound;

    private bool _wasAlerted;
    private bool _sawPlayer;
    private bool _wasAttacking;
    
    public void PlaySounds(EnemyState state)
    {
        if (state.Alerted != _wasAlerted)
        {
            OnAlertedChanged(state.Alerted);
        }

        if (state.SeePlayer != _sawPlayer)
        {
            OnSeePlayerChanged(state.SeePlayer);
        }

        if (state.Attacking != _wasAttacking)
        {
            OnAttackingChanged(state.Attacking);
        }

        _wasAlerted = state.Alerted;
        _sawPlayer = state.SeePlayer;
        _wasAttacking = state.Attacking;
    }

    public void OnAlertedChanged(bool isAlerted)
    {
        PlayOneShot(isAlerted ? alertedSound : noLongerAlertedSound);
    }

    public void OnSeePlayerChanged(bool seePlayer)
    {
        PlayOneShot(seePlayer ? seePlayerSound : noLongerSeePlayerSound);
    }

    public void OnAttackingChanged(bool attacking)
    {
        PlayOneShot(attacking ? attackSound : noLongerAttackSound);
    }

    private void PlayOneShot(AudioClip audioClip)
    {
        if (audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}