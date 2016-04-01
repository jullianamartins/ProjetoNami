using UnityEngine;
using System.Collections;

public class Cam_Anim_Book : MonoBehaviour {
    bool start;
    float tempoInicial;
	// Use this for initialization
	void Start () {
        start = true;
        GameObject.Find("livro_fase 1").GetComponent<Animator>().SetBool("Open", true);
        tempoInicial = Time.time;
        /*
        GameObject[] objscene = GameObject.FindGameObjectsWithTag("ItenScena");
        foreach(GameObject obj in objscene)
        {

        }
        */
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time - tempoInicial >= 0.72)
        {

            if (GameObject.Find("livro_fase 1").GetComponent<Animator>().GetBool("Open") && start)
            {

                start = false;

                GameObject[] objscene = GameObject.FindGameObjectsWithTag("ItemScena");

                foreach (GameObject obj in objscene)
                {

                    obj.GetComponent<MeshRenderer>().enabled = true;
                }

            }
        }
	}
}
