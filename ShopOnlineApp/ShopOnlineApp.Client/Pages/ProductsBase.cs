﻿using Microsoft.AspNetCore.Components;
using ShopOnlineApp.Models.Dtos;
using ShopOnlineApp.Client.Services.Contracts;

namespace ShopOnlineApp.Client.Pages
{
    public class ProductsBase:ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }
        
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        [Inject]
        public IManageProductsLocalStorageService ManageProductsLocalStorageService { get; set; }

        [Inject]
        public IManageCartItemsLocalStorageService ManageCartItemsLocalStorageService { get; set; }

        public IEnumerable<ProductDto> Products { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string ErrorMessage { get; set; }

        //runs when the item is first called
        protected override async Task OnInitializedAsync()
        {
            try
            {
                await ClearLocalStorage();

                Products =  await ManageProductsLocalStorageService.GetCollection();

                var shoppingCartItems = await ManageCartItemsLocalStorageService.GetCollection();
               
                var totalQty = shoppingCartItems.Sum(i => i.Qty);

                ShoppingCartService.RaiseEventOnShoppingCartChanged(totalQty);

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                
            }
            
        }
        //is this really where this LINQ belongs??
        protected IOrderedEnumerable<IGrouping<int, ProductDto>> GetGroupedProductsByCategory()
        { 
            return from product in Products
                   group product by product.CategoryId into prodByCatGroup
                   orderby prodByCatGroup.Key
                   select prodByCatGroup;
        }
        protected string GetCategoryName(IGrouping<int, ProductDto> groupedProductDtos)
        {
            return groupedProductDtos.FirstOrDefault(pg => pg.CategoryId == groupedProductDtos.Key).CategoryName;
        }

        private async Task ClearLocalStorage()
        {
            await ManageProductsLocalStorageService.RemoveCollection();
            await ManageCartItemsLocalStorageService.RemoveCollection();
        }

    }
}
