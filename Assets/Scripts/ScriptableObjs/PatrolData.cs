using Pathfinding;
using UnityEngine;
using System;
using UnityEngine.AI;


namespace ScriptableObjs
{
    [CreateAssetMenu(fileName = "PatrolData", menuName = "EnemyState/PatrolData", order = 0)]
    public class PatrolData : StateData
    {
        /*

        ***get MonoBehaviour data here***

    Examples:
        private PlayerMovement playerMovement;
            public PlayerMovement GetPlayerMoveMent(Animator animator)
            {
                if (playerMovement == null)
                {
                    // was getcomponentinparent
                    playerMovement = animator.GetComponentInParent<PlayerMovement>();
                }

                return playerMovement;
            }

            private EnemyMovement enemyMovement;

            public EnemyMovement GetEnemyMovement(Animator animator)
            {
                if (enemyMovement == null)
                {
                    enemyMovement = animator.GetComponentInParent<EnemyMovement>();
                }

                return enemyMovement;
            }
        */

        // how fast the ai is patrolling
        [SerializeField] public float speed;
        [SerializeField] private float setWaitTime;
        [SerializeField] private float tolerableDistToWaypt = 3f;
        private float waitTime;
        private Transform[] wayPoints;
        private int randomIndex;
        private EnemyInfo enemyInfo;
        private Rigidbody2D rb;
        private Quaternion spawnRotation;
        private Transform enemyTransform;
        private NavMeshAgent agent;
        private Vector3 nextWayPoint;
        private LineRenderer lr;

        public override void OnEnter(EnemyState enemyState, Animator animator, AnimatorStateInfo asi)
        {
            // initialize waitTime
            waitTime = setWaitTime;
            enemyInfo = enemyState.GetEnemyInfo(animator);
            wayPoints = enemyInfo.WayPoints;
            rb = enemyInfo.rb;
            spawnRotation = enemyInfo.spawnRotation;
            enemyTransform = enemyInfo.enemyTransform;

            // pick a random spot to move towards
            agent = enemyInfo.agent;
            nextWayPoint = getNewWaypoint();
            agent.SetDestination(nextWayPoint);
            lr = enemyInfo.lr;

            lr.startWidth = 0.15f;
            lr.endWidth = 0.15f;
            lr.positionCount = 0;
        }

        public override void OnUpdate(EnemyState enemyState, Animator animator, AnimatorStateInfo asi)
        {
            visualizePath();
            updateLookDirection();
            // move from current position to randomly assignmed waypoint position
            Patrol();
        }

        public override void OnExit(EnemyState enemyState, Animator animator, AnimatorStateInfo asi)
        {

        }

        private Vector3 getNewWaypoint()
        {
            randomIndex = UnityEngine.Random.Range(0, wayPoints.Length);
            return wayPoints[randomIndex].position;
        }

        // update look in the direction of movement
        private void updateLookDirection()
        {
            if (agent.hasPath)
            {
                if (agent.path.corners.Length >= 2)
                {
                    for (int i = 1; i < agent.path.corners.Length;++i)
                    {

                        Vector2 lookDirection = (Vector2)(agent.path.corners[i]) - rb.position;

                        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;

                        rb.rotation = lookAngle;
                    }
                }

            }

        }

        private void visualizePath()
        {
            if (agent.hasPath)
            {
                lr.positionCount = agent.path.corners.Length;
                lr.SetPosition(0, rb.transform.position);
                if (agent.path.corners.Length < 2)
                    return;
                for (int i = 1; i < agent.path.corners.Length;++i)
                {
                    var pointPosition = new Vector2(agent.path.corners[i].x, agent.path.corners[i].y);
                    lr.SetPosition(i, pointPosition);
                }
            }
        }

        private void Patrol()
        {
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                // delay for a little bit
                if (waitTime <= 0)
                {
                    nextWayPoint = getNewWaypoint();
                    agent.SetDestination(nextWayPoint);
                    waitTime = setWaitTime;
                }
                waitTime -= Time.deltaTime;
            }


        }

    }
}


