using UnityEngine;
using System.Collections;
using ProjetoNami.BD;
using ProjetoNami.Model;
using UnityEngine.UI;

public class EventosBotoesMenuPrincipal : MonoBehaviour {

    private UsuarioDao usuarioDao = new UsuarioDao();
    private Usuario usuarioSelecionado;

    public Dropdown dropDownJogadores;
    public System.Collections.Generic.List<Usuario> listaUsuarios;



    void Awake()
    {
        Utils.salvarUsuarioSelecionadoPlayerPrefs("False");
    }


    void Start()
    {
        dropDownJogadores.options = gerarListaUsuarios();
    }


    //Evento do botão de Sair. Irá fechar o jogo
    public void onClickButtonSair(string name)
    {
		Debug.Log ("Executou botao sair");
        PlayerPrefs.DeleteAll();
		Application.Quit ();
	}


    //Evento do botão de Novo. Irá para a tela de criar um novo personagem
    public void onClickButtonNovo(string name)
    {
        Debug.Log(name);
        Debug.Log("Execultou o botão de Novo");
        Application.LoadLevel("MenuNovoJogador");
    }


    //Evento do botão de continuar o jogo. Irá para a tela da fase onde o jogador parou
    public void onClickButtonContinuarJogo(string name)
    {
        if (Utils.verificarSeExisteUsuarioSelecionadoPlayerPrefs())
        {
            Debug.Log("Execultou o botão de continuar o jogo");
            Application.LoadLevel(usuarioSelecionado.proximaFase);
        }
        else
        {
            UnityEditor.EditorUtility.DisplayDialog("Usuário não selecionado", "Por favor selecione um jogador para poder selecionar um jogador", "Ok");
        }
        
    }


    //Evento do botão de selecionar fases. Irá para a tela de selecionar fases
    public void onClickButtonSelecionarFases(string name)
    {
        Application.LoadLevel("SelecionarFase");
    }


    //Evento do botão de relatórios. Irá para a tela de relatórios
    public void onClickButtonRelatorios(string name)
    {
        Debug.Log("Execultou o botão de continuar o jogo");
        Application.LoadLevel("MenuRelatorios");
    }


    /**
    * Método que busca todos os usuários que estão cadastrados no banco de dados.
    * Retorna uma lista de objetos que podem ser inseridos no Dropdown de selecionar os jogadores.
    **/
    private System.Collections.Generic.List<Dropdown.OptionData> gerarListaUsuarios()
    {

        listaUsuarios = usuarioDao.consultarUsuario();

        Dropdown.OptionData primeiroItem = new Dropdown.OptionData("Selecione o Jogador");
        System.Collections.Generic.List<Dropdown.OptionData> listaDropDown = new System.Collections.Generic.List<Dropdown.OptionData>();
        listaDropDown.Add(primeiroItem);

        foreach (Usuario u in listaUsuarios)
        {
            Dropdown.OptionData item = new Dropdown.OptionData(u.nome);
            listaDropDown.Add(item);
        }

        return listaDropDown;
    }


    /**
    * Método da seleção do DropDown. Quando for selecionado ele vai salvar nas prefs do Unity.
    **/
    public void onSelectJogadorDropDown(string name)
    {
        Debug.Log("Número do selecionado: " + dropDownJogadores.value);
        usuarioSelecionado = listaUsuarios[dropDownJogadores.value - 1];
        Utils.salvarUsuarioPlayerPrefs(usuarioSelecionado.id, usuarioSelecionado.nome, usuarioSelecionado.idade, usuarioSelecionado.sexo, usuarioSelecionado.programa, usuarioSelecionado.patologia, usuarioSelecionado.proximaFase);
        Utils.salvarUsuarioSelecionadoPlayerPrefs("True");
    }


    
}
