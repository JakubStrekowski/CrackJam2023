using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LaserBeam : MonoBehaviour
{
    public float MaxDistance;
    public LayerMask LayerMask;
    public float Radius;
    public Transform ShootingTransform;
    public SingleLaserBeam[] Beams;
    public ParticleSystem Particles;
    public Animator Animator;
    public bool UpdateStuff;
    public bool DealDamage;

    public GameObject LaserEnd;

    public MeshRenderer PifPafRenderer;
    [ColorUsage(false, true)] public Color PifPafColor;

    [ContextMenu("Shoot")]
    public void Shoot()
    {
        Animator.SetTrigger("Shoot");
    }

    private void LateUpdate()
    {
        if (UpdateStuff)
        {
            var distance = MaxDistance;
            if (Physics.SphereCast(ShootingTransform.position, Radius, ShootingTransform.forward, out RaycastHit hit,
                MaxDistance, LayerMask))
            {
                distance = hit.distance + ShootingTransform.localPosition.z;
            }

            var particlesShape = Particles.shape;
            particlesShape.length = distance;

            foreach (var beam in Beams)
            {
                beam.Distance = distance + .5f;
            }

            LaserEnd.transform.localPosition = Vector3.forward * distance;
        }

        var prop = new MaterialPropertyBlock();
        prop.SetColor("_Glow", PifPafColor);
            
        PifPafRenderer.SetPropertyBlock(prop);
        
        LaserEnd.SetActive(DealDamage);
    }
}
