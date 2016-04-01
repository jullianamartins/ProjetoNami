using UnityEngine;
using System.Collections;
using Leap;
using UnityEngine.UI;
using ProjetoNami.BD;
using ProjetoNami.Model;

public class MovimentacaoLeap : MonoBehaviour
{

    private GameObject objetoSelecionado;

    public AudioSource somErro;
    public AudioSource somAcerto;

    public Text textoTempo;
    public string faseAtual;
    public string proximaFase;

    private string proximaFaseDefinida;
    private bool finalizadoFase;

    public MindWaveConector mindWaveConnector;
    public HandController controller;
    private Frame frame;

    private float tempoDecorrido;
    private int quantAcertos;
    private int quantErros;

    private FaseDao faseDao;
    private UsuarioDao usuarioDao;
    private bool isFinalizado;

    //Teste de animação do Panel final
    public PainelFinal panelFinal;


    // Use this for initialization
    void Start()
    {
        faseDao = new FaseDao();
        usuarioDao = new UsuarioDao();

        isFinalizado = false;
    }


    // Update is called once per frame
    void Update()
    {
        frame = controller.GetFrame();

        tempoDecorrido += Time.deltaTime;
        setTempoDecorridoFase();

        if (objetoSelecionado != null)
        {
            verificarPosicao(frame, objetoSelecionado);
        }

        verificarConcluiuFase();
    }


    /**
    * Método que verifica se o usuário conseguiu colocar os 3 componentes no local certo 
    **/
    private void verificarConcluiuFase()
    {
        if (faseAtual.Equals("Fase1") || faseAtual.Equals("Fase2"))
        {
            if (quantAcertos >= 3)
            {
                proximaFaseDefinida = proximaFase;
                salvarFase();
                quantAcertos = 0;
            }

        }
        else if (faseAtual.Equals("Fase3") || faseAtual.Equals("Fase4"))
        {
            if (quantAcertos >= 8)
            {
                proximaFaseDefinida = proximaFase;
                salvarFase();
                quantAcertos = 0;
            }

        }
        else if (faseAtual.Equals("Fase5"))
        {
            if (quantAcertos >= 4)
            {
                proximaFaseDefinida = "Fase1";
                salvarFase();
                quantAcertos = 0;
            }
        }
    }


    /**
    * Método que salva a fase e exibe o painel final
    **/
    private void salvarFase()
    {
        usuarioDao.atualizarFaseUsuario(PlayerPrefs.GetInt("id"), proximaFaseDefinida);
        faseDao.salvarFase(faseAtual, tempoDecorrido, mindWaveConnector.getMediaAtencao(), quantErros, PlayerPrefs.GetInt("id"));

        isFinalizado = true;
        panelFinal.setInformacoes(textoTempo.text, quantErros.ToString(), mindWaveConnector.getMediaAtencao(), proximaFaseDefinida);
        panelFinal.ativarPainel();
    }


    /**
    * Método que trata as movimentações da mão e passa para o objeto selecionado
    **/
    private void verificarPosicao(Frame frame, GameObject objetoParaMovimentar)
    {

        foreach (var h in frame.Hands)
        {
            if (h.IsRight || h.IsLeft)
            {
                Leap.Vector position = h.PalmPosition;
                Vector3 unityPosition = position.ToUnityScaled(false);
                Vector3 worldPosition = controller.transform.TransformPoint(unityPosition);
                objetoParaMovimentar.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);
                Debug.Log("Nome do objeto movim: " + objetoParaMovimentar.name.ToString());

            }
        }

    }


    //Método que fica pegando o tempo da fase
    public void setTempoDecorridoFase()
    {
        if (!isFinalizado)
        {
            textoTempo.text = System.TimeSpan.FromSeconds(Mathf.RoundToInt(tempoDecorrido)).ToString();
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
        somAcerto.PlayOneShot(somAcerto.clip, 1.0f);
        this.quantAcertos += 1;
        GameObject.Find("ursinho").GetComponent<Ursinho_Comtrole_Anim>().AnimPonto();
    }


    /**
    * Adiciona 1 erro
    **/
    public void adicionarErro()
    {
        somErro.PlayOneShot(somErro.clip, 1.0f);
        this.quantErros += 1;
        GameObject.Find("ursinho").GetComponent<Ursinho_Comtrole_Anim>().AnimPerda();
    }


}
