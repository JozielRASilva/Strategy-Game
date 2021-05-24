using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Moedas : MonoBehaviour
{
    public Text moedaTxt;
    public int moeda;
    public int valorMoeda = 1;

    public static Moedas moedas;

    public UnityEvent OnGetCash;

    void Start()
    {
        moedas = this;
        moedaTxt.text = moeda.ToString();
    }

    public void AddMoeda()
    {
        moeda += valorMoeda;
        moedaTxt.text = moeda.ToString();
        OnGetCash?.Invoke();
    }

     public void AddMoeda(int value)
    {
        moeda += value;
        moedaTxt.text = moeda.ToString();

        OnGetCash?.Invoke();
    }

}
