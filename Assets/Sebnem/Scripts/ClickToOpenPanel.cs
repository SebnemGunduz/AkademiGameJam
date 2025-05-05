using UnityEngine;

public class ClickToOpenPanel : MonoBehaviour
{
    public GameObject objectToActivate; // Aktifle�tirilecek obje

    private void OnMouseDown()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }
}
