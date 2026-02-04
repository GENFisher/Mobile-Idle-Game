using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    private EconomyManager economyManager;
    private UpgradeManager upgradeManager;
    private UIManager uiManager;
    private void Start()
    {
        economyManager = EconomyManager.Instance;
        upgradeManager = UpgradeManager.Instance;
        uiManager = UIManager.Instance;

        //setup basic game state
        uiManager.UpdateMoney(economyManager.GetMoney());
        foreach (RecycableType type in System.Enum.GetValues(typeof(RecycableType)))
        {
            int amount = economyManager.GetTrashAmount(type);
            uiManager.UpdateAmount(type, amount);
        }
        //end of basic game state setup
    }
}
