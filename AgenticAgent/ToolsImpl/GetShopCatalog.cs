namespace AgenticAgent.ToolsImpl;

public sealed class GetShopCatalog : BaseTool
{
    private readonly List<(string Product, double Price)> _catalog =
    [
        ("Artisan Coffee Beans (250g)", 14.99),
        ("Carolina Reaper Hot Sauce Set", 18.50),
        ("Dune Paperback Sci-Fi Book", 16.00),
        ("Italian Truffle Olive Oil", 22.00),
        ("Craft Beer Tasting Pack", 24.50),
        ("Keycap Puller & Switch Tester Kit", 19.99),

        ("Wooden Travel Chess Set", 39.00),
        ("Pour-Over Coffee Dripper & Carafe", 45.00),
        ("BBQ Grilling Tool Set (Stainless Steel)", 52.00),
        ("Vinyl Jazz Record - Miles Davis", 38.00),
        ("Camera Lens Cleaning & Filter Kit", 49.99),
        ("Mountain Bike Hydration Backpack", 65.00),

        ("Wireless Mechanical Keyboard (75%)", 95.00),
        ("Minimalist Leather Strap Watch", 120.00),
        ("Retro High-Top Sneakers", 110.00),
        ("Cast Iron Dutch Oven for Cooking", 89.00),
        ("Pro Gaming Wireless Mouse", 135.00)
    ];

    [ToolMetadata(
        "get_shop_catalog",
        "Returns all available products in the shop along with their prices in USD. Use this when the user wants to buy a gift, browse available items, or find gift options under a specific budget."
    )]
    public string Execute()
    {
        return string.Join(
            Environment.NewLine,
            _catalog.Select(item => $"- {item.Product}: ${item.Price:F2}")
        );
    }
}