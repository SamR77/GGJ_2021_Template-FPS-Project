using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interaction : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Interactable";

    public GameObject currentSelectedObj = null;

    //public LayerMask interactableObj;

    public float reach = 5.0f;

    public InteractionObject currentInteractionObjScript;

    private Outline outline;

    // Update is called once per frame
    void Update()
    {
        if (currentSelectedObj != null)
        {
            outline = currentSelectedObj.GetComponent<Outline>();

            outline.enabled = false;
            currentSelectedObj = null;
        }

        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, reach) && hit.transform.tag == "Interactable")
        {
            currentSelectedObj = hit.transform.gameObject;
            currentInteractionObjScript = currentSelectedObj.GetComponent<InteractionObject>();

            var outline = currentSelectedObj.GetComponent<Outline>();
            outline.OutlineMode = Outline.Mode.OutlineAll;
            outline.OutlineColor = Color.yellow;
            outline.OutlineWidth = 7f;
            outline.enabled = true;

            Debug.Log(hit.transform.gameObject.name + " Selected");

            if (Input.GetMouseButtonDown(0) && currentSelectedObj.tag == "Interactable")
            {
                DoObjectAction();
                
                // Debug.Log(hit.transform.gameObject.name + " Clicked");

            }


        }
    }

    private void DoObjectAction()
    {
        if (currentInteractionObjScript.pickup)
        {
            currentInteractionObjScript.Pickup();
        }

        if (currentInteractionObjScript.message)
        {
            currentInteractionObjScript.Info();
        }
        

    }
}
