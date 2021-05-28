using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] float timeForDead = 1.7f;
    [SerializeField] RunAnimation runAnimation;

    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleDeath()
    {
        //gameOverCanvas.enabled = true;

        //// this stops time. so the backgroung will remain stationary when the gameover UI pops up
        //Time.timeScale = 0;

        //// giving the player an option to "unlock" the cursor. Right now we have to press escape button, but something else will be better
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
        StartCoroutine(DeathProcess());
    }

    IEnumerator DeathProcess()
    {
        runAnimation.isDead = true;
        
        yield return new WaitForSeconds(timeForDead);

        Cursor.visible = true;
        Time.timeScale = 0;
        gameOverCanvas.enabled = true;
        // giving the player an option to "unlock" the cursor. Right now we have to press escape button, but something else will be better
        Cursor.lockState = CursorLockMode.None;
    }
}
