using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    //[SerializeField] int ammoAmount = 50;
    [SerializeField] int incrementAmount = 20;

    [System.Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoAmount;
        public int maxAmmo;
    }

    [SerializeField] AmmoSlot[] ammoSlots;

    // Start is called before the first frame update
    void Start()
    {
        // will probably remain empty
        foreach(AmmoSlot slot in ammoSlots)
        {
            slot.ammoAmount = slot.maxAmmo;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetAmmoAmount(AmmoType ammoType)
    {
        AmmoSlot slot = GetAmmoSlot(ammoType);
        return slot.ammoAmount;
    }

    public void ReduceAmmo(AmmoType ammoType)
    {
        AmmoSlot slot = GetAmmoSlot(ammoType);
        if (slot.ammoAmount > 0)
        {
            slot.ammoAmount--;
        }

        else if (slot.ammoAmount <= 0)
        {
            Debug.Log("OUT OF AMMO!");
        }
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach(AmmoSlot slot in ammoSlots)
        {
            if (slot.ammoType == ammoType)
            {
                return slot;
            }
        }
        return null;
    }

    public void AmmoIncrement(AmmoType ammoType)
    {
        AmmoSlot slot = GetAmmoSlot(ammoType);

        if (ammoType == AmmoType.Bullets)
        {
            //slot.ammoAmount = slot.ammoAmount + incrementAmount;
            int newAmmoAmount = slot.ammoAmount + incrementAmount;

            if (newAmmoAmount < slot.maxAmmo)
            {
                slot.ammoAmount = newAmmoAmount % slot.maxAmmo;
            }

            else if (newAmmoAmount > slot.maxAmmo)
            {
                slot.ammoAmount = slot.maxAmmo % newAmmoAmount;
            }

            else
            {
                slot.ammoAmount = slot.maxAmmo;
            }

        }

        else
        {
            // was for shot, now no shotgun so didn't do the same thing as in last IF statement. If needed another weapon do as in IF
            slot.ammoAmount = slot.ammoAmount + (int)(incrementAmount * 0.4);
        }
    }
}
