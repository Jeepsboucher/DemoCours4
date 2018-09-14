using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour {
    [SerializeField] public GameObject dialogPrefab;
    [SerializeField] public GameObject mainCanvas;

    private bool actionAxisInUser = true;
    private GameObject player;
    private bool dialogIsInitiated = false;
    private DialogText currentDialog;
    private DialogDisplayer currentDialogDisplayer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        ProcessInput();
    }

    public void StartDialog(DialogText newDialog)
    {
        dialogIsInitiated = true;
        player.GetComponent<PlayerMovement>().DisableControl();
        currentDialog = newDialog;
        GameObject currentDialogObject = Instantiate(dialogPrefab, mainCanvas.transform);
        currentDialogDisplayer = currentDialogObject.GetComponent<DialogDisplayer>();
        currentDialogDisplayer.SetDialogText(currentDialog.GetDialogText());
    }

    private void ProcessInput()
    {
        if(ShouldProcessInput())
        {
            actionAxisInUser = true;
            if(currentDialog.IsNextDialog())
            {
                currentDialog = currentDialog.GetNextDialog();
                currentDialogDisplayer.SetDialogText(currentDialog.GetDialogText());
            }
            else
            {
                EndDialog();
            }
        }
        ValideAxisInUser();
    }

    private void EndDialog()
    {
        dialogIsInitiated = false;
        currentDialogDisplayer.CloseDialog();
        player.GetComponent<PlayerMovement>().EnableControl();
        currentDialog = null;
    }

    private bool ShouldProcessInput()
    {
        if(dialogIsInitiated)
        {
            if(!actionAxisInUser && Input.GetAxis("Jump") != 0)
            {
                return true;
            }
        }
        return false;
    }

    private void ValideAxisInUser()
    {
        if(Input.GetAxis("Jump") != 0)
        {
            actionAxisInUser = true;
        }
        else
        {
            actionAxisInUser = false;
        }
    }
}
