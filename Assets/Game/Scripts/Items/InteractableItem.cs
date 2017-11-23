
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


    public void Initialise()
    {
        //TODO: Start animation
    }


    public EItemType PickUp(out int count, out GameObject prefabRef)
    {
        gameObject.SetActive(false);

        count = ItemCount;
        prefabRef = ItemPrefab;
        
        return Type;
    }

}
