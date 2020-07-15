using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary> 하나의 유닛 객체만을 컨트롤하는 플레이어 </summary>
public class CharController : PlayerComponent {


    
    public UnitComponent controller_unit {
        get {
            return unit_pool.Count == 0 ? null : unit_pool[0];
        }
    }



    void move ( Vector3 dir ) {
        if ( !controller_unit ) {
            return;
        }

        controller_unit.move ( dir );
    }


    void rotation ( Vector3 dir ) {
        if ( !controller_unit ) {
            return;
        }

        controller_unit.rotation ( dir );
    }


    void Update ( ) {

        if ( !controller_unit ) {
            return;
        }


        Vector3 dir = Vector3.zero;
        Vector3 rot = Vector3.zero;


        if ( Input.GetKey ( KeyCode.W ) ) {
            dir += view_camera.transform.forward;
            rot += view_camera.transform.forward;
        }

        if ( Input.GetKey ( KeyCode.A ) ) {
            dir += -view_camera.transform.right;
            rot += -view_camera.transform.right;
        }

        if ( Input.GetKey ( KeyCode.S ) ) {
            dir += -view_camera.transform.forward;
        }

        if ( Input.GetKey ( KeyCode.D ) ) {
            dir += view_camera.transform.right;
            rot += view_camera.transform.right;
        }


        if ( dir.magnitude > 0 ) {
            dir.y = 0f;
            move ( dir );
        }

        if(rot.magnitude > 0) {
            rotation ( rot );
        }
    }
}