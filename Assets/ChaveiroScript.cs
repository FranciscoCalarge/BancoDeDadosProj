using UnityEngine;

public class ChaveiroScript : MonoBehaviour
{
    public Item item;


     void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(gameObject.name);
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

    public Item(string nome,int qtd,string tipo,string descricao)
    {
        NomeItem = nome;
        QuantidadeItem = (int)qtd;
        TipoItem = tipo;
        DescricaoItem = descricao;
    }

}
