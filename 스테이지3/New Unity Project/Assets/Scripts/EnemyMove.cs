using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    public enum FaceDirection { FaceRight = -1, FaceLeft = 1 };
    public FaceDirection Facing = FaceDirection.FaceRight;

    public bool isDrag = false;

    private Transform ThisTransform = null;

    [SerializeField]
    Transform[] PathCenter;

    [SerializeField]
    float moveSpeed = 2f;

    int EnemyIndex = 1;
    int PathCenterIndex = 0;
    Animator animator;

    float dirX;

    Vector3 past;
    Vector3 next;

    // Use this for initialization
    void Start () {

        transform.position = PathCenter [PathCenterIndex].transform.position;
        past = transform.position;
    }

    // Update is called once per frame
    void Update () {
        Move();
        Vector3 scale = transform.localScale;
        
        next = transform.position;
        
        
        if(next.x-past.x>0)
        {
            if (scale.x < 0){
                scale.x *= -1;
            }
            
            transform.localScale = scale;
        }
        
        else
        {
            if(scale.x>0)

                scale.x *= -1;
            transform.localScale = scale;
        }

        past = next;
        

	}
    void Move()
    {
        
            transform.position = Vector2.MoveTowards(transform.position, PathCenter[PathCenterIndex].transform.position, moveSpeed * Time.deltaTime);

            if (transform.position == PathCenter[PathCenterIndex].transform.position)
            {
                PathCenterIndex += 1;
            }
            if (PathCenterIndex == PathCenter.Length)
                PathCenterIndex = 0;

            if(EnemyIndex == 4 && EnemyIndex == 5 && EnemyIndex == 6)
            {
                transform.localScale = new Vector3(-2, 2, 0);
            }

    }
    

}

