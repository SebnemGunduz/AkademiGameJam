using UnityEngine;

public class ClickToOpenPanel : MonoBehaviour
{
    public GameObject objectToActivate; // Aktifleştirilecek obje

    private void OnMouseDown()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }
}
