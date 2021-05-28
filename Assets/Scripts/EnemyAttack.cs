using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerHealth target;      // TODO: for the time being we will specify the target in the inspector, but later on we will automate it           (COMPLETED!)
    [SerializeField] float damage = 5f;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    public void OnDamageTaken()
    {
	    //Debug.Log("I know that we're being attacked!");
    }

    public void AttackHitEvent()
    {
        if (PauseMenu.isGamePaused) return;

        if (target == null)
        {
            return;
        }

        else
        {
            target.decrementHealth(damage);
        }
    }

    // look for fantasy animals assets pack in unity store
}
