using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Transform child;
    private GameObject panel;
    private Button button;

    void Start()
    {
        child = transform.Find("Wood");

        if (child != null)
        {
            panel = child.gameObject;
        }

        button = GetComponent<Button>();    

        panel.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("can detect hover");
        panel.SetActive(true);
        button.interactable = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exit hover");
        panel.SetActive(false);
        button.interactable = false;
    }
}
