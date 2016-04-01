using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PainelFinal : MonoBehaviour {

    public Text tempoFinal;
    public Text quantErros;
    public Text mediaConcentracao;
    private string proximaFase;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ativarPainel()
    {
        this.gameObject.SetActive(true);
    }

    public void setInformacoes(string tempoFinal, string quantErros, string mediaConcentracao, string proximaFase)
    {
        this.tempoFinal.text = tempoFinal;
        this.quantErros.text = quantErros;
        this.mediaConcentracao.text = mediaConcentracao;
        this.proximaFase = proximaFase;
    }

    public void onClickProximaFase()
    {
        Application.LoadLevel(this.proximaFase);
    }

    public void onClickMenuPrincipal()
    {
        Application.LoadLevel("MenuPrincipal");
    }


}
