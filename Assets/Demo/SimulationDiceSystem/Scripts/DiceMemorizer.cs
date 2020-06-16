using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceMemorizer {
    private const int Min = 1;
    private const int Max = 6;


    // Comment : 결과 값들의 경의의 수를 저장 할 해쉬 테이블
    private static Dictionary<int, Dictionary<int, List<int[]>>> hashtable = new Dictionary<int, Dictionary<int, List<int[]>>> ( );


    /// <summary>해쉬 테이블을 초기화 합니다.</summary>
    /// <param name="cnt">주사위의 최대 개수</param>
    public static void initialize ( int cnt, int seed = -1 ) {
        Random.InitState ( seed );

        int[] array = Enumerable.Range ( Min, Max ).ToArray ( );
        for ( int i = 1; i <= cnt; ++i ) {
            hashtable.Add ( i, new Dictionary<int, List<int[]>> ( ) );
            combination ( i, array );
        }
    }


    public static void set_seed ( int seed ) {
        Random.InitState ( seed );
    }


    /// <summary>해당 주사위의 개수에서 나올 수 있는 경우의 수를 해쉬 테이블에 저장합니다.</summary>
    public static void add ( int dice_cnt ) {
        if ( hashtable.ContainsKey ( dice_cnt ) ) { // 
            return;
        }
        hashtable.Add ( dice_cnt, new Dictionary<int, List<int[]>> ( ) );
        combination ( dice_cnt, Enumerable.Range ( Min, Max ).ToArray ( ) );
    }


    public static int[] get ( int dices, int result ) {
        if ( !hashtable.ContainsKey ( dices ) || !hashtable[dices].ContainsKey ( result ) ) {
            return null;
        }

        int r = Random.Range ( 0, hashtable[dices][result].Count * 2 - 1 );
        int index = Mathf.CeilToInt ( r / 2f );

        int[] arr = new int[hashtable[dices][result][index].Length];
        System.Array.Copy ( hashtable[dices][result][index], arr, arr.Length );
        for ( int i = 0; i < arr.Length; ++i ) {
            int a = Random.Range ( 0, arr.Length );
            int tmp = arr[i];
            arr[i] = arr[a];
            arr[a] = tmp;
        }

        return arr;
    }


    public static List<int[]> get_list ( int dices, int result ) {
        if ( !hashtable.ContainsKey ( dices ) || !hashtable[dices].ContainsKey ( result ) ) {
            return null;
        }
        return hashtable[dices][result];
    }


    /// <summary>
    /// 해당 주사위의 개수에서 나올 수 있는 모든 순열의 조합(중복)을 구합니다.<para></para>
    /// ※ 중복 조합에서 이미 가지고 있는 값을 구하지 않습니다.<para></para>
    /// ex) [1..6] => (1,1), (2,1), (2,2), (3,1) ... (6,4), (6,5), (6,6)
    /// </summary>
    /// <param name="cnt">주사위 개수</param>
    /// <param name="array">조합의 요소</param>
    private static void combination ( int cnt, params int[] array ) {
        int sabores = array.Length - 1;
        int[] select = new int[cnt + 1];
        select.Initialize ( );

        while ( true ) {
            for ( int i = 0; i < cnt; ++i ) {
                if ( select[i] > sabores ) {
                    select[i + 1] += 1;
                    for ( int k = i; k >= 0; --k ) {
                        select[k] = select[i + 1];
                    }
                }
            }

            if ( select[cnt] > 0 ) {
                break;
            }

            int r = 0;
            int[] copy = new int[cnt];
            for ( int i = 0; i < cnt; ++i ) {
                r += array[select[i]];
                copy[i] = array[select[i]];
            }
            save_data ( cnt, r, copy );

            select[0] += 1;
        }

        // sorting (더블위치 바꾸기)
        if ( cnt > 1 ) {
            foreach ( var key in hashtable[cnt].Keys ) {
                if ( hashtable[cnt][key].Count == 1 ) {
                    continue;
                }

                float d = key / (float)cnt;
                if ( Mathf.Approximately ( (int)d, d ) ) {
                    for ( int i = 0; i < hashtable[cnt][key].Count; ++i ) {
                        int val = hashtable[cnt][key][i][0];
                        bool need = true;
                        for ( int j = 1; j < hashtable[cnt][key][i].Length; ++j ) {
                            if ( val != hashtable[cnt][key][i][j] ) {
                                need = false;
                                break;
                            }
                        }
                        if ( need ) {
                            if ( i != 0 ) {
                                int[] temp = hashtable[cnt][key][0];
                                hashtable[cnt][key][0] = hashtable[cnt][key][i];
                                hashtable[cnt][key][i] = temp;
                            }
                            break;
                        }
                    }
                }
            }
        }
    }


    private static void save_data ( int dice_cnt, int dice_value, int[] data ) {
        if ( !hashtable[dice_cnt].ContainsKey ( dice_value ) ) {
            hashtable[dice_cnt].Add ( dice_value, new List<int[]> ( ) );
        }
        hashtable[dice_cnt][dice_value].Add ( data );
    }
}
