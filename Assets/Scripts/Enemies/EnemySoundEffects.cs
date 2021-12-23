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

    public Animator stateProvider;

    private bool _wasAlerted;
    private bool _sawPlayer;
    private bool _wasAttacking;

    private void Update()
    {
        var isAlerted = stateProvider.GetBool("Alerted");
        var seesPlayer = stateProvider.GetBool("SeePlayer");
        var isAttacking = stateProvider.GetBool("Attacking");

        if (isAlerted && !_wasAlerted)
        {
            audioSource.PlayOneShot(alertedSound);
        }

        if (!isAlerted && _wasAlerted)
        {
            audioSource.PlayOneShot(noLongerAlertedSound);
        }

        if (seesPlayer && !_sawPlayer)
        {
            audioSource.PlayOneShot(seePlayerSound);
        }

        if (!seesPlayer && _sawPlayer)
        {
            audioSource.PlayOneShot(noLongerSeePlayerSound);
        }

        if (isAttacking && !_wasAttacking)
        {
            audioSource.PlayOneShot(attackSound);
        }

        _wasAlerted = isAlerted;
        _sawPlayer = seesPlayer;
        _wasAttacking = isAttacking;
    }
}