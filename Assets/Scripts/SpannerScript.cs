using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpannerScript : MonoBehaviour
{
    public GameObject[] fruitPrefabs;
    public GameObject bombPrefab;

    public Transform[] spawnPlaces;

    public int chanceToSpawnBomb = 10;
    public float minWait = 0.3f;
    public float maxWait = 1f;
    public float minForce = 12f;
    public float maxForce = 17f;

    private void Start()
    {
        StartCoroutine(SpawnFruitEnumerator()); 
    }

    private IEnumerator SpawnFruitEnumerator()
    {
        while (true)
        {
            float waitTime = Random.Range(minWait, maxWait);
            yield return new WaitForSeconds(waitTime);

            Debug.Log("Spawning Fruit after " + waitTime);
            Transform spawnPlace = spawnPlaces[Random.Range(0, spawnPlaces.Length)];

            GameObject bombOrFruitPrefab;
            if (Random.Range(0, 100) < this.chanceToSpawnBomb)
            {
                bombOrFruitPrefab = this.bombPrefab;
            } else
            {
                bombOrFruitPrefab = this.fruitPrefabs[Random.Range(0, this.fruitPrefabs.Length)];
            }

            GameObject bombOrFruit = Instantiate(bombOrFruitPrefab, spawnPlace.transform.position, spawnPlace.transform.rotation);

            bombOrFruit.GetComponent<Rigidbody2D>().AddForce(spawnPlace.transform.up * Random.Range(minForce, maxForce), ForceMode2D.Impulse);
            Destroy(bombOrFruit, 5); // Zerstören nach 5 Sekunden --> dann ist die Frucht mit Sicherheit nicht mehr zu sehen
        }
    }
}
