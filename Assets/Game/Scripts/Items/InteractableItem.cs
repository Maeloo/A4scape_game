
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EItemType
{
    not_defined,
    Beer
};


public class InteractableItem : MonoBehaviour
{
  
    [SerializeField]
    protected EItemType Type;

    public int ItemCount = 5;
    public GameObject ItemPrefab;

    bool bActive = true;

    [SerializeField]
    protected GameObject _ButtonIcon;


    private void Start()
    {
        _ButtonIcon.SetActive(false);

        ToggleActive();
    }

    public void OnPlayerTriggered(bool bTriggered)
    {
        _ButtonIcon.SetActive(bTriggered);
    }

    public void ToggleActive()
    {
        bActive = !bActive;
        GetComponent<BoxCollider2D>().enabled = bActive;
        GetComponent<SpriteRenderer>().enabled = bActive;
    }

    public EItemType PickUp(out int count, out GameObject prefabRef)
    {
        ToggleActive();

        count = ItemCount;
        prefabRef = ItemPrefab;

        Invoke("ToggleActive", 30f);

        return Type;
    }

}
