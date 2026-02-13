using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }
    [SerializeField] private UpgradePool[] upgradePools;

    private Dictionary<UpgradeType, UpgradeState> upgrades = new Dictionary<UpgradeType, UpgradeState>();

    void Awake()
    {
        Instance = this;

        InitializeUpgrades();
    }

    private void InitializeUpgrades()
    {
        foreach (var pool in upgradePools)
        {
            foreach (var upgradeData in pool.upgrades)
            {
                if (!upgrades.ContainsKey(upgradeData.type))
                {
                    upgrades.Add(
                        upgradeData.type,
                        new UpgradeState(upgradeData)
                    );
                }
                else
                {
                    Debug.LogWarning(
                        $"Duplicate upgrade type found: {upgradeData.type}"
                    );
                }
            }
        }
    }

    public int GetLevel(UpgradeType type)
    {
        return upgrades[type].level;
    }

    public void Upgrade(UpgradeType type, int amount)
    {
        EconomyManager.Instance.SpendMoney(GetCost(type, amount));
        upgrades[type].level += amount;
        OnUpgradePurchased?.Invoke(type);
    }

    public event Action<UpgradeType> OnUpgradePurchased;

    // Calculated properties based on levels
    public float GetCurrentValue(UpgradeType type)
    {
        if (!upgrades.ContainsKey(type))
        {
            Debug.LogWarning($"Upgrade not found: {type}");
            return 0;
        }

        var state = upgrades[type];
        var data = state.data;

        return data.baseValue *
               Mathf.Pow(data.effectGrowth, state.level);
    }

    public int GetCost(UpgradeType type, int amount)
    {
        var state = upgrades[type];
        var data = state.data;

        float totalCost =
            data.baseCost *
            Mathf.Pow(data.costGrowth, state.level) *
            (Mathf.Pow(data.costGrowth, amount) - 1f) /
            (data.costGrowth - 1f);

        return Mathf.RoundToInt(totalCost);
    }

    public float GetUpgradePreview(UpgradeType type, int levels)
    {
        var state = upgrades[type];
        var data = state.data;

        return data.baseValue * Mathf.Pow(data.effectGrowth, state.level + levels);
    }
}

