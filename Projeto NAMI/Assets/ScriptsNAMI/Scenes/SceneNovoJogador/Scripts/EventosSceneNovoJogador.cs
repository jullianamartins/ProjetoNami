using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ProjetoNami.BD;

public class EventosSceneNovoJogador : MonoBehaviour {

    public InputField inputFieldNomeJogador;
    public InputField inputFieldIdadeJogador;
    public InputField inputFieldPatologiaJogador;
    public ToggleGroup radioButtonSexoJogador;

    private string nomeJogador;
    private string idadeJogador;
    private string sexoJogador = "Masculino";
    private string patologiaJogador;
    private UsuarioDao usuarioDao;


	public void onClickButtonCancelar(string name)
    {
        Debug.Log("Executou o botão de cancelar");
        Application.LoadLevel("MenuPrincipal");
    }


    public void onClickButtonCadastrar(string name)
    {
        try
        {
            usuarioDao = new UsuarioDao();
            usuarioDao.salvarUsuario(nomeJogador, idadeJogador, sexoJogador, "sem programa", patologiaJogador);
            Debug.Log("Nome Jogador: "+ nomeJogador + " Idade Jogador: "+ idadeJogador + " Sexo Jogador: "+ sexoJogador + " Patologia Jogador: " + patologiaJogador);
            UnityEditor.EditorUtility.DisplayDialog("Usuário cadastrado", "Usuário cadastrado com sucesso.", "Ok");
        } catch(System.Exception e)
        {
            UnityEditor.EditorUtility.DisplayDialog("Erro no cadastro", "Ocorreu um erro no cadastro, por favor tente novamente.", "Ok");
        }
    }


    public void onTextFieldNome(string name)
    {
        nomeJogador = inputFieldNomeJogador.text;
    }


    public void onTextFieldIdade(string name)
    {
        idadeJogador = inputFieldIdadeJogador.text;
    }


    public void onTextFieldPatologia(string name)
    {
        patologiaJogador = inputFieldPatologiaJogador.text;
    }


    public void onClickRadioButton(string name)
    {
        sexoJogador = name;
    }


}
