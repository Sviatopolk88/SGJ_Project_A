using UnityEngine;

public class DialogCheck : MonoBehaviour
{
    public static bool IsDialog = false;
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
        IsDialog = true;
        /*
        var pistol = FindObjectOfType<Pistol>();
        var shotgun = FindObjectOfType<Shotgun>();
        if (pistol != null)
        {
            pistol.IsDialog = true;
        }
        if (shotgun != null)
        {
            shotgun.IsDialog = true;
        }
       
        transform.GetComponent<PlayerMove>().IsDialog = true;
        transform.GetComponent<MouseLook>().IsDialog = true;
        */
        dialog.RelatedNPC.transform.GetChild(1).GetComponent<Dialog>().StartDialog();
        dialog.RelatedNPC.GetComponent<BossDialogSystem>().AskQuestion(true, false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
