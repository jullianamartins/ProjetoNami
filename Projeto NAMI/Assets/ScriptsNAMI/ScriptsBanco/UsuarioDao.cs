using UnityEngine;
using System.Collections;
using ProjetoNami.BD;
using ProjetoNami.Model;
using MySql.Data.MySqlClient;


namespace ProjetoNami.BD
{

    public class UsuarioDao : MonoBehaviour {


        public void salvarUsuario(string nome, string idade, string sexo, string programa, string patologia)
        {
            MySqlConnection conexao = BancoDeDados.conectarAoDatabase();

            MySqlCommand dbComando = conexao.CreateCommand();
            dbComando.CommandText = "INSERT INTO user(nome, idade, sexo, programa, patologia, proximaFase) VALUES ('" + nome + "', '" + idade + "', '" + sexo + "', '" + programa + "', '" + patologia + "', 'Fase1')";

            MySqlDataReader leitor = dbComando.ExecuteReader();
            leitor.Read();
            leitor.Close();
            leitor = null;

            BancoDeDados.fecharConexao();

        }


        public void atualizarUsuario(Usuario usuario)
        {
            MySqlConnection conexao = BancoDeDados.conectarAoDatabase();

            MySqlCommand dbComando = conexao.CreateCommand();
            dbComando.CommandText = "update user set nome = '" + usuario.nome + "', idade = '" + usuario.idade + "', sexo = '" + usuario.sexo + "', patologia = '" + usuario.patologia + "' where idUser = " + usuario.id;

            MySqlDataReader leitor = dbComando.ExecuteReader();
            leitor.Read();
            leitor.Close();
            leitor = null;

            BancoDeDados.fecharConexao();

        }


        public void atualizarFaseUsuario(int id, string proximaFase)
        {
            MySqlConnection conexao = BancoDeDados.conectarAoDatabase();

            MySqlCommand dbComando = conexao.CreateCommand();
            dbComando.CommandText = "update user set proximaFase = '" + proximaFase + "' where idUser = " + id;

            MySqlDataReader leitor = dbComando.ExecuteReader();
            leitor.Read();
            leitor.Close();
            leitor = null;

            BancoDeDados.fecharConexao();

        }


        public System.Collections.Generic.List<Usuario> consultarUsuario()
        {
            MySqlConnection conexao = BancoDeDados.conectarAoDatabase();

            MySqlCommand dbComando = conexao.CreateCommand();
            dbComando.CommandText = "SELECT * FROM user";

            MySqlDataReader leitor = dbComando.ExecuteReader();

            System.Collections.Generic.List<Usuario> listaUsuarios = new System.Collections.Generic.List<Usuario>();

            while (leitor.Read())
            {
                Usuario usuario = new Usuario();
                usuario.id = int.Parse(leitor.GetString("idUser"));
                usuario.nome = leitor.GetString("nome");
                usuario.idade = leitor.GetString("idade");
                usuario.sexo = leitor.GetString("sexo");
                usuario.proximaFase = leitor.GetString("proximaFase");
                //TODO Este campo de patologia está dando erro. Tem que verificar.
                //usuario.patologia = leitor.GetString("patologia");
                usuario.dataCadastro = new System.DateTime(leitor.GetDateTime("dataCadastro").Ticks);

                listaUsuarios.Add(usuario);
            }

            BancoDeDados.fecharConexao();

            return listaUsuarios;

        }


    }

}