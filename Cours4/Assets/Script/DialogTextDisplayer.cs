using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogTextDisplayer : MonoBehaviour {

    private Text textComponent;

    void Awake() {
        textComponent = gameObject.GetComponentInChildren<Text>();
    }

    public void SetDialogText(string newDialogText) {
        textComponent.text = newDialogText;
    }

    public void CloseDialog() {
        Destroy(gameObject);
    }
}