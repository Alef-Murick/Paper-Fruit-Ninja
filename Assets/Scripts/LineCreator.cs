using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class LineCreator : MonoBehaviour
{
    int vertexCount = 0;
    bool mouseDown = false;
    LineRenderer line;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
        }
        if (mouseDown)
        {
            line.positionCount = vertexCount + 1;
            UnityEngine.Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            line.SetPosition(vertexCount, mousePos);
            vertexCount++;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
            vertexCount = 0;
            line.positionCount = 0;
        }
    }
}
