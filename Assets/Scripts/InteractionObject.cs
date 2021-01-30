using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionObject : MonoBehaviour
{


    [Header("this object can be picked up")]
    public bool pickup;

    [Header("Used for objects that give simple info about themsleves")]
    public bool message;
    public string text;

    private TextMeshProUGUI infoText;   


    void Start()
    {
        infoText = GameObject.Find("Info_TMP").GetComponent<TextMeshProUGUI>();
    }


    public void Info()
    {
        StartCoroutine(ShowInfo(text, 5.0f));
        //Debug.Log("Yes this is a " + this.gameObject.name);
    }

    public void Pickup()
    {
        StartCoroutine(ShowInfo("You Picked Up " + this.gameObject.name, 5.0f));
        this.gameObject.SetActive(false);
        //Debug.Log("You Picked up " + this.gameObject.name);


        // Add logic here to add the item to an inventory A list or trigger a bool.. whatever


    }

  


    IEnumerator ShowInfo(string text, float delay)
    {
        infoText.text = text;
        yield return new WaitForSeconds(delay);
        infoText.text = null;
    }


}






