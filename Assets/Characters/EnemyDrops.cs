using UnityEditor.Rendering;
using UnityEngine;

public class EnemyDrops : MonoBehaviour
{
    public GameObject[] dropItems;
    public float dropChance = 0.5f;

    public void Destroy()
    {
        if (Random.value < dropChance)
        {
            DropItem();
        }
    }

    void DropItem()
    {
        GameObject itemToDrop = dropItems[Random.Range(0, dropItems.Length)];

        Instantiate(itemToDrop, transform.position, Quaternion.identity);
    }
}
