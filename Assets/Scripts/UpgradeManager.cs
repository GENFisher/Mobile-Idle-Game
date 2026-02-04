using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    public int spawnSpeedLevel = 1;
    public int beltSpeedLevel = 1;

    public float baseSpawnInterval = 5;
    public float baseBeltSpeed = 1;

    public float SpawnInterval =>
        baseSpawnInterval * Mathf.Pow(0.9f, spawnSpeedLevel);

    public float BeltSpeed =>
        baseBeltSpeed * Mathf.Pow(1.1f, beltSpeedLevel);



    void Awake()
    {
        Instance = this;
    }
}

