using UnityEngine;
using Sirenix.OdinInspector;

namespace ZombieDiorama.Character.Handler.Team
{
    public class SquadMember : MonoBehaviour
    {
        public bool ExtraMember;
        [HideInInspector] public Health Health;

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
            if (!TeamHandler.Instance) return;

            GetSquadFunction();
        }

        [Button("Remove Function")]
        public void RemoveFromSquad()
        {
            if (!TeamHandler.Instance) return;

            TeamHandler.Instance.RemoveFromSquad(this);

        }

        public Squad.SquadFunction GetSquadFunction()
        {
            return TeamHandler.Instance.GetSquadFunction(this);
        }

    }
}
