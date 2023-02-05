using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LaserBeam : MonoBehaviour
{
    public bool ConstantShooting;
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
    private static readonly int Shooting = Animator.StringToHash("Shooting");
    private static readonly int Shoot1 = Animator.StringToHash("Shoot");
    private static readonly int Glow = Shader.PropertyToID("_Glow");

    [ContextMenu("Shoot")]
    public void Shoot()
    {
        Animator.SetTrigger(Shoot1);
    }

    private void Update()
    {
        Animator.SetBool(Shooting, ConstantShooting);
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

                if (DealDamage && hit.rigidbody != null && hit.rigidbody.CompareTag("Player"))
                {
                    hit.rigidbody.GetComponent<PlayerDeath>().TrySetDeath();
                }
            }

            var particlesShape = Particles.shape;
            particlesShape.length = distance;

            foreach (var beam in Beams)
            {
                beam.Distance = distance + .5f;
            }

            LaserEnd.transform.position = transform.position + transform.forward * distance;
        }

        var prop = new MaterialPropertyBlock();
        prop.SetColor(Glow, PifPafColor);
            
        PifPafRenderer.SetPropertyBlock(prop);
        
        LaserEnd.SetActive(DealDamage);
    }
}
