using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    Image activeMaskImage;

    [SerializeField]
    Image selectedMaskImage;

    // Update is called once per frame
    void Update()
    {
        activeMaskImage.sprite = player.showActive();
        selectedMaskImage.sprite = player.showSelection();
    }
}
