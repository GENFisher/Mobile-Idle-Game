using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private EconomyManager economyManager;

    [SerializeField] private List<TrashUIEntry> entries;

    private Dictionary<RecycableType, TMP_Text> uiMap;

    [SerializeField] private TMP_Text moneyText;

    private void Awake()
    {
        Instance = this;
        economyManager = EconomyManager.Instance;
        uiMap = new Dictionary<RecycableType, TMP_Text>();

        foreach (var entry in entries)
        {
            uiMap[entry.type] = entry.amountText;
        }
    }

    public void UpdateAmount(RecycableType type, int amount)
    {
        if (uiMap.TryGetValue(type, out var text))
        {
            text.text = $"{type}: {amount}";
        }
    }

    public void UpdateMoney(int amount)
    {
        if (moneyText != null)
        {
            moneyText.text = $"Money: {amount}";
        }
    }

    public void TogglePanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }
    private void OnEnable()
    {
        EconomyManager.Instance.OnMoneyChanged += OnMoneyChanged;
        OnMoneyChanged(economyManager.GetMoney());
    }

    private void OnDisable()
    {
        EconomyManager.Instance.OnMoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int newAmount) 
    { 
        moneyText.text = $"Money: {newAmount}"; 
    }
}


    [System.Serializable]
public class TrashUIEntry
{
    public RecycableType type;
    public TMP_Text amountText;
}
