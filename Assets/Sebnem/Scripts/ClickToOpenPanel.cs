using UnityEngine;

public class ClickToOpenPanel : MonoBehaviour
{
    public GameObject objectToActivate; // Aktifleþtirilecek obje

    private void OnMouseDown()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }
}
