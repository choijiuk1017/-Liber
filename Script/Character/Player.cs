using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject Hp; //�÷��̾� ü�� ��Ʈ �̹��� 1
    public GameObject Hp2; //�÷��̾� ü�� ��Ʈ �̹��� 2
    public GameObject Hp3; //�÷��̾� ü�� ��Ʈ �̹��� 3
    public GameObject player; //�÷��̾�
    public GameObject Panel; //���� ���� ĵ����
    public GameObject Panel2; //ESC ĵ���� 
    public GameManager manager; //GameManager

    GameObject scanObject; //NPC�� ���� ������Ʈ
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    Animator animator;
    [SerializeField]
    Transform pos;
    [SerializeField]
    float checkRadius;
    [SerializeField]
    LayerMask islayer;


    bool isGround;

    public int maxHealth = 3; //�ִ� ü���� 3���� ����

    public int health = 3;

    public float movePower;
    public float maxSpeed;
    public float JumpPower;

    bool isJumping = false;

    Vector3 dirVec;


    public void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        //������Ʈ�� ������

        health = maxHealth;
        //ü���� �ִ�ü������ ����, 3���� ����

    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //����
        if(Input.GetKeyDown(KeyCode.Space))
        {
                isJumping = true;   
        }
        

        //stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 10f, rigid.velocity.y); // Nomalized�� �̿��Ͽ� �÷��̾��� �̵� �ӵ� �����ϵ��� ��
        }


        //Direction Sprite
        if (Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1; //�÷��̾ ���������� ���� �������� ���� �������� ���� ������ ���� ������ ��
        }

        //�ȱ� �ִϸ��̼� ���
        if (Input.GetAxisRaw("Horizontal")== 0) //�÷��̾ �������� ������ �ִϸ��̼� ��� x
        {
            animator.SetBool("isWalking", false);
        }
        else if(Input.GetAxisRaw("Horizontal") < 0) //�÷��̾ �����̸� �ִϸ��̼� ���
        {
            animator.SetBool("isWalking", true);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            animator.SetBool("isWalking", true);
        }

        spriteRenderer.flipX = Input.GetAxisRaw("Horizontal")  == -1;
       

        //�÷��̾� ����
        if(health == 0) //�÷��̾��� ü���� 0�� �Ǹ�
        {
            Die(); //���
            return;
        }


        if(Input.GetKeyDown("escape"))//ESCŰ�� �Է½�
        {
            Esc(); //ESC�Լ� ȣ��
        }

        //�÷��̾� ���� Ray ����� ����ϱ� ���Ͽ� �÷��̾ �ٶ󺸴� ���� ����
        if(Input.GetButtonDown("Horizontal") && Input.GetKeyDown(KeyCode.RightArrow))
        {
            dirVec = Vector3.right; //�÷��̾ ������ �̵�Ű�� ������ �������� ���ٰ� ����
        }
        else if(Input.GetButtonDown("Horizontal") && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            dirVec = Vector3.left; //�÷��̾ ���� �̵�Ű�� ������ ������ ���ٰ� ����
        }
        else if(Input.GetButtonDown("Jump") && Input.GetKeyDown(KeyCode.Space)) //�������� ��� �Ʒ��� �κ� ����
        {
            dirVec = Vector3.down; //�÷��̾ ������ �ϸ� �Ʒ����� ���ٰ� ����
        }

        //I Ű ������ ��ȭ
        if (Input.GetKeyDown(KeyCode.I) && scanObject != null) //�÷��̾ IŰ�� �Է� �ް� scanObject�� ���� ���
        {
            manager.Action(scanObject); // �÷��̾ IŰ�� ������ ��� GameManager�� Action�Լ� ȣ�� 
        }

       
    }

    void FixedUpdate()
    {
        Move();
        Jump();

        if (health == 0) //Hp�� 0�̸� ������ �� ����
            return; 

        //�÷��̾� �̵� ����
        float h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal"); //Action�Լ��� ���θ� ������ ���� �̿��Ͽ� �÷��̾ ��ȭ���� ��� �̵��� �Ұ��ϵ��� ����

        //NPC ���� ���
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0,0,0,0)); //�ٶ󺸴� �������� Ray ���, ���ӿ��� �߶ߴ��� Ȯ���ϱ� ���� �ʷϻ����� ��
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("NPC")); //Ray�� NPC�� ����� Ȯ��

        if(rayHit.collider != null) //������
        {
            scanObject = rayHit.collider.gameObject; //scanObject�� NPC�� Ȯ��,Null�� �ƴ�
        }
        else //���� ������
        {
            scanObject = null; //scanObject�� Null��
        }
    }

    //�÷��̾� �ǰ� 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Monster") //Monster �±װ� ���� ������Ʈ�� �浹��
        {

            OnDamaged(); //������ �Ծ��� ��� ���� �ð� ���� �Լ� ȣ��

            health--; //ü���� 1�� ����
            if (health == 2) //ü���� 2�Ͻ�  
            {
                Hp3.SetActive(false); //HP �ϳ� ������
            }
            if (health == 1) //ü���� 1�Ͻ�
            {
                Hp2.SetActive(false); //HP �ϳ� �� ������
            }
            if (health == 0) //ü���� 0�Ͻ�
            {
                Hp.SetActive(false); //HP�� �ϳ� �� ������
            }
        }

        if (collision.gameObject.tag == "DeadZone") //�÷��̾ DeadZone �±װ� ���� ��ü�� ���� ���
        {
            Die();  //����
        }

    }

    void Move() //�÷��̾� �̵�
    {
        Vector3 moveVelocity = Vector3.zero;

        if(Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;
        }

        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    void Jump() //�÷��̾� ����
    {
        if (!isJumping)
            return;

        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, JumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }


    //�ǰݽ� ���� �ð�
    void OnDamaged()
    {
        gameObject.layer = 6; //���̾� 6, �� �÷��̾��� ���̾� ����

        spriteRenderer.color = new Color(1, 1, 1, 0.4f); //�ǰݽ� ��� ��������

        Invoke("OffDamaged", 1); //�ٽ� ������ �Ա�� ��������������� 1�� ����
    }

    void OffDamaged() 
    {
        gameObject.layer = 6; //�÷��̾��� ���̾
        spriteRenderer.color = new Color(1, 1, 1, 1); //������� ���ƿ�
    }

    //�÷��̾� ���� �Լ�
    public void Die()
    {
        Time.timeScale = 0; //�ð��� ����
        player.SetActive(false); //�÷��̾� �����
        Panel.SetActive(true);  //���� ���� ȭ���� �˾�
    }

    //ESC �Լ�
    public void Esc() 
    {
        Time.timeScale = 0; //�ð��� ����
        Panel2.SetActive(true); //ESCȭ�� �˾�
    }
    
  
}
   
