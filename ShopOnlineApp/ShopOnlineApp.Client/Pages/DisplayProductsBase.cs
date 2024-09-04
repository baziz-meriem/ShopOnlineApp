using Microsoft.AspNetCore.Components;
using ShopOnlineApp.Models.Dtos;

namespace ShopOnlineApp.Client.Pages
{
    public class DisplayProductsBase:ComponentBase
    {
        [Parameter]
        public IEnumerable<ProductDto> Products { get; set; }
    
    }
}
