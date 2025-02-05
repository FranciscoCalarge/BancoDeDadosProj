using System;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject chaveiroPrefab;
    IDbConnection conexaoDb;

    List<Item> items;

    [SerializeField] TMP_Text nomeTextPro;
    [SerializeField] TMP_Text tipoTextPro;
    [SerializeField] Slider qtdSlider;
    [SerializeField] TMP_Text descriptionTextPro;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        items = new List<Item>();
        conexaoDb = CriarEAbrirDB();

        ComandoCriarTabela();

        ComandoCarregarTabela("SACOLA");
       
        conexaoDb.Close();
    }

    public void ComandoCriarTabela()
    {
        IDbCommand comandoCriarTabela = conexaoDb.CreateCommand();
        comandoCriarTabela.CommandText =
        "CREATE TABLE IF NOT EXISTS SACOLA (id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                           "nome VARCHAR(255), " +
                                           "quantidade INTEGER, " +
                                           "tipo VARCHAR(255), " +
                                           "descricao VARCHAR(255)  )";


        comandoCriarTabela.ExecuteReader();
    }

    public void ComandoInserirTabela(string nome_item, int quantidade_item, string tipo_item, string descricao_item)
    {
        IDbCommand comandoInserir = conexaoDb.CreateCommand();
        comandoInserir.CommandText = 
        String.Format("INSERT INTO SACOLA (id,nome,quantidade,tipo,descricao) VALUES (null,'{0}','{1}' ,'{2}','{3}')",
                                             nome_item,quantidade_item,tipo_item,descricao_item);
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
                Item item = new Item((int)reader.GetInt32(0),reader.GetString(1),(int)reader.GetInt32(2),reader.GetString(3), reader.GetString(4));

                InstanciarDado(item);
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

    public void AdicionarDado()
    {
        conexaoDb = CriarEAbrirDB();

        ComandoInserirTabela(nomeTextPro.text,(int)Mathf.Floor(qtdSlider.value), tipoTextPro.text, descriptionTextPro.text);

        InstanciarDado(new Item(nomeTextPro.text, (int)Mathf.Floor(qtdSlider.value), tipoTextPro.text, descriptionTextPro.text));
        conexaoDb.Close();
    }

    public void RemoverDado(Item item)
    {
        conexaoDb = CriarEAbrirDB();

        ComandoRemoverTabela("SACOLA", item);

        conexaoDb.Close();
    }

    public void ComandoRemoverTabela(string tabela, Item item)
    {
        IDbCommand comandoRemover = conexaoDb.CreateCommand();
        comandoRemover.CommandText = String.Format("DELETE FROM {0} WHERE id={1}", tabela, item.Id );

        comandoRemover.ExecuteNonQuery();



    }

    public void InstanciarDado(Item item)
    {
        items.Add(item);
        GameObject aux = Instantiate(chaveiroPrefab);

        aux.name = item.NomeItem;
        aux.GetComponent<ChaveiroScript>().item = item;
        SpringJoint joint = aux.GetComponent<SpringJoint>();
        joint.connectedBody = GameObject.Find("Sphere").GetComponent<Rigidbody>();
        aux.GetComponent<MeshRenderer>().material = aux.GetComponent<MeshRenderer>().material;

        float random = UnityEngine.Random.value;

        joint.damper = random*100;
        aux.GetComponent<MeshRenderer>().material.SetFloat("_hue_offset", random);
    }

    public void RefreshTab()
    {
        nomeTextPro.text =  (Mathf.PI).ToString();
        Debug.Log(nomeTextPro.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EncerrarJogo()
    {
        Application.Quit();
    }
}
