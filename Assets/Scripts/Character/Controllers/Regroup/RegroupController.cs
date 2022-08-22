using UnityEngine;
namespace ZombieDiorama.Character.Controllers.Regroup
{
    public class RegroupController : Utilities.Patterns.Singleton<RegroupController>
    {

        public Vector3 RegroupPoint;
        public int CurrentRegroupId = -1;
        public bool TimeLimitToRegroup = false;
        public float RegroupTime = 10;
        public float TimeStamp;
        public Transform PointReference;

        protected override void Awake()
        {
            base.Awake();

            CurrentRegroupId = -1;
        }

        public void SetPoint(Vector3 position)
        {
            RegroupPoint = position;

            CurrentRegroupId++;

            TimeStamp = Time.time + RegroupTime;
        }

        private void Update()
        {
            if (CanRegroup())
            {
                PointReference.gameObject.SetActive(true);

                PointReference.position = RegroupPoint;
            }
            else
            {
                PointReference.gameObject.SetActive(false);
            }
        }

        public Transform GetRegroupPoint()
        {
            PointReference.position = RegroupPoint;
            return PointReference;
        }

        public int GetRegroupId()
        {
            return CurrentRegroupId;
        }

        public bool CanRegroup(int id = -1)
        {
            if (CurrentRegroupId < 0) return false;

            if (TimeLimitToRegroup)
            {
                if (Time.time > TimeStamp) return false;
            }

            if (id != CurrentRegroupId) return true;

            return false;
        }
    }
}
