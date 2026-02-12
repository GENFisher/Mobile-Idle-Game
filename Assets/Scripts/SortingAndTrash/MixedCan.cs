using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MixedCan : MonoBehaviour, IPointerClickHandler
{
    private TrashSpawner trashSpawner;
    private void Start()
    {
        trashSpawner = FindFirstObjectByType<TrashSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Trash")
        {
            EconomyManager.Instance.AddMoney((int)UpgradeManager.Instance.GetCurrentValue(UpgradeType.MixedTrashPrice));
            Destroy(collision.gameObject);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        PerformAction();
    }

    private void PerformAction()
    {
        EconomyManager.Instance.AddMoney((int)UpgradeManager.Instance.GetCurrentValue(UpgradeType.MixedTrashPrice));
        Destroy(trashSpawner.selectedObject);
    }
}
