using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUI_CloseEscMenu : MonoBehaviour
{
    [SerializeField] GameObject hotbar;
    [SerializeField] GameObject esc;
    [SerializeField] PlayerController player;
    public void PlayButton(){
        esc.SetActive(false);
        hotbar.SetActive(true);
        player.UnlockMovement();
    }
}