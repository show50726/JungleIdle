using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class board : MonoBehaviour
{
    public float sizeX = 9;
    public float sizeY = 9;

    public Color CanGoColor = Color.red;

    public GameObject tileQuad;
    public Quad playerQuad;
    private int px, py;

    private Quad[,] QuadArray = new Quad[10, 10];
    public Camera cam;

    int index;

    void creatBackground()
    {
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                GameObject tQuad = Instantiate(tileQuad) as GameObject;
                tQuad.transform.position = new Vector2(i - 4.3f, j - 4.3f);
                QuadArray[i + 1,j + 1] = tQuad.GetComponent<Quad>();
                tQuad.GetComponent<Quad>().c = Color.white;
                tQuad.GetComponent<Quad>().x = i + 1;
                tQuad.GetComponent<Quad>().y = j + 1;
                if (Mathf.Floor(sizeX/2) == i && Mathf.Floor(sizeX / 2) == j)
                {
                    playerQuad = tQuad.GetComponent<Quad>();
                    px = i+1;
                    py = j+1;
                    tQuad.GetComponent<Quad>().c = Color.blue;
                }
            }
        }
    }

    void Start()
    {
        creatBackground();

        //print(playerQuad.transform.position);
    }


    void Update()
    {
        if(px+1 <= sizeX)
        {
            QuadArray[px + 1,py].c = CanGoColor;
            QuadArray[px + 1,py].CanMoveTo = true;
        }
        if (px-1 >= 1)
        {
            QuadArray[px - 1,py].c = CanGoColor;
            QuadArray[px - 1,py].CanMoveTo = true;
        }
        if (py+1 <= sizeY)
        {
            QuadArray[px, py + 1].c = CanGoColor;
            QuadArray[px, py + 1].CanMoveTo = true;
        }
        if (py-1 >= 1)
        {
            QuadArray[px, py - 1].c = CanGoColor;
            QuadArray[px, py - 1].CanMoveTo = true;
        }

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Infinity;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit)
        {
            if (hit.transform.tag == "tile")
            {
                
                if (Input.GetMouseButtonDown(0) && hit.transform.GetComponent<Quad>().CanMoveTo == true)
                {

                    if (px + 1 <= sizeX)
                    {
                        QuadArray[px + 1, py].c = Color.white;
                        QuadArray[px + 1, py].CanMoveTo = false;
                    }
                    if (px - 1 >= 1)
                    {
                        QuadArray[px - 1, py].c = Color.white;
                        QuadArray[px - 1, py].CanMoveTo = false;
                    }
                    if (py + 1 <= sizeY)
                    {
                        QuadArray[px, py + 1].c = Color.white;
                        QuadArray[px, py + 1].CanMoveTo = false;
                    }
                    if (py - 1 >= 1)
                    {
                        QuadArray[px, py - 1].c = Color.white;
                        QuadArray[px, py - 1].CanMoveTo = false;
                    }

                    print(hit.transform.position.x);
                    playerQuad = hit.transform.GetComponent<Quad>();
                    hit.transform.GetComponent<Quad>().c = Color.blue;
                    px = playerQuad.x;
                    py = playerQuad.y;
                    print(hit.transform.position);    
                }
            }
        }

    }

}