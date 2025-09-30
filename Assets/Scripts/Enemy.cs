using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyState
{
    Idle,               //대기 상태
    Trace,              //추적 상태
    Attack,             //공격 
    RunAway             //도망
}

public class Enemy : MonoBehaviour
{
    public int Health = 10;
    private int currentHealth;
    
    public float moveSpeed = 2f;
    public float traceRange = 15f;
    public float attackRange = 6f;
    public float runRange = 20f;
    public float attackCooldown = 1.5f;

    public Slider hpSlider;
    public GameObject projectilePrefab;
    public Transform firePoint;

    public PlayerShooting playerShooting;
    
    private Transform player;

    private float lastAttackTime;

    public EnemyState state = EnemyState.Idle;

    private void Start()
    {
        playerShooting = FindObjectOfType<PlayerShooting>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastAttackTime = -attackCooldown;
        currentHealth = Health;
        hpSlider.value = 1f;
    }

    private void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(player.position, transform.position);
        


        switch(state)
        {
            case EnemyState.Idle:
                if (dist < traceRange)
                {
                    state = EnemyState.Trace;
                }
                break;

            case EnemyState.Trace:
                if (dist < attackRange)
                {
                    state = EnemyState.Attack;
                }
                else if (dist > traceRange)
                {
                    state = EnemyState.Idle;
                }
                else
                {
                    TracePlayer();
                }
                break;
            
            case EnemyState.Attack:
                if (dist > attackRange)
                {
                    state = EnemyState.Trace;
                }
                else
                {
                    AttackPlayer();
                }
                break;
            
            case EnemyState.RunAway:
                RunAway();
                if (dist > runRange)
                {
                    state = EnemyState.Idle;
                }
                break;
        }

        //플레이어 방향구하기
        //Vector3 direction = (player.position - transform.position).normalized;
        //transform.position += direction * moveSpeed * Time.deltaTime;
        //transform.LookAt(transform.position);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        currentHealth -= playerShooting.currentWeapon.weaponDamage;
        hpSlider.value = (float)currentHealth / Health;
        if (currentHealth <= 3) state = EnemyState.RunAway;
        if (currentHealth < 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void TracePlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.LookAt(player.position);
    }
    
    void AttackPlayer()
    {
        //일정 쿨다운 마다 발사
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            ShootProjectile();
        }
    }

    void RunAway()
    {

        Vector3 dir = (player.position - transform.position).normalized;
        transform.position -= dir * moveSpeed * Time.deltaTime;
        transform.LookAt(player.position);

    }

    void ShootProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            transform.LookAt(player.position);

            GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            EnemyProjectile ep = proj.GetComponent<EnemyProjectile>();

            if (ep != null)
            {
                Vector3 dir = (player.position - firePoint.position).normalized;
                ep.SetDirection(dir);
            }
        }
    }
}
