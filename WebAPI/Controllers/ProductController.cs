using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ProductController : ApiController
    {
        static readonly IProductRepository repo = new ProductsRepository();

        public IEnumerable<Product> GetAllProducts()
        {
            return repo.GetAll();
        }

        public Product GetProduct(int id)
        {
            var product = repo.Get(id);

            if (product == null) 
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return product;
        }

        public IEnumerable<Product> GetProductByCategory(string category)
        {
            return repo.GetAll().Where(item => string.Equals(item.Category, category, StringComparison.OrdinalIgnoreCase));
        }

        public HttpResponseMessage PostProduct(Product item)
        {
            item = repo.Add(item);
            var reponse = Request.CreateResponse<Product>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.ID });
            reponse.Headers.Location = new Uri(uri);
            return reponse;
        }
    }
}
