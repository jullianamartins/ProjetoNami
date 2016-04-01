using UnityEngine;
using System.Collections;

public class TratarColisaoSuporteObjeto : MonoBehaviour {

    private bool podeAnimar;
    private GameObject objeto;
    float time;

    public MovimentacaoLeap movimentacaoLeap;
    public string nomeObjetoCerto;
    public string nomeObjetoErrado;

    private TratarColisaoObjeto scriptObjeto;

    private float quantObjeto = 2;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        
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
            Debug.Log("Nome objeto: "+ (c.gameObject.name.ToString()));
            Debug.Log("Nome do suporte: "+ this.name.ToString());

            objeto = c.gameObject;
            objeto.transform.position = new Vector3(objeto.transform.position.x, objeto.transform.position.y, objeto.transform.position.z);

            scriptObjeto = c.gameObject.GetComponent(typeof(TratarColisaoObjeto)) as TratarColisaoObjeto;
            scriptObjeto.estaNoLocal = true;

            BoxCollider rigidBodyObjeto = c.gameObject.GetComponent(typeof(BoxCollider)) as BoxCollider;
            rigidBodyObjeto.enabled = false;

            movimentacaoLeap.retirarObjeto();

            quantObjeto += -1f;
            colocarPosicaoObjeto(c.gameObject);

            movimentacaoLeap.adicionarAcerto();

        }
        else if (c.gameObject.name.Equals(nomeObjetoErrado))
        {
            scriptObjeto = c.gameObject.GetComponent(typeof(TratarColisaoObjeto)) as TratarColisaoObjeto;
            movimentacaoLeap.retirarObjeto();
            scriptObjeto.voltarPosicaoInicial();
            movimentacaoLeap.adicionarErro();
        }
    }


    private void colocarPosicaoObjeto(GameObject objeto)
    {
        objeto.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x - quantObjeto, this.gameObject.transform.position.y + quantObjeto, this.gameObject.transform.position.z);
    }

}
