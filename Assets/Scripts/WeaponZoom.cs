using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    Camera mainCamera;
    const float defaulfZoom = 60f;
    const float zoomInAmount = 30f;
    const float zoomedSensitivity = 0.5f;
    const float defaultSensitivity = 2.0f;
    const float zoomRange = 100f;
    KeyCode zoomKey = KeyCode.C;
    RigidbodyFirstPersonController fps;
    bool keyDown = false;
    bool inZoom = false;
    float defaultNearClipping = 0.1f;
    float runNearClipping = 0.3f;
    float defaultRange;
    [SerializeField] Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponentInParent<Camera>();
        fps = GetComponentInParent<RigidbodyFirstPersonController>();
        weapon = GetComponent<Weapon>();
        defaultRange = weapon.range;
        mainCamera.fieldOfView = defaulfZoom;
        //mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, 0.55f);
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.isGamePaused) return;

        if (Input.GetKeyDown(zoomKey))
        {
            mainCamera.fieldOfView = zoomInAmount;
            fps.mouseLook.XSensitivity = zoomedSensitivity;
            fps.mouseLook.YSensitivity = zoomedSensitivity;
            weapon.range = zoomRange;
            //print("c down");
            keyDown = true;
            inZoom = true;
        }

        else if (Input.GetKeyUp(zoomKey) && keyDown)
        {
            mainCamera.fieldOfView = defaulfZoom;
            fps.mouseLook.XSensitivity = defaultSensitivity;
            fps.mouseLook.YSensitivity = defaultSensitivity;
            weapon.range = defaultRange;
            //print("c up");
            inZoom = false;
            keyDown = false;
        }

        if (fps.Running)
        {
            //mainCamera.nearClipPlane = runNearClipping;
        }
        else
        {
            mainCamera.nearClipPlane = defaultNearClipping;
        }
    }

    public bool GetInZoom()
    {
        return inZoom;
    }
}
