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

        float valorAtual = mesh.material.GetFloat("_Elasticidade");

        if (magnitude == 0)
        {
            valorAtual += 0.2f;
        }
        else if (magnitude != 0)
        {
            valorAtual -= 0.5f;
        }

        Mathf.Clamp(valorAtual, 4, 9);

        mesh.material.SetFloat("_Elasticidade", valorAtual);

    }

}