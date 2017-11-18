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


    public void Initialise()
    {
        //TODO: Start animation
    }


    public EItemType PickUp()
    {
        //TODO: fade
        gameObject.SetActive(false);
        //GetComponent<BoxCollider2D>().enabled = false;
        return Type;
    }

}
