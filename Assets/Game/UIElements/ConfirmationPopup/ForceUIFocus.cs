using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ForceUIFocus : MonoBehaviour
{
    [SerializeField] private Button defaultButton;
    private GameObject lastSelected;

    public void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (lastSelected != null
                && lastSelected.gameObject.activeSelf
                && lastSelected.GetComponent<Button>() != null
                && lastSelected.GetComponent<Button>().interactable)
            {
                EventSystem.current.SetSelectedGameObject(lastSelected);
            }
        }
        else
        {
            lastSelected = EventSystem.current.currentSelectedGameObject;
        }

    }

    public void SetDefaultButton(Button defaultButton)
    {
        this.defaultButton = defaultButton;
    }

    // make sure the default button is selected
    public void SelectDefaultButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        defaultButton.Select();
    }

    private void OnBecameInvisible()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

}