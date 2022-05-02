using UnityEngine;

public class DialogCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DIalogComponent dialog))
        {
            other.gameObject.SetActive(false);
            StartDialog(dialog);
            
        }
    }

    private void StartDialog(DIalogComponent dialog)
    {
       
        transform.GetComponent<PlayerMove>().IsDialog = true;
        transform.GetComponent<MouseLook>().IsDialog = true;
        dialog.RelatedNPC.transform.GetChild(1).GetComponent<Dialog>().StartDialog();
        dialog.RelatedNPC.GetComponent<BossDialogSystem>().AskQuestion(true, false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
