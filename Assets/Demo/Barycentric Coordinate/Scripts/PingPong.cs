using UnityEngine;

public class PingPong : MonoBehaviour {

    [SerializeField] private float lenth = 2.5f;
    private float save_x;

    private void Start ( ) {
        save_x = transform.position.x;
    }

    private void Update ( ) {
        transform.position = new Vector3 ( save_x + Mathf.Lerp( -lenth, lenth, Mathf.PingPong(Time.time, 1f)), transform.position.y, transform.position.z );
    }
}