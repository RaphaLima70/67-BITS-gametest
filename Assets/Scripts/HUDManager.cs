using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI cashTxt, capacityTxt;
    [SerializeField]
    private BagController bag;
    private void Start()
    {
        UpdateHUD();
    }

    public void UpdateHUD()
    {
        cashTxt.text = "CASH: " + GameManager.Instance.GetCash();
        capacityTxt.text = "CAPACITY: " + GameManager.Instance.GetDeadEnemiesAmount() + "/" + GameManager.Instance.GetCurrentCapacity();
    }
}
