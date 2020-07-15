using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RightLeftDetect : MonoBehaviour
{
    public PlayerComponent player;

    public Transform source;
    public Transform target;

    public TextMeshProUGUI TM_text;


    void Update ( ) {

        Vector3 t_pos = target.position - source.position;
        Vector3 cross = Vector3.Cross ( source.forward, t_pos );
        float discriminate = Vector3.Dot ( Vector3.up, cross );

        if ( discriminate > 0 ) {
            TM_text.text = "Right";
        } else if( discriminate < 0) {
            TM_text.text = "Left";
        } else {
            TM_text.text = "Center?";
        }

        transform.LookAt ( player.view_camera.transform );
    }

}
