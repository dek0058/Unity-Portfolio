# Unity 2019.3.15f

**SimulationDiceSystem**
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
