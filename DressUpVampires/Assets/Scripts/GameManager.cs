using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int difficultyScale = 2;
    private int victoryPoint;

    [SerializeField] private AccessorySlot[] accessorySlots;

    [SerializeField] private Button readyButton;

   private GameObject tempGameObject;
   private DragItem tempStorage;

    private void Awake()
    {
       readyButton.onClick.AddListener(() 
           => RoundVictoryCheck());
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
        }
        else
        {
            Debug.Log("You lose");
        }
    }
}
