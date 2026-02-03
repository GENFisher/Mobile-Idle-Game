using UnityEngine;

[CreateAssetMenu(menuName = "Trash/Trash Type")]
public class TrashType : ScriptableObject
{
    public string trashId;

    public GameObject prefab;

    [Header("Spawn Settings")]
    public float weight = 1f;

    [Header("Gameplay")]
    public int scoreValue;
    public float lifetime;

    [Header("Tags")]
    public bool isRecyclable;
    public bool isHazardous;
}
