using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;


public class PooledListView : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{


    

    #region Child Components

    [SerializeField] ScrollRect ScrollRect;
    [SerializeField] RectTransform viewPortT;
    [SerializeField] RectTransform DragDetectionT;
    [SerializeField] RectTransform ContentT;
    [SerializeField] ListViewItemPool ItemPool;

    #endregion



    #region Layout Parameters

    [SerializeField] float ItemHeight = 1;      // TODO: Replace it with dynamic height
    [SerializeField] int BufferSize;

    #endregion



    #region Layout Variables

    int TargetVisibleItemCount { get { return Mathf.Max(Mathf.CeilToInt(viewPortT.rect.height / ItemHeight), 0); } }
    int TopItemOutOfView { get { return Mathf.CeilToInt(ContentT.anchoredPosition.y / ItemHeight); } }

    float dragDetectionAnchorPreviousY = 0;

    #endregion



    #region Data

    ListViewItemModel[] data;
    int dataHead = 0;
    int dataTail = 0;

    #endregion


    [System.Serializable]
    public class List
    {
        public int months;
        public int price;
    }

    public string Url;

    [System.Serializable]
    public class PlayerList
    {
        public List[] musky_and_price;
    }


    PlayerList myPlayerList = new PlayerList();



    


    public IEnumerator getData()
    {

        UnityWebRequest request = UnityWebRequest.Get(Url);
        request.chunkedTransfer = false;
        yield return request.Send();
        if (request.isNetworkError)
        {
            Debug.Log("errtr");
        }
        else
        {
            if (request.isDone)
            {
                myPlayerList = JsonUtility.FromJson<PlayerList>(request.downloadHandler.text);
                Debug.Log("done");
               
            }
        }

    }

    public void Setup(ListViewItemModel[] data)
    {
        ScrollRect.onValueChanged.AddListener(OnDragDetectionPositionChange);

        this.data = data;

        DragDetectionT.sizeDelta = new Vector2(DragDetectionT.sizeDelta.x, this.data.Length * ItemHeight);

        for(int i = 0; i < TargetVisibleItemCount + BufferSize; i++)
        {
            GameObject itemGO = ItemPool.ItemBorrow();
            itemGO.transform.SetParent(ContentT);
            itemGO.SetActive(true);
            itemGO.transform.localScale = Vector3.one;
            itemGO.GetComponent<ListViewItem>().Setup(data[dataTail], myPlayerList.musky_and_price[dataTail].months, myPlayerList.musky_and_price[dataTail].price);
            dataTail++;
            
        }
    }



    #region UI Event Handling

    public void OnDragDetectionPositionChange(Vector2 dragNormalizePos)
    {
        float dragDelta = DragDetectionT.anchoredPosition.y - dragDetectionAnchorPreviousY;

        ContentT.anchoredPosition = new Vector2(ContentT.anchoredPosition.x, ContentT.anchoredPosition.y + dragDelta);

        UpdateContentBuffer();

        dragDetectionAnchorPreviousY = DragDetectionT.anchoredPosition.y;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        dragDetectionAnchorPreviousY = DragDetectionT.anchoredPosition.y;
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    #endregion



    #region Infinite Scroll Mechanism

    void UpdateContentBuffer()
    {
        if(TopItemOutOfView > BufferSize)
        {
            if(dataTail >= data.Length)
            {
                return;
            }

            Transform firstChildT = ContentT.GetChild(0);
            firstChildT.SetSiblingIndex(ContentT.childCount - 1);
            firstChildT.gameObject.GetComponent<ListViewItem>().Setup(data[dataTail], myPlayerList.musky_and_price[dataTail].months, myPlayerList.musky_and_price[dataTail].price);
            ContentT.anchoredPosition = new Vector2(ContentT.anchoredPosition.x, ContentT.anchoredPosition.y - firstChildT.gameObject.GetComponent<ListViewItem>().ItemHeight);
            dataHead++;
            dataTail++;
        }
        else if(TopItemOutOfView < BufferSize)
        {
            if(dataHead <= 0)
            {
                return;
            }

            Transform lastChildT = ContentT.GetChild(ContentT.childCount - 1);
            lastChildT.SetSiblingIndex(0);
            dataHead--;
            dataTail--;
            lastChildT.gameObject.GetComponent<ListViewItem>().Setup(data[dataHead], myPlayerList.musky_and_price[dataTail].months, myPlayerList.musky_and_price[dataTail].price);
            ContentT.anchoredPosition = new Vector2(ContentT.anchoredPosition.x, ContentT.anchoredPosition.y + lastChildT.gameObject.GetComponent<ListViewItem>().ItemHeight);

        }
    }

    #endregion
}
