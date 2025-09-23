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
        //���� �ð� �� �ڵ� ���� (�޸� ����)
        Destroy(gameObject, lifeTime);
        damage = weapon.weaponDamage;
        speed = weapon.fireSpeed;
        lifeTime = weapon.lifeTime;
    }

    private void Update()
    {
        //������ foward ����(��)���� �̵�
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Projectile ����
            Destroy(gameObject);
        }
    }

}
