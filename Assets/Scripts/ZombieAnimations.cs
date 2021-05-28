using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimations : MonoBehaviour
{
    [SerializeField] AudioClip idleAudio;
    [SerializeField] AudioClip runAudio;
    [SerializeField] AudioClip attackAudio;
    [SerializeField] AudioClip deathAudio;
    [SerializeField] AudioSource audioSource;
    [SerializeField] EnemyHealth enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.isDead)
        {
            enabled = false;
            return;
        }
    }

    // Animation event methods for audio
    // all audio shall have 3D effect, to precieve distance better

    public void ZombieIdleAudio()
    {
        if (PauseMenu.isGamePaused)
        {
            audioSource.volume = 0.1f;
            return;
        }

        audioSource.PlayOneShot(idleAudio);
    }

    public void ZombieRunAudio()
    {
        if (PauseMenu.isGamePaused)
        {
            audioSource.volume = 0.1f;
            return;
        }
        audioSource.PlayOneShot(runAudio);
    }

    public void ZombieAttackAudio()
    {
        if (PauseMenu.isGamePaused)
        {
            audioSource.volume = 0.1f;
            return;
        }
        audioSource.PlayOneShot(attackAudio);
    }

    public void ZombieDeathAudio()
    {
        if (PauseMenu.isGamePaused)
        {
            audioSource.volume = 0.1f;
            return;
        }
        audioSource.PlayOneShot(deathAudio);
    }

}
