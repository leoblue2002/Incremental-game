using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousedrag : MonoBehaviour
{
    private Vector3 mousePosition;
    private Rigidbody2D rb;
    private Vector2 direction;
    public float movespeed = 75f;
    public float rotatespeed = 75;

    public bool selected = false;
    public bool letgo = true;
    public bool exited = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMouseEnter()
    {
        if (!Input.GetMouseButton(0))
        {
            this.selected = true;
        }
        exited = false;
    }

    private void OnMouseExit()
    {
        if (!Input.GetMouseButton(0))
        {
            this.selected = false;
        }
        exited = true;
    }

    private void OnMouseUp()
    {
        if (exited)
        {
            letgo = true;
            selected = false;
        }
    }

    private void FixedUpdate()
    {
        float eh = Input.GetAxis("Horizontal") * rotatespeed * Time.deltaTime * -1;
        if (Input.GetMouseButton(0) && selected)
        {
            rb.AddTorque(eh, ForceMode2D.Force);
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && selected)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePosition - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * movespeed, direction.y * movespeed);
            letgo = false;
        }

    }
}
