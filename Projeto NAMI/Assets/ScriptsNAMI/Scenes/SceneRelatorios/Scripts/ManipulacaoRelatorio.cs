using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ProjetoNami.Model;
using ProjetoNami.BD;
using System;
using System.Collections;


[System.Serializable]
public class Item
{
    public string fase;
    public string tempoFinal;
    public string concentracao;
    public string quantErro;
    public string dataRealizada;
}

public class ManipulacaoRelatorio : MonoBehaviour
{

    UsuarioDao usuarioDao = new UsuarioDao();
    FaseDao faseDao = new FaseDao();

    public Dropdown dropDownSelecionarJogador;

    public InputField inputFieldNomeJogador;
    public InputField inputFieldIdadeJogador;
    public InputField inputFieldPatologiaJogador;
    public Toggle toggleMasculino;
    public Toggle toggleFeminino;

    private System.Collections.Generic.List<Fase> listaDeFases;

    //Atributos do ScrollView
    public GameObject itemRelatorio;
    public System.Collections.Generic.List<Item> itemList;
    public Transform contentPanel;


    private Usuario usuarioSelecionado;

    System.Collections.Generic.List<Usuario> listaUsuarios;

    // Use this for initialization

    void Start()
    {
        Dropdown.OptionData dado = new Dropdown.OptionData("");
        dropDownSelecionarJogador.options = gerarListaUsuarios();
    }

    // Update is called once per frame
    void Update()
    {
    }

    /**
    * Método que busca todos os usuários que estão cadastrados no banco de dados.
    * Retorna uma lista de objetos que podem ser inseridos no Dropdown de selecionar os jogadores.
    **/
    public System.Collections.Generic.List<Dropdown.OptionData> gerarListaUsuarios()
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
    * Método da seleção de um Jogador no Dropdown.
    **/
    public void onSelectJogador()
    {
        Debug.Log("Número do selecionado: " + dropDownSelecionarJogador.value);
        usuarioSelecionado = listaUsuarios[dropDownSelecionarJogador.value - 1];
        setDadosJogador(usuarioSelecionado.nome, usuarioSelecionado.idade, usuarioSelecionado.sexo, usuarioSelecionado.patologia);
    }

    public void gerarRelatorio()
    {
        listaDeFases = faseDao.consultarFasesUsuario(usuarioSelecionado.id);
        itemList.Clear();
        popularItemList();
    }

    //Método de popular a lista de itens
    private void popularItemList()
    {

        Debug.Log("Chegou ao menu de popular a lista ");
        foreach (Fase fase in listaDeFases)
        {
            Item novoItem = new Item();
            novoItem.fase = fase.nome;
            novoItem.concentracao = fase.concentracao;
            novoItem.tempoFinal = System.TimeSpan.FromSeconds(Mathf.RoundToInt(fase.tempoFinal)).ToString();
            novoItem.quantErro = fase.quantErro.ToString();
            novoItem.dataRealizada = fase.dataRealizada.ToString();
            itemList.Add(novoItem);
        }
        Debug.Log("Quantidade de itens: " + itemList.Count);
        contentPanel.DetachChildren();
        popularScroll();
    }

    //Método de popular o ScrollView
    public void popularScroll()
    {
        foreach (var item in itemList)
        {
            GameObject newItem = Instantiate(itemRelatorio) as GameObject;
            ItemRelatorio scriptRelatorio = newItem.GetComponent<ItemRelatorio>();
            scriptRelatorio.fase.text = item.fase;
            scriptRelatorio.tempoFinal.text = item.tempoFinal;
            scriptRelatorio.concentracao.text = item.concentracao;
            scriptRelatorio.quantErro.text = item.quantErro;
            scriptRelatorio.dataRealizada.text = item.dataRealizada;
            newItem.transform.SetParent(contentPanel);
            Vector3 vetorItem = newItem.GetComponent<RectTransform>().localPosition;
            newItem.GetComponent<RectTransform>().localPosition = new Vector3(contentPanel.localPosition.x, contentPanel.localPosition.y, 0);
            newItem.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            newItem.SetActive(true);
        }

    }

    /**
    * Método da mudança do InputField do campo do Nome do Jogador.
    **/
    public void onTextFieldNome(string name)
    {
        usuarioSelecionado.nome = inputFieldNomeJogador.text;
    }

    /**
    * Método da mudança do InputField do campo da Idade do Jogador.
    **/
    public void onTextFieldIdade(string name)
    {
        usuarioSelecionado.idade = inputFieldIdadeJogador.text;
    }

    /**
    * Método da mudança do InputField do campo de Observações(Patologia) do Jogador.
    **/
    public void onTextFieldPatologia(string name)
    {
        usuarioSelecionado.patologia = inputFieldPatologiaJogador.text;
    }

    /**
    * Método da mudança dos RadiosButtons do campo de Sexo do Jogador.
    **/
    public void onClickRadioButton(string name)
    {
        usuarioSelecionado.sexo = name;
    }


    /**
    * Método da mudança dos RadiosButtons do campo de Sexo do Jogador.
    **/
    public void onClickButtonAlterar()
    {
        Debug.Log("Dados do usuario selecionado: " + usuarioSelecionado.nome + " " + usuarioSelecionado.idade + " " + usuarioSelecionado.sexo + " " + usuarioSelecionado.patologia);
        usuarioDao.atualizarUsuario(usuarioSelecionado);
    }


    /**
    * Ação do botão de Voltar para o Menu Principal.
    **/
    public void onClickButtonVoltar()
    {
        Application.LoadLevel("MenuPrincipal");
    }


    /**
    *  Método que coloca os dados do jogador nos InputField para que possam ser alterados.
    **/
    private void setDadosJogador(string nome, string idade, string sexo, string patologia)
    {
        inputFieldNomeJogador.text = nome;
        inputFieldIdadeJogador.text = idade;
        if (sexo.Equals("Masculino") || sexo.Equals("masculino"))
        {
            toggleMasculino.isOn = true;

        }
        else if (sexo.Equals("Feminino") || sexo.Equals("feminino"))
        {
            toggleFeminino.isOn = true;
        }
        if(patologia != null)
        {
            inputFieldPatologiaJogador.text = patologia;
        }
    }


}
