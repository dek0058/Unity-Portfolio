using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public Animator animator;
    public bool is_complete {
        get => anim_queue.Count == 0 && next_anim;
    }


    public enum AttachPoint {
        Origin,
        Chest,
        Head,
    }
    [SerializeField] private Transform[] attach_points = new Transform[(int)AttachPoint.Head + 1];

    public new Rigidbody2D rigidbody2D;
    [HideInInspector] public Vector2 velocity = Vector2.zero;
    public float movement_speed = 200f;
    public float angle = 0f;
    [HideInInspector] public bool is_move = false;

    [HideInInspector] public bool next_anim = true;
    private Queue<int> anim_queue = new Queue<int> ( );

    #region Public Fnuctions

    /// <summary>
    /// 캐릭터 부착 포인트의 Transform을 반환합니다.
    /// </summary>
    public Transform this [AttachPoint point] {
        get => attach_points[(int)point];
    }

    
    public Vector2 forward {
        get {
            if ( Mathf.Approximately ( angle, 0f ) ) {
                return Vector2.down;
            } else if ( Mathf.Approximately ( angle, 90f ) ) {
                return Vector2.left;
            } else if ( Mathf.Approximately ( angle, 180f ) ) {
                return Vector2.up;
            } else if ( Mathf.Approximately ( angle, 270f ) ) {
                return Vector2.right;
            } else {
                return Vector2.zero;
            }
        }
    }


    /// <summary>애니메이션을 큐에 대기 시킵니다.</summary>
    /// <param name="id">애니메이션 ID</param>
    public void add_animation ( int id ) {
        if ( !animator ) {
            return;
        }
        anim_queue.Enqueue ( id );
    }


    /// <summary>대기 큐를 모두 제거하고 애니메이션을 즉시 실행시킵니다.</summary>
    /// <param name="id">애니메이션 ID</param>
    public void set_animation ( int id ) {
        if ( !animator ) {
            return;
        }
        anim_queue.Clear ( );
        animator.SetInteger ( "OrderID", id );
        animator.SetTrigger ( "Action" );
    }

    #endregion
    #region MonBehaviour Callbacks

    private void Awake ( ) {
        if ( !animator ) {
            animator = GetComponent<Animator> ( );
        }

        if ( !rigidbody2D ) {
            rigidbody2D = GetComponent<Rigidbody2D> ( );
        }
    }


    private void OnEnable ( ) {
        CharacterAnimationHelper.Initialize ( animator, this );
    }


    private void Update ( ) {
        if ( anim_queue.Count > 0 ) {
            if ( next_anim ) {
                next_anim = false;
                animator.SetInteger ( "OrderID", anim_queue.Dequeue ( ) );
                animator.SetTrigger ( "Action" );
            }
        }
    }


    private void FixedUpdate ( ) {
        if(velocity.x > 0f) {
            angle = 270f;
        } else if(velocity.x < 0f) {
            angle = 90f;
        } else if(velocity.y > 0f) {
            angle = 180f;
        } else if(velocity.y < 0f) {
            angle = 0f;
        }
        animator.SetFloat ( "Angle", angle / 360f );

        rigidbody2D.velocity = velocity * Time.fixedDeltaTime;

        if( is_move ) {
            if ( animator.GetInteger ( "OrderID" ) != 1 ) {
                set_animation ( 1 );
            }

        } else {
            if ( animator.GetInteger ( "OrderID" ) != 0 ) {
                set_animation ( 0 );
            }
        }

        velocity = Vector2.zero;
    }

    #endregion
}
