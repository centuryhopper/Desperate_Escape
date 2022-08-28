using Pathfinding;
using UnityEngine;
using System;


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

            // Debug.Log(waitTime);

            // pick a random spot to move towards
            randomIndex = UnityEngine.Random.Range(0, wayPoints.Length);

            enemyInfo.repeatCall(nameof(UpdatePath), .5f,
            Vector2.Distance(rb.position, wayPoints[wayPointIndex % wayPoints.Length].position) / 2f > 2.5f ? 8f : 5f);

            enemyInfo.CallWithDelay(()=> UnityEngine.Debug.LogWarning($"hello there"), 3f);
        }

        public override void OnUpdate(EnemyState enemyState, Animator animator, AnimatorStateInfo asi)
        {
            // move from current position to randomly assignmed waypoint position

            pathFindToNextWaypoint();


        }

        public override void OnExit(EnemyState enemyState, Animator animator, AnimatorStateInfo asi)
        {

        }

        public void pathFindToNextWaypoint()
        {
            if (path == null)
            {
                UnityEngine.Debug.Log($"no path");
                // get out if there's no valid path to patrol
                return;
            }

            // if we traversed the entire path
            if (vectorPathIndex >= path.vectorPath.Count)
            {
                //Debug.Log("vectorPath index: " + vectorPathIndex);
                //Debug.Log("vectorPath size: " + path.vectorPath.Count);
                //Debug.Log("reached one of the waypoints");
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            float distToWayPt = Vector2.Distance(rb.position, wayPoints[wayPointIndex % wayPoints.Length].position);
            if (distToWayPt < .5f && wayPoints.Length == 1)
            {
                // Quaternion rotation = Quaternion.LookRotation(Vector3.up, Vector3.forward).normalized;
                //Debug.Log("back in place");
                enemyTransform.rotation = spawnRotation;
                return;
            }


            if ((Vector2)path.vectorPath[vectorPathIndex] != rb.position)
            {
                // vector2 that points from where the enemy currently is to the specified waypoint
                Vector2 lookDir = ((Vector2)path.vectorPath[vectorPathIndex] - rb.position).normalized;

                // force to apply to the enemy rigidbody
                Vector2 force = lookDir * speed * Time.deltaTime;
                rb.AddForce(force);

                float lookAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
                rb.rotation = lookAngle;
            }

            // distance that calculates how far the enemy will move
            float distance = Vector2.Distance(rb.position, path.vectorPath[vectorPathIndex]);

            if (distance < nextWayPointDist)
            {
                // wait for couple seconds
                //StartCoroutine(IdleEnemy());

                // increment
                ++vectorPathIndex;
                //Debug.Log("incrementing vectorPathIndex");
            }

        }

        void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                path = p;
                //Debug.Log("resetting vectorPathIndex to 0 and incrementing wayPoint");
                vectorPathIndex = 0;

                if (wayPoints.Length > 1)
                {
                    ++wayPointIndex;
                }
            }
            else
            {
                // Debug.Log("couldn't complete path calculation");
            }
        }

        void UpdatePath()
        {
            // may need to change this condition to check for how close the enemy is to the current waypoint DONE
            if (seeker.IsDone())
            {
                // Debug.Log("wayPointIndex: " + wayPointIndex);

                // wait for couple seconds
                //StartCoroutine(IdleEnemy());
                seeker.StartPath(rb.position, wayPoints[wayPointIndex % wayPoints.Length].position, OnPathComplete);
            }
            else
            {
                // Debug.Log(wayPointIndex % wayPoints.Length + " is not reached yet");
            }
        }

    }
}


