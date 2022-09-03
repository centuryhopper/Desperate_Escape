using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjs;
using Pathfinding;
using System;
using UnityEngine.AI;

public class EnemyInfo : MonoBehaviour
{
    [SerializeField] Transform[] wayPoints;
    public Transform[]  WayPoints { get => wayPoints; private set {} }

    public Seeker seeker {get; private set;}
    public Rigidbody2D rb {get; private set;}

    public Quaternion spawnRotation {get; private set;}
    public Transform enemyTransform {get; private set;}
    public NavMeshAgent agent {get; private set;}
    public Transform navMeshChild {get; private set;}
    public LineRenderer lr {get; private set;}




    void Awake()
    {
        spawnRotation = transform.rotation;
        enemyTransform = this.transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        lr = GetComponent<LineRenderer>();

/*

By default NavMeshAgent component tends to rotate the GameObject as soon as you hit play. This can be undesirable (as in, your object may be rotate away from your plane of view and hence become invisible), so you should fix its rotation

*/
        agent.updateRotation = false;
		agent.updateUpAxis = false;
    }

}
