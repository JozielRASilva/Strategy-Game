using UnityEngine;
using ZombieDiorama.Character.Info;
using ZombieDiorama.Character.Behaviours;
using ZombieDiorama.Character.Behaviours.Custom;

namespace ZombieDiorama.Character.Handler
{
    public class NPCNavMeshExample : MonoBehaviour
    {

        public SOAttributes attributes;
        public float timeToRun = 2;
        public float Distance = 1;

        public TargetHandler targetController;

        public NavMeshHandler navMeshController;

        BehaviourTree behaviourTree;

        void Start()
        {
            SetBehaviour();
        }

        public void SetBehaviour()
        {
            if (!behaviourTree)
                behaviourTree = gameObject.AddComponent<BehaviourTree>();

            if (!attributes) return;

            BTSequence collect = new BTSequence();
            collect.SetNode(new BTMoveByNavMesh(navMeshController, targetController, attributes.speed, attributes.distance));

            behaviourTree.Build(collect);

        }

    }
}