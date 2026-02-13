using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UpgradeItemUI : MonoBehaviour
{
    private UpgradeManager upgradeManager;
    private EconomyManager economyManager;
    private UpgradePoolUI upgradePoolUI;

    [Header("Upgrade info")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    public UpgradeType upgradeType;

    [Header("Upgrade button")]
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TMP_Text costOfUpgradeText;
    [SerializeField] private TMP_Text impactOfUpgradeText;

    private void Awake()
    {
        upgradeManager = UpgradeManager.Instance;
        economyManager = EconomyManager.Instance;
        upgradePoolUI = GetComponentInParent<UpgradePoolUI>();
    }

    void UpdateButtonState()
    {
        int multiplier = upgradePoolUI.GetMultiplier();
        int cost = upgradeManager.GetCost(upgradeType, multiplier);
        bool canAfford = economyManager.GetMoney() >= cost;

        costOfUpgradeText.text = $"Cost: {cost}";
        impactOfUpgradeText.text = $"{upgradeManager.GetCurrentValue(upgradeType).ToString("F2")}->{upgradeManager.GetUpgradePreview(upgradeType, multiplier).ToString("F2")}";
        upgradeButton.interactable = canAfford;
    }

    public void UpgradeOnButtonClick()
    {
        upgradeManager.Upgrade(upgradeType, upgradePoolUI.GetMultiplier());
    }

    public void Initialize(UpgradeData data)
    {
        nameText.text = data.upgradeName;
        descriptionText.text = data.description;
        upgradeType = data.type;
        Refresh();
    }

    private void OnEnable()
    {
        economyManager.OnMoneyChanged += OnMoneyChanged;
        upgradeManager.OnUpgradePurchased += OnUpgradePurchased;
        upgradePoolUI.OnMultiplierChanged += HandleMultiplierChanged;
        Refresh();
    }

    private void OnDisable()
    {
        economyManager.OnMoneyChanged -= OnMoneyChanged;
        upgradeManager.OnUpgradePurchased -= OnUpgradePurchased;
        upgradePoolUI.OnMultiplierChanged -= HandleMultiplierChanged;
    }


    private void Refresh()
    {
        UpdateButtonState();
    }

    private void OnMoneyChanged(int money)
    { 
        Refresh();
    }

    private void OnUpgradePurchased(UpgradeType type)
    {
        if (type == upgradeType)
            Refresh();
    }

    private void HandleMultiplierChanged()
    {
        Refresh();
    }
}
