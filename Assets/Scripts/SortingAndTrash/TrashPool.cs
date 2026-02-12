using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Trash/Trash Pool")]
public class TrashPool : ScriptableObject
{
    public List<TrashType> trashTypes = new();

    public TrashType GetRandomTrash()
    {
        float totalWeight = 0f;

        foreach (var trash in trashTypes)
            totalWeight += trash.weight;

        float roll = Random.Range(0f, totalWeight);

        foreach (var trash in trashTypes)
        {
            roll -= trash.weight;
            if (roll <= 0f)
                return trash;
        }

        return trashTypes[0]; // fallback
    }
}

