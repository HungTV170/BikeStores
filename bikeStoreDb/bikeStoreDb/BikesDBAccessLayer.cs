using bikeStoreDb.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikeStoreDb
{
    public class BikesDBAccessLayer
    {
        private readonly IDatabase db;

        public BikesDBAccessLayer(IDatabase db)
        {
            this.db = db;
        }
        //List all Bikes which have model_year = 2016.
        //Columns: Product Name, Brand, Category, Price
        public List<DataObjTransferProduct> getAllBikeyear_2016()
        {
            

            var item = db.GetEntities<category>().Join(
                db.GetEntities<product>(),
                c => c.category_id,
                p => p.category_id,
                (c, p) => new
                {
                category_id = c.category_id,
                category_name = c.category_name,
                product_id = p.product_id,
                product_name = p.product_name,
                list_price = p.list_price,
                model_year = p.model_year,
                brand_id = p.brand_id,
                })
                .Where(c => c.category_name.Contains("Bikes"))
                .Where(c => c.model_year == 2016);



            var records = item.Join(
                db.GetEntities<brand>(),
                i => i.brand_id,
                b => b.brand_id,
                (i, b) => new
                {
                Product_name = i.product_name,
                Brand = b.brand_name,
                Category = i.category_name,
                Price = i.list_price
                }
                ).OrderBy(c => c.Category);

            List<DataObjTransferProduct> products = new List<DataObjTransferProduct>();
            foreach (var record in records)
                {
                products.Add(new DataObjTransferProduct()
                {
                    Product_name = record.Product_name,
                    Brand = record.Brand,
                    Category = record.Category,
                    Price = record.Price,
                }) ;
                }


            return products;

            }
        }

    public interface IDatabase
    {
        DbSet<T> GetEntities<T>() where T : class;
    }


    public class DataObjTransferProduct
    {
        public string Product_name {get;set;}
            public string Brand { get; set; }
            public string Category { get; set; }
            public decimal Price { get; set; }
    }

}
