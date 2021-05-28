using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupType : MonoBehaviour
{
    public enum HealthType
    {
        HealthPotion
    };

    [SerializeField] public HealthType healthType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 40f * Time.deltaTime, 0f), Space.Self);
    }
}
