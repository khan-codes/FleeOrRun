using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] Ammo ammoSlot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Ammo")
    //    {
    //        AmmoPickupType pickupType = collision.gameObject.transform.GetComponent<AmmoPickupType>();
    //        ammoSlot.AmmoIncrement(pickupType.ammoType);
    //        Destroy(collision.gameObject);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ammo")
        {
            //print("Trigger: " + other.gameObject.name);
            AmmoPickupType pickupType = other.gameObject.transform.GetComponent<AmmoPickupType>();
            ammoSlot.AmmoIncrement(pickupType.ammoType);
            Destroy(other.gameObject);
        }
    }
}
