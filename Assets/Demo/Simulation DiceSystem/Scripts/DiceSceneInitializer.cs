using UnityEngine;
using UnityEngine.UI;

public class DiceSceneInitializer : MonoBehaviour {

    public static bool bInitialize = false;
    public static int Seed = 0;


    [SerializeField] private DiceComponent[] dice = null;
    [SerializeField] private Slider ui_slider = null;
    [SerializeField] private Text ui_text = null;

    [SerializeField] private int max_dice_number = 2;

    private int result = 2;

    #region UI Actions

    public void button ( ) {
        bool first = true;
        for(int i = 0; i < dice.Length; ++i ) {
            if(!dice[i].is_complete) {
                first = false; break;
            }
        }

        if ( first ) {
            int[] r = DiceMemorizer.get ( dice.Length, result );
            for ( int i = 0; i < dice.Length; ++i ) {
                dice[i].aspeed = 1f;
                dice[i].add_animation ( 0 );
                dice[i].add_animation ( r[i] );
            }
        } else {
            for ( int i = 0; i < dice.Length; ++i ) {
                dice[i].aspeed = 100f;
            }
        }
    }


    public void slider ( ) {
        result = (int)Mathf.Clamp ( ui_slider.value * 12, 0, 10 ) + 2;
        ui_text.text = "Result:" + result;
    }

    #endregion
    private void Start ( ) {
        if( !bInitialize ) {
            bInitialize = true;
            Seed = System.TimeZoneInfo.ConvertTimeBySystemTimeZoneId ( System.DateTime.UtcNow, "Korea Standard Time" ).Second;
            DiceMemorizer.initialize ( max_dice_number, Seed );
        }
        slider ( );
    }
}
