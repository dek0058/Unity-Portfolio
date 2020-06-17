using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class BCInitializer : MonoBehaviour {

    [SerializeField] private Transform[] bc_transform;
    [SerializeField] private Transform target;

    private Vector2 convert_vector2 ( Vector3 position ) {
        return new Vector2 ( position.x, position.z );
    }

    private void Update ( ) {

       Vector2 min = new Vector2 (
            Mathf.Min ( Mathf.Min ( bc_transform[0].position.x, bc_transform[1].position.x ), bc_transform[2].position.x ),
            Mathf.Min ( Mathf.Min ( bc_transform[0].position.z, bc_transform[1].position.z ), bc_transform[2].position.z )
        );
        Vector2 max = new Vector2 (
            Mathf.Max ( Mathf.Max ( bc_transform[0].position.x, bc_transform[1].position.x ), bc_transform[2].position.x ),
            Mathf.Max ( Mathf.Max ( bc_transform[0].position.z, bc_transform[1].position.z ), bc_transform[2].position.z )
        );

        Vector2 u = convert_vector2(bc_transform[1].position) - convert_vector2(bc_transform[0].position);
        Vector2 v = convert_vector2(bc_transform[2].position) - convert_vector2(bc_transform[0].position);

        float c = Vector2.Dot ( u, u ) * Vector2.Dot ( v, v ) - Vector2.Dot ( u, v ) * Vector2.Dot ( u, v );
        if(c == 0) {
            return;
        }

        Vector2 w = convert_vector2(target.position) - convert_vector2(bc_transform[0].position);

        float s = (Vector2.Dot ( w, u ) * Vector2.Dot ( v, v ) - Vector3.Dot ( w, v ) * Vector3.Dot ( v, u )) / c;
        float t = (Vector2.Dot ( w, v ) * Vector2.Dot ( u, u ) - Vector3.Dot ( w, u ) * Vector3.Dot ( u, v )) / c;
        float one_minus_st = 1f - s - t;
        if ( ((s >= 0f) && (s <= 1f)) && ((t >= 0f) && (t <= 1f)) && ((one_minus_st >= 0f) && (one_minus_st <= 1f)) ) {
            Debug.DrawLine ( target.position, Vector3.up * 100f, Color.green);
        }
    }


    private void OnDrawGizmos ( ) {

        Gizmos.color = Color.red;
        int cnt = bc_transform.Length / 3;
        for ( int i = 0; i < cnt; ++i ) {
            Gizmos.DrawLine ( bc_transform[i].position, bc_transform[i + 1].position );
            Gizmos.DrawLine ( bc_transform[i + 1].position, bc_transform[i + 2].position );
            Gizmos.DrawLine ( bc_transform[i].position, bc_transform[i + 2].position );
        }
    }
}