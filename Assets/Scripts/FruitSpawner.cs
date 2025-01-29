using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject fruit;
    public GameObject bomb;
    public float maxX;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartSpawning", 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartSpawning()
    {
        InvokeRepeating("SpawnFruitGroups", 1, 6f);
    }

    public void StopSpawning()
    {
        CancelInvoke("SpawnFruitGroups");
        StopCoroutine("SpawnFruit");
    }

    public void SpawnFruitGroups()
    {
        StartCoroutine("SpawnFruit");

        if (Random.Range(0, 6) > 4)
        {
            SpawnBomb();
        }
    }

    IEnumerator SpawnFruit()
    {
        for (int i = 0; i < 5; i++)
        {
            float RandomX = Random.Range(-maxX, maxX);
            float RandomY = Random.Range(12f, 18f);
            float RandomZ = Random.Range(-20f, 20f);

            Vector3 pos = new Vector3(RandomX, transform.position.y, 0);
            GameObject f = Instantiate(fruit, pos, Quaternion.identity);

            f.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, RandomY), ForceMode2D.Impulse);
            f.GetComponent<Rigidbody2D>().AddTorque(RandomZ);

            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnBomb()
    {
        float RandomX = Random.Range(-maxX, maxX);
        float RandomY = Random.Range(12f, 18f);
        float RandomZ = Random.Range(-50f, 50f);

        Vector3 pos = new Vector3(RandomX, transform.position.y, 0);
        GameObject b = Instantiate(bomb, pos, Quaternion.identity);

        b.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, RandomY), ForceMode2D.Impulse);
        b.GetComponent<Rigidbody2D>().AddTorque(RandomZ);
    }
}
