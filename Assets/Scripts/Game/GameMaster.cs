using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;

    public bool sessionStarted;
    private float sessionTime;

    public static event System.Action OnSessionStarted;
    public static event System.Action<float> OnSessionTimeChanged;
    public static event System.Action<bool> OnPaused;
    public static event System.Action OnSessionFinished;


    private bool _paused;
    public bool paused
    {
        get { return _paused; }
        set
        {
            _paused = value;
            OnPaused?.Invoke(value);
            
        }
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        paused = true;
        ClockScript.OnTimerFinished += EndSession;

    }


    private void LatePause()
    {
        paused = true;
    }

    private void Start()
    { 
        Invoke("LatePause", 0.1f);
    }

    private void OnDisable()
    {
        ClockScript.OnTimerFinished -= EndSession;
    }


    public void ResetRession()
    {
        sessionTime = 0;
        paused = false;
        sessionStarted = true;
        OnSessionStarted?.Invoke();
    }


    private void Update()
    {   
        if (!paused)
        {
            sessionTime += Time.deltaTime;
            OnSessionTimeChanged?.Invoke(sessionTime);
        }
        
    }

    private void EndSession()
    {
        sessionStarted = false;
        paused = true;
        OnSessionFinished?.Invoke();
    }
}
