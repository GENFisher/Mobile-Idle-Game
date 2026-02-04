using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }

    [Header("Upgrade Levels")]
    public int spawnIntervalLevel = 0;
    public int beltSpeedLevel = 0;
    public int mixedTrashPriceLevel = 0;
    public int recycleingYieldLevel = 0;
    public int recycleingPenaltyLevel = 0;

    [Header("Base Values")]
    public float baseSpawnInterval = 5;
    public float baseBeltSpeed = 1;
    public int baseMixedTrashPrice = 10;
    public int baseRecycleingYield = 10;
    public int baseRecycleingPenalty = 10;

    // Calculated properties based on levels
    public float SpawnInterval =>
        baseSpawnInterval / Mathf.Pow(1.1f, spawnIntervalLevel);

    public float BeltSpeed =>
        baseBeltSpeed * Mathf.Pow(1.1f, beltSpeedLevel);

    public int MixedTrashPrice =>
        Mathf.RoundToInt(baseMixedTrashPrice * Mathf.Pow(1.1f, mixedTrashPriceLevel));

    public int RecycleingYield => 
        Mathf.RoundToInt(baseRecycleingYield * Mathf.Pow(1.1f, recycleingYieldLevel));

    public int RecycleingPenalty =>
        (int)(baseRecycleingPenalty / Mathf.Pow(1.1f, recycleingPenaltyLevel));
}

