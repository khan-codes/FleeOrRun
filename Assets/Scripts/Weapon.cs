using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] public float range = 40f;
    [SerializeField] int damageDone = 3;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] AmmoType ammoType;
    [SerializeField] GameObject hitVFX;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] RunAnimation runAnimation;
    bool canShoot = true;
    [SerializeField] float timeBetweenShots = 0.06f;
    public TextMeshProUGUI ammoText;
    [SerializeField] RaycastHit hit;


    // Start is called before the first frame update
    void Start()
    {
        // will probably remain empty
    }

    private void OnEnable()
    {
        canShoot = true;      // or can add this line at the end of Update function   // this line is here because if we switch our weapon in the delay interval, the canShoot varibale remains false
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canShoot && ammoSlot.GetAmmoAmount(ammoType) != 0)
        {
            StartCoroutine(Shoot());
        }

        ammoText.text = ammoSlot.GetAmmoAmount(ammoType).ToString();
    }

    IEnumerator Shoot()
    {
        canShoot = false;

        ProcessRaycast();
        ammoSlot.ReduceAmmo(ammoType);
        PlayMuzzuleFlash();
        runAnimation.ShootingAnimation(true);

        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
        runAnimation.ShootingAnimation(false);
    }


    void PlayMuzzuleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        //RaycastHit hit;
        bool gotHit = Physics.Raycast(origin: FPCamera.transform.position, direction: FPCamera.transform.forward, out hit, maxDistance: range);

        if (gotHit)
        {
            CreateHitImpact(hit);
            EnemyHealth targetHealth = hit.transform.GetComponent<EnemyHealth>();

            if (targetHealth)
            {
                hit.transform.GetComponent<EnemyHealth>().deathShot = hit;
            }
            
            if (targetHealth == null) return;
            targetHealth.decreaseHealth(damageDone);
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject tmpHitVFX = Instantiate(hitVFX, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(tmpHitVFX, 0.5f);
    }
}
