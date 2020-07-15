using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitComponent : MonoBehaviour {


    #region Public Fields

    [HideInInspector] 
    public Vector3 velocity;

    [HideInInspector]
    public new Rigidbody rigidbody;
    [HideInInspector]
    public Animator animator;

    public float angle;
    public float rotation_speed = 1f;
    public float movement_speed = 270f;
    #endregion

    #region Private Fields
    
    #endregion



    public void move ( Vector3 dir ) {
        Vector3 speed = dir * movement_speed * Time.fixedDeltaTime;
        velocity = speed;
    }


    public void rotation ( Vector3 dir ) {
        angle = Mathf.Atan2 ( dir.x, dir.z ) * Mathf.Rad2Deg;
    }



    private void Awake ( ) {
        rigidbody = GetComponent<Rigidbody> ( );
        animator = GetComponentInChildren<Animator> ( );
    }


    private void FixedUpdate ( ) {

        if ( velocity.magnitude > 0 ) {
            rigidbody.velocity = velocity;
            velocity = Vector3.zero;
        }


        float target_angle = Mathf.LerpAngle ( transform.eulerAngles.y, angle, Time.fixedDeltaTime * rotation_speed );
        transform.eulerAngles = new Vector3 ( 0f, target_angle, 0f );

        animator.SetFloat ( "Speed", rigidbody.velocity.magnitude );
    }
}