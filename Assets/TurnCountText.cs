using TMPro;
using UnityEngine;

public class TurnCountText : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<TMP_Text>().text = TurnManager.Instance.CurrentTurn.ToString();

    }
}
