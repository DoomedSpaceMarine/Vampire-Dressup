using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //Arrays for all accessory types
    [SerializeField] private DragItem[] head;
    [SerializeField] private DragItem[] torso;
    [SerializeField] private DragItem[] legs;
    [SerializeField] private DragItem[] feet;

    public int inventoryState; // 0 = Head, 1 = Torso, 2 = Legs, 3 = Feet

    //Sort buttons for accessory types
    [SerializeField] private Button headButton;
    [SerializeField] private Button torsoButton;
    [SerializeField] private Button legsButton;
    [SerializeField] private Button feetButton;

    private void Awake()
    {
        headButton.onClick.AddListener(()
            => ShowHeadAccessories());

        torsoButton.onClick.AddListener(()
            => ShowTorsoAccessories()); 

        legsButton.onClick.AddListener(()
            => ShowLegsAccessories());

        feetButton.onClick.AddListener(()
            => ShowFeetAccessories());
    }

    private void Start()
    {
        ShowHeadAccessories();
    }

    public void ShowHeadAccessories()
    {
        inventoryState = 0;
        for(int i = 0; i < head.Length; i++)
        {
            if (head[i].isInventory)
            {
                head[i].gameObject.SetActive(true);
            }

            torso[i].gameObject.SetActive(false);
            legs[i].gameObject.SetActive(false);
            feet[i].gameObject.SetActive(false);
        }
    }

    private void ShowTorsoAccessories()
    {
        inventoryState = 1;
        for (int i = 0; i < torso.Length; i++)
        {
            if (torso[i].isInventory)
            {
                torso[i].gameObject.SetActive(true);
            }

            head[i].gameObject.SetActive(false);
            legs[i].gameObject.SetActive(false);
            feet[i].gameObject.SetActive(false);
        }
    }

    private void ShowLegsAccessories()
    {
        inventoryState = 2;
        for (int i = 0; i < legs.Length; i++)
        {
            if (legs[i].isInventory)
            {
                legs[i].gameObject.SetActive(true);
            }

            head[i].gameObject.SetActive(false);
            torso[i].gameObject.SetActive(false);
            feet[i].gameObject.SetActive(false);
        }
    }

    private void ShowFeetAccessories()
    {
        inventoryState = 3;
        for (int i = 0; i < feet.Length; i++)
        {
            if (feet[i].isInventory)
            {
                feet[i].gameObject.SetActive(true);
            }

            head[i].gameObject.SetActive(false);
            torso[i].gameObject.SetActive(false);
            legs[i].gameObject.SetActive(false);
        }
    }


}
