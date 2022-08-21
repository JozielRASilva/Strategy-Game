using UnityEngine;
using Sirenix.OdinInspector;

namespace ZombieDiorama.Character.Controllers.Team
{
    public class SquadMember : MonoBehaviour
    {
        public bool ExtraMember;
        public Health Health;

        private void Awake()
        {
            if (!Health)
            {
                Health = GetComponent<Health>();
            }
            Health.ActionOnKill += RemoveFromSquad;
        }

        private void Update()
        {
            if (Health.IsAlive() && GetSquadFunction().Equals(Squad.SquadFunction.NONE))
            {
                GetSquadFunction();
            }
            else if (!Health.IsAlive())
            {
                RemoveFromSquad();
            }
        }

        [Button("Get Function")]
        public void CheckSquadFunction()
        {
            if (!TeamController.Instance) return;

            GetSquadFunction();
        }

        [Button("Remove Function")]
        public void RemoveFromSquad()
        {
            if (!TeamController.Instance) return;

            TeamController.Instance.RemoveFromSquad(this);

        }

        public Squad.SquadFunction GetSquadFunction()
        {
            return TeamController.Instance.GetSquadFunction(this);
        }

    }
}
