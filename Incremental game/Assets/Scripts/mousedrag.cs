using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousedrag : MonoBehaviour
{
    private Vector3 mousePosition;
    private Rigidbody2D rb;
    private Vector2 direction;
    public float movespeed = 100f;
    private bool selected = false;
    private bool letgo = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMouseEnter()
    {
        if (!Input.GetMouseButton(0))
        {
            selected = true;
        }
    }


    private void OnMouseExit()
    {
        if (letgo)
        {
            selected = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && selected)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePosition - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * movespeed, direction.y * movespeed);
            letgo = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            letgo = true;
        }
    }
}
