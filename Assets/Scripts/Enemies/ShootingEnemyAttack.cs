using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EmissionColorAnimator))]
public class ShootingEnemyAttack : MonoBehaviour
{
    public GameObject Bullet;

    public Transform Gun;

    public float AfterShotPause;

    bool W = false;
    bool A = false;
    bool E = false;

    bool attacking = false;

    float timer = 0;

    public int Damage;

    public float KnockbackStrength;

    public float bulletLife;

    public float bulletSpeed;

    public MeshRenderer laserCoilMeshRenderer;

    private EmissionColorAnimator _emissionColorAnimator;


    private void Start()
    {
        _emissionColorAnimator = GetComponent<EmissionColorAnimator>();
        if (laserCoilMeshRenderer != null)
        {
            var material = laserCoilMeshRenderer.material;
            _emissionColorAnimator.SetColorFromTime(material, 1f);
        }
    }

    public void CheckAttack(EnemyState state)
    {
        if (attacking == true)
        {
            timer -= Time.deltaTime;


            if (A == false)
            {
                A = true;
                GameObject B = Instantiate(Bullet, Gun.position, Gun.rotation);
                B.GetComponent<EnemyBullet>().SetParameters(Damage, bulletSpeed, KnockbackStrength, bulletLife);
            }

            if (timer <= 0)
            {
                attacking = false;
                state.Attacking = false;
            }
        }
        else if (state.Attacking == true)
        {
            Attack();
        }
    }

    private void Attack()
    {
        timer = AfterShotPause;
        W = true;
        A = false;
        E = false;
        attacking = true;
        if (laserCoilMeshRenderer != null)
        {
            _emissionColorAnimator.Animate(laserCoilMeshRenderer.material, AfterShotPause);
        }
    }
}