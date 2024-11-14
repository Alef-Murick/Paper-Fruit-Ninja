using System.Collections;
using System.Collections.Generic;
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
        if (Application.platform == RuntimePlatform.Android) // Android Controller
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    line.positionCount = vertexCount + 1;
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    line.SetPosition(vertexCount, mousePos);
                    vertexCount++;

                    BoxCollider2D box = gameObject.AddComponent<BoxCollider2D>();
                    box.transform.position = line.transform.position;
                    box.size = new Vector2(0.1f, 0.1f);
                }

                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    if (Input.GetMouseButtonUp(0))
                    {
                        mouseDown = false;
                        vertexCount = 0;
                        line.positionCount = 0;
                        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
                        foreach (BoxCollider2D box in colliders)
                        {
                            Destroy(box);
                        }
                    }
                }
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer) // Mouse Controller
        {

            if (Input.GetMouseButtonDown(0))
            {
                mouseDown = true;
            }
            if (mouseDown)
            {
                line.positionCount = vertexCount + 1;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                line.SetPosition(vertexCount, mousePos);
                vertexCount++;

                BoxCollider2D box = gameObject.AddComponent<BoxCollider2D>();
                box.transform.position = line.transform.position;
                box.size = new Vector2(0.1f, 0.1f);
            }
            if (Input.GetMouseButtonUp(0))
            {
                mouseDown = false;
                vertexCount = 0;
                line.positionCount = 0;
                BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
                foreach (BoxCollider2D box in colliders)
                {
                    Destroy(box);
                }
            }
        }
    }
}
