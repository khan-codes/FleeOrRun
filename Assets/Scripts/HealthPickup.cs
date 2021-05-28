using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Health")
    //    {
    //        HealthPickupType pickupType = collision.gameObject.transform.GetComponent<HealthPickupType>();
    //        //ammoSlot.AmmoIncrement(pickupType.ammoType);
    //        playerHealth.IncreaseHealth();
    //        // spawn an FX
    //        Destroy(collision.gameObject);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Health")
        {
            //print("Trigger: " + other.gameObject.name);
            HealthPickupType pickupType = other.gameObject.transform.GetComponent<HealthPickupType>();
            playerHealth.IncreaseHealth();
            // spawn an FX
            Destroy(other.gameObject);
        }
    }
}
