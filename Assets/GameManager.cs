using System;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;

public class GameManager : MonoBehaviour
{
    public GameObject chaveiroPrefab;
    IDbConnection conexaoDb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        conexaoDb = CriarEAbrirDB();
        IDbCommand comandoCriarTabela = conexaoDb.CreateCommand();
        comandoCriarTabela.CommandText = 
        "CREATE TABLE IF NOT EXISTS SACOLA (id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                           "nome VARCHAR(255), " +
                                           "quantidade INTEGER, " +
                                           "tipo VARCHAR(255), " +
                                           "descricao VARCHAR(255)  )";
        ComandoCarregarTabela("SACOLA");


        comandoCriarTabela.ExecuteReader();
        
        conexaoDb.Close();
    }

    public void ComandoInserirTabela(string nome_item, int quantidade_item, string tipo_item, string descricao_item)
    {
        IDbCommand comandoInserir = conexaoDb.CreateCommand();
        comandoInserir.CommandText = String.Format("INSERT INTO SACOLA (id,nome,quantidade,tipo,descricao) VALUES (null,'{0}','{1}' ,'{2}','{3}')", nome_item,quantidade_item,tipo_item,descricao_item);
        comandoInserir.ExecuteNonQuery();

    }


    public void ComandoCarregarTabela(string tabela)
    {
        IDbCommand comandoSelecionar = conexaoDb.CreateCommand();
        comandoSelecionar.CommandText = String.Format("SELECT * FROM {0}",tabela);

        using (IDataReader reader = comandoSelecionar.ExecuteReader())
        {
            while (reader.Read())
            {
                GameObject aux =  Instantiate(chaveiroPrefab);
                aux.name =reader.GetString(1);
                SpringJoint joint = aux.GetComponent<SpringJoint>();
                joint.connectedBody = GameObject.Find("Sphere").GetComponent<Rigidbody>();
                joint.tolerance = .5f;
            }
        }


    }

    private IDbConnection CriarEAbrirDB()
    {
        string dbUri = "URI=file:Sacolinha.s3db";
        IDbConnection conexaoDb = new SqliteConnection(dbUri);
        conexaoDb.Open();

        return conexaoDb;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
