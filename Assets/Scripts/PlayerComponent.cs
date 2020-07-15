using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour {


    [HideInInspector]
    public Camera view_camera;

    /// <summary> 가지고 있는 유닛 객체의 수 </summary>
    public List<UnitComponent> unit_pool = new List<UnitComponent> ( );


    private void Awake ( ) {
        view_camera = Camera.main;
    }
}
