using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventorySlot : MonoBehaviour
{
    [SerializeField] Text name_text, number_text;
    public void Init(itemData data)
    {
        name_text.text = data.amount.ToString();
        number_text.text = data.name;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
