using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        startButton.onClick.AddListener(()
            => StartButton());

        creditsButton.onClick.AddListener(()
            => CreditsButton());

        exitButton.onClick.AddListener(()
            => ExitButton());   
    }

    private void StartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void CreditsButton()
    {

    }

    private void ExitButton()
    {
        Application.Quit();
    }
}
