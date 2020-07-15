using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewingAngle : MonoBehaviour
{
    public CharacterMovement character;

    private float _FOV;
    public float FOV = 90f;

    private float delta = 0f;

    private List<CharacterMovement> targets = new List<CharacterMovement> ( );

    public void set_FOV ( float angle ) {
        if(angle <= 0f) {
            angle = 1f;
        }
        _FOV = angle;
        delta = Mathf.Cos ( (_FOV / 2f) * Mathf.Deg2Rad );
    }


    private void Start ( ) {
        set_FOV ( FOV );
    }

    private void Update ( ) {
        try {
            foreach ( var t in targets ) {
                Vector2 forward = character.forward;
                Vector2 target_dir = t[CharacterMovement.AttachPoint.Chest].position - character[CharacterMovement.AttachPoint.Head].position;
                float dot = Vector2.Dot ( forward, target_dir.normalized );
                if (dot > delta) { // 시야 포착
                    float dist = Vector2.Dot ( target_dir, target_dir ); // 거리 비교용
                    LayerMask layer = 1 << LayerMask.NameToLayer ( "Character" ) | 1 << LayerMask.NameToLayer ( "Obstacle" ); // 충돌 판별용 레이어
                    RaycastHit2D hit = Physics2D.Raycast ( character[CharacterMovement.AttachPoint.Head].position, target_dir, dist, layer );
                    if( hit ) {
                        Color color = Color.red;
                        if ( !hit.transform.Equals ( t.transform ) ) {
                            color = Color.blue;
                        }
                        Debug.DrawLine ( character[CharacterMovement.AttachPoint.Head].position, hit.point, color );
                    }
                }
            }
        } catch ( System.Exception e ) {
            Debug.Log ( e.Message );
        }
    }


    private void OnValidate ( ) {
        set_FOV ( FOV );
    }


    private void OnTriggerEnter2D ( Collider2D collision ) {
        if( collision.gameObject.layer == LayerMask.NameToLayer("Character")) {
            targets.Add ( collision.GetComponent<CharacterMovement> ( ) );
        }
    }

    private void OnTriggerExit2D ( Collider2D collision ) {
        CharacterMovement com = collision.GetComponent<CharacterMovement> ( );
        if(!com ) {
            return;
        }
        if ( targets.Contains ( com ) ) {
            targets.Remove ( com );
        }
    }
}
