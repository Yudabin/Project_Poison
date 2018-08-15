
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    //이동
    public float movePower = 6f;
    public bool inputLeft = false;
    public bool inputRight = false;
    public bool inputUp = false;
    public bool inputDown = false;

    //체력
    public int maxHealth = 600;
    int health;
    bool isDie = false;

    //아이템
    bool isItem = false;
    //public GameObject[] phone;
    public Transform[] PathCenter;
    public GameObject item;
    public int score;
    public Transform[] itemarr;
    private float TimeLeft = 2.0f;
    private float nextTime = 0.0f;

    int i = 1;
    public float PathCenterposX = 5f;

    float posX, posY = -1f;

    //충돌
    Rigidbody2D rigid;

    //애니메이터
    Animator animator;

    // 애니메이션을 위한
    public Animator faceAnimator;
    public Animator handsAnimator;
    public Animator bodyAnimator;

    SpriteRenderer spriteRenderer;

    //충돌시 깜빡거림
    bool isUnBeatTime = true;

    bool flag = true;

    Vector3 movement;
    public float speed = 20; //속도

    // Use this for initialization
    void Start() {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponentInChildren<Animator>();
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();

        health = maxHealth;

        UiButtonManager ui = GameObject.FindWithTag("Managers").GetComponent<UiButtonManager>();
        ui.Init();

        for (i = 0; i < PathCenter.Length; i++)
        {
            item.transform.position = PathCenter[i].transform.position;
            item.SetActive(false);
        }

        StartCoroutine("ItemTime");
    }

    void Update()
    {
        //Check Health
        if (health == 0)
        {
            if (!isDie)
                Die();
            return;
        }
        if (isItem == false)
        {
            isItem = true;
            //ItemOn();
        }

    }
    void SetItemOn()
    {
        isItem = false;
    }
 
    public void Shuffle()
    {
        for(int i = 0; i< itemarr.Length; i++)
        {
            int random = Random.Range(0, itemarr.Length);
            Transform temp = itemarr[random];
            itemarr[random] = itemarr[i];
            itemarr[i] = temp;
        }
    }
    IEnumerator ItemTime()
    {
        while (true)
        {
            Shuffle();
            //Instantiate(생성시킬 오브젝트, 생성될 위치, 생성됐을때 회전값);
            //
            for(int i = 0; i<itemarr.Length; i++)
            {
                item.transform.position = itemarr[i].position;
                item.SetActive(true);
                yield return new WaitForSeconds(5f);
                item.SetActive(false);
            }
        }

    }

    void FixedUpdate()
    {
        //Check Health
        if (health == 0)
            return;
        Move();
    }

    void Die()
    {
        //Die Flag On
        isDie = true;

        rigid.velocity = Vector2.zero;

        //Die Motion
        animator.Play("Die");

    }
    void RestartStage()
    {
        GameManager.RestartStage();
    }

    // Update is called once per frame
    //moving
    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        if ((!inputRight && !inputLeft && !inputUp && !inputDown))
        {
            animator.SetBool("isMoving", false);
        }
        else if (inputLeft)
        {
            animator.SetBool("isMoving", true);
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(-17, 17, 10);

        }
        else if (inputRight)
        {
            animator.SetBool("isMoving", true);
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(17, 17, 10);
        }
        else if (inputUp)
        {
            animator.SetBool("isMoving", true);
            moveVelocity = Vector3.up;
        }
        else if (inputDown)
        {
            animator.SetBool("isMoving", true);
            moveVelocity = Vector3.down;
        }

        Vector3 pos = transform.position;
        if(pos.x > 9.6) //오른쪽으로 나감
        {
            inputRight = false;
        }
        else if(pos.x< -7.9)
        {
            inputLeft = false;
        }
        if (pos.y > 5.5)
        {
            inputUp = false;
        }
        else if (pos.y < -0.01)
        {
            inputDown = false;
        }
        transform.position += moveVelocity * movePower * Time.deltaTime * 20;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //rigidBody가 무언가와 충돌할때 호출되는 함수
         //Collider2D other로 부딪힌 객체를 받아온다.
         if (other.CompareTag("enemy") && !isUnBeatTime)
         {
            health -= 25;
            if (health > 0)
            {
                isUnBeatTime = true;
                StartCoroutine("UnBeatTime");
            }
         }
        else if (other.CompareTag("Item"))
        {
            item.SetActive(false);
            isItem = true;
            StartCoroutine("ItemOn");
        }

         /*else if (other.gameObject.tag == "Bottom")
         {
             health = 0;
         }*/
         /*
         if (other.gameObject.tag.Equals("enemy"))
         {
             Destroy(this.gameObject);
          }*/

      

    }
    //크기조정
    IEnumerator ItemOn()
    {
        Vector3 ThisSize = transform.localScale;
        
        while (true)
        {
            if (ThisSize.x > 30)
            {
                ThisSize.x = ThisSize.y = 30;
                transform.localScale = ThisSize;
            }
            else
            {
                ThisSize.x  +=1;
                ThisSize.y = ThisSize.x;
                transform.localScale = ThisSize;
                yield return new WaitForSeconds(0.1f);
            }
              
        }
    }
    void CheckItem()
    {

    }
    IEnumerator ItemOff()
    {
        Vector3 ThisSize = transform.localScale;

        while (true)
        {
            if (ThisSize.x < 17)
            {
                ThisSize.x = ThisSize.y = 17;
                transform.localScale = ThisSize;
            }
            else
            {
                ThisSize.x -= 1;
                ThisSize.y = ThisSize.x;
                transform.localScale = ThisSize;
                yield return new WaitForSeconds(0.1f);
            }

        }
    }
    //깜박거리기
    IEnumerator UnBeatTime()
        {
            int countTime = 0;
            while (countTime < 10)
            {
                if (countTime % 2 == 0)
                    spriteRenderer.color = new Color32(255, 255, 255, 90);
                else
                    spriteRenderer.color = new Color32(255, 255, 255, 180);
                yield return new WaitForSeconds(0.2f);
                countTime++;
            }
            spriteRenderer.color = new Color32(255, 255, 255, 255);

            isUnBeatTime = false;
        }
        /* void OnGUI()
         {
             GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
             GUILayout.BeginVertical();
             GUILayout.Space(10);
             GUILayout.BeginHorizontal();
             GUILayout.Space(15);
             string heart = "";
             for (int i = 0; i < health; i++)
             {
                 heart += "♥";
             }
             GUILayout.Label(heart);

             GUILayout.FlexibleSpace();
             GUILayout.EndHorizontal();
             GUILayout.FlexibleSpace();
             GUILayout.EndVertical();
             GUILayout.EndArea();
         }*/
    }

