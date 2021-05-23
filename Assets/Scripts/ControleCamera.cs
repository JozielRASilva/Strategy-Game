using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ControleCamera : MonoBehaviour
{
    public Camera cam;
    private Vector3 posanterior;
    public Transform alvo;
    public float camdistancia = -40;

    [Title("Rotation Axis")]
    public bool moveAxisX = true;
    public float AngleX = 180;
    public bool moveAxisY = true;
    public float AngleY = 180;

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

            if (moveAxisY)
                cam.transform.Rotate(new Vector3(1, 0, 0), direcao.y * AngleY);
            else
                cam.transform.eulerAngles = new Vector3(AngleY, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);

            if (moveAxisX)
                cam.transform.Rotate(new Vector3(0, 1, 0), -direcao.x * AngleX, Space.World);
            else
                cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, AngleX, cam.transform.eulerAngles.z);

            cam.transform.Translate(new Vector3(0, 0, camdistancia));
            posanterior = cam.ScreenToViewportPoint(Input.mousePosition);


        }

    }

    private void LateUpdate()
    {
        cam.transform.position = alvo.position;
        cam.transform.Translate(new Vector3(0, 0, camdistancia));


        if (!moveAxisY)
            cam.transform.eulerAngles = new Vector3(AngleY, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);

        if (!moveAxisX)
            cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, AngleX, cam.transform.eulerAngles.z);
    }
}
