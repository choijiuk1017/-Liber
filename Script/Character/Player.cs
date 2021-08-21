using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject Hp; //플레이어 체력 하트 이미지 1
    public GameObject Hp2; //플레이어 체력 하트 이미지 2
    public GameObject Hp3; //플레이어 체력 하트 이미지 3
    public GameObject player; //플레이어
    public GameObject Panel; //게임 오버 캔버스
    public GameObject Panel2; //ESC 캔버스 
    public GameObject Panel3; //인벤토리 캔버스
    public GameManager manager; //GameManager

    GameObject scanObject; //NPC와 같은 오브젝트
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    Animator animator;

    public int maxHealth = 3; //최대 체력은 3으로 설정

    int health = 3; 

    public float maxSpeed;
    public float JumpPower;

    Vector3 dirVec;


    public void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        //컴포넌트들 가져옴

        health = maxHealth;
        //체력을 최대체력으로 설정, 3으로 설정

    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //jump
        if (Input.GetButtonDown("Jump")) //jump버튼을 눌렀을 경우
        {
           rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse); //점프
       
        }

        //stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 10f, rigid.velocity.y); // Nomalized를 이용하여 플레이어의 이동 속도 일정하도록 함
        }


        //Direction Sprite
        if (Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1; //플레이어가 오른쪽으로 가면 오른쪽을 보고 왼쪽으로 가면 왼쪽을 보고 가도록 함
        }

        //걷기 애니메이션 출력
        if (rigid.velocity.normalized.x == 0)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }

        spriteRenderer.flipX = Input.GetAxisRaw("Horizontal")  == -1;
       

        //플레이어 죽음
        if(health == 0) //플레이어의 체력이 0이 되면
        {
            Die(); //쥬금
            return;
        }

        if (player)

        if(Input.GetKeyDown("escape"))//ESC키를 입력시
        {
            Esc(); //ESC함수 호출
        }

        //플레이어 시점 Ray 기능을 사용하기 위하여 플레이어가 바라보는 방향 설정
        if(Input.GetButtonDown("Horizontal") && Input.GetKeyDown(KeyCode.RightArrow))
        {
            dirVec = Vector3.right; //플레이어가 오른쪽 이동키를 누르면 오른쪽을 본다고 설정
        }
        else if(Input.GetButtonDown("Horizontal") && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            dirVec = Vector3.left; //플레이어가 왼쪽 이동키를 누르면 왼쪽을 본다고 설정
        }
        else if(Input.GetButtonDown("Jump") && Input.GetKeyDown(KeyCode.Space)) //점프했을 경우 아래쪽 부분 조사
        {
            dirVec = Vector3.down; //플레이어가 점프를 하면 아래쪽을 본다고 설정
        }

        //I 키 누르면 대화
        if (Input.GetKeyDown(KeyCode.I) && scanObject != null) //플레이어가 I키를 입력 받고 scanObject가 있을 경우
        {
            manager.Action(scanObject); // 플레이어가 I키를 눌렀을 경우 GameManager의 Action함수 호출 
        }

       
    }

    void FixedUpdate()
    {

        if (health == 0) //Hp가 0이면 움직일 수 없음
            return; 

        //플레이어 이동 관련
        float h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal"); //Action함수의 여부를 따지는 것을 이용하여 플레이어가 대화중일 경우 이동이 불가하도록 설정

        //플레이어 이동
        rigid.AddForce(Vector2.right * h * 10f, ForceMode2D.Impulse); 

        if (rigid.velocity.x > maxSpeed) //속도가 최대 속도보다 클때
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y); //최대 속도 유지
        }
        else if (rigid.velocity.x < maxSpeed*(-1)) //왼쪽 이동시 최대 속도보다 클때
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y); //최대 속도 유지
        }

        //NPC 조사 기능
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0,1,0)); //바라보는 방향으로 Ray 출력, 게임에서 잘뜨는지 확인하기 위해 초록색으로 함
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("NPC")); //Ray에 NPC가 닿는지 확인

        if(rayHit.collider != null) //닿으면
        {
            scanObject = rayHit.collider.gameObject; //scanObject가 NPC라 확인,Null이 아님
        }
        else //닿지 않으면
        {
            scanObject = null; //scanObject가 Null임
        }
    }

    //플레이어 피격 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Monster") //Monster 태그가 붙은 오브젝트와 충돌시
        {

            OnDamaged(); //데미지 입었을 경우 무적 시간 관련 함수 호출

            health--; //체력이 1씩 깎임
            if (health == 2) //체력이 2일시  
            {
                Hp3.SetActive(false); //HP 하나 없어짐
            }
            if (health == 1) //체력이 1일시
            {
                Hp2.SetActive(false); //HP 하나 더 없어짐
            }
            if (health == 0) //체력이 0일시
            {
                Hp.SetActive(false); //HP가 하나 더 없어짐
            }
        }

        if (collision.gameObject.tag == "DeadZone") //플레이어가 DeadZone 태그가 붙은 물체에 닿을 경우
        {
            Die();  //죽음
        }

    }

    //피격시 무적 시간
    void OnDamaged()
    {
        gameObject.layer = 6; //레이어 6, 즉 플레이어의 레이어 설정

        spriteRenderer.color = new Color(1, 1, 1, 0.4f); //피격시 잠깐 투명해짐

        Invoke("OffDamaged", 1); //다시 데미지 입기고 불투명해지기까지 1초 지연
    }

    void OffDamaged() 
    {
        gameObject.layer = 6; //플레이어의 레이어가
        spriteRenderer.color = new Color(1, 1, 1, 1); //원래대로 돌아옴
    }

    //플레이어 죽음 함수
    public void Die()
    {
        Time.timeScale = 0; //시간이 멈춤
        player.SetActive(false); //플레이어 사라짐
        Panel.SetActive(true);  //게임 오버 화면이 팝업
    }

    //ESC 함수
    public void Esc() 
    {
        Time.timeScale = 0; //시간이 멈춤
        Panel2.SetActive(true); //ESC화면 팝업
    }
    
  
}
   
