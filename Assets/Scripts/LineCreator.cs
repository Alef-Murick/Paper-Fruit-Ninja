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
        //else if (Application.platform == RuntimePlatform.WindowsPlayer) // Mouse Controller
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseDown = true;
            }

            if (mouseDown)
            {
                line.positionCount = vertexCount + 1;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0f;  // Ensure the z-value is zero for 2D positioning
                line.SetPosition(vertexCount, mousePos);
                vertexCount++;

                // Check if the PolygonCollider2D already exists, if not, add it
                PolygonCollider2D polygonCollider = gameObject.GetComponent<PolygonCollider2D>();
                if (polygonCollider == null)
                {
                    polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
                }

                // Update the collider points based on the line's positions
                Vector3[] positions = new Vector3[line.positionCount];
                line.GetPositions(positions);

                Vector2[] colliderPoints = new Vector2[positions.Length];
                for (int i = 0; i < positions.Length; i++)
                {
                    colliderPoints[i] = new Vector2(positions[i].x, positions[i].y);
                }

                // Set the new path for the PolygonCollider2D
                polygonCollider.SetPath(0, colliderPoints);
            }

            if (Input.GetMouseButtonUp(0))
            {
                mouseDown = false;
                vertexCount = 0;
                line.positionCount = 0;

                // Optionally, destroy the PolygonCollider2D when the line is finished
                PolygonCollider2D polygonCollider = GetComponent<PolygonCollider2D>();
                if (polygonCollider != null)
                {
                    Destroy(polygonCollider);
                }
            }
        }
    }
}
