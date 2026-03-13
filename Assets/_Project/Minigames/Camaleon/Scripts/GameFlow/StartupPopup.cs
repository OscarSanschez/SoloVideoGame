using UnityEngine;

public class StartupPopup : MonoBehaviour
{
    public GameObject popupPanel;

    void Start()
    {
        // Show popup when the scene starts
        popupPanel.SetActive(true);
    }

    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }
}