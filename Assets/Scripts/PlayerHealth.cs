using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health = 100f;
    DeathHandler deathHandler;
    [SerializeField] float healthIncrement = 20f;
    [SerializeField] Sprite[] images;
    [SerializeField] GameObject imageGO;
    [SerializeField] float displayTime = 2f;
    [SerializeField] HealthBar healthBar;


    // Start is called before the first frame update
    void Start()
    {
        deathHandler = GetComponent<DeathHandler>();
        healthBar = FindObjectOfType<HealthBar>();
        imageGO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetSlider(health);
    }

    public void decrementHealth(float damage)
    {
        if (health > 0)
        {
            health -= damage;
            StartCoroutine(DisplaySplatter());
        }
        else
        {
            //print("Player Dead");
            deathHandler.HandleDeath();
        }
    }

    public void IncreaseHealth()
    {
        float newHealth = health + healthIncrement;
        
        if (newHealth < 100)
        {
            health = newHealth % 100;
        }

        else if (newHealth > 100)
        {
            health = 100 % newHealth;
        }

        else
        {
            health = 100f;
        }
    }

    IEnumerator DisplaySplatter()
    {
        imageGO.SetActive(true);
        RandomSpriteSelection();
        yield return new WaitForSeconds(displayTime);
        imageGO.SetActive(false);
    }

    public void RandomSpriteSelection()
    {
        int rand = Random.Range(0, images.Length);
        Sprite randSprite = images[rand];
        imageGO.GetComponent<UnityEngine.UI.Image>().sprite = randSprite;
    }
}
