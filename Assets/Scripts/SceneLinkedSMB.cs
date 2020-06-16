using UnityEngine;
using UnityEngine.Animations;


public class SceneLinkedSMB<TMonoBegarviour> : SealedSMB where TMonoBegarviour : MonoBehaviour {
    protected TMonoBegarviour mono_behaviour;
    private bool first_frame_happened;
    private bool last_frame_happened;

    public static void Initialize ( Animator _animator, TMonoBegarviour _mono_behaviour ) {
        SceneLinkedSMB<TMonoBegarviour>[] sceneLinkedSMBs = _animator.GetBehaviours<SceneLinkedSMB<TMonoBegarviour>> ( );
        for ( int i = 0; i < sceneLinkedSMBs.Length; i++ ) {
            sceneLinkedSMBs[i].InternalInitialize ( _animator, _mono_behaviour );
        }
    }

    protected void InternalInitialize ( Animator _animator, TMonoBegarviour _mono_behaviour ) {
        mono_behaviour = _mono_behaviour;
        OnStart ( _animator );
    }

    public sealed override void OnStateEnter ( Animator _animator, AnimatorStateInfo _state_info, int _layer_index, AnimatorControllerPlayable _controller ) {
        first_frame_happened = false;
        OnSLStateEnter ( _animator, _state_info, _layer_index );
        OnSLStateEnter ( _animator, _state_info, _layer_index, _controller );
    }

    public sealed override void OnStateUpdate ( Animator _animator, AnimatorStateInfo _state_info, int _layer_index, AnimatorControllerPlayable _controller ) {
        if ( !_animator.gameObject.activeSelf ) {
            return;
        }

        if ( _animator.IsInTransition ( _layer_index ) && _animator.GetNextAnimatorStateInfo ( _layer_index ).fullPathHash == _state_info.fullPathHash ) {
            OnSLTransitionToStateUpdate ( _animator, _state_info, _layer_index );
            OnSLTransitionToStateUpdate ( _animator, _state_info, _layer_index, _controller );
        }

        if ( !_animator.IsInTransition ( _layer_index ) && first_frame_happened ) {
            OnSLStateNoTransitionUpdate ( _animator, _state_info, _layer_index );
            OnSLStateNoTransitionUpdate ( _animator, _state_info, _layer_index, _controller );
        }

        if ( _animator.IsInTransition ( _layer_index ) && !last_frame_happened && first_frame_happened ) {
            last_frame_happened = true;
            OnSLStatePreExit ( _animator, _state_info, _layer_index );
            OnSLStatePreExit ( _animator, _state_info, _layer_index, _controller );
        }

        if ( !_animator.IsInTransition ( _layer_index ) && !first_frame_happened ) {
            first_frame_happened = true;
            OnSLStatePostEnter ( _animator, _state_info, _layer_index );
            OnSLStatePostEnter ( _animator, _state_info, _layer_index, _controller );
        }

        if ( _animator.IsInTransition ( _layer_index ) && _animator.GetCurrentAnimatorStateInfo ( _layer_index ).fullPathHash == _state_info.fullPathHash ) {
            OnSLTransitionFromStateUpdate ( _animator, _state_info, _layer_index );
            OnSLTransitionFromStateUpdate ( _animator, _state_info, _layer_index, _controller );
        }
    }

    public sealed override void OnStateExit ( Animator _animator, AnimatorStateInfo _state_info, int _layer_index, AnimatorControllerPlayable _controller ) {
        last_frame_happened = false;
        OnSLStateExit ( _animator, _state_info, _layer_index );
        OnSLStateExit ( _animator, _state_info, _layer_index, _controller );
    }

    public virtual void OnStart ( Animator _animator ) {
    }

    public virtual void OnSLStateEnter ( Animator _animator, AnimatorStateInfo _state_info, int layer_index ) {
    }
    public virtual void OnSLStateEnter ( Animator _animator, AnimatorStateInfo _state_info, int layer_index, AnimatorControllerPlayable _controller ) {
    }

    public virtual void OnSLTransitionToStateUpdate ( Animator _animator, AnimatorStateInfo _state_info, int layer_index ) {
    }
    public virtual void OnSLTransitionToStateUpdate ( Animator _animator, AnimatorStateInfo _state_info, int layer_index, AnimatorControllerPlayable _controller ) {
    }

    public virtual void OnSLStatePostEnter ( Animator _animator, AnimatorStateInfo _state_info, int layer_index ) {
    }
    public virtual void OnSLStatePostEnter ( Animator _animator, AnimatorStateInfo _state_info, int layer_index, AnimatorControllerPlayable _controller ) {
    }

    public virtual void OnSLStateNoTransitionUpdate ( Animator _animator, AnimatorStateInfo _state_info, int layer_index ) {
    }
    public virtual void OnSLStateNoTransitionUpdate ( Animator _animator, AnimatorStateInfo _state_info, int layer_index, AnimatorControllerPlayable _controller ) {
    }

    public virtual void OnSLStatePreExit ( Animator _animator, AnimatorStateInfo _state_info, int layer_index ) {
    }
    public virtual void OnSLStatePreExit ( Animator _animator, AnimatorStateInfo _state_info, int layer_index, AnimatorControllerPlayable _controller ) {
    }

    public virtual void OnSLTransitionFromStateUpdate ( Animator _animator, AnimatorStateInfo _state_info, int layer_index ) {
    }
    public virtual void OnSLTransitionFromStateUpdate ( Animator _animator, AnimatorStateInfo _state_info, int layer_index, AnimatorControllerPlayable _controller ) {
    }

    public virtual void OnSLStateExit ( Animator _animator, AnimatorStateInfo _state_info, int layer_index ) {
    }
    public virtual void OnSLStateExit ( Animator _animator, AnimatorStateInfo _state_info, int layer_index, AnimatorControllerPlayable _controller ) {
    }
}

public abstract class SealedSMB : StateMachineBehaviour {
    public sealed override void OnStateEnter ( Animator _animator, AnimatorStateInfo _state_info, int _layer_index ) {
    }
    public sealed override void OnStateUpdate ( Animator _animator, AnimatorStateInfo _state_info, int _layer_index ) {
    }
    public sealed override void OnStateExit ( Animator _animator, AnimatorStateInfo _state_info, int _layer_index ) {
    }
}