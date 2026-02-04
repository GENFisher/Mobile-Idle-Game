using UnityEngine;

public enum RecycableType
{
    Plastic,
    Metal
}

[CreateAssetMenu(menuName = "Trash/Trash Type")]
public class TrashType : ScriptableObject
{
    public new string name;

    public RecycableType trashType;

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
