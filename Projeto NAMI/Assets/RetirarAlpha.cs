using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RetirarAlpha : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Color colorPanel = this.GetComponent<Image>().color;
        this.GetComponent<Image>().color = new Color(colorPanel.r, colorPanel.g, colorPanel.b, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
