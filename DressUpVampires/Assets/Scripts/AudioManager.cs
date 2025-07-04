using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource selectClothing;
    [SerializeField] private AudioSource dressClothing;

    public void SFX_SelectClothing()
    {
        selectClothing.Play();
    }

    public void SFX_DressClothing()
    {
        dressClothing.Play();
    }
}
