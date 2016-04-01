using UnityEngine;
using System.Collections;

public class TratarColisaoSuporteObjeto4 : MonoBehaviour {

    private bool podeAnimar;
    private GameObject objeto;
    float time;

    public MovimentacaoLeap movimentacaoLeap;
    public GameObject polvoNaCaixa;
    public GameObject jacareNaCaixa;
    public GameObject tubaraoNaCaixa;
    public GameObject leaoNaCaixa;
    public string nomeObjetoCerto;
    public string nomeObjetoErrado;

    private TratarColisaoObjeto4 scriptObjeto;

    private int quantObjeto = -1;

    private float[] scalaObjeto = { 0.5944499f, 0.5944499f, 0.5944499f };
    private float[][] posicaoObjetoNumero = { new float[] { 33.126f, 12.092f, -9.079f }, new float[] { 34.39f, 11.91f, -9.03f }, new float[] { 34.84f, 10.712f, -9.78f }, new float[] { 33.84f, 10.712f, -9.78f } };
    //private float[][] posicaoObjetoLetra = { new float[] { 20.08f, 11.08f, -9.03f }, new float[] { 21.15f, 11.08f, -9.03f }, new float[] { 20.21f, 11.08f, -9.83f }, new float[] { 21.1f, 11.08f, -9.83f } };

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
        if (c.gameObject.name.Equals("Tubarao") || c.gameObject.name.Equals("Polvo") || c.gameObject.name.Equals("Leao") || c.gameObject.name.Equals("Jacare"))
        {
            Debug.Log("Chegou até aqui");
            Debug.Log("Nome objeto: " + (c.gameObject.name.ToString()));
            Debug.Log("Nome do suporte: " + this.name.ToString());

            objeto = c.gameObject;
            objeto.transform.position = new Vector3(objeto.transform.position.x, objeto.transform.position.y, objeto.transform.position.z);

            scriptObjeto = c.gameObject.GetComponent(typeof(TratarColisaoObjeto4)) as TratarColisaoObjeto4;
            scriptObjeto.estaNoLocal = true;

            BoxCollider rigidBodyObjeto = c.gameObject.GetComponent(typeof(BoxCollider)) as BoxCollider;
            rigidBodyObjeto.enabled = false;

            movimentacaoLeap.retirarObjeto();

            quantObjeto += 1;
            colocarPosicaoObjeto(c.gameObject);

            movimentacaoLeap.adicionarAcerto();

        }
        else
        {
            scriptObjeto = c.gameObject.GetComponent(typeof(TratarColisaoObjeto4)) as TratarColisaoObjeto4;
            movimentacaoLeap.retirarObjeto();
            scriptObjeto.voltarPosicaoInicial();
            movimentacaoLeap.adicionarErro();
        }
    }


    private void colocarPosicaoObjeto(GameObject objeto)
    {
        switch (objeto.name)
        {
            case "Leao":
                objeto.SetActive(false);
                leaoNaCaixa.SetActive(true);
                break;
            case "Jacare":
                objeto.SetActive(false);
                jacareNaCaixa.SetActive(true);
                break;
            case "Tubarao":
                objeto.SetActive(false);
                tubaraoNaCaixa.SetActive(true);
                break;
            case "Polvo":
                objeto.SetActive(false);
                polvoNaCaixa.SetActive(true);
                break;
        }
        //objeto.gameObject.transform.localScale = new Vector3(scalaObjeto[0], scalaObjeto[1], scalaObjeto[2]);
        //objeto.gameObject.transform.position = new Vector3(posicaoObjetoNumero[quantObjeto][0], posicaoObjetoNumero[quantObjeto][1], posicaoObjetoNumero[quantObjeto][2]);       
    }

}
