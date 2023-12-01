using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverAnimController : MonoBehaviour {
    public void ReactionOnButton(bool Menu) {
        if(Menu) Application.LoadLevel("Start");
        Application.LoadLevel("Game");
    }
}
