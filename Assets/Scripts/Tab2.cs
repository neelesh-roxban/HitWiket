using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class Tab2 : MonoBehaviour
{

    public GameObject listTemplet;

   [System.Serializable]
    public class List
    {
        public string product_price;
        public string quantity;
    }

    public string Url;

    [System.Serializable]
    public class PlayerList
    {
       public List[] coin_and_price;
    }


    public PlayerList myPlayerList = new PlayerList();

    void Start()
    {
        StartCoroutine(getData());
        

    }


    void DrawUI()
    {       
        GameObject g;
        

        int N = myPlayerList.coin_and_price.Length;

        for (int i = 0; i < N; i++)
        {
            g = Instantiate(listTemplet, transform);
            g.transform.GetChild(2).GetComponent<Text>().text = myPlayerList.coin_and_price[i].product_price;
            g.transform.GetChild(3).GetComponent<Text>().text = myPlayerList.coin_and_price[i].quantity;


        }

        Destroy(listTemplet);
    }

    IEnumerator getData()
    {

        UnityWebRequest request = UnityWebRequest.Get(Url);
        request.chunkedTransfer = false;
        yield return request.Send();
        if (request.isNetworkError)
        {

        }
        else
        {
            if (request.isDone)
            {
                myPlayerList= JsonUtility.FromJson<PlayerList>(request.downloadHandler.text);
                DrawUI();
            }
        }
       
    }

   

}
    

