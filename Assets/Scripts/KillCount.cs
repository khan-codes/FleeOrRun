using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillCount : MonoBehaviour
{
    [SerializeField] int killAmount = 0;
    Vector3 killPosition;
    public TextMeshProUGUI killCountText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DisplayKillCount();
    }

    public void IncrementCount(Vector3 posi)
    {
        killAmount += 1;
        killPosition = posi;
    }

    public int GetKillAmount()
    {
        return killAmount;
    }

    public Vector3 GetKillPosition()
    {
        return killPosition;
    }

    public void DisplayKillCount()
    {
        killCountText.text = killAmount.ToString();
    }
}


