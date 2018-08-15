using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreatureMovement : MonoBehaviour {

    //바라보는 방향
    private enum FaceDirection { FaceLeft = -1, FaceRight = 1};
    //public FaceDirection Facing = FaceDirection.FaceRight;

    // 이 객체에 대한 참조
    private Rigidbody2D ThisBody = null;
    private Transform ThisTransform = null;
    private Collider2D ThisCollider = null;


    //이동
    public float movePower = 1f;
    Vector3 movement;
    int movementFlag = 0;
    bool isTracing;
    private GameObject traceTarget;

    public PlayerMovement target;
    private Transform targetTransform;

    public int movementDirection = 1;
    public int movingTo = 0;
    public Transform[] PathCenter;
    GameObject enemy;

    public int creatureType = 1;

    //애니메이터
    Animator animator;

    public bool isMoving = false;

    //이동관련
    [SerializeField]
    public Transform[] rotationCenter;
    float posX, posY, angle = -1f, digree;
    public float angularSpeed = 2f;
    int i = 0;
    bool flag = true, flip = true, clear = false;
    Vector2 targetDir, Dir;

    // Use this for initialization
    void Start()
    {
        //ThisBody = GetComponent<Rigidbody2D>();
        //ThisTransform = GetComponent<Transform>();
        //targetTransform = target.GetComponent<Transform>();
        animator = gameObject.GetComponentInChildren<Animator>();

        enemy = GameObject.Find("Enemy");
        //****enemy.transform.position = PathCenter.transform.position;
        StartCoroutine("ChangeMovement");
    }

    //Coroutine
    IEnumerator ChangeMovement()
    {
        // var movementFlag = PathCenter[Random.Range(0, PathCenter.Length)];
        movementFlag = Random.Range(0, 11);
        if (movementFlag == 0)
            animator.SetBool("isMoving", false);
        else
            animator.SetBool("isMoving", true);
        //wait
        yield return new WaitForSeconds(1f);

        StartCoroutine("ChangeMovement");
    }

   /* public IEnumerator<Transform> GetNextPathPoint()
    {
        if (PathCenter == null || PathCenter.Length < 1)
        {
            yield break;
        }
        while (true)
        {
            yield return PathCenter[movingTo];
            if (PathCenter.Length == 1)
            {
                continue;
            }
            if (PathType == PathTypes.linear)
            {
                if (movingTo <= 0)
                {
                    movementDirection = 1;
                }
                else if (movingTo >= PathCenter.Length - 1)
                {
                    movementDirection = -1;
                }
            }
            movingTo = movingTo + movementDirection;
            if (PathType == PathTypes.loop)
            {
                movingTo = 0;
            }
            if (movingTo < 0)
            {
                movingTo = PathCenter.Length - 1;
            }
        }
    }*/
      
    void OnTriggerEnter2D (Collider2D other)
    {
         if (creatureType == 0)
                return;
         if (other.gameObject.tag== "Player")
         {
                traceTarget = other.gameObject;
                StopCoroutine("ChangeMovement");
          }
    }

    void OnTriggerStay2D(Collider2D other)
    {
         if (creatureType == 0)
                return;
         if (other.gameObject.tag == "Player")
         {
                isTracing = true;
                animator.SetBool("isMoving", true);
         }
    }
    void OnTriggerExit2D(Collider2D other)
     {
          if (creatureType == 0)
                return;
          if (other.gameObject.tag == "Player")
          {
                isTracing = false;
                StartCoroutine("ChangeMovement");
          }
    }
    void FixedUpdate()
    {
        /*if (ThisName.Equals("Enemy"))
          {
               if (flag)
               {
                   posX = rotationCenter[0].position.x - Mathf.Cos(angle);
                   posY = rotationCenter[0].position.y - Mathf.Sin(angle);
                   transform.position = new Vector2(posX, posY);
                   angle = angle + Time.deltaTime * angularSpeed;
               }
           }*/
       
            Move();
        }

        void Move()
        {
            Vector3 moveVelocity = Vector3.zero;
            string dist = "";

        //Trace or Random
        if (isTracing)
        {
            Vector3 playerPos = traceTarget.transform.position;

            if (playerPos.x < transform.position.x)
                dist = "Left";
            else if (playerPos.x > transform.position.x)
                dist = "Right";
        }
        else {
            if (movementFlag == 1)
                dist = "Left";
            else if (movementFlag == 2)
                dist = "Right";
        }

        //movement assign
        if (dist == "Left")
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(-2, 2, 1);
        }
        else if (movementFlag == 2)
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(2, 2, 1);
        }
        transform.position += moveVelocity * movePower * Time.deltaTime;
    }
       /* public void Change_i()
        {
            flag = false;
            i = (i + 1) % 4;
            angle-= 1.47f;
        }*/
        /*void OnBecameInvisible()
        {
            ChangeMovement();
        }*/
        
    }
