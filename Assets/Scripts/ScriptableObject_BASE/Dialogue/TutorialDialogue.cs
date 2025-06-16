using UnityEngine;

[CreateAssetMenu(fileName = "Tutorial", menuName = "Dialogue/Tutorial")]
public class TutorialDialogue : DialogueBase
{
    [Header("Tutorial Identifier")]
    public string taskId;
    [Header("Tutorial Info")]
    public string taskDescription;
    [Header("Tutorial Sequence")]
    public TutorialDialogue nextDialogue;
    [Header("Completion (dont touch)")]
    public bool isCompleted = false;

    public void ResetTutorialState() // reset in new game start
    {
        isCompleted = false;
    }

}
