using UnityEngine;
using System.Collections;

namespace ProjetoNami.Model
{

    public class Fase {

        public int id { get; set; }
        public string nome { get; set; }
        public float tempoFinal { get; set; }
        public int quantErro { get; set; }
        public string concentracao { get; set; }
        public System.DateTime dataRealizada { get; set; }
        public int idUser { get; set; }

    }

}
