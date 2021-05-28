using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class RunAnimation : MonoBehaviour
{
    Animator player_anim;
    [SerializeField] RigidbodyFirstPersonController fps;
    [SerializeField] public bool isDead = false;
    [SerializeField] GameObject newCamera;
    [SerializeField] GameObject mainCamera;
    [SerializeField] AudioClip walking;
    [SerializeField] AudioClip running;
    [SerializeField] AudioClip shooting;
    [SerializeField] AudioClip death;
    [SerializeField] AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        player_anim = GetComponent<Animator>();
        audioSource.clip = walking;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.isGamePaused) return;

        RunningAnimation();
        WalkingAnimation();
        DeadAnimation();
        //ThrowAnimation();
    }

    private void RunningAnimation()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            player_anim.SetBool("Run", false);
            return;
        }

        else if (fps.Running)
        {
            player_anim.SetBool("Run", true);
        }
        else
        {
            player_anim.SetBool("Run", false);
        }
    }

    private void WalkingAnimation()
    {
        if (fps.Walking)
        {
            player_anim.SetBool("Walk", true);
        }
        else
        {
            player_anim.SetBool("Walk", false);
            player_anim.SetTrigger("Idle");
        }
    }

    public void PlayStepAudio()
    {
        audioSource.PlayOneShot(walking);
    }

    public void PlayRunAudio()
    {
        audioSource.PlayOneShot(running);
    }

    public void PlayDeathAudio()
    {
        audioSource.PlayOneShot(death);
    }

    //public void Shoot()
    //{
    //    //player_anim.SetTrigger("m_weapon_shoot");
    //}

    public void ShootingAnimation(bool val)
    {
        player_anim.SetBool("Shoot", val);
        PlayShootingAudio();

        if (!val)
        {
            player_anim.SetTrigger("Idle");
        }
    }

    private void PlayShootingAudio()
    {
        if (PauseMenu.isGamePaused) return;
        else audioSource.PlayOneShot(shooting);
    }

    private void DeadAnimation()
    {
        if (isDead)
        {
            player_anim.SetTrigger("Dead");
            enabled = false;
        }
    }

    public void ThrowAnimation(bool val)
    {
        player_anim.SetBool("Throw", val);
    }

    public void SwitchCameraInThrow(bool val)
    {
        // turn back camera off / or turn the look back script off
        //mainCamera.SetActive(!val);
        mainCamera.GetComponent<Camera>().enabled = !val;
        newCamera.SetActive(val);
        //newCamera.GetComponent<Camera>().enabled = val;
    }
}
