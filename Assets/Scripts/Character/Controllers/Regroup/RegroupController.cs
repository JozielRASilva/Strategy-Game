using UnityEngine;
using ZombieDiorama;
namespace ZombieDiorama.Character.Controllers.Regroup
{
    public class RegroupController : Utilities.Patterns.Singleton<RegroupController>
    {

        public Vector3 RegroupPoint;
        public int currentRegroupId = -1;

        public bool TimeLimitToRegroup = false;
        public float regroupTime = 10;
        public float TimeStamp;

        public Transform pointReference;

        protected override void Awake()
        {
            base.Awake();

            currentRegroupId = -1;
        }

        public void SetPoint(Vector3 position)
        {
            RegroupPoint = position;

            currentRegroupId++;

            TimeStamp = Time.time + regroupTime;
        }

        private void Update()
        {
            if (CanRegroup())
            {
                pointReference.gameObject.SetActive(true);

                pointReference.position = RegroupPoint;
            }
            else
            {
                pointReference.gameObject.SetActive(false);
            }
        }

        public Transform GetRegroupPoint()
        {
            pointReference.position = RegroupPoint;
            return pointReference;
        }

        public int GetRegroupId()
        {
            return currentRegroupId;
        }

        public bool CanRegroup(int id = -1)
        {
            if (currentRegroupId < 0) return false;

            if (TimeLimitToRegroup)
            {
                if (Time.time > TimeStamp) return false;
            }

            if (id != currentRegroupId) return true;

            return false;
        }
    }
}
