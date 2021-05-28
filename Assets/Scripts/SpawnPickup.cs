using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickup : MonoBehaviour
{
    [SerializeField] KillCount killCount;
    [SerializeField] GameObject healthTypePickup;
    [SerializeField] GameObject ammoTypePickup;
    //[SerializeField] GameObject bomb;
    Vector3 spawnPosition;
    List<GameObject> pickups = new List<GameObject>();
    [SerializeField] bool canSpawn = false;
    int tempKillCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        killCount = FindObjectOfType<KillCount>();
        pickups.Add(healthTypePickup);
        pickups.Add(ammoTypePickup);
    }

    // Update is called once per frame
    void Update()
    {
        CheckForSpawn();
        Spawn();
    }

    public void Spawn()
    {
        if (killCount.GetKillAmount() % 3 == 0 && canSpawn)
        {
            // Spawn here
            tempKillCount = killCount.GetKillAmount();
            spawnPosition = killCount.GetKillPosition();
            int randNumber = Random.Range(0, 100);
            GameObject pick = Instantiate(pickups[randNumber % 2], new Vector3(spawnPosition.x, spawnPosition.y, spawnPosition.z), Quaternion.identity);
            pick.transform.SetParent(transform);
            canSpawn = false;
        }
    }

    private void CheckForSpawn()
    {
        if (tempKillCount == killCount.GetKillAmount())
        {
            canSpawn = false;
        }
        else
        {
            canSpawn = true;
        }
    }
}
