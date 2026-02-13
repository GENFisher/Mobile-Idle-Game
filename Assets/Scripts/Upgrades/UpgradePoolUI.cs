using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UpgradePoolUI : MonoBehaviour
{
    [SerializeField] UpgradePool pool;
    [SerializeField] private GameObject upgradePrefab;
    private int currentMultiplier = 1;

    [Header("Upgrade multipliers")]
    [SerializeField] private Button multiplier1Button;
    [SerializeField] private Button multiplier2Button;
    [SerializeField] private Button multiplier3Button;
    [SerializeField] private int multiplier1;
    [SerializeField] private int multiplier2;
    [SerializeField] private int multiplier3;

    public event System.Action OnMultiplierChanged;

    private void Awake()
    {
        currentMultiplier = multiplier1; // default
        SetupMultiplierButtons();
        GenerateUI();
    }

    private void GenerateUI()
    {
        foreach (var upgrade in pool.upgrades)
        {
            var uiElement = Instantiate(upgradePrefab, this.transform).GetComponent<UpgradeItemUI>();
            uiElement.Initialize(upgrade);
        }
        RectTransform transform = this.GetComponent<RectTransform>();
        GridLayoutGroup grid = this.GetComponent<GridLayoutGroup>();
        transform.sizeDelta = new Vector2(transform.sizeDelta.x, grid.cellSize.y * pool.upgrades.Length + grid.spacing.y * (pool.upgrades.Length - 1));
    }

    private void SetupMultiplierButtons()
    {
        multiplier1Button.onClick.AddListener(() => SetMultiplier(multiplier1, multiplier1Button));
        multiplier2Button.onClick.AddListener(() => SetMultiplier(multiplier2, multiplier2Button));
        multiplier3Button.onClick.AddListener(() => SetMultiplier(multiplier3, multiplier3Button));

        UpdateButtonVisuals(multiplier1Button); // default selected
    }

    private void SetMultiplier(int multiplier, Button clickedButton)
    {
        currentMultiplier = multiplier;
        UpdateButtonVisuals(clickedButton);
        OnMultiplierChanged?.Invoke();
    }

    private void UpdateButtonVisuals(Button selected)
    {
        multiplier1Button.interactable = multiplier1Button != selected;
        multiplier2Button.interactable = multiplier2Button != selected;
        multiplier3Button.interactable = multiplier3Button != selected;
    }

    public int GetMultiplier() { return currentMultiplier; }
}
