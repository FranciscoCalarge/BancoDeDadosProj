using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine.UIElements;
using System.Data.Common;


public static class ItemModel
{
    public static Item InsertItem(DbConnection connection)
    {
        
        return new Item();
    }
}

[Table("SACOLA")]
public class Item
{
    [PrimaryKey][AutoIncrement]
    [Column("id")]
    public int Id { get; set; }

    [Unique, NotNull]
    [Column("nome_item")]
    public string NomeItem { get; set; }

    [Column("quantidade_item")]
    public int QuantidadeItem { get; set; }

    [Column("tipo_item")]
    public string TipoItem { get; set; }

    [Column("descricao_item")]
    public string DescricaoItem { get; set; }
    
}
