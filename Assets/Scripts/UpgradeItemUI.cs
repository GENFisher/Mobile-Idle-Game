using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItemUI : MonoBehaviour
{
    public UpgradeType upgradeType;

    [Header("Buttons")]
    [SerializeField] private Button button1;
    [SerializeField] private Button button10;
    [SerializeField] private Button button100;

    [Header("Cost Texts")]
    [SerializeField] private TMP_Text costOf1;
    [SerializeField] private TMP_Text costOf10;
    [SerializeField] private TMP_Text costOf100;

    [Header("Upgrade Impact")]
    [SerializeField] private TMP_Text impactOf1;
    [SerializeField] private TMP_Text impactOf10;
    [SerializeField] private TMP_Text impactOf100;

    void Update()
    {
        UpdateButtonState(button1, costOf1, impactOf1, 1);
        UpdateButtonState(button10, costOf10, impactOf10, 10);
        UpdateButtonState(button100, costOf100, impactOf100, 100);
    }

    void UpdateButtonState(Button button, TMP_Text costText, TMP_Text impactText, int amount)
    {
        int cost = UpgradeManager.Instance.GetUpgradeCost(upgradeType, amount);
        bool canAfford = EconomyManager.Instance.GetMoney() >= cost;

        costText.text = $"Cost: {cost}";
        impactText.text = $"{UpgradeManager.Instance.GetCurrentValue(upgradeType).ToString("F1")}->{UpgradeManager.Instance.GetUpgradePreview(upgradeType, amount).ToString("F1")}";
        button.interactable = canAfford;
    }
}
