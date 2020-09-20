using UnityEngine;

[CreateAssetMenu(fileName = "Book", menuName = "Item/Book")]
public class BookItem : Item
{
    public override ItemTag itemTag { get; set; } = ItemTag.Book;
    public TextUI content = null;

    public override void Use()
    {
        BookManager.Instance.Active(content);
    }
}
