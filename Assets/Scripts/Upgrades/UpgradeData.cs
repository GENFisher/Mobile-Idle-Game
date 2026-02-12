using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public UpgradeType type;
    public float baseValue;
    public int baseCost;

    [Tooltip("Multiplier per upgrade level")]
    public float effectGrowth = 1.1f;

    [Tooltip("Cost growth per upgrade level")]
    public float costGrowth = 1.15f;

    [TextArea]
    public string description;

    // Optional: icon for UI
    public Sprite icon;
}
