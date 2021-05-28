using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BetterZombie : MonoBehaviour
{
    private GameObject[] zombieModels;
    [SerializeField] GameObject model;
    int X_min, X_max, Z_min, Z_max;
    CapsuleCollider capsuleCollider;
    [SerializeField] GameObject ParentZombiePrefab;
    [SerializeField] Avatar controllerAvatar;
    Avatar empty;

    // Start is called before the first frame update
    void Start()
    {
        zombieModels = Resources.LoadAll("PolygonZombies/Prefabs/").Cast<GameObject>().ToArray();
        X_min = 5;
        X_max = -38;
        Z_min = -35;
        Z_max = 39;
        InstantiateRandomPosition();
        this.transform.SetParent(ParentZombiePrefab.transform);
        //AddCapsuleCollider();
        // some weird shit was happening to the animator's avator. The solution is to change from on to the other (2 changes). The curly braces are just show that is snippet of code is separate
        {
            GetComponent<Animator>().avatar = empty;
            SetAvatar();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void InstantiateRandomPosition()
    {
        int rand = Random.Range(0, zombieModels.Count<GameObject>());
        model = Instantiate(zombieModels[rand], new Vector3(0f, 0f, 0f) /*this will later be a random (but smart) vector3 object*/ , Quaternion.Euler(0, 90f, 0));
        model.transform.SetParent(transform);

        int randX = Random.Range(X_min, X_max);
        int randZ = Random.Range(Z_min, Z_max);

        RaycastHit hit;

        Vector3 position = new Vector3(randX, 0, randZ);

        bool gotHit = Physics.Raycast(origin: position + new Vector3(0, 100.0f, 0), direction: Vector3.down, out hit, maxDistance: 200.0f);
        //Do a raycast along Vector3.down -> if you hit something the result will be given to you in the "hit" variable
        //This raycast will only find results between +-100 units of your original"position" (ofc you can adjust the numbers as you like)
        if (gotHit)
        {
            transform.position = hit.point;
        }
        else
        {
            Debug.Log("there seems to be no ground at this position");
        }
    }

    public void AddCapsuleCollider()
    {
        capsuleCollider =  gameObject.AddComponent<CapsuleCollider>();
        capsuleCollider.height = 2f;
        capsuleCollider.radius = 0.5f;
        capsuleCollider.center = new Vector3(0f, 1.2f, 0f);
        capsuleCollider.direction = 1;  // 0, 1, 2 represent X, Y, Z directions. We needed Y so went with 1.
    }
    public void SetAvatar()
    {
        Animator zombieAnim = GetComponent<Animator>();
        zombieAnim.avatar = controllerAvatar;
    }
}
