using UnityEngine;
using UnityEngine.Serialization;

namespace ZombieDiorama.Character
{
    public class CharacterAnimation : MonoBehaviour
    {
        [Header("Animation")]
        [FormerlySerializedAs("xSpeed")] public string XSpeed = "xSpeed";
        [FormerlySerializedAs("shootTrigger")] public string ShootTrigger = "Shoot";

        [Header("IK")]
        public float WeightToLookToMouse = 0.5f;
        public float RightHandIKWeight = 0.5f;

        private Animator animator;
        private Vector3 movement = new Vector3();

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            movement = Input.GetAxis("Horizontal") * transform.right;
            movement += Input.GetAxis("Vertical") * transform.forward;

            Debug.DrawRay(transform.position, movement * 2, Color.cyan);

            animator.SetFloat(XSpeed, movement.magnitude);

            if (Input.GetButtonDown("Fire1"))
            {
                animator.SetTrigger(ShootTrigger);
            }
        }

        private void OnAnimatorIK(int layer)
        {
            if (!animator) return;

            Vector3 mouse = Input.mousePosition;
            mouse.z = Camera.main.transform.position.z;

            mouse = Camera.main.ScreenToWorldPoint(mouse);

            Debug.DrawLine(mouse, transform.position, Color.magenta);

            animator.SetLookAtWeight(WeightToLookToMouse);
            animator.SetLookAtPosition(mouse);
        }
    }
}
