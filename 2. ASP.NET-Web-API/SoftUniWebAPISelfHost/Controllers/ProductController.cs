using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SoftUniWebAPISelfHost
{

    [RoutePrefix("api/products")]
    public class ProductController : ApiController
    {
        [Route("list")]
        [HttpGet]
        public string ListProducts()
        {
            return "products";
        }

        [Route("{id:int}")]
        public string GetProduct(int id)
        {
            return "product " + id;
        }

        [Route("{name}")]
        [Authorize]
        public IHttpActionResult GetProductByName(string name)
        {
            var product = new Product(name);

            return Ok(product);
        }
    }
}