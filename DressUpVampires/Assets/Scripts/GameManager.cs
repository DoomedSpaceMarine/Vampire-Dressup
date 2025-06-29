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
    [SerializeField] private Image victoryImage;
    [SerializeField] private Sprite youWinScreen;
    [SerializeField] private Sprite nextClueScreen;
    [SerializeField] private Sprite nothingGainedScreen;
    [SerializeField] private Sprite revengeSprite;

    //Lose Screen
    [SerializeField] private GameObject loseObject;
    [SerializeField] private Button restartButton;

    //Excellence system
    [SerializeField] private TextMeshProUGUI hintText; //Under Victory Screen gameobject
    private bool hint1;
    private string storeHint1;
    private bool hint2;
    private string storeHint2;

    //Invitation text object
    [SerializeField] private TextMeshProUGUI invitationText;

    //Opening Screen
    public GameObject openingScreen;

    public bool isThisEnd;

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
                invitationText.text = "Invitation 1\r\nWe kindly invite you to the Seven Seas Masquarade of Lady Selene Zvuk. We ask you to the dress to the occasion.\r\nNote:\r\nWe welcome hair thats light like sea foam,\r\nclothes that remind us of fearless swashbucklers\r\nwe especially adore puffy sleaves";
                masqueradeCounter++;
                hint1 = false;
                hint2 = false;
                difficultyScale = 1;
                break;

            case 1:
                for (int i = 0; i < accessorySlots.Length; i++)
                {
                    accessorySlots[i].targetTag = AccessoryTags.Invitation2;
                    accessorySlots[i].currentTag = AccessoryTags.None;
                    accessorySlots[i].EmptySlot();
                }
                invitationText.text = "Invitation 2\r\nWith respect we invite you to the Royal Masqurade as hosted by Countess Ramona Renfield. We ask you to dress to the occasion.\r\nNote:\r\nOur countess is looking for well tailored suits that do not shy away from a great amounts of fabric, including but not limited to ruffles.";
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
                if(hint1 & !hint2)
                {
                    invitationText.text = "Invitation 3\r\nWith utmost respect we invite you to the Skin Masquerade, lovingly hosted by the feared Lord Relyn Von Valtien. We ask you to dress to the occasion.\r\nNote:\r\nA person should feel comfort in their own skin. Tonight you may show just how comfortable you are… \r\n The host enjoys…" + storeHint1;
                }
                else if(!hint1 & hint2)
                {
                    invitationText.text = "Invitation 3\r\nWith utmost respect we invite you to the Skin Masquerade, lovingly hosted by the feared Lord Relyn Von Valtien. We ask you to dress to the occasion.\r\nNote:\r\nA person should feel comfort in their own skin. Tonight you may show just how comfortable you are… \r\n The host enjoys…" + storeHint2;
                }
                else if (hint1 & hint2)
                {
                    invitationText.text = "Invitation 3\r\nWith utmost respect we invite you to the Skin Masquerade, lovingly hosted by the feared Lord Relyn Von Valtien. We ask you to dress to the occasion.\r\nNote:\r\nA person should feel comfort in their own skin. Tonight you may show just how comfortable you are… \r\n The host enjoys… " + storeHint1 + storeHint2;
                }
                else
                {
                    invitationText.text = "Invitation 3\r\nWith utmost respect we invite you to the Skin Masquerade, lovingly hosted by the feared Lord Relyn Von Valtien. We ask you to dress to the occasion.\r\nNote:\r\nA person should feel comfort in their own skin. Tonight you may show just how comfortable you are…";
                }
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
            if(masqueradeCounter < 3)
            {
                victoryObject.SetActive(true);
                victoryText.text = "You win this round. Vampire dies";
                buttonText.text = "Continue";

                if (difficultyScale == 1 && victoryPoint >= 2)
                {
                    hintText.gameObject.SetActive(true);
                    storeHint1 = hintText.text = "\r\n A tired look";
                    hint1 = true;
                    victoryImage.sprite = nextClueScreen;
                }
                else if (difficultyScale == 2 && victoryPoint >= 3)
                {
                    hintText.gameObject.SetActive(true);
                    storeHint2 = hintText.text = "\r\n Bare skin\r\nwell worn beads/pearls/buttons";
                    hint2 = true;
                    victoryImage.sprite = nextClueScreen;
                }
                else
                {
                    hintText.gameObject.SetActive(false);
                    victoryImage.sprite = nothingGainedScreen;
                }
            }
            else
            {
                victoryObject.SetActive(true);
                victoryText.text = "All Vampires die and you win the game!";
                hintText.gameObject.SetActive(false);
                isThisEnd = true;
                buttonText.text = "Restart";
                masqueradeCounter = 0;
                victoryImage.sprite = revengeSprite;
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
        openingScreen.SetActive(true);
    }

    public void CloseOpeningScreen()
    {
        openingScreen.SetActive(false);
    }

    public void OpenOpeningScreen()
    {
        if (isThisEnd)
        {
            openingScreen.SetActive(true);
            isThisEnd= false;
        }    
    }
}
