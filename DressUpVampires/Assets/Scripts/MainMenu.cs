using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;

    //Credits
    [SerializeField] private GameObject creditsCanvas;
    [SerializeField] private Button returnButton;

    private void Awake()
    {
        startButton.onClick.AddListener(()
            => StartButton());

        creditsButton.onClick.AddListener(()
            => CreditsButton());

        exitButton.onClick.AddListener(()
            => ExitButton());

        returnButton.onClick.AddListener(()
            => CloseCredits());

        CloseCredits();
    }

    private void StartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void CreditsButton()
    {
        creditsCanvas.SetActive(true);
    }

    private void ExitButton()
    {
        Application.Quit();
    }

    private void CloseCredits()
    {
        creditsCanvas.SetActive(false);
    }
}
