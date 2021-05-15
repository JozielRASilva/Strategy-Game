using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Moedas : MonoBehaviour
{
    public Text moedaTxt;
    public int moeda;
    public int valorMoeda;

    void Start()
    {
        moeda = 0;
        valorMoeda = 1;
    }

    void AddMoeda() 
    {
        moeda += valorMoeda;
        moedaTxt.text = "Moedas: " + moeda.ToString();
    }
}
