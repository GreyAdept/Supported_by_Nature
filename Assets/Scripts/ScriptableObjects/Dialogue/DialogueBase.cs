using UnityEngine;

public abstract class DialogueBase : ScriptableObject
{
    [Header("NPC")]
    public string npcName = "Costeicco Guru";
    public Sprite npcImage;
    [Header("Main Dialogue")]
    [TextArea(4,8)] public string dialogueText;
    public int typeSpeed;
}
