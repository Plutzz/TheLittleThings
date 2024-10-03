using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacting : InteractableBase {
    public override void Interact() {

        Debug.Log("ur mom");

    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.transform.CompareTag("Player")) {
            Interactable = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if(collider.transform.CompareTag("Player")) {
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
