using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjs;
using Pathfinding;
using System;

public class EnemyInfo : MonoBehaviour
{
    [SerializeField] Transform[] wayPoints;
    public Transform[]  WayPoints { get => wayPoints; private set {} }

    public Seeker seeker {get; private set;}
    public Rigidbody2D rb {get; private set;}

    public Quaternion spawnRotation {get; private set;}
    public Transform enemyTransform {get; private set;}

    void Awake()
    {
        spawnRotation = transform.rotation;
        enemyTransform = this.transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void repeatCall(string functionName, float time, float repeatRate)
    {
        InvokeRepeating(functionName, time, repeatRate);
    }
}
