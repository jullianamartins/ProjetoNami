using UnityEngine;
using System.Collections;

public class TratarColisaoObjeto2 : MonoBehaviour {

    public MovimentacaoLeap movimentacaoLeap;
    public bool estaNoLocal;
    private Vector3 posicaoInicial;

    private float deltaTime;
    private bool podeAnimar;


    // Use this for initialization
    void Start()
    {
        posicaoInicial = this.gameObject.transform.position;
    }


    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name.Equals("palm"))
        {
            if (!estaNoLocal)
            {
                Debug.Log("Valor do estaNoLocal: " + estaNoLocal);
                movimentacaoLeap.adicionarObjeto(this.gameObject);
            }
        }

    }


    public void voltarPosicaoInicial()
    {
        this.transform.position = posicaoInicial;
    }


}
