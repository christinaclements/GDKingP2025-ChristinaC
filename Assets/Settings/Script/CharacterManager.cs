using TMPro;
using UnityEngine;

public class PinsDB : MonoBehaviour {
    public Pins pinsDB;

    public static int selection = 0;
    public SpriteRenderer sprite;
    public TMP_Text nameLabel;
    private void Start(){
        updateCharacter();
    }
    public void updateCharacter() { 
        Pin current = pinsDB.getPin(selection);
        sprite.sprite = current.prefab.GetComponent<SpriteRenderer>().sprite;
        nameLabel.SetText(current.name);
    }
    public void next(){
        int numberPins = pinsDB.count();
        if (selection < numberPins - 1){
            selection = selection + 1;
        }
        else {
            selection = 0;
        }
        updateCharacter();
    }
    public void previous() {
        if (selection > 0)
        {
            selection = selection - 1;
        }
        else {
            selection = pinsDB.count() - 1;
        }
        updateCharacter();
    }
}
