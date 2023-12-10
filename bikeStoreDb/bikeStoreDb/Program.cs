using bikeStoreDb.Migrations;
using bikeStoreDb.Model;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace bikeStoreDb
{
    internal class Program
    {
        static void query1()
        {
            using (var db = new BikeStoresDb())
            {
                string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string fileName = "query1.txt";

                using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, fileName), false))
                {

                    var item = db.categories.Join(
                        db.products,
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
                        }).Where(c => c.category_name.Contains("Bikes"))
                        .Where(c => c.model_year == 2016);

                    var records = item.Join(
                        db.brands,
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
                    foreach (var record in records)
                    {
                        outputFile.WriteLine(record);
                        Console.WriteLine(record.ToString());
                    }



                }


            }
        }
        
        static void query2()
        {
            using (var db = new BikeStoresDb())
            {
                var item = db.categories.Join(
                    db.products,
                    c=>c.category_id,
                    p=>p.category_id,
                    (c, p) => new
                    {
                        category_id=c.category_id,
                        category_name=c.category_name,
                        product_id=p.product_id,
                        product_name=p.product_name,
                        brand_id=p.brand_id,
                        model_year=p.model_year,
                        list_price=p.list_price
                    }).Where(c=>string.Equals(c.category_name, "Road Bikes"))
                    .Where(c=>c.list_price>500);

                foreach(var record in item)
                {
                    Console.WriteLine(record);
                }
            }
        }


        //static void query3()
        //{

        //    using (var db = new BikeStoresDb())
        //    {
        //        var item = db.stores.Join(
        //            db.orders,
        //            s => s.store_id,
        //            o => o.store_id,
        //            (s, o) =>
        //            {

        //            });
        //    }

        //}

        // List all Bikes model which available in the ‘Santa Cruz Bikes’ store, show also number of items

        static void query4()
        {
            using(var db = new BikeStoresDb())
            {
                var store_id = db.stores.Where(s=>s.store_name.Equals("Santa Cruz Bikes"))
                    .Select(s=>s.store_id);

                int storeId=0;
                foreach ( var record in store_id)
                {
                    storeId = record;
                    
                }

                var product_id = db.stocks.Where(s => s.store_id == storeId)
                    .Select(s => s.product_id).Distinct();
                List<int> product_ints = new List<int>();
                foreach (var record in product_id)
                {
                    product_ints.Add(record);
                    //Console.WriteLine(record);
                }


                var category_id = db.products.Join(
                    db.categories,
                    p => p.category_id,
                    c => c.category_id,
                    (p, c) => new
                    {
                        product_id = p.product_id,
                        category_name = c.category_name,
                        category_id = c.category_id,

                    }).Where(c => c.category_name.Contains("Bike"))
                    .Where(c => product_ints.Contains(c.product_id));

                var type = category_id.Select(c => c.category_name).Distinct();
                Console.WriteLine($"so san pham la:{category_id.Count()}");
                Console.WriteLine("loai san pham la:");
                foreach(var item in type)
                {
                    Console.WriteLine(item);
                }
                



            }
        }

        //List all Bikes model which not available in the ‘Baldwin Bikes’ store
        static void query5()
        {
            using (var db = new BikeStoresDb())
            {
                var store_id = db.stores.Where(s => s.store_name.Equals("Baldwin Bikes"))
                    .Select(s => s.store_id);

                int storeId = 0;
                foreach (var record in store_id)
                {
                    storeId = record;

                }

                var product_id = db.stocks.Where(s => s.store_id == storeId)
                    .Select(s => s.product_id).Distinct();
                List<int> product_ints = new List<int>();
                foreach (var record in product_id)
                {
                    product_ints.Add(record);
                    //Console.WriteLine(record);
                }


                var category_id = db.products.Join(
                    db.categories,
                    p => p.category_id,
                    c => c.category_id,
                    (p, c) => new
                    {
                        product_id = p.product_id,
                        category_name = c.category_name,
                        category_id = c.category_id,

                    }).Where(c => c.category_name.Contains("Bike"))
                    .Where(c => product_ints.Contains(c.product_id));

                var type = category_id.Select(c => c.category_name).Distinct().ToList();
                //Console.WriteLine(type.GetType());
                var type2 = db.categories.Where(c => c.category_name.Contains("Bikes"))
                    .Where(c => !type.Contains(c.category_name));
                Console.WriteLine("loai san pham la:");
                foreach (var item in type2)
                {
                    Console.WriteLine(item);
                }




            }
        }


        //Get total revenue 
        static void query6()
        {
            using(var db = new BikeStoresDb())
            {
                var totalRevenue = db.order_items
                    .Sum(s => (double) s.list_price/ (double) (s.discount+1));
                 
                Console.WriteLine($"loai du lieu :{totalRevenue.GetType()},tong gia tri khoang:{totalRevenue}");
            }
        }

        //Get revenue of the ‘Rowlett Bikes’ store on 2016
        static void query7()
        {
            using(var db = new BikeStoresDb())
            {
                var store_id = db.stores.Where(s => s.store_name.Equals("Rowlett Bikes"))
                    .Select(s => s.store_id).ToList();


                DateTime dateTime = DateTime.Parse("2024-12-31");

                int selectId = store_id[0];
                
                var order_items = db.orders.Where(o => o.store_id == selectId)
                    .Select(o => new { 
                        order_id=o.order_id, 
                        shipped_date= o.shipped_date }).ToList();
                

                List<int> lOrder_item=new List<int>();
                foreach (var item in order_items)
                {

                    var shippedDate = item.shipped_date ?? dateTime;

                    if (shippedDate.Year == 2016)
                    {
                        lOrder_item.Add(item.order_id);
                    }

                }

                var totalRevenue = db.order_items.Where(t => lOrder_item.Contains(t.order_id))
                    .Sum(s => (double)s.list_price / (double)(s.discount + 1));

                Console.WriteLine($"Gia trị trong nam 2016 khoang:{totalRevenue}");







            }
        }

        //Get the revenue gain by staff: fabiola.jackson@bikes.shop
        static void query8(string email,out double revenue)
        {
            using(var db = new BikeStoresDb())
            {
                //string staff = "fabiola.jackson@bikes.shop";
                var id = db.staffs.Where(s => s.email.Equals(email))
                    .Select(s => s.staff_id);

                int staff_id=0;
                foreach (var item in id)
                {
                   
                    staff_id = item;
                }

                var total = db.orders.Where(o => o.staff_id == staff_id && o.order_status == 4)
                    .Join(
                    db.order_items,
                    o => o.order_id,
                    i => i.order_id,
                    (o, i) => new
                    {
                        order_id = o.order_id,
                        order_status = o.order_status,
                        list_price = i.list_price,
                        discount = i.discount,
                    });

                try
                {
                    var total2 = total.Sum(s => (double)s.list_price / (double)(s.discount + 1));
                    Console.WriteLine($"Doanh thu {email} khoang {total2}");

                    revenue= total2;
                }catch (Exception e) {
                    Console.WriteLine($"Nhan vien {email} ko ban duoc mat hang nao");

                    revenue = 0;
                }
            }
        }

        //Get the staff have a biggest revenue on 2016
        static void query9()
        {
            using (var db = new BikeStoresDb())
            {

                var order_orderitem = db.orders.Join(
                    db.order_items,
                    o => o.order_id,
                    oi => oi.order_id,
                    (o, oi) => new
                    {
                        order_id = oi.order_id,
                        staff_id = o.staff_id,
                        order_status = o.order_status,
                        list_price = oi.list_price,
                        discount = oi.discount,
                        shipped_date=o.shipped_date,
                    }).Where(s=>s.order_status==4)
                    .Where(s=>s.shipped_date.HasValue&& s.shipped_date.Value.Year==2016 )
                    .GroupBy(s => s.staff_id);


                List<int> ListStaff=new List<int>();
                double maxMoney = 0;
                foreach(var group in order_orderitem)
                {
                    double money;
                    Console.WriteLine($"ma nhan vien{group.Key}");
                    try
                    {
                         money= group.Sum(s => (double)s.list_price / (double)(s.discount + 1));
                    }catch(Exception e)
                    {
                        money= 0;
                    }
                    
                    Console.WriteLine(money);
                    //money = 50;
                    if (maxMoney < money)
                    {

                        maxMoney = money;
                        ListStaff.Clear();
                        ListStaff.Add(group.Key);
                    }
                    else if(maxMoney==money)
                    {
                        ListStaff.Add(group.Key);
                    }
                }

                if (ListStaff.Count() == 1)
                {
                    int id = ListStaff[0];
                    var email = db.staffs.Where(s => s.staff_id == id).ToList();
                    Console.WriteLine($"Nhan vien {email[0].email} ban duoc {maxMoney} va nhieu nhat");
                }
                else
                {
                    var email = db.staffs.Where(s => ListStaff.Contains(s.staff_id)).ToList();

                    foreach(staff staff in email)
                    {
                        Console.WriteLine($"Nhan vien {staff.email} ban duoc {maxMoney} va nhieu nhat");
                    }
                }


                
            }
        }

        //Get the most valuable customer - pay the most 

           static void query10()
        {
            using (var db = new BikeStoresDb())
            {

                var order_orderitem = db.orders.Join(
                    db.order_items,
                    o => o.order_id,
                    oi => oi.order_id,
                    (o, oi) => new
                    {
                        order_id = oi.order_id,
                        customer_id = o.customer_id,
                        order_status = o.order_status,
                        list_price = oi.list_price,
                        discount = oi.discount,
                       
                    }).Where(s => s.order_status == 4)

                    .GroupBy(s => s.customer_id);


                List<int> ListCustomer = new List<int>();
                double maxMoney = 0;
                foreach (var group in order_orderitem)
                {
                    double money;
                    //Console.WriteLine($"ma khach hang{group.Key}");
                    try
                    {
                        money = group.Sum(s => (double)s.list_price / (double)(s.discount + 1));
                    }
                    catch (Exception e)
                    {
                        money = 0;
                    }

                    //Console.WriteLine(money);
                    money = 50;
                    if (maxMoney < money)
                    {

                        maxMoney = money;
                        ListCustomer.Clear();
                        ListCustomer.Add(group.Key);
                    }
                    else if (maxMoney == money)
                    {
                        ListCustomer.Add(group.Key);
                    }
                }

                if (ListCustomer.Count() == 1)
                {
                    int id = ListCustomer[0];
                    var email = db.customers.Where(s => s.customer_id == id).ToList();
                    Console.WriteLine($"Nhan vien {email[0].email} ban duoc {maxMoney} va nhieu nhat");
                }
                else
                {
                    var email = db.customers.Where(s => ListCustomer.Contains(s.customer_id)).ToList();

                    foreach (customer staff in email)
                    {
                        Console.WriteLine($"Nhan vien {staff.email} ban duoc {maxMoney} va nhieu nhat");
                    }
                }



            }
        }

        //Get all orders related to product: 11,'Surly Straggler 650b’. List the order status  and the price to sell that product.
        static void query11()
        {
            using(var db = new BikeStoresDb())
            {
                try
                {
                    var Surly = db.products.Join(
                        db.order_items,
                        p => p.product_id,
                        oi => oi.product_id,
                        (p, oi) => new
                        {
                            product_id = p.product_id,
                            order_id = oi.order_id,
                            price=oi.list_price
                        }).Where(s=>s.product_id==11);

                    var Straggler = db.orders.Join(
                        Surly,
                        o => o.order_id,
                        s => s.order_id,
                        (o, s) => new
                        {
                            order_id = o.order_id,
                            product_id = s.product_id,
                            
                            customer_id = o.customer_id,
      
                            order_date=o.order_date,
      
                            required_date=o.required_date,
      
                            store_id=o.store_id,
      
                            staff_id=o.staff_id,
      
                            order_status=o.order_status,
      
                            shipped_date=o.shipped_date,

                        });

                    foreach(var order in Straggler)
                    {
                        Console.WriteLine($"{order.product_id}-{order.order_id}-{order.order_status}");
                    }

                    foreach(var item in Surly)
                    {
                        Console.WriteLine($"price {item.price}");
                    }


                }
                catch(Exception e)
                {
                    Console.WriteLine("have err");
                }
            }
        }

        static void Main(string[] args)
        {
            //query1();
            //query2();
            //query4();
            //query5();
            //query6();
            //query7();
            //double money;
            //query8("fabiola.jackson@bikes.shop",out money);
            //Console.WriteLine(money);
            //query9();
            //query10();
            //query11();

            using(var db = new BikeStoresDb())
            {
                int s = db.stores.Count();
                Console.WriteLine(s);
            }
            Console.ReadKey();
        }
    }
}
