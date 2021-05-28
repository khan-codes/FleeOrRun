using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    KeyCode throwKey = KeyCode.X;
    [SerializeField] bool canThrow = true;
    [SerializeField] private float timeBetweenShots = 4f;
    [SerializeField] RunAnimation runAnimation;
    [SerializeField] AmmoType ammoType;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] int damageDone = 10;   // for all the zombies in the blast radius
    [SerializeField] GameObject rifle;

    // Start is called before the first frame update
    void Start()
    {
        runAnimation = FindObjectOfType<RunAnimation>();
        
    }

    private void OnEnable()
    {
        canThrow = true;      // or can add this line at the end of Update function   // this line is here because if we switch our weapon in the delay interval, the canShoot varibale remains false
    }

    // Update is called once per frame
    void Update()
    {
        //print("Hello from grenade");
        ThrowGrenade();
    }

    private void ThrowGrenade()
    {
        if (Input.GetKey(throwKey) && canThrow)
        {
            rifle.SetActive(false);
            StartCoroutine(Throw());
        }
    }

    IEnumerator Throw()
    {
        canThrow = false;

        ammoSlot.ReduceAmmo(ammoType);
        runAnimation.SwitchCameraInThrow(true);
        runAnimation.ThrowAnimation(true);

        yield return new WaitForSeconds(timeBetweenShots);
        canThrow = true;
        rifle.SetActive(true);
        runAnimation.ThrowAnimation(false);
        runAnimation.SwitchCameraInThrow(false);
    }
}

// added another camera to soldier object, enable it when throwing meanwhile disable the main camera
