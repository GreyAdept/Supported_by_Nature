using TMPro;
using UnityEngine;


public class ClockScript : MonoBehaviour
{
    private TextMeshProUGUI timeText;
    private int _seconds;

    private bool timerTrigger;
    public int Seconds
    {
        get { return _seconds; }
        set
        {
            if (value == _seconds) //if the previous value is the same as current, do nothing
            {
                _seconds = value;
            }
            else
            {
                OnSecondsChanged?.Invoke(_seconds);
                countdownTime -= 1;
                if (countdownTime > 0) //decrease countdown timer
                {
                    var countdownMinutes = Mathf.FloorToInt(countdownTime / 60);
                    var countdownSeconds = Mathf.FloorToInt(countdownTime - countdownMinutes * 60);
                    prettyTime = string.Format("{0:00}:{1:00}", countdownMinutes, countdownSeconds);
                    timeText.text = prettyTime;
                }
                else
                {
                    if (timerTrigger == false) //when timer runs out, end the game
                    {
                        Debug.Log("Time run out!");
                        OnTimerFinished?.Invoke();
                        timerTrigger = true;
                    }                
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
    public static event System.Action OnTimerFinished;

    private int countdownTime = 60; 
    private int countdownSeconds;
    private int countdownMinutes;
   
    private void Awake()
    {   
        timeText = GetComponentInChildren<TextMeshProUGUI>();

        GameMaster.OnSessionTimeChanged += GetSeconds;
    }

    private void OnDisable()
    {
        GameMaster.OnSessionTimeChanged -= GetSeconds;
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

    private void GetSeconds(float context)
    {
        Seconds = Mathf.FloorToInt(context - minutes * 60);
    }

}
