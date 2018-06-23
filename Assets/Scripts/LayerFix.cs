using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerFix : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "player";
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = -1;
    }
	
}
