using UnityEngine;
using UnityEngine.UI;

public class ActionButtonHighlighter : MonoBehaviour
{

    private Image image;
    private Color color = Color.white;
    private bool isHighlighted;


    void Start()
    {
        image = GetComponent<Image>();
        isHighlighted = true;
    }

  
    void Update()
    {
        if (isHighlighted)
        {
            color.b = Mathf.Sin(Time.time * 7);
            image.color = color;
        }
    }
}
