using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public GameObject special;
    public GameObject hitCoins;
    public GameObject muskteer;

   
    public void specialTabButton()
    {
        special.SetActive(true);
        hitCoins.SetActive(false);
        muskteer.SetActive(false);
    }
    public void hitCoinsTabButton()
    {
        special.SetActive(false);
        hitCoins.SetActive(true);
        muskteer.SetActive(false);
    }
    public void muskteerTabButton()
    {
        special.SetActive(true);
        hitCoins.SetActive(false);
        muskteer.SetActive(true);

    }


}
