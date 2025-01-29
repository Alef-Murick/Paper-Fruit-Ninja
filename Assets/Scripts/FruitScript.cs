using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour
{
    public GameObject cut1;
    public GameObject cut2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Line"))
        {
            GameObject c1 = Instantiate(cut1, transform.position, cut1.transform.rotation) as GameObject;
            GameObject c2 = Instantiate(cut2, new Vector3(transform.position.x - 2f, transform.position.y, 0), cut2.transform.rotation) as GameObject;

            Destroy(gameObject);
        }
    }
}

