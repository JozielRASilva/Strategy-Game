using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using MonsterLove.Pooller;
using UnityEngine.Serialization;

namespace ZombieDiorama.ObjectPlacer
{
    public class SettableObjectPreview : MonoBehaviour
    {
        public GameObject Preview;
        public MeshRenderer Mesh;
        public GameObject SpaceBloker;

        [Title("Over Position")]
        public LayerMask WhereCanNotSet;

        [Title("Bottom Ground")]
        public Vector3 UnderOffset;
        public float CollisionFactor = 0.2f;
        public LayerMask WhereCanSetOver;

        [Title("Material")]
        public Material CanSetMaterial;
        public Material CanNotSetMaterial;
        public Material SelectedMaterial;

        [Title("Options to Set")]
        public bool CanRotate;
        
        private BoxCollider _boxCollider;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
            if (!Mesh)
                Mesh = Preview.GetComponent<MeshRenderer>();
        }

        private void OnEnable()
        {
            if (!_boxCollider)
                _boxCollider = GetComponent<BoxCollider>();
            if (!Mesh)
                Mesh = Preview.GetComponent<MeshRenderer>();
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
            if (SpaceBloker) SpaceBloker.SetActive(false);
            transform.position = position;

            if (!CanNotSetMaterial || !Mesh || !CanSetMaterial) return;
            if (CanSet())
            {
                for (int i = 0; i < Mesh.sharedMaterials.Length; i++)
                {
                    Mesh.sharedMaterials[i] = CanSetMaterial;
                }
                Mesh.material = CanSetMaterial;
            }
            else
            {
                for (int i = 0; i < Mesh.sharedMaterials.Length; i++)
                {
                    Mesh.sharedMaterials[i] = CanNotSetMaterial;
                }
                Mesh.material = CanNotSetMaterial;
            }
        }

        public void ShowSelected(Vector3 position, Vector3 rotation)
        {
            if (SpaceBloker) SpaceBloker.SetActive(true);

            transform.position = position;
            FillRotation(rotation);

            if (!SelectedMaterial || !Mesh) return;

            for (int i = 0; i < Mesh.sharedMaterials.Length; i++)
            {
                Mesh.sharedMaterials[i] = SelectedMaterial;
            }
            Mesh.material = SelectedMaterial;
        }

        public void Rotate(Vector3 value)
        {
            if (CanRotate)
                transform.Rotate(value.x * Time.deltaTime, value.y * Time.deltaTime, value.z * Time.deltaTime, Space.Self);
        }

        public void FillRotation(Vector3 value)
        {
            if (CanRotate)
                transform.eulerAngles = value;
        }

        public bool CheckCollision(LayerMask mask)
        {
            if (!Preview || !_boxCollider) return false;
            return Physics.CheckBox(transform.position + Preview.transform.localPosition, _boxCollider.size / 2, transform.rotation, mask);
        }

        public bool CheckUnder(LayerMask mask)
        {
            if (!Preview || !_boxCollider) return false;
            Vector3 factor = _boxCollider.size;
            factor.y *= CollisionFactor;
            return Physics.CheckBox(transform.position + UnderOffset, factor / 2, transform.rotation, mask);
        }

        private void OnDrawGizmos()
        {
            if (Preview && _boxCollider)
            {
                if (CanSet()) Gizmos.color = Color.green;
                else Gizmos.color = Color.red;

                Gizmos.matrix = this.transform.localToWorldMatrix;
                Gizmos.DrawWireCube(Preview.transform.localPosition, _boxCollider.size);

                Vector3 factor = _boxCollider.size;
                factor.y *= CollisionFactor;
                Gizmos.DrawWireCube(UnderOffset, factor);
            }
        }

        private void OnValidate()
        {
            if (!_boxCollider) _boxCollider = GetComponent<BoxCollider>();
        }

        public void EnableObject()
        {
            gameObject.SetActive(true);
        }

        public void DisableObject()
        {
            PoolManager.ReleaseObject(this.gameObject);
        }
    }
}