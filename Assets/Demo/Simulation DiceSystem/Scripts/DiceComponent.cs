using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceComponent : MonoBehaviour {

    public Animator animator;
    public bool is_complete {
        get => anim_queue.Count == 0 && next_anim;
    }

    private float _aspeed = 0f;
    public float aspeed {
        get => _aspeed;
        set {
            _aspeed = value;
            animator.SetFloat ( "Aspeed", _aspeed );
        }
    }

    [HideInInspector] public bool next_anim = true;
    private Queue<int> anim_queue = new Queue<int> ( );

    #region Public Functions

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
        if(!animator) {
            return;
        }
        anim_queue.Clear ( );
        animator.SetInteger ( "OrderID", id );
        animator.SetTrigger ( "Action" );
    }

    #endregion
    #region Monobehaviour Callbacks

    private void Awake ( ) {
        if(!animator) {
            animator = GetComponent<Animator> ( );
        }

        aspeed = _aspeed;
    }


    private IEnumerator Start ( ) {
        yield return new WaitUntil ( () => DiceSceneInitializer.bInitialize );

    }


    private void OnEnable ( ) {
        DiceAnimationHelper.Initialize ( animator, this );
    }


    private void Update ( ) {
        if (anim_queue.Count > 0) {
            if(next_anim) {
                next_anim = false;
                animator.SetInteger ( "OrderID", anim_queue.Dequeue() );
                animator.SetTrigger ( "Action" );
            }
        }
    }

    #endregion
}