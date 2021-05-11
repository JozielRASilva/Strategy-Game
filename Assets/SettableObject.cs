using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettableObject : MonoBehaviour
{

    public GameObject preview;

    public List<string> TagsToIgnore = new List<string>();

    public BoxCollider boxCollider;

    public Vector3 underOffset;
    public float collisionFactor = 0.2f;

    private MeshRenderer mesh;

    public LayerMask PlacesWhereCanSet;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        mesh = preview.GetComponent<MeshRenderer>();
    }

    public bool CanSet()
    {
        return false;
    }

    public void ShowPreview()
    {

    }

    public bool CheckCollision(LayerMask mask)
    {
        return Physics.CheckBox(preview.transform.position, boxCollider.size, transform.rotation, mask);
    }

    public bool CheckUnder(LayerMask mask)
    {
        Vector3 factor = boxCollider.size;
        factor.y *= collisionFactor;
        return Physics.CheckBox(transform.position + underOffset, factor/2, transform.rotation, mask);
    }

    private void OnDrawGizmos()
    {
        if (preview && boxCollider)
        {
            if (CheckUnder(PlacesWhereCanSet)) Gizmos.color = Color.green;
            else Gizmos.color = Color.red;

            Gizmos.matrix = this.transform.localToWorldMatrix;

            Vector3 factor = boxCollider.size;
            factor.y *= collisionFactor;
            Gizmos.DrawWireCube(underOffset, factor);
        }
    }

}
