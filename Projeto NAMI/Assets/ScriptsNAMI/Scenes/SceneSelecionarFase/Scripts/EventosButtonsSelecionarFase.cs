using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EventosButtonsSelecionarFase : MonoBehaviour {

    public Button buttonFase1;
    public Button buttonFase2;
    public Button buttonFase3;
    public Button buttonFase4;
    public Button buttonFase5;
    public Button buttonFase6;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void eventoButtonFase1(string name)
    {
        Application.LoadLevel("Fase1");
    }


    public void eventoButtonFase2(string name)
    {
        Application.LoadLevel("Fase2");
    }


    public void eventoButtonFase3(string name)
    {
        Application.LoadLevel("Fase3");
    }


    public void eventoButtonFase4(string name)
    {
        Application.LoadLevel("Fase4");
    }


    public void eventoButtonFase5(string name)
    {
        Application.LoadLevel("Fase5");
    }


    public void eventoButtonFase6(string name)
    {
        Application.LoadLevel("LotsOfBlocks");
    }

    
    public void eventoButtonVoltar(string name)
    {
        Application.LoadLevel("MenuPrincipal");
    }

}
