using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private int cash, deadEnemiesCounter, currentCapacity;
    [SerializeField]
    private HUDManager hudManager;
    [SerializeField]
    private PlayerController player;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        //DontDestroyOnLoad(gameObject);
    }

    public int GetCash()
    {
        return cash;
    }
    public int GetCurrentCapacity()
    {
        return currentCapacity;
    }
    public int GetDeadEnemiesAmount()
    {
        return deadEnemiesCounter;
    }

    public void ChangeCashValue(int value)
    {
        cash = value;
        hudManager.UpdateHUD();
    }

    public void AddCapacity(int value)
    {
        currentCapacity += value;
        player.SetNewColor();
        hudManager.UpdateHUD();
    }

    public void AddDeadEnemy()
    {
        deadEnemiesCounter++;
        hudManager.UpdateHUD();
    }

    public void ResetEnemiesCounter()
    {
        deadEnemiesCounter = 0;
        hudManager.UpdateHUD();
    }
    public void RestartScene()
    {
        SceneManager.LoadScene("Level 1");
    }
}
