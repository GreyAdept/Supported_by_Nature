using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikeManager : MonoBehaviour
{
    [SerializeField] private int pikeCount;
    [SerializeField] float pikeScore;
    
    [SerializeField] private MilestoneHandler mh;
    public GameObject[] pikes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {   
        
    }

    void Start()
    {   
        TurnManager.Instance.onTurnChanged.AddListener(UpdatePikes);
        foreach (GameObject pike in pikes)
        {   
            pike.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        }
        mh = GetComponent<MilestoneHandler>();
        pikeCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        pikeScore = (float)mh.currentBiodiversity / (float)mh.maxBiodiversity * 10;
        pikeCount = Mathf.RoundToInt(pikeScore / 2);
        
    }

    private void UpdatePikes(int random)
    {   
        Debug.Log("Updating pikes");
        
        if (pikeCount > 0)
        {
            for (int i = 0; i < pikeCount; i++)
            {
                GameObject p = pikes[i];
                p.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
            }

            for (int i = 4; i > pikeCount; i--)
            {
                GameObject p = pikes[i];
                p.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            }
        }
    }
}
