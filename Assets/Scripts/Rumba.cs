using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rumba : MonoBehaviour
{
    public Transform InitDir;
    public float speed;
    public float rotation;

    private Vector3 direction;
    private Rigidbody body;

    private LaserBeam[] beams;

    public float delat, delay2;
    public float interval;

    public GameObject Saw;

    public float SawRot;
    
    private void Awake()
    {
        direction = (InitDir.position - transform.position).normalized;
        body = GetComponent<Rigidbody>();
        beams = GetComponentsInChildren<LaserBeam>();
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(delat);
        body.isKinematic = false;

        yield return new WaitForSeconds(delay2);
        while (true)
        {
            Attack();
            yield return new WaitForSeconds(interval);
        }
    }

    private void FixedUpdate()
    {
        body.velocity = direction * speed;
        
    }

    private void Update()
    {
        Saw.transform.Rotate(0f, SawRot * Time.deltaTime, 0f);
        if(!body.isKinematic)
        transform.Rotate(0f, rotation * Time.deltaTime, 0f);
    }

    [ContextMenu("Attack")]
    public void Attack()
    {
        foreach (var beam in beams)
        {
            beam.Shoot();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Player")
        {
            direction = Vector3.Reflect(direction, other.contacts[0].normal);
            direction.y = 0;
            direction = direction.normalized;
        }
        else
        {
            other.rigidbody.GetComponent<PlayerDeath>().SetDeath();
        }
    }
}
