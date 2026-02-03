using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MixedCan : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    TMPro.TextMeshProUGUI allMoneyText;
    [SerializeField]
    TMPro.TMP_Text heldMoneyText;
    private int heldMoney;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Trash")
        {
            heldMoney += collision.gameObject.GetComponent<TrashItem>().GetScore();
            heldMoneyText.text = heldMoney.ToString();
            Destroy(collision.gameObject);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Mixed can clicked!");
        PerformAction();
    }

    private void PerformAction()
    {
        TrashSpawner.allMoney += heldMoney;
        heldMoney = 0;
        heldMoneyText.text = heldMoney.ToString();
        allMoneyText.text = "Money: " + TrashSpawner.allMoney.ToString();
    }
}
