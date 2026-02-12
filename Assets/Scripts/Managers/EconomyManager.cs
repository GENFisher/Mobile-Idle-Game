using System;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager Instance { get; private set; }
    

    private int totalMoney = 0;

    private Dictionary<RecycableType, int> trashAmounts = new();

    public event Action<int> OnMoneyChanged;

    private void Awake()
    {
        Instance = this;

        foreach (RecycableType type in System.Enum.GetValues(typeof(RecycableType)))
        {
            trashAmounts[type] = 0;
        }
    }

    //Functions for handeling money
    public int GetMoney()
    { 
        return totalMoney;
    }
    
    public void AddMoney(int amount) 
    { 
        totalMoney += amount;
        OnMoneyChanged?.Invoke(totalMoney);
    }

    public void SpendMoney(int amount) 
    { 
        totalMoney -= amount;
        OnMoneyChanged?.Invoke(totalMoney);
    }


    //Functions for handeling trash amounts
    public int GetTrashAmount(RecycableType type)
    {
        return trashAmounts[type];
    }
    public void AddTrash(RecycableType type, int amount)
    {
        trashAmounts[type] += amount;
    }
    public void RemoveTrash(RecycableType type, int amount)
    {
        trashAmounts[type] -= amount;
        if (trashAmounts[type] < 0)
        {
            trashAmounts[type] = 0;
        }
    }
}
