using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ProjetoNami.BD;
using ProjetoNami.Model;
using System;

public class Utils : MonoBehaviour {


    /**
    * Método que busca todos os usuários que estão cadastrados no banco de dados.
    * Retorna uma lista de objetos que podem ser inseridos no Dropdown de selecionar os jogadores.
    **/
    public static System.Collections.Generic.List<Dropdown.OptionData> gerarListaUsuarios(UsuarioDao usuarioDao)
    {

        System.Collections.Generic.List<Usuario> listaUsuarios = usuarioDao.consultarUsuario();

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
    * Salvar usuario no Player Prefs.
    **/
    public static void salvarUsuarioPlayerPrefs(int id, string nome, string idade, string sexo, string programa, string patologia, string proximaFase)
    {
        PlayerPrefs.SetInt("id", id);
        PlayerPrefs.SetString("nome", nome);
        PlayerPrefs.SetString("idade", idade);
        PlayerPrefs.SetString("sexo", sexo);
        PlayerPrefs.SetString("programa", programa);
        PlayerPrefs.SetString("patologia", patologia);
        PlayerPrefs.SetString("proximaFase", proximaFase);
        PlayerPrefs.Save();
    }


    /**
    * Salvar uma String para ser parseada pelo Boolean.
    **/
    public static void salvarUsuarioSelecionadoPlayerPrefs(string valorBooleano)
    {
        PlayerPrefs.SetString("usuarioSelecionado", valorBooleano);
        PlayerPrefs.Save();
    }


    /**
    * Verifica se o atributo usuarioSelecionado no PlayerPrefs está True ou False e retorna.
    * Coloque True ou False
    **/
    public static bool verificarSeExisteUsuarioSelecionadoPlayerPrefs()
    {
        try
        {
            return bool.Parse(PlayerPrefs.GetString("usuarioSelecionado"));
        } catch(Exception e)
        {
            return false;
        }
    }


}
