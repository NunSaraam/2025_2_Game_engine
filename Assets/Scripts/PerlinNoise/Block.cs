using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Dirt,
    Grass,
    Water,

}

public class Block : MonoBehaviour
{
    public Sprite itemicon;
    public int maxStack = 64;

    public BlockType type = BlockType.Dirt;

    public int maxHP = 3;

    [HideInInspector] public int hp;

    public int dropCount = 1;
    public bool mineable = true;

    public Block blockPrefab;

    private void Awake()
    {
        hp = maxHP;
        if (GetComponent<Collider>() == null) gameObject.AddComponent<BoxCollider>();

        if (string.IsNullOrEmpty(gameObject.tag) || gameObject.tag == "Untaged")
        {
            gameObject.tag = "Block";
        }
    }

    public void Hit(int damage)
    {
        if (!mineable) return;

        hp -= damage;

        if (hp <= 0)
        {
            if (InventoryManager.Instance != null)
            {
                if (blockPrefab == null)
                {
                    return;
                }

                bool added = InventoryManager.Instance.AddItem(blockPrefab, dropCount);

                if (added)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
