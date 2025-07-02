using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;

    public bool sessionStarted;
    private float sessionTime;

    public static event System.Action OnSessionStarted;
    public static event System.Action<float> OnSessionTimeChanged;

    public bool paused;


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
    }

    private void Start()
    {
        paused = true;
        //ResetRession();
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
}
