using UnityEngine;

public abstract class DialogueBase : ScriptableObject
{
    [Header("NPC")]
    public string npcName = "Costeicco Guru";
    public LocalizedText npcNameLocalized;
    public Sprite npcImage;
    [Header("Main Dialogue")]
    [TextArea(4, 8)] public string dialogueText;
    public LocalizedText dialogueTextLocalized;
    public int typeSpeed;
}
