using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{

    public Image[] tabImages;
    public Sprite selectedTabSprite;
    public Sprite unselectedTabSprite;
    public GameObject[] pages;

    void Start()
    {
        ActivateTab(0); // Activate the first tab by default
    }

    public void ActivateTab(int tabNumber)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            tabImages[i].sprite = unselectedTabSprite; // Set all tabs to unselected sprite
        }
        pages[tabNumber].SetActive(true); // Activate the selected page
        tabImages[tabNumber].sprite = selectedTabSprite; // Set the selected tab sprite
    }

}
