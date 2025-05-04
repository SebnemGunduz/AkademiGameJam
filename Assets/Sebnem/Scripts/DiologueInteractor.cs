using UnityEngine;

public class DialogueInteractor : MonoBehaviour
{
    public DialogueManager dialogueManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DialogueTrigger trigger = collision.GetComponent<DialogueTrigger>();
        if (trigger != null)
        {
            // Trigger’ý doðrudan DialogueManager’a gönder
            dialogueManager.StartDialogue(trigger, this.tag);
        }
    }
}
