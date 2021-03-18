using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PooledListView listView;
    [SerializeField] int DataCount;

    IEnumerator Start()
    {
        ListViewItemModel[] demoData = new ListViewItemModel[DataCount];
        for(int i = 0; i < DataCount; i++)
        {
            demoData[i] = new ListViewItemModel(i + 1);
        }
        yield return StartCoroutine(listView.getData());
        listView.Setup(demoData);
    }
}
