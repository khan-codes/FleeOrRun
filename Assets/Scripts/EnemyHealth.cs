using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health = 10;
    [SerializeField] public bool isDead = false;
    [SerializeField] KillCount killCount;
    public EnemyAI enemyAI;
    public SpriteRenderer spriteRenderer;
    [SerializeField] GameObject model;
    public List<GameObject> actors = new List<GameObject>();
    string searchTag = "ZombieModelMesh";
    public RaycastHit deathShot;
    [SerializeField] GameObject bloodSplatter;
    [SerializeField] float waitBeforeDestroying = 2f;
    [SerializeField] SpawnBetterZombies spawnBetterZombies;

    // Start is called before the first frame update
    void Start()
    {
        enemyAI = FindObjectOfType<EnemyAI>();
        killCount = FindObjectOfType<KillCount>();
        spawnBetterZombies = FindObjectOfType<SpawnBetterZombies>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        //model = GameObject.fin
        if (searchTag != null)
        {
            FindObjectwithTag(searchTag);
        }

        foreach(GameObject go in actors)
        {
            model = go;
            //print(go.name + "   " + go.tag);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
            
    }

    public void FindObjectwithTag(string _tag)
    {
        actors.Clear();
        Transform parent = transform;
        GetChildObject(parent, _tag);
    }

    public void GetChildObject(Transform parent, string _tag)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == _tag)
            {
                actors.Add(child.gameObject);
            }
            if (child.childCount > 0)
            {
                GetChildObject(child, _tag);
            }
        }
    }

    public void decreaseHealth(int damage)
    {
        //BroadcastMessage("OnDamageTaken");
        enemyAI.OnDamageTaken();
	
	    if (health > 0)
        {
            health -= damage;
            //print("health decremented, now is: " + health);
        }

        else if (health <= 0)
        {
            Die();
            //dying visual FX
            //Destroy(gameObject);
        }
    }

    private void Die()
    {
        if (isDead) return;
        Animator dead = GetComponent<Animator>();
        GetComponent<CapsuleCollider>().enabled = false;
        dead.SetTrigger("Dead");
        isDead = true;
        spriteRenderer.enabled = false;
        killCount.IncrementCount(transform.position);
        SpawnBlood();
        spawnBetterZombies.zombieCount -= 1;
        StartCoroutine(DestroyingObject());
    }

    IEnumerator DestroyingObject()
    {
        yield return new WaitForSeconds(waitBeforeDestroying);

        // destroy game object
        enabled = false;
        Destroy(gameObject);
    }

    void SpawnBlood()
    {
        GameObject hips = model.transform.FindChild("Root").gameObject.transform.FindChild("Hips").gameObject;
        GameObject blood = Instantiate(original: bloodSplatter, position: deathShot.point, rotation: Quaternion.identity, parent: hips.transform);
        //print("parent: " + blood.transform.parent.name);
        // maybe a coroutine..?
        //Destroy(blood);
    }

    public bool IsDead()
    {
        return isDead;
    }
}
