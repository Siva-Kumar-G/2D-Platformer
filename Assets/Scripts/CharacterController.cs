using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Rigidbody2D rb;
    private void Start()
    {

    }
    private void Update()
    {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(new Vector2(10, 0));
        }





        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(new Vector2(-10, 0));
        }
    }
}
