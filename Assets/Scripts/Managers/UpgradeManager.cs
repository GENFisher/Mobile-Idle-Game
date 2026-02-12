using System;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    [SerializeField] private float upgradeEffectGrowth = 1.1f;
    [SerializeField] private float upgradeCostGrowth = 1.15f;
    void Awake()
    {
        Instance = this;
    }

    private int spawnIntervalLevel = 0;
    private int beltSpeedLevel = 0;
    private int mixedTrashPriceLevel = 0;
    private int recyclingYieldLevel = 0;
    private int recyclingPenaltyLevel = 0;

    [Header("Base Values")]
    [SerializeField] private float baseSpawnInterval = 5;
    [SerializeField] private float baseBeltSpeed = 1;
    [SerializeField] private int baseMixedTrashPrice = 10;
    [SerializeField] private int baseRecycleingYield = 10;
    [SerializeField] private int baseRecycleingPenalty = 10;

    [Header("Upgrade Costs")]
    [SerializeField] private int baseSpawnIntervalUpgradeCost = 10;
    [SerializeField] private int baseBeltSpeedUpgradeCost = 10;
    [SerializeField] private int baseMixedTrashPriceUpgradeCost = 10;
    [SerializeField] private int baseRecyclingYieldUpgradeCost = 10;
    [SerializeField] private int baseRecyclingPenaltyUpgradeCost = 10;

    public event Action<UpgradeType> OnUpgradePurchased;

    // Calculated properties based on levels
    public float GetCurrentValue(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.SpawnInterval: return baseSpawnInterval / Mathf.Pow(upgradeEffectGrowth, spawnIntervalLevel);
            case UpgradeType.BeltSpeed: return baseBeltSpeed * Mathf.Pow(upgradeEffectGrowth, beltSpeedLevel);
            case UpgradeType.MixedTrashPrice: return Mathf.RoundToInt(baseMixedTrashPrice * Mathf.Pow(upgradeEffectGrowth, mixedTrashPriceLevel));
            case UpgradeType.RecyclingYield: return Mathf.RoundToInt(baseRecycleingYield * Mathf.Pow(upgradeEffectGrowth, recyclingYieldLevel));
            case UpgradeType.RecyclingPenalty: return (int)(baseRecycleingPenalty / Mathf.Pow(upgradeEffectGrowth, recyclingPenaltyLevel));
        }
        return 0;
    }

    private int GetLevel(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.SpawnInterval: return spawnIntervalLevel;
            case UpgradeType.BeltSpeed: return beltSpeedLevel;
            case UpgradeType.MixedTrashPrice: return mixedTrashPriceLevel;
            case UpgradeType.RecyclingYield: return recyclingYieldLevel;
            case UpgradeType.RecyclingPenalty: return recyclingPenaltyLevel;
        }

        return 0;
    }

    private int GetBaseCost(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.SpawnInterval: return baseSpawnIntervalUpgradeCost;
            case UpgradeType.BeltSpeed: return baseBeltSpeedUpgradeCost;
            case UpgradeType.MixedTrashPrice: return baseMixedTrashPriceUpgradeCost;
            case UpgradeType.RecyclingYield: return baseRecyclingYieldUpgradeCost;
            case UpgradeType.RecyclingPenalty: return baseRecyclingPenaltyUpgradeCost;
        }

        return 0;
    }

    public int GetUpgradeCost(UpgradeType type, int amount)
    {
        int currentLevel = GetLevel(type);
        int baseCost = GetBaseCost(type);
        float growth = upgradeCostGrowth;

        float totalCost =
            baseCost *
            Mathf.Pow(growth, currentLevel) *
            (Mathf.Pow(growth, amount) - 1f) /
            (growth - 1f);

        return Mathf.RoundToInt(totalCost);
    }

    public float GetUpgradePreview(UpgradeType type, int levels)
    {
        int currentLevel = GetLevel(type);

        switch (type)
        {
            case UpgradeType.SpawnInterval:
                return baseSpawnInterval / Mathf.Pow(1.1f, currentLevel + levels);

            case UpgradeType.BeltSpeed:
                return baseBeltSpeed * Mathf.Pow(1.1f, currentLevel + levels);

            case UpgradeType.MixedTrashPrice:
                return Mathf.RoundToInt(baseMixedTrashPrice * Mathf.Pow(1.1f, currentLevel + levels));

            case UpgradeType.RecyclingYield:
                return Mathf.RoundToInt(baseRecycleingYield * Mathf.Pow(1.1f, currentLevel + levels));

            case UpgradeType.RecyclingPenalty:
                return (int)(baseRecycleingPenalty / Mathf.Pow(1.1f, currentLevel + levels));
        }

        return 0;
    }


    // Upgrade on button press
    public void Upgrade(UpgradeType type, int upgradeLevels)
    {
        switch (type)
        {
            case UpgradeType.SpawnInterval:
                EconomyManager.Instance.SpendMoney(GetUpgradeCost(type, upgradeLevels));
                spawnIntervalLevel += upgradeLevels;
                break;

            case UpgradeType.BeltSpeed:
                EconomyManager.Instance.SpendMoney(GetUpgradeCost(type, upgradeLevels));
                beltSpeedLevel += upgradeLevels;
                break;

            case UpgradeType.MixedTrashPrice:
                EconomyManager.Instance.SpendMoney(GetUpgradeCost(type, upgradeLevels));
                mixedTrashPriceLevel += upgradeLevels;
                break;

            case UpgradeType.RecyclingYield:
                EconomyManager.Instance.SpendMoney(GetUpgradeCost(type, upgradeLevels));
                recyclingYieldLevel += upgradeLevels;
                break;

            case UpgradeType.RecyclingPenalty:
                EconomyManager.Instance.SpendMoney(GetUpgradeCost(type, upgradeLevels));
                recyclingPenaltyLevel += upgradeLevels;
                break;
        }
        OnUpgradePurchased?.Invoke(type);
    }

    // Convenience methods for UI buttons because it sometimes dont appear in the inspector when using enum parameters
    public void UpgradeSpawnInterval(int upgradeLevels)
    {
        Upgrade(UpgradeType.SpawnInterval, upgradeLevels);
    }

    public void UpgradeBeltSpeed(int upgradeLevels)
    {
        Upgrade(UpgradeType.BeltSpeed, upgradeLevels);
    }

    public void UpgradeMixedTrashPrice(int upgradeLevels)
    {
        Upgrade(UpgradeType.MixedTrashPrice, upgradeLevels);
    }

    public void UpgradeRecyclingYield(int upgradeLevels)
    {
        Upgrade(UpgradeType.RecyclingYield, upgradeLevels);
    }

    public void UpgradeRecyclingPenalty(int upgradeLevels)
    {
        Upgrade(UpgradeType.RecyclingPenalty, upgradeLevels);
    }
}

