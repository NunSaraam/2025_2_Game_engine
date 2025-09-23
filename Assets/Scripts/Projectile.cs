using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public WeaponSO weapon;
    public float speed;
    public float lifeTime;
    public int damage;

    private void Start()
    {
        //일정 시간 후 자동 삭제 (메모리 관리)
        Destroy(gameObject, lifeTime);
        damage = weapon.weaponDamage;
        speed = weapon.fireSpeed;
        lifeTime = weapon.lifeTime;
    }

    private void Update()
    {
        //로컬의 foward 방향(앞)으로 이동
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Projectile 제거
            Destroy(gameObject);
        }
    }

}
