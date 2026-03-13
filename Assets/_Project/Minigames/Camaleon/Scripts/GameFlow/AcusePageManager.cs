using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AccuseMenuManager : MonoBehaviour
{
    [Header("Confirmation Popup")]
    public GameObject confirmationPanel;
    public TMP_Text confirmationText;

    [Header("Result Panels")]
    public GameObject victoryPanel;
    public GameObject lossPanel;

    [Header("Optional")]
    public TMP_Text resultText;

    private int selectedNpcIndex = -1;

    // Hardcoded: option 5 is the chameleon
    private const int chameleonIndex = 5;

    private void Start()
    {
        confirmationPanel.SetActive(false);
        victoryPanel.SetActive(false);
        lossPanel.SetActive(false);
    }

    public void SelectNPC(int npcIndex)
    {
        selectedNpcIndex = npcIndex;
        confirmationPanel.SetActive(true);
        confirmationText.text = "¿Quieres acusar al NPC " + npcIndex + " de ser el camaleón?";
    }

    public void ConfirmAccusation()
    {
        confirmationPanel.SetActive(false);

        if (selectedNpcIndex == chameleonIndex)
        {
            victoryPanel.SetActive(true);

            if (resultText != null)
                resultText.text = "¡Correcto! El NPC " + selectedNpcIndex + " era el camaleón.";
        }
        else
        {
            lossPanel.SetActive(true);

            if (resultText != null)
                resultText.text = "Incorrecto. El NPC " + selectedNpcIndex + " no era el camaleón.";
        }
    }

    public void CancelAccusation()
    {
        confirmationPanel.SetActive(false);
        selectedNpcIndex = -1;
    }

    public void CloseResultPanels()
    {
        // Application.Quit(); // Works with the built game
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
