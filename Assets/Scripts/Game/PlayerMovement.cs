using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //public KeyCode up;
    //public KeyCode down;
    private Rigidbody2D myRB;
    [SerializeField]
    private float speed;
    private Vector2 limits;
    private float limitInferior;
    public int player_lives = 4;
    private Vector2 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        //if (up == KeyCode.None) up = KeyCode.UpArrow;
        //if (down == KeyCode.None) down = KeyCode.DownArrow;
        myRB = GetComponent<Rigidbody2D>();
        SetMinMax();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0));
        float targetX = Mathf.Clamp(mouseWorldPosition.x, -limits.x, limits.x);
        float targetY = Mathf.Clamp(mouseWorldPosition.y, -limits.y, limits.y);
        Vector2 targetPosition = new Vector2(targetX, targetY);
        Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        myRB.MovePosition(newPosition);
        //if (Input.GetKey(up) && transform.position.y < limits)
        //{
        //    myRB.velocity = new Vector2(0f, speed);
        //}
        //else if (Input.GetKey(down) && transform.position.y > limitInferior)
        //{
        //    myRB.velocity = new Vector2(0f, -speed);
        //}
        //else
        //{
        //    myRB.velocity = Vector2.zero;
        //}
    }

   
    public void OnMousePosition(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }
    private void SetMinMax()
    {
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        limits = new Vector2(bounds.x, bounds.y);
    }
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Candy")
        {
            CandyGenerator.instance.ManageCandy(other.gameObject.GetComponent<CandyController>(), this);
        }
    }
}
