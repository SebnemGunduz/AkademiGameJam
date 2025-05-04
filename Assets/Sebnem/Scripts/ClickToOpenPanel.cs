using UnityEngine;

public class ClickToOpenPanel : MonoBehaviour
{
    public GameObject panelToOpen;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol týk
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (panelToOpen != null)
                {
                    panelToOpen.SetActive(true);
                    Time.timeScale = 0f; // Ýsteðe baðlý
                }
            }
        }
    }
}
