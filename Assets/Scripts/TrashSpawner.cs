using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class TrashSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private TrashPool trashPool;
    [SerializeField] private int maxTrash = 20;
    private float spawnInterval;
    public GameObject selectedObject;
    private float maxSpawnX = -0.8f;
    private float minSpawnX = 0.8f;

    public static int allMoney;
    private int currentTrashCount;

    private void Awake()
    {
        spawnInterval = UpgradeManager.Instance.SpawnInterval;
    }
    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private void Update()
    {
        //Experimental: spawn interval changing
        spawnInterval = UpgradeManager.Instance.SpawnInterval;
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (currentTrashCount < maxTrash)
                SpawnTrash();

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnTrash()
    {
        TrashType trashType = trashPool.GetRandomTrash();
        Vector3 spawnPoint = new Vector3(
            Random.Range(minSpawnX, maxSpawnX),
            5.5f,
            0
        );

        GameObject trashInstance = Instantiate(
            trashType.prefab,
            spawnPoint,
            Quaternion.Euler(0, 0, Random.Range(0, 360))
        );

        TrashItem trashItem = trashInstance.GetComponent<TrashItem>();
        if (trashItem != null)
            trashItem.Initialize(trashType, this);
        currentTrashCount++;
    }

    public void OnTrashDestroyed()
    {
        currentTrashCount--;
    }
}

