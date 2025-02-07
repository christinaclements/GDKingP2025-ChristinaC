using UnityEngine;
using TMPro;
using System.Reflection.Emit;

public class DashIconBehavior : MonoBehaviour {

    TextMeshProUGUI label;
    float cooldown;
    float cooldownRate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){

        label = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update() {
        string message = "";
        if (PinBehavior.cooldown > 0.0) { }
        message = string.Format("{0:0.0}",  PinBehavior.cooldown);
        label.SetText(message);
    }
}
