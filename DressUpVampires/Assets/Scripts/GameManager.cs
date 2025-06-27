using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int difficultyScale = 2;
    private int victoryPoint;

    private int masqueradeCounter;

    [SerializeField] private AccessorySlot[] accessorySlots;

    [SerializeField] private Button readyButton;

   private GameObject tempGameObject;
   private DragItem tempStorage;

    private void Awake()
    {
       readyButton.onClick.AddListener(() 
           => RoundVictoryCheck());
    }

    private void Start()
    {
        ResetTagsAndSlots();
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
                masqueradeCounter++;
                break;

            case 1:
                for (int i = 0; i < accessorySlots.Length; i++)
                {
                    accessorySlots[i].targetTag = AccessoryTags.Invitation2;
                    accessorySlots[i].currentTag = AccessoryTags.None;
                    accessorySlots[i].EmptySlot();
                }
                masqueradeCounter++;
                break;

            case 2:
                for (int i = 0; i < accessorySlots.Length; i++)
                {
                    accessorySlots[i].targetTag = AccessoryTags.Invitation3;
                    accessorySlots[i].currentTag = AccessoryTags.None;
                    accessorySlots[i].EmptySlot();
                }
                masqueradeCounter++;
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
            ResetTagsAndSlots();

        }
        else
        {
            Debug.Log("You lose");
            masqueradeCounter = 0;
            ResetTagsAndSlots();
        }
    }
}
