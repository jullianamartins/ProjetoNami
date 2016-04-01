using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using ProjetoNami.Model;
using System;

namespace ProjetoNami.BD
{

    public class BancoDeDados : MonoBehaviour {

        private static MySqlConnection conexao;
        private static string enderecoDatabase = "localhost";
        private static string nomeDatabase = "databaseprojetonami";
        private static string userDatabase = "root";
        private static string passwordDatabase = "";

        private static string linhaDeConexao = "server=" + enderecoDatabase + 
            ";database=" + nomeDatabase + 
            ";User ID=" + userDatabase + 
            ";password=" + passwordDatabase + 
            ";Pooling=false;"+
            "Convert Zero Datetime=True";


        /**
        * Abre a conexão com o Banco de dados
        * Retorna um objeto MySqlConnection para ser possível realizar as transações com o banco de dados
        **/
        public static MySqlConnection conectarAoDatabase()
        {
            
            conexao = new MySqlConnection(linhaDeConexao);
            conexao.Open();
            return conexao;
                        
        }


        /**
        * Fecha a conexão com o Banco de dados
        **/
        public static void fecharConexao()
        {
            conexao.Close();
        }


        /**
        * Cria um objeto do tipo MySqlDateTime com a data e hora atual
        * Objeto utilizado para gravar no banco de dados a data e hora atual
        **/
        public static MySql.Data.Types.MySqlDateTime pegarHoraAtualMySql()
        {
            System.DateTime dataAtual = System.DateTime.Now;
            MySql.Data.Types.MySqlDateTime data = new MySql.Data.Types.MySqlDateTime(dataAtual);
            return data;
        }


    }


}