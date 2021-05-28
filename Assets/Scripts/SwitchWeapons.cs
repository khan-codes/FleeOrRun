using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapons : MonoBehaviour
{
    List<Transform> listOfWeapons = new List<Transform>();
    KeyCode switchKey = KeyCode.Q;
    bool canChange = true;
    int index = 0;
    float waitTime = 0.09f;
    WeaponZoom weaponZoom;
    bool inZoom;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            listOfWeapons.Add(child);
        }
        weaponZoom = FindObjectOfType<WeaponZoom>();
        inZoom = weaponZoom.GetInZoom();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.isGamePaused) return;
        //print("Number of weapons being carried: " + listOfWeapons.Count);
        inZoom = weaponZoom.GetInZoom();
        if (canChange && !inZoom)  StartCoroutine(Switch());
    }

    IEnumerator Switch()
    {
        canChange = false;
        if (Input.GetKey(switchKey))
        {
            listOfWeapons[index].gameObject.SetActive(false);
            index = (index + 1) % listOfWeapons.Count;
            listOfWeapons[index].gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(waitTime);
        canChange = true;
    }
}
