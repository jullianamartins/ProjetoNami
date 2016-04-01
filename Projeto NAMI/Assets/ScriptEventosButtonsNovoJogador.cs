using UnityEngine;
using System.Collections;

public class ScriptEventosButtonsNovoJogador : MonoBehaviour {

    public Input inputNome;

    public void onClickButtonCancelar(string name)
    {
        Application.LoadLevel("MenuPrincipal");
    }

    public void onClickButtonCadastrar()
    {

        //ConexaoBanco.inserir("clailton", "16", "Masculino", "sem programa");

    }
}
