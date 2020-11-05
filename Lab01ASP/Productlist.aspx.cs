using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Lab01ASP.Models;
using System.Web.UI.WebControls;
using System.Web.ModelBinding;

namespace Lab01ASP
{
    public partial class Productlist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public IQueryable<Product> GetProducts([QueryString("id")] int?
categoryId)
        {
            var _db = new Lab01ASP.Models.ProductContext();
            IQueryable<Product> query = _db.Products;
            if (categoryId.HasValue && categoryId > 0)
            {
                query = query.Where(p => p.CategoryID == categoryId);
            }
            return query;
        }
    }
}