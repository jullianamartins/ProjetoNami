using UnityEngine;
using System.Collections;

public class TratarColisaoSuporteObjeto2 : MonoBehaviour {

    private bool podeAnimar;
    private GameObject objeto;
    float time;

    public MovimentacaoLeap movimentacaoLeap2;
    public string nomeObjetoCerto;
    public string nomeObjetoErrado;

    private TratarColisaoObjeto2 scriptObjeto;

    private int quantObjeto = -1;

    private float[] scalaObjeto = { 2.315322f, 2.315323f, 5.282665f };
    private float[][] posicaoObjetoLaranja = { new float[]{ 33.29f, 11.07f, -8.57f }, new float[] { 34.2f, 11.07f, -8.57f }, new float[] { 33.44f, 10.96f, -9.54f }, new float[] { 34.2f, 11.05f, -9.57f } };
    private float[][] posicaoObjetoVerde = { new float[] { 20.14f, 11.01f, -8.74f }, new float[] { 21.02f, 11.01f, -8.74f }, new float[] { 21.09f, 11.03f, -9.52f }, new float[] { 20.22f, 10.99f, -9.51f } };

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
        if (this.name.Equals("CaixaLaranja"))
        {
            objeto.gameObject.transform.position = new Vector3(posicaoObjetoLaranja[quantObjeto][0], posicaoObjetoLaranja[quantObjeto][1], posicaoObjetoLaranja[quantObjeto][2]);
        }
        else
        {
            objeto.gameObject.transform.position = new Vector3(posicaoObjetoVerde[quantObjeto][0], posicaoObjetoVerde[quantObjeto][1], posicaoObjetoVerde[quantObjeto][2]);
        }
    }

}
