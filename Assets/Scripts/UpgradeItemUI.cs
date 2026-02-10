using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItemUI : MonoBehaviour
{
    [Header("Upgrade info")]
    private TMP_Text nameText;
    private TMP_Text descriptionText;
    [SerializeField] private string name;
    [SerializeField] private string description;
    public UpgradeType upgradeType;

    [Header("Upgrade button")]
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TMP_Text buyAmount;
    [SerializeField] private TMP_Text costOfUpgrade;
    [SerializeField] private TMP_Text impactOfUpgrade;

    private void Awake()
    {
        nameText = transform.Find("Title").GetComponent<TMP_Text>();
        descriptionText = transform.Find("Description").GetComponent<TMP_Text>();
        Initialize();
    }
    void Update()
    {
        UpdateButtonState(UIManager.Instance.buyAmount);
    }

    void UpdateButtonState(int amount)
    {
        int cost = UpgradeManager.Instance.GetUpgradeCost(upgradeType, amount);
        bool canAfford = EconomyManager.Instance.GetMoney() >= cost;

        buyAmount.text = $"Buy {amount}";
        costOfUpgrade.text = $"Cost: {cost}";
        impactOfUpgrade.text = $"{UpgradeManager.Instance.GetCurrentValue(upgradeType).ToString("F2")}->{UpgradeManager.Instance.GetUpgradePreview(upgradeType, amount).ToString("F1")}";
        upgradeButton.interactable = canAfford;
    }

    public void UpgradeOnButtonClick()
    {
        UpgradeManager.Instance.Upgrade(upgradeType, UIManager.Instance.buyAmount);
    }

    private void Initialize()
    {
            nameText.text = name;
            descriptionText.text = description;
    }
}
