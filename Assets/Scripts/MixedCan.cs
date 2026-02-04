using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MixedCan : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Trash")
        {
            EconomyManager.Instance.AddMoney((int)UpgradeManager.Instance.MixedTrashPrice);
            Destroy(collision.gameObject);
        }
    }
}
