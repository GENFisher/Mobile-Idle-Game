using UnityEngine;
using UnityEngine.EventSystems;

public class RecyclableCan : MonoBehaviour, IPointerClickHandler
{
    private TrashSpawner trashSpawner;
    [SerializeField] private RecycableType acceptedType;

    private void Start()
    {
        trashSpawner = FindFirstObjectByType<TrashSpawner>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        PerformAction();
    }

    private void PerformAction()
    {
        if (trashSpawner != null)
        {
            if (trashSpawner.selectedObject != null)
            {
                if (trashSpawner.selectedObject.GetComponent<TrashItem>().trashType.trashType == acceptedType)
                {
                    EconomyManager.Instance.AddTrash(acceptedType, (int)UpgradeManager.Instance.GetCurrentValue(UpgradeType.RecyclingYield));
                    Destroy(trashSpawner.selectedObject);
                }
                else
                {
                    EconomyManager.Instance.RemoveTrash(acceptedType, (int)UpgradeManager.Instance.GetCurrentValue(UpgradeType.RecyclingPenalty));
                    Destroy(trashSpawner.selectedObject);
                }
                UIManager.Instance.UpdateAmount(acceptedType, EconomyManager.Instance.GetTrashAmount(acceptedType));
            }
        }
    }
}
