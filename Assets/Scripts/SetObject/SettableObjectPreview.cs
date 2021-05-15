using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SettableObjectPreview : MonoBehaviour
{

    public GameObject preview;

    [Title("Over Position")]
    public LayerMask WhereCanNotSet;

    [Title("Bottom Ground")]
    public Vector3 underOffset;
    public float collisionFactor = 0.2f;
    public LayerMask WhereCanSetOver;

    [Title("Material")]
    public Material canSetMaterial;
    public Material canNotSetMaterial;

    [Title("Options to Set")]
    public bool canRotate;

    private BoxCollider boxCollider;
    private MeshRenderer mesh;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        mesh = preview.GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        if (!boxCollider)
            boxCollider = GetComponent<BoxCollider>();
        if (!mesh)
            mesh = preview.GetComponent<MeshRenderer>();
    }

    public bool CanSet()
    {
        bool collision = CheckCollision(WhereCanNotSet);
        bool under = CheckUnder(WhereCanSetOver);

        if (collision) return false;

        if (!under) return false;

        return true;
    }

    public void ShowPreview(Vector3 position)
    {
        transform.position = position;

        if (!canNotSetMaterial || !mesh || !canSetMaterial) return;
        if (CanSet())
        {
            mesh.material = canSetMaterial;
        }
        else
        {
            mesh.material = canNotSetMaterial;
        }
    }

    public void Rotate(Vector3 value)
    {
        if (canRotate)
            transform.Rotate(value.x * Time.deltaTime, value.y * Time.deltaTime, value.z * Time.deltaTime, Space.Self);
    }

    public bool CheckCollision(LayerMask mask)
    {
        if (!preview || !boxCollider) return false;
        return Physics.CheckBox(transform.position + preview.transform.localPosition, boxCollider.size / 2, transform.rotation, mask);
    }

    public bool CheckUnder(LayerMask mask)
    {
        if (!preview || !boxCollider) return false;
        Vector3 factor = boxCollider.size;
        factor.y *= collisionFactor;
        return Physics.CheckBox(transform.position + underOffset, factor / 2, transform.rotation, mask);
    }

    private void OnDrawGizmos()
    {
        if (preview && boxCollider)
        {
            if (CanSet()) Gizmos.color = Color.green;
            else Gizmos.color = Color.red;

            Gizmos.matrix = this.transform.localToWorldMatrix;
            Gizmos.DrawWireCube(preview.transform.localPosition, boxCollider.size);

            Vector3 factor = boxCollider.size;
            factor.y *= collisionFactor;
            Gizmos.DrawWireCube(underOffset, factor);
        }
    }

    private void OnValidate()
    {
        if (!boxCollider) boxCollider = GetComponent<BoxCollider>();
    }


    public void EnableObject()
    {
        gameObject.SetActive(true);
    }

    public void DisableObject()
    {
        gameObject.SetActive(false);
    }

}
