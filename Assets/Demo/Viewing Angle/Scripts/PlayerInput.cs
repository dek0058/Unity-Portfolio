using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public CharacterMovement character;

    private void Update ( ) {

        bool is_press = false;

        if ( Mathf.Approximately ( character.velocity.x, 0f ) ) {
            if ( Input.GetKey ( KeyCode.W ) ) {
                character.velocity.y += character.movement_speed;
                character.is_move = true;
                is_press = true;
            }

            if ( Input.GetKey ( KeyCode.S ) ) {
                character.velocity.y -= character.movement_speed;
                character.is_move = true;
                is_press = true;

            }
        }

        if ( Mathf.Approximately ( character.velocity.y, 0f ) ) {
            if ( Input.GetKey ( KeyCode.A ) ) {
                character.velocity.x -= character.movement_speed;
                character.is_move = true;
                is_press = true;
            }

            if ( Input.GetKey ( KeyCode.D ) ) {
                character.velocity.x += character.movement_speed;
                character.is_move = true;
                is_press = true;
            }
        }


        if(!is_press ) {
            character.is_move = false;
        }
    }
}
