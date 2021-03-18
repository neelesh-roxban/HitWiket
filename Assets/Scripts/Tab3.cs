using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class Tab3 : MonoBehaviour
{

    
    public GameObject listTemplet;

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

    void Start()
    {
        StartCoroutine(getData());


    }


    void DrawUI()
    {
        
        GameObject g;


        int N = myPlayerList.musky_and_price.Length;

        for (int i = 0; i < N; i++)
        {
            g = Instantiate(listTemplet, transform);           
            g.transform.GetChild(2).GetComponent<Text>().text = myPlayerList.musky_and_price[i].months.ToString();
            g.transform.GetChild(3).GetComponent<Text>().text = myPlayerList.musky_and_price[i].months.ToString();
           


        }

        
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
                myPlayerList = JsonUtility.FromJson<PlayerList>(request.downloadHandler.text);
                
            }
        }

    }



}


