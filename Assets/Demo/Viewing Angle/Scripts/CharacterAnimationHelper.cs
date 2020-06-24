using UnityEngine;

public class CharacterAnimationHelper : SceneLinkedSMB<CharacterMovement> {

    [SerializeField] private float exit_time = 1f;


    public override void OnSLStateNoTransitionUpdate ( Animator _animator, AnimatorStateInfo _state_info, int layer_index ) {
        if ( _state_info.normalizedTime >= exit_time ) {
            mono_behaviour.next_anim = true;
        }
    }
}