
[System.Serializable]
public class UpgradeState
{
    public UpgradeData data;
    public int level;

    public UpgradeState(UpgradeData data)
    {
        this.data = data;
        this.level = 0;
    }
}