using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    
    public float movePower = 2f;

   
    Animator animator;
    SpriteRenderer spriteRenderer;



    Vector3 movement;

    int movementFlag = 0;




    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

   
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        StartCoroutine("ChangeMovement");
    }

    IEnumerator ChangeMovement()
    {
        movementFlag = Random.Range(0, 3);


        yield return new WaitForSeconds(1.5f);

        StartCoroutine("ChangeMovement");
    }
    // Update is called once per frame
    void Update()
    {
        //Direction Sprite
        
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if(movementFlag == 1)
        {

            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(32, 36, 1);
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        }
        else if (movementFlag == 2)
        {

            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(32, 36, 1);

        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }
}
