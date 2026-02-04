using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private List<TrashUIEntry> entries;

    private Dictionary<RecycableType, TMP_Text> uiMap;

    [SerializeField]private TMP_Text moneyText;

    private void Awake()
    {
        Instance = this;
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
}


[System.Serializable]
public class TrashUIEntry
{
    public RecycableType type;
    public TMP_Text amountText;
}
