using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class ProductsRepository: IProductRepository
    {
        private List<Product> products = new List<Product>();
        private int index = 1;

        public ProductsRepository()
        {
            Add(new Product { Name = "Tomato soup", Category = "Groceries", Price = 1.39M });
            Add(new Product { Name = "Yo-yo", Category = "Toys", Price = 3.75M });
            Add(new Product { Name = "Hammer", Category = "Hardware", Price = 16.99M });
        }

        public Product Add(Product item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            item.ID = (index++).ToString();
            products.Add(item);
            return item;
        }

        public IEnumerable<Product> GetAll()
        {
            return products;
        }

        public Product Get(int id)
        {
            var product = products.Find(item => item.ID == id.ToString());
            if (product == null)
            {
                return null;
            }
            return product;
        }


        public void Remove(int id)
        {
            products.RemoveAll(item => item.ID == id.ToString());
        }

        bool Update(Product item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            var prodcut = products.Find(itm => itm.ID == item.ID);
            if (prodcut == null)
            {
                return false;
            }
            Remove(int.Parse(prodcut.ID));
            products.Add(item);
            return true;
        }

    }
}