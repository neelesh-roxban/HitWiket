using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ListViewItem : MonoBehaviour
{
          
    public Text Quantity;
    public Text Cost;

    public int ItemHeight { get { return 100; }}



    

    public void Setup(ListViewItemModel model,int cost,int quantity)
    {
        gameObject.name = ((int)(model.Data)).ToString();
        Quantity.text = cost.ToString();
        Cost.text = quantity.ToString();
    }
}
