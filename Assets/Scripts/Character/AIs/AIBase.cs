using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ZombieDiorama.Character.AIs
{
    public interface AIBase
    {
        void SetBehaviour();
        void RestartBehaviour();
        void StopBehaviour();
    }
}