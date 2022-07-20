using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System;
using UnityEngine.UI;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] inventorySlot _inventory_slot;
    [SerializeField] Transform _inventory_group;
    [SerializeField]
    List<itemData> itemDatas;
     // Start is called before the first frame update
    void Start()
    {
        _inventory_group = this.transform;
        itemDatas = this.getAllItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addItem(itemData item)
    {
        itemDatas = this.getAllItems();

        bool is_exist = false;
        for(int i = 0; i< itemDatas.Count; i++)
        {
        
            if (itemDatas[i].ID != item.ID)
                continue;

            itemDatas[i].amount += item.amount;

            if (itemDatas[i].amount <= 10)
                is_exist = true;
            else
            {
                item.amount = itemDatas[i].amount - 10;
                itemDatas[i].amount = 10;
            }
               
        }

        if (is_exist)
        {
            SaveInventory();
            return;
        }

        itemDatas.Add(item);
        SaveInventory();
    }


    public void SaveInventory()
    {
        string json = "[";
        for (int i = 0; i < itemDatas.Count; i++)
        {
            Debug.Log(itemDatas[i].ToString());
            json += JsonUtility.ToJson(itemDatas[i]) +",";
        }

        if (json.EndsWith(","))
            json = json.Remove(json.Length -1, 1);

        json += "]";
        PlayerPrefs.SetString("INVENTORY", json);
        ShowInventory();
    }

    public List<itemData> getAllItems()
    {
        string data_inventory_raw = PlayerPrefs.GetString("INVENTORY", "[]");
   
        itemDatas = new List<itemData>();
        try
        {
            var data_parsed = JSON.Parse(data_inventory_raw).AsArray;
            foreach (var tmp_item in data_parsed)
            {
                //Debug.Log(tmp_item.Value);
               // var tmp_item_parsed = JSON.Parse(tmp_item.Value);
                itemData item = new itemData();

                item.name = tmp_item.Value["name"];
                item.ID = tmp_item.Value["ID"];
                item.amount = tmp_item.Value["amount"];

                itemDatas.Add( item);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("parse data error! " + e.ToString());
        }
        ShowInventory();
        return itemDatas;
    }


    void ShowInventory()
    {
        for (int  i= 0; i< _inventory_group.transform.childCount; i++)
        {
            if(i > itemDatas.Count)
            {
                _inventory_group.GetChild(i).gameObject.SetActive(false);
                continue;
            }
            _inventory_group.GetChild(i).GetComponent<inventorySlot>().Init(itemDatas[i]);
            _inventory_group.GetChild(i).gameObject.SetActive(true);
        }
        if(_inventory_group.transform.childCount < itemDatas.Count)
        {
            int tmp = itemDatas.Count - _inventory_group.transform.childCount;
            for (int i = itemDatas.Count - tmp; i < itemDatas.Count; i++)
            {

                inventorySlot g = Instantiate<inventorySlot>(_inventory_slot, this.transform.position, Quaternion.identity, _inventory_group);
                g.Init(itemDatas[i]);
            }
        }
    }
}

[System.Serializable]
public class itemData
{
    public string name;
    public int ID;
    public int amount;
    
}
