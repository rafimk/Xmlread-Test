
namespace Xmlread_Test.Models;

public class AsnLine
{
    public int lineNumber { get; set; }
    public string tuid { get; set; }
    public int quantityReceived { get; set; }
    public int quantityExprected { get; set; }
    public List<AttributesValues> attributesValues { get; set; }
}