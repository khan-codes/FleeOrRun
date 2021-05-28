using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookBack : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject backCamera;
    [SerializeField] GameObject rectileUI;
    KeyCode cameraSwitchKey = KeyCode.B;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.isGamePaused) return;

        SwitchCameras();
    }

    public void SwitchCameras()
    {
        if (Input.GetKeyDown(cameraSwitchKey))
        {
            //mainCamera.enabled = false;
            mainCamera.GetComponent<Camera>().enabled = false;
            rectileUI.SetActive(false);

            //backCamera.active = true;
            backCamera.SetActive(true);
        }

        else if (Input.GetKeyUp(cameraSwitchKey))
        {
            //mainCamera.enabled = true;
            mainCamera.GetComponent<Camera>().enabled = true;
            rectileUI.SetActive(true);

            //backCamera.active = false;
            backCamera.SetActive(false);
        }
    }
}
