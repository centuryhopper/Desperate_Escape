using UnityEngine;

namespace ScriptableObjs
{
    public abstract class StateData : ScriptableObject
    {
        public abstract void OnEnter(EnemyState enemyState, Animator animator, AnimatorStateInfo asi);

        public abstract void OnUpdate(EnemyState enemyState, Animator animator, AnimatorStateInfo asi);

        public abstract void OnExit(EnemyState enemyState, Animator animator, AnimatorStateInfo asi);
    }
}
