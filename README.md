## Unity 2019.3.15f

# **SimulationDiceSystem**
![kOeXgheks6](https://user-images.githubusercontent.com/47653276/84733203-91ac3400-afd8-11ea-8368-545a32c0d9d4.gif)


![POWERPNT_UuuPwp9DrZ](https://user-images.githubusercontent.com/47653276/84733995-c0c3a500-afda-11ea-8e9f-1ec6a5aa2d6f.png)

* DiceMemorizer.cs
```c#
// Comment : 결과 값들의 경의의 수를 저장 할 해쉬 테이블
private static Dictionary<int, Dictionary<int, List<int[]>>> hashtable = new Dictionary<int, Dictionary<int, List<int[]>>> ( );

/// <summary>해쉬 테이블을 초기화 합니다.</summary>
/// <param name="cnt">주사위의 최대 개수</param>
public static void initialize ( int cnt, int seed = -1 );

/// <summary>
/// 해당 주사위의 개수에서 나올 수 있는 모든 순열의 조합(중복)을 구합니다.<para></para>
/// ※ 중복 조합에서 이미 가지고 있는 값을 구하지 않습니다.<para></para>
/// ex) [1..6] => (1,1), (2,1), (2,2), (3,1) ... (6,4), (6,5), (6,6)
/// </summary>
/// <param name="cnt">주사위 개수</param>
/// <param name="array">조합의 요소</param>
private static void combination ( int cnt, params int[] array );

public static int[] get ( int dices, int result );
```

* Dice Animator

![Unity_6ycBq1I5Kn](https://user-images.githubusercontent.com/47653276/84734374-a63dfb80-afdb-11ea-8e1c-26c6074adaae.png)

* DiceComponent.cs
```c#
public bool next_anim = true;
private Queue<int> anim_queue = new Queue<int> ( );

/// <summary>애니메이션을 큐에 대기 시킵니다.</summary>
/// <param name="id">애니메이션 ID</param>
public void add_animation ( int id ) {
   if ( !animator ) {
       return;
   }
   anim_queue.Enqueue ( id );
}

private void Update ( ) {
    if (anim_queue.Count > 0) {
        if(next_anim) {
            next_anim = false;
            animator.SetInteger ( "OrderID", anim_queue.Dequeue() );
            animator.SetTrigger ( "Action" );
        }
    }
}

```

and
```c#
if(_state_info.normalizedTime >= exit_time) {
        mono_behaviour.next_anim = true;
}
```

# **BarycentricCoordinate Collision2D**

![유니티충돌처리스크린샷](https://user-images.githubusercontent.com/47653276/84907499-3a988300-b0ee-11ea-8583-70877fb13b86.gif)

BCInitializer.cs
```c#
[SerializeField] private Transform[] bc_transform;
[SerializeField] private Transform target;
private Vector2 convert_vector2(Vector3 position) {
    return new Vector2(position.x, position.z);
}
private void Update() {
    Vector2 min = new Vector2(Mathf.Min(Mathf.Min(bc_transform[0].position.x, bc_transform[1].position.x), bc_transform[2].position.x), Mathf.Min(Mathf.Min(bc_transform[0].position.z, bc_transform[1].position.z), bc_transform[2].position.z));
    Vector2 max = new Vector2(Mathf.Max(Mathf.Max(bc_transform[0].position.x, bc_transform[1].position.x), bc_transform[2].position.x), Mathf.Max(Mathf.Max(bc_transform[0].position.z, bc_transform[1].position.z), bc_transform[2].position.z));
    Vector2 u = convert_vector2(bc_transform[1].position) - convert_vector2(bc_transform[0].position);
    Vector2 v = convert_vector2(bc_transform[2].position) - convert_vector2(bc_transform[0].position);
    float c = Vector2.Dot(u, u) * Vector2.Dot(v, v) - Vector2.Dot(u, v) * Vector2.Dot(u, v);
    if (c == 0) {
        return;
    }
    Vector2 w = convert_vector2(target.position) - convert_vector2(bc_transform[0].position);
    float s = (Vector2.Dot(w, u) * Vector2.Dot(v, v) - Vector3.Dot(w, v) * Vector3.Dot(v, u)) / c;
    float t = (Vector2.Dot(w, v) * Vector2.Dot(u, u) - Vector3.Dot(w, u) * Vector3.Dot(u, v)) / c;
    float one_minus_st = 1 f - s - t;
    if (((s >= 0 f) && (s <= 1 f)) && ((t >= 0 f) && (t <= 1 f)) && ((one_minus_st >= 0 f) && (one_minus_st <= 1 f))) {
        Debug.DrawLine(target.position, Vector3.up * 100 f, Color.green);
    }
}
```
