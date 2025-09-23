using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health = 10;
    private int currentHealth;
    public float moveSpeed = 2f;
    private Transform player;
    public PlayerShooting playerShooting;
    private void Start()
    {
        playerShooting = FindObjectOfType<PlayerShooting>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = Health;
    }

    private void Update()
    {
        if (player == null) return;

        //플레이어 방향구하기
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(transform.position);
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
        Debug.Log($"{currentHealth} / {Health}");
        if (currentHealth < 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
