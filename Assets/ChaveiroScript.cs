using UnityEngine;

public class ChaveiroScript : MonoBehaviour
{
    public Item item;


     void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject.Find("GAMEMANAGER").GetComponent<GameManager>().RemoverDado(item);
            Destroy(gameObject);
        }
    }

}

public class Item
{
    public int Id { get; set; }
    public string NomeItem { get; set; }
    public int QuantidadeItem { get; set; }
    public string TipoItem { get; set; }
    public string DescricaoItem { get; set; }

    public Item(int id,string nome,int qtd,string tipo,string descricao)
    {
        Id = id;
        NomeItem = nome;
        QuantidadeItem = (int)qtd;
        TipoItem = tipo;
        DescricaoItem = descricao;
    }
    public Item(string nome,int qtd,string tipo,string descricao)
    {
        Id = (int)Random.value*10000;
        NomeItem = nome;
        QuantidadeItem = (int)qtd;
        TipoItem = tipo;
        DescricaoItem = descricao;
    }

}
