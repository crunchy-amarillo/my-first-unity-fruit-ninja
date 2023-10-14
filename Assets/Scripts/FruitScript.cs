using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour
{
    public GameObject slicedFruitPrefab;
    public float explosionForce = 5f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        BladeScript blade = collision.GetComponent<BladeScript>();
        if (!blade)
        {
            return;
        }

        CreateSlicedFruit();
    }

    public void CreateSlicedFruit()
    {
        GameObject slicedFruit = Instantiate(this.slicedFruitPrefab, transform.position, transform.rotation);

        // Früchte enthalten Children (Teile des 3D Objektes)
        Rigidbody[] rigidbodySlicedChildren = slicedFruit.transform.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigidbodySlicedChild in rigidbodySlicedChildren)
        {
            rigidbodySlicedChild.transform.rotation = Random.rotation;
            rigidbodySlicedChild.AddExplosionForce(Random.Range(500, 1000), transform.position, this.explosionForce);
        }

        FindObjectOfType<GameManager>().IncreaseScore(1); // FindObjectOfType ist recht inperformant und sollte in Update nicht genutzt werden

        Destroy(gameObject);
        Destroy(slicedFruit, 5);
    }
}
