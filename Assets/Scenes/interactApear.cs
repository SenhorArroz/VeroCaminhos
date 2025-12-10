using UnityEngine;

public class interactApear : MonoBehaviour
{
    public GameObject interactUI;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        interactUI.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        interactUI.SetActive(false);
    }
}
