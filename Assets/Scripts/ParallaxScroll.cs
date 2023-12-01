using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour {

    public Renderer background;
    public float backgroundSpeed = 0.02f;
    public float offset = -3;
    void Update()
    {
            float backgroundOffset = offset * backgroundSpeed;
            background.material.mainTextureOffset = new Vector2(backgroundOffset, 0);
    }
}
