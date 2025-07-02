using UnityEngine;

public abstract class APopUp : MonoBehaviour
{   
   public virtual void OpenPopUp()
   {
        GameObject thisPopUp = this.gameObject;
        thisPopUp.SetActive(true);
        GameObject.Find("Main Elements Group").GetComponent<CanvasGroup>().interactable = false;
   }

   public virtual void ClosePopUp()
   {
        GameObject thisPopUp = this.gameObject;
        thisPopUp.SetActive(false);
        GameObject.Find("Main Elements Group").GetComponent<CanvasGroup>().interactable = true;
    }
}
