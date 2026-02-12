using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItemUI : MonoBehaviour
{
    private UpgradeManager upgradeManager;
    private EconomyManager economyManager;
    private UIManager uiManager;

    [Header("Upgrade info")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private string title;
    [SerializeField] private string description;
    public UpgradeType upgradeType;

    [Header("Upgrade button")]
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TMP_Text buyAmount;
    [SerializeField] private TMP_Text costOfUpgrade;
    [SerializeField] private TMP_Text impactOfUpgrade;

    private void Awake()
    {
        upgradeManager = UpgradeManager.Instance;
        economyManager = EconomyManager.Instance;
        uiManager = UIManager.Instance;
        Initialize();
    }

    void UpdateButtonState(int buyAmount)
    {
        int cost = upgradeManager.GetUpgradeCost(upgradeType, buyAmount);
        bool canAfford = economyManager.GetMoney() >= cost;

        this.buyAmount.text = $"Buy {buyAmount}";
        costOfUpgrade.text = $"Cost: {cost}";
        impactOfUpgrade.text = $"{upgradeManager.GetCurrentValue(upgradeType).ToString("F2")}->{upgradeManager.GetUpgradePreview(upgradeType, buyAmount).ToString("F2")}";
        upgradeButton.interactable = canAfford;
    }

    public void UpgradeOnButtonClick()
    {
        upgradeManager.Upgrade(upgradeType, uiManager.buyAmount);
    }

    private void Initialize()
    {
        nameText.text = title;
        descriptionText.text = description;
    }

    private void OnEnable()
    {
        economyManager.OnMoneyChanged += OnMoneyChanged;
        uiManager.OnBuyAmountChanged += OnBuyAmountChanged;
        upgradeManager.OnUpgradePurchased += OnUpgradePurchased;
        Refresh();
    }

    private void OnDisable()
    {
        economyManager.OnMoneyChanged -= OnMoneyChanged;
        uiManager.OnBuyAmountChanged -= OnBuyAmountChanged;
        upgradeManager.OnUpgradePurchased -= OnUpgradePurchased;
    }

    private void Refresh()
    {
        UpdateButtonState(uiManager.buyAmount);
    }

    private void OnMoneyChanged(int money)
    { 
        Refresh(); 
    }

    private void OnBuyAmountChanged(int buyAmount)
    {
        UpdateButtonState(buyAmount);
    }

    private void OnUpgradePurchased(UpgradeType type)
    {
        if (type == upgradeType)
            Refresh();
    }
}
