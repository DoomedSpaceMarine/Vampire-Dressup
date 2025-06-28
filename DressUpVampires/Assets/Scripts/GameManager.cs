using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int difficultyScale = 1;
    private int victoryPoint;

    private int masqueradeCounter;

    [SerializeField] private AccessorySlot[] accessorySlots;

    [SerializeField] private Button readyButton;

    //Victory Screen
    [SerializeField] private GameObject victoryObject;
    [SerializeField] private TextMeshProUGUI victoryText;
    [SerializeField] private Button continueButton;
    [SerializeField] private TextMeshProUGUI buttonText;

    //Lose Screen
    [SerializeField] private GameObject loseObject;
    [SerializeField] private Button restartButton;

    //Excellence system
    [SerializeField] private TextMeshProUGUI hintText; //Under Victory Screen gameobject

    //Invitation text object
    [SerializeField] private TextMeshProUGUI invitationText;

    private void Awake()
    {
       readyButton.onClick.AddListener(() 
           => RoundVictoryCheck());
        
        continueButton.onClick.AddListener(()
            => CloseVictoryScreen());

        restartButton.onClick.AddListener(()
            => CloseLoseScreen());
    }

    private void Start()
    {
        ResetTagsAndSlots();
        CloseVictoryScreen();
        CloseLoseScreen();
    }

    public void ResetTagsAndSlots()
    {
        switch (masqueradeCounter)
        {
            case 0:
                for (int i = 0; i < accessorySlots.Length; i++)
                {
                    accessorySlots[i].targetTag = AccessoryTags.Invitation1;
                    accessorySlots[i].currentTag = AccessoryTags.None;
                    accessorySlots[i].EmptySlot();
                }
                invitationText.text = "Invitation 1 hint text";
                masqueradeCounter++;
                difficultyScale = 1;
                break;

            case 1:
                for (int i = 0; i < accessorySlots.Length; i++)
                {
                    accessorySlots[i].targetTag = AccessoryTags.Invitation2;
                    accessorySlots[i].currentTag = AccessoryTags.None;
                    accessorySlots[i].EmptySlot();
                }
                invitationText.text = "Invitation 2 hint text";
                masqueradeCounter++;
                difficultyScale = 2;
                break;

            case 2:
                for (int i = 0; i < accessorySlots.Length; i++)
                {
                    accessorySlots[i].targetTag = AccessoryTags.Invitation3;
                    accessorySlots[i].currentTag = AccessoryTags.None;
                    accessorySlots[i].EmptySlot();
                }
                invitationText.text = "Invitation 3 hint text";
                masqueradeCounter++;
                difficultyScale = 3;
                break;

        }
    }

    public void RoundVictoryCheck()
    {
        victoryPoint = 0;

        for (int i = 0; i < accessorySlots.Length; i++)
        {
            if(accessorySlots[i].slotIsFull)
            {

                if (accessorySlots[i].currentTag == accessorySlots[i].targetTag)
                {
                    victoryPoint++;
                }
            }
            else
            {
                Debug.Log("Skipped because null");
            }
            
        }

        if (victoryPoint >= difficultyScale)
        {
            Debug.Log("You win");
            if(masqueradeCounter <= 3)
            {
                victoryObject.SetActive(true);
                victoryText.text = "You win this round. Vampire dies";
                buttonText.text = "Continue";

                if (difficultyScale == 1 && victoryPoint >= 2)
                {
                    hintText.gameObject.SetActive(true);
                    hintText.text = "Hint 1";
                }
                else if (difficultyScale == 2 && victoryPoint >= 3)
                {
                    hintText.gameObject.SetActive(true);
                    hintText.text = "Hint 2";
                }
                else
                {
                    hintText.gameObject.SetActive(false);
                }
            }
            else
            {
                victoryObject.SetActive(true);
                victoryText.text = "All Vampires die and you win the game!";
                buttonText.text = "Restart";
                masqueradeCounter = 0;
            }
            ResetTagsAndSlots();

        }
        else
        {
            Debug.Log("You lose");
            loseObject.SetActive(true);
            masqueradeCounter = 0;
            ResetTagsAndSlots();
        }
    }

    private void CloseVictoryScreen()
    {
        victoryObject.SetActive(false);
    }

    private void CloseLoseScreen()
    {
        loseObject.SetActive(false);
    }
}
