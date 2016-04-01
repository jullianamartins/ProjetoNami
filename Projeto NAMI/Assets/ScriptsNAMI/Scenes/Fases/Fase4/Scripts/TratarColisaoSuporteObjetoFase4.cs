using UnityEngine;
using System.Collections;

public class TratarColisaoSuporteObjetoFase4 : MonoBehaviour {

    private bool podeAnimar;
    private GameObject objeto;
    float time;

    public MovimentacaoLeap movimentacaoLeap2;
    public string nomeObjetoCerto;
    public string nomeObjetoErrado;

    private TratarColisaoObjeto2 scriptObjeto;

    private int quantObjeto = -1;

    private float[] scalaObjeto = { 0.05391315f, 0.05391314f, 0.03199843f };
    private float[][] posicaoObjetoNumero = { new float[] { 33.32f, 10.95f, -8.94f }, new float[] { 34.31f, 10.95f, -8.94f }, new float[] { 34.31f, 10.95f, -9.84f }, new float[] { 33.37f, 10.95f, -9.84f } };
    private float[][] posicaoObjetoLetra = { new float[] { 20.08f, 11.08f, -9.03f }, new float[] { 21.15f, 11.08f, -9.03f }, new float[] { 20.21f, 11.08f, -9.83f }, new float[] { 21.1f, 11.08f, -9.83f } };

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        /*if (podeAnimar)
        {
            time += Time.deltaTime;
            if (time <= 5)
            {
                objeto.transform.position = new Vector3(objeto.transform.position.x - this.transform.position.x * 0.1f, objeto.transform.position.y, objeto.transform.position.z);
            }
            else
            {
        podeAnimar = false;
                time = 0;
            }
        }*/
    }


    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.name.Equals(nomeObjetoCerto))
        {
            Debug.Log("Chegou até aqui");
            Debug.Log("Nome objeto: " + (c.gameObject.name.ToString()));
            Debug.Log("Nome do suporte: " + this.name.ToString());

            objeto = c.gameObject;
            objeto.transform.position = new Vector3(objeto.transform.position.x, objeto.transform.position.y, objeto.transform.position.z);

            scriptObjeto = c.gameObject.GetComponent(typeof(TratarColisaoObjeto2)) as TratarColisaoObjeto2;
            scriptObjeto.estaNoLocal = true;

            BoxCollider rigidBodyObjeto = c.gameObject.GetComponent(typeof(BoxCollider)) as BoxCollider;
            rigidBodyObjeto.enabled = false;

            movimentacaoLeap2.retirarObjeto();

            quantObjeto += 1;
            colocarPosicaoObjeto(c.gameObject);

            movimentacaoLeap2.adicionarAcerto();

        }
        else if (c.gameObject.name.Equals(nomeObjetoErrado))
        {
            scriptObjeto = c.gameObject.GetComponent(typeof(TratarColisaoObjeto2)) as TratarColisaoObjeto2;
            movimentacaoLeap2.retirarObjeto();
            scriptObjeto.voltarPosicaoInicial();
            movimentacaoLeap2.adicionarErro();
        }
    }


    private void colocarPosicaoObjeto(GameObject objeto)
    {
        objeto.gameObject.transform.localScale = new Vector3(scalaObjeto[0], scalaObjeto[1], scalaObjeto[2]);
        if (this.name.Equals("Numeros"))
        {
            objeto.gameObject.transform.position = new Vector3(posicaoObjetoNumero[quantObjeto][0], posicaoObjetoNumero[quantObjeto][1], posicaoObjetoNumero[quantObjeto][2]);
        }
        else
        {
            objeto.gameObject.transform.position = new Vector3(posicaoObjetoLetra[quantObjeto][0], posicaoObjetoLetra[quantObjeto][1], posicaoObjetoLetra[quantObjeto][2]);
        }
    }

}
