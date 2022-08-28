using System.Collections.Generic;
using UnityEngine;


namespace ScriptableObjs
{
    public class EnemyState : StateMachineBehaviour
    {
        private EnemyInfo enemyInfo;
        public EnemyInfo GetEnemyInfo(Animator animator)
        {
            if (enemyInfo == null)
            {
                enemyInfo = animator.GetComponent<EnemyInfo>();
            }
            return enemyInfo;
        }

        // list of scriptable objects
        public List<StateData> dataLst = new List<StateData>();
        public void UpdateAll(EnemyState c, Animator a, AnimatorStateInfo asi)
        {
            for (int i = 0; i < dataLst.Count; ++i)
            {
                if (dataLst[i] == null) { Debug.LogWarning($"{dataLst[i].name} in OnStateUpdate is null"); return; }
                dataLst[i].OnUpdate(c, a, asi);
            }
        }

        override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            for (int i = 0; i < dataLst.Count; ++i)
            {
                if (dataLst[i] == null) { Debug.LogWarning($"{dataLst[i].name} in OnStateEnter is null"); return; }
                dataLst[i].OnEnter(this, animator, animatorStateInfo);
            }
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            UpdateAll(this, animator, animatorStateInfo);
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            for (int i = 0; i < dataLst.Count; ++i)
            {
                if (dataLst[i] == null) { Debug.LogWarning($"{dataLst[i].name} in OnStateExit is null"); return; }
                dataLst[i].OnExit(this, animator, animatorStateInfo);
            }
        }
    }
}
