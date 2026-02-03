using UnityEngine;
using UnityEngine.EventSystems;

public class RecyclableCan : MonoBehaviour, IPointerClickHandler
{
    private TrashSpawner trashSpawner;
    [SerializeField] private TrashType TrashType;
    private string recyclableId;

    private void Start()
    {
        trashSpawner = FindFirstObjectByType<TrashSpawner>();
        recyclableId = TrashType.trashId.ToString();
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
                if (trashSpawner.selectedObject.GetComponent<TrashItem>().trashType.trashId == recyclableId)
                {
                    Destroy(trashSpawner.selectedObject);
                    Debug.Log("Plastic recycled!+point");
                }
                else
                {
                    Destroy(trashSpawner.selectedObject);
                    Debug.Log("This item is not recyclable in this can.-point");
                }
            }
            else
            {
                Debug.Log("No item selected to recycle.");
            }
        }
    }
}
