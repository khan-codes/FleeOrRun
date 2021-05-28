using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnBetterZombies : MonoBehaviour
{
    private GameObject[] zombieModels;
    [SerializeField] GameObject model;
    [SerializeField] GameObject zombieModel;
    int X_min, X_max, Z_min, Z_max;
    CapsuleCollider capsuleCollider;
    [SerializeField] Avatar controllerAvatar;
    [SerializeField] GameObject zombiePrefab;
    [SerializeField] int timeInterval = 3;
    [SerializeField] GameObject spriteRendererPrefab;
    [SerializeField] GameObject spriteRendererGO;
    Avatar empty;
    int last;
    int current;
    [SerializeField] int maxZombies = 10;
    [SerializeField] int lapNumber = 2;
    [SerializeField] public int zombieCount = 0;
    [SerializeField] bool lap = true;


    // Start is called before the first frame update
    void Start()
    {
        zombieModels = Resources.LoadAll("PolygonZombies/Prefabs/").Cast<GameObject>().ToArray();
        X_min = 5;
        X_max = -38;
        Z_min = -35;
        Z_max = 39;
        InstantiateRandomPosition();
    }

    private void Update()
    {
        if (PauseMenu.isGamePaused) return;

        if ((int)Time.time % timeInterval == 0)
        {
            current = (int)Time.time;
            if (current > last)
            {
                if (zombieCount < maxZombies)
                {
                    if (lap)
                    {
                        InstantiateRandomPosition();
                        zombieCount++;
                    }

                    if (zombieCount == lapNumber)
                    {
                        lap = true;
                    }
                }

                else
                {
                    lap = false;
                }
            }

            last = current;
        }
    }

    public void InstantiateRandomPosition()
    {
        int rand = Random.Range(0, zombieModels.Count());
        zombieModel = Instantiate(zombiePrefab, new Vector3(0f, 0f, 0f) , Quaternion.identity);

        model = Instantiate(zombieModels[rand], new Vector3(0f, /*-1f*/ 0f, 0f) /*this will later be a random (but smart) vector3 object*/ , Quaternion.identity);
        model.transform.SetParent(zombieModel.transform);

        int randX = Random.Range(X_min, X_max);
        int randZ = Random.Range(Z_min, Z_max);

        RaycastHit hit;

        Vector3 position = new Vector3(randX, 0, randZ);

        bool gotHit = Physics.Raycast(origin: position + new Vector3(0, 100.0f, 0), direction: Vector3.down, out hit, maxDistance: 200.0f);
        //Do a raycast along Vector3.down -> if you hit something the result will be given to you in the "hit" variable
        //This raycast will only find results between +-100 units of your original"position" (ofc you can adjust the numbers as you like)
        if (gotHit)
        {
            zombieModel.transform.position = hit.point;
            //zombieModel.transform.position = new Vector3(hit.point.x, hit.point.y - 0.56f, hit.point.z);
        }
        else
        {
            Debug.Log("there seems to be no ground at this position");
        }

        zombieModel.transform.SetParent(this.transform);

        spriteRendererGO = Instantiate(spriteRendererPrefab, zombieModel.transform.position, /*Quaternion.identity*/ Quaternion.Euler(90f, 0f, 0f));
        spriteRendererGO.transform.position = new Vector3(zombieModel.transform.position.x, zombieModel.transform.position.y + 3, zombieModel.transform.position.z);
        spriteRendererGO.transform.SetParent(zombieModel.transform);

        // some weird shit was happening to the animator's avator. The solution is to change from on to the other (2 changes). The curly braces are just show that is snippet of code is separate
        {
            zombieModel.GetComponent<Animator>().avatar = empty;
            SetAvatar();
        }

        //AddCapsuleCollider();
    }

    public void AddCapsuleCollider()
    {
        capsuleCollider = zombieModel.AddComponent<CapsuleCollider>();
        capsuleCollider.height = 2f;
        capsuleCollider.radius = 0.5f;
        capsuleCollider.center = new Vector3(0f, 1.2f, 0f);
        capsuleCollider.direction = 1;  // 0, 1, 2 represent X, Y, Z directions. We needed Y so went with 1.
    }

    public void SetAvatar()
    {
        Animator zombieAnim = zombieModel.GetComponent<Animator>();
        zombieAnim.avatar = controllerAvatar;
    }
}
