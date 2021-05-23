using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleCamera : MonoBehaviour
{
    public Camera cam;
    private Vector3 posanterior;
    public Transform alvo;
    public float camdistancia = -40;
     
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            posanterior = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 direcao = posanterior - cam.ScreenToViewportPoint(Input.mousePosition);
            cam.transform.position = alvo.position;
            cam.transform.Rotate(new Vector3(1, 0, 0), direcao.y * 180);
            cam.transform.Rotate(new Vector3(0, 1, 0), -direcao.x * 180, Space.World);
            cam.transform.Translate(new Vector3(0, 0, camdistancia));
            posanterior = cam.ScreenToViewportPoint(Input.mousePosition);

            
        }
    }
}
