using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Moedas : MonoBehaviour
{
    public Text moedaTxt;
    public int moeda;
    public int valorMoeda = 1;

    public static Moedas moedas;

    void Start()
    {
        moedas = this;
        moeda = 0;
    }

     public void AddMoeda() 
    {
        moeda += valorMoeda;
        moedaTxt.text = "Moedas: " + moeda.ToString();
    }
}
