using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
	[SerializeField] MenuButtonController menuButtonController;
	[SerializeField] Animator animator;
	[SerializeField] AnimatorFunctions animatorFunctions;
	[SerializeField] int thisIndex;
    [SerializeField] int gameSceneIndex = 1;
    [SerializeField] int optionsSceneIndex = 2;
    [SerializeField] LevelLoader levelLoader;

    // Update is called once per frame
    void Update()
    {
		if(menuButtonController.index == thisIndex)
		{
			animator.SetBool ("selected", true);
			if(Input.GetAxis ("Submit") == 1)
            {
                if (thisIndex == 0) PlayButtonFunction();

                else if (thisIndex == 1) OptionsButtonFunction();

                else if (thisIndex == 2) QuitButtonFunction();

                animator.SetBool ("pressed", true);
			}
            else if (animator.GetBool ("pressed"))
            {
				animator.SetBool ("pressed", false);
				animatorFunctions.disableOnce = true;
			}
		}

        else
        {
			animator.SetBool ("selected", false);
		}
    }

    public void PlayButtonFunction()
    {
        levelLoader.LoadNextLevel();
        //SceneManager.LoadScene(gameSceneIndex);
    }

    public void OptionsButtonFunction()
    {
        SceneManager.LoadScene(optionsSceneIndex);
    }

    public void QuitButtonFunction()
    {
        Debug.Log("QUIT");
        Application.Quit(0);
    }
}
