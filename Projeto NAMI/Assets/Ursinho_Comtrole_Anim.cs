using UnityEngine;
using System.Collections;

public class Ursinho_Comtrole_Anim : MonoBehaviour {
    float tempo_start_anim;
    float tempo_anim;
    // Use this for initialization
    void Start () {
        tempo_anim = 0.5f;
    }
	
	// Update is called once per frame
	void Update () {
        if (this.GetComponent<Animator>().GetBool("Celebration") &&(Time.time - tempo_start_anim > tempo_anim ))
        {
            this.GetComponent<Animator>().SetBool("Celebration", false);
        }
        if (this.GetComponent<Animator>().GetBool("Sad") && (Time.time - tempo_start_anim > tempo_anim))
        {
            this.GetComponent<Animator>().SetBool("Sad", false);
        }
    }
    public void AnimPonto()
    {
        this.GetComponent<Animator>().SetBool("Celebration", true);
        print("GANHOU PONTO!!!");
        tempo_start_anim = Time.time;
       // this.GetComponent<Animator>().SetBool("Celebration", false);
    }
    public void AnimPerda()
    {
        this.GetComponent<Animator>().SetBool("Sad", true);
        tempo_start_anim = Time.time;
        print("PERDEU PONTO!!!");
        // this.GetComponent<Animator>().SetBool("Sad", false);
    }
}
