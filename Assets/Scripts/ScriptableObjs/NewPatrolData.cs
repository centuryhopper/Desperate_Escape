using Pathfinding;
using UnityEngine;
using System;


namespace ScriptableObjs
{
    [CreateAssetMenu(fileName = "NewPatrolData", menuName = "EnemyState/NewPatrolData", order = 0)]
    public class NewPatrolData : StateData
    {
        // how fast the ai is patrolling
        [SerializeField] public float speed;
        [SerializeField] private float setWaitTime;
        [SerializeField] private float tolerableDistToWaypt = 3f;
        private float waitTime;
        private Transform[] wayPoints;
        private int randomIndex;
        private EnemyInfo enemyInfo;
        private Seeker seeker;
        private Rigidbody2D rb;
        private Quaternion spawnRotation;
        private Transform enemyTransform;
        private bool reachedEndOfPath = false;
        public float nextWayPointDist = 3f;
        private Path path;
        private int wayPointIndex = 0;
        private int vectorPathIndex = 0;



        public override void OnEnter(EnemyState enemyState, Animator animator, AnimatorStateInfo asi)
        {
            // initialize waitTime
            waitTime = setWaitTime;
            enemyInfo = enemyState.GetEnemyInfo(animator);
            wayPoints = enemyState.GetEnemyInfo(animator).WayPoints;
            seeker = enemyInfo.seeker;
            rb = enemyInfo.rb;
            spawnRotation = enemyInfo.spawnRotation;
            enemyTransform = enemyInfo.enemyTransform;

            // pick a random spot to move towards
            randomIndex = UnityEngine.Random.Range(0, wayPoints.Length);

            // enemyInfo.repeatCall(nameof(UpdatePath), .5f,
            // Vector2.Distance(rb.position, wayPoints[wayPointIndex % wayPoints.Length].position) / 2f > 2.5f ? 8f : 5f);

        }

        public override void OnUpdate(EnemyState enemyState, Animator animator, AnimatorStateInfo asi)
        {

        }

        public override void OnExit(EnemyState enemyState, Animator animator, AnimatorStateInfo asi)
        {

        }

    }
}


