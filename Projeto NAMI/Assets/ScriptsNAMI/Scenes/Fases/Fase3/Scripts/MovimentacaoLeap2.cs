using UnityEngine;
using System.Collections;
using Leap;
using UnityEngine.UI;
using ProjetoNami.BD;
using ProjetoNami.Model;

public class MovimentacaoLeap2 : MonoBehaviour {
    private GameObject objetoSelecionado;

    public Text textoTempo;
    public string faseAtual;
    public string proximaFase;

    public MindWaveConector mindWaveConnector;
    public HandController controller;
    private Frame frame;

    private float tempoDecorrido;
    private int quantAcertos;
    private int quantErros;

    private FaseDao faseDao;
    private UsuarioDao usuarioDao;


    // Use this for initialization
    void Start()
    {
        faseDao = new FaseDao();
        usuarioDao = new UsuarioDao();
    }


    // Update is called once per frame
    void Update()
    {
        frame = controller.GetFrame();

        tempoDecorrido += Time.deltaTime;
        textoTempo.text = System.TimeSpan.FromSeconds(Mathf.RoundToInt(tempoDecorrido)).ToString();

        if (objetoSelecionado != null)
        {
            verificarPosicao(frame, objetoSelecionado);
        }

        //verificarConcluiuFase();
    }


    /**
    * Método que verifica se o usuário conseguiu colocar os 3 componentes no local certo 
    **/
    private void verificarConcluiuFase()
    {
        if (quantAcertos >= 3)
        {
            usuarioDao.atualizarFaseUsuario(PlayerPrefs.GetInt("id"), proximaFase);
            faseDao.salvarFase(faseAtual, tempoDecorrido, mindWaveConnector.getMediaAtencao(), quantErros, PlayerPrefs.GetInt("id"));
            Application.LoadLevel(proximaFase);
        }
    }


    /**
    * Método que trata as movimentações da mão e passa para o objeto selecionado
    **/
    private void verificarPosicao(Frame frame, GameObject objetoParaMovimentar)
    {

        foreach (var h in frame.Hands)
        {
            if (h.IsRight)
            {
                Leap.Vector position = h.PalmPosition;
                Vector3 unityPosition = position.ToUnityScaled(false);
                Vector3 worldPosition = controller.transform.TransformPoint(unityPosition);
                objetoParaMovimentar.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);
                Debug.Log("Nome do objeto movim: " + objetoParaMovimentar.name.ToString());

            }
        }

    }


    public void adicionarObjeto(GameObject objeto)
    {
        if (this.objetoSelecionado == null)
        {
            this.objetoSelecionado = objeto;
        }
    }

    public void retirarObjeto()
    {
        this.objetoSelecionado = null;
    }


    /**
    * Adiciona 1 acerto
    **/
    public void adicionarAcerto()
    {
        this.quantAcertos += 1;
    }


    /**
    * Adiciona 1 erro
    **/
    public void adicionarErro()
    {
        this.quantErros += 1;
    }


}
