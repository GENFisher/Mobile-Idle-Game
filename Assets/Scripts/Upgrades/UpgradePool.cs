using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgradePool", menuName = "Upgrades/UpgradePool")]
public class UpgradePool : ScriptableObject
{
    public string poolName;
    public UpgradePoolType poolType;
    public UpgradeData[] upgrades;
}

