using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] itemData item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            InventoryManager.Instant.addItem(item);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {

            item.amount = -item.amount;
            InventoryManager.Instant.addItem(item);
        }
    }
}
