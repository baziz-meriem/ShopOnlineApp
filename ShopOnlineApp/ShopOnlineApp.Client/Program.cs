using Blazored.LocalStorage;
using Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ShopOnlineApp.Client.Services.Contracts;
using ShopOnlineApp.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

///url of the local web component
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:60040/") });

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IManageProductsLocalStorageService, ManageProductsLocalStorageService>();
builder.Services.AddScoped<IManageCartItemsLocalStorageService, ManageCartItemsLocalStorageService>();

await builder.Build().RunAsync();
