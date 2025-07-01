using TMPro;
using UnityEngine;


public class ClockScript : MonoBehaviour
{
    private TextMeshProUGUI timeText;
    private int _seconds;
    public int Seconds
    {
        get { return _seconds; }
        set
        {
            if (value == _seconds)
            {
                _seconds = value;
            }
            else
            {
                OnSecondsChanged?.Invoke(_seconds);
                countdownTime -= 1;
                if (countdownTime > 0)
                {
                    var countdownMinutes = Mathf.FloorToInt(countdownTime / 60);
                    var countdownSeconds = Mathf.FloorToInt(countdownTime - countdownMinutes * 60);
                    prettyTime = string.Format("{0:00}:{1:00}", countdownMinutes, countdownSeconds);
                    timeText.text = prettyTime;
                }
                //Debug.Log("Ticked!");
                _seconds = value;
            }
            
        }
    }
    private int minutes;
    private string prettyTime;

    private int lastSecond;
    public static event System.Action<int> OnSecondsChanged;

    private int countdownTime = 600; 
    private int countdownSeconds;
    private int countdownMinutes;
   
    private void Awake()
    {   
        timeText = GetComponentInChildren<TextMeshProUGUI>();

        GameMaster.OnSessionTimeChanged += (float context) =>
        {
            Seconds = Mathf.FloorToInt(context - minutes * 60);
        };
    }

    private void Update()
    {
        if (GameMaster.Instance.paused)
        {
            timeText.color = Color.yellow;
        }
        else
        {
            timeText.color = Color.white;
        }
    }

}
