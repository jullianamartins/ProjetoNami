using UnityEngine;
using System.Collections;
using ProjetoNami.Model;
using MySql.Data.MySqlClient;

namespace ProjetoNami.BD
{

    public class FaseDao : MonoBehaviour {


        public void salvarFase(string nome, float tempoFinal, string concentracao, int quantErro, int idUser)
        {
            MySqlConnection conexao = BancoDeDados.conectarAoDatabase();

            MySqlCommand dbComando = conexao.CreateCommand();
            dbComando.CommandText = "INSERT INTO fase(nome, tempoFinal, concentracao, quant_erro, User_idUser) VALUES ('" + nome + "', '" + tempoFinal + "', '" + concentracao + "', '" + quantErro + "', '" + idUser + "')";

            MySqlDataReader leitor = dbComando.ExecuteReader();
            leitor.Read();
            leitor.Close();
            leitor = null;

            BancoDeDados.fecharConexao();

        }


        public void atualizarFase(Fase fase)
        {
            MySqlConnection conexao = BancoDeDados.conectarAoDatabase();

            MySqlCommand dbComando = conexao.CreateCommand();
            dbComando.CommandText = "update fase set nome = '" + fase.nome + "', tempoFinal = '" + fase.tempoFinal + "', concetracao = '" + fase.concentracao + "', quant_erro = '" + fase.quantErro + "', User_idUser = '" + fase.idUser + "' where idFase = " + fase.id;

            MySqlDataReader leitor = dbComando.ExecuteReader();
            leitor.Read();
            leitor.Close();
            leitor = null;

            BancoDeDados.fecharConexao();

        }


        public System.Collections.Generic.List<Fase> consultarFase()
        {
            MySqlConnection conexao = BancoDeDados.conectarAoDatabase();

            MySqlCommand dbComando = conexao.CreateCommand();
            dbComando.CommandText = "SELECT * FROM fase";

            MySqlDataReader leitor = dbComando.ExecuteReader();

            System.Collections.Generic.List<Fase> listaFases = new System.Collections.Generic.List<Fase>();

            while (leitor.Read())
            {
                Fase fase = new Fase();
                fase.id = int.Parse(leitor.GetString("idFase"));
                fase.nome = leitor.GetString("nome");
                fase.tempoFinal = leitor.GetFloat("tempoFinal");
                fase.quantErro = int.Parse(leitor.GetString("quant_erro"));
                fase.concentracao = leitor.GetString("concentracao");
                fase.idUser = int.Parse(leitor.GetString("User_idUSer"));
                //TODO Este campo de patologia está dando erro. Tem que verificar.
                //usuario.patologia = leitor.GetString("patologia");
                fase.dataRealizada = new System.DateTime(leitor.GetDateTime("dataRealizada").Ticks);

                listaFases.Add(fase);
            }

            BancoDeDados.fecharConexao();

            return listaFases;

        }

        //Retorna todas as fase do usuário
        public System.Collections.Generic.List<Fase> consultarFasesUsuario(int idUser)
        {
            MySqlConnection conexao = BancoDeDados.conectarAoDatabase();

            MySqlCommand dbComando = conexao.CreateCommand();
            dbComando.CommandText = "SELECT * FROM fase where User_idUser = "+ idUser;

            MySqlDataReader leitor = dbComando.ExecuteReader();

            System.Collections.Generic.List<Fase> listaFases = new System.Collections.Generic.List<Fase>();

            while (leitor.Read())
            {
                Fase fase = new Fase();
                fase.id = int.Parse(leitor.GetString("idFase"));
                fase.nome = leitor.GetString("nome");
                fase.tempoFinal = leitor.GetFloat("tempoFinal");
                fase.concentracao = leitor.GetString("concentracao");
                fase.quantErro = int.Parse(leitor.GetString("quant_erro"));
                fase.dataRealizada = new System.DateTime(leitor.GetDateTime("dataRealizada").Ticks);
                fase.idUser = int.Parse(leitor.GetString("User_idUSer"));
                //TODO Este campo de patologia está dando erro. Tem que verificar.
                //usuario.patologia = leitor.GetString("patologia");

                listaFases.Add(fase);
            }

            BancoDeDados.fecharConexao();

            return listaFases;

        }


    }

}
