using UnityEngine;

public class Deforma : MonoBehaviour
{

    MeshRenderer mesh;
    Rigidbody rigid;

    float elasticidade;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        rigid = GetComponent<Rigidbody>();
        elasticidade = 7;

    }

    private void Efeito()
    {

        mesh.material.SetFloat("_Elasticidade", elasticidade);

    }

    private void Processa()
    {

        Vector3 velocidade = rigid.velocity;

        float magnitude = velocidade.magnitude;
        float valor = 0.2f;

        if (magnitude > 0)
        {
            valor = Mathf.Abs(magnitude);
            if (valor > 9) valor = 9;
        }
        else if (magnitude < 0)
        {
            valor = -Mathf.Abs(magnitude);
            if (valor < - 4) valor = -4;
        }

        mesh.material.SetFloat("_Elasticidade", valor);

    }

}