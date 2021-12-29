using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Color _emissionColor;
    private readonly Color _noEmissionColor = Color.black;
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

    private void Start()
    {
        if (laserCoilMeshRenderer != null)
        {
            var material = laserCoilMeshRenderer.material;
            _emissionColor = material.GetColor(EmissionColor);
            SetEmission(material, 0);
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

            if (laserCoilMeshRenderer != null)
            {
                SetEmission(laserCoilMeshRenderer.material, Mathf.Clamp01(timer / AfterShotPause));
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
    }

    private void SetEmission(Material material, float fraction)
    {
        material.SetColor(EmissionColor, Vector4.Lerp(_noEmissionColor, _emissionColor, fraction));
    }
}