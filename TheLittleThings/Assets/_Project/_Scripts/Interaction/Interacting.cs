using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interacting : InteractableBase {
    
        public override void Interact() {
        Debug.Log("E has been pressed.");
    }

    void OnTriggerEnter(Collider collider) {
        if(collider.transform.CompareTag("Player")) {  
            InteractText.instance?.EnableInteractPrompt();
            Interactable = true;
        }
    }

    void OnTriggerExit(Collider collider) {
        if(collider.transform.CompareTag("Player")) {
            InteractText.instance?.DisableInteractPrompt();
            Interactable = false;
        }
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.E)) {
            Interaction = true;
        } else {
            Interaction = false;
        }

        if(Interaction && Interactable) {
            Interact();
        }
    }

}
