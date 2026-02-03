using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TrashItem : MonoBehaviour, IPointerDownHandler
{
    public TrashType trashType;
    protected TrashSpawner spawner;
    public static float speed = .5f;
    private bool isSlelected;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (spawner.selectedObject == null)
        {
            isSlelected = true;
            this.gameObject.transform.Find("Outline").gameObject.GetComponent<SpriteRenderer>().enabled = true;
            spawner.selectedObject = this.gameObject;
        }
        else
        {
            isSlelected = true;
            spawner.selectedObject.transform.Find("Outline").gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.transform.Find("Outline").gameObject.GetComponent<SpriteRenderer>().enabled = true;
            spawner.selectedObject = this.gameObject;
        }
    }

    public void Initialize(TrashType type, TrashSpawner owner)
    {
        trashType = type;
        spawner = owner;

        if (trashType.lifetime > 0)
            Destroy(gameObject, trashType.lifetime);
    }

    public int GetScore()
    {
               return trashType.scoreValue;
    }

    private void OnDestroy()
    {
        if (spawner != null)
            spawner.OnTrashDestroyed();
    }
}

