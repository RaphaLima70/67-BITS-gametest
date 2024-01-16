using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private BagController bag;
    [SerializeField]
    private int enemyValue, upgradeCost;

    public void BuyCapacity()
    {
        if (GameManager.Instance.GetCash() >= upgradeCost)
        {
            int currentCash = GameManager.Instance.GetCash();
            int result = Mathf.FloorToInt(currentCash / upgradeCost);
            GameManager.Instance.AddCapacity(result);
            GameManager.Instance.ChangeCashValue(currentCash - (result * upgradeCost));
        }
    }

    private void SellEnemies()
    {
        int currentCash = GameManager.Instance.GetCash();
        GameManager.Instance.ChangeCashValue(currentCash + bag.EnemyCounter() * enemyValue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (bag.EnemyCounter() > 0)
            {
                SellEnemies();
                bag.ClearBag();
            }
        }
    }
}
