using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ShopDunk.Models.ViewModel
{
    public class CartItem
    {
        public Product _product { get; set; }
        public ProDetail _prodetail { get; set; }
        public int _quantity { get; set; }
        public Color _color { get; set; }
        public Order _order { get; set; }

    }
    public class Cart
    {
        public OrderDetail _orderdetail { get; set; }   
        public List<CartItem> items = new List<CartItem>();
        public Customer _cus { get; set; }
        public Color _color { get; set; }
        public Product _product { get; set; }
        public string ProName { get; set; }
        public string CusName { get; set; }
        public string CusEmail { get; set; }
        public int OrderID { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public string CusPhone { get; set; }
        public string AddressDeliverry { get; set; }

        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        public void Add_Product_Cart(Product _pro, int _quan = 1)
        {
            var item = Items.FirstOrDefault(s => s._product.ProID == _pro.ProID);
            if (item == null)
                items.Add(new CartItem
                {
                    _product = _pro,
                    _quantity = _quan,
                });
            else
                item._quantity += _quan;
        }
        public int Total_quantity()
        {
            return items.Sum(s => s._quantity);
        }
        public decimal Total_money()
        {
            var total = items.Sum(s => s._quantity * s._product.PriceReduce);
            return (decimal)total;
        }
        public void Update_quantity(int id, int _new_quan)
        {
            var item = items.Find(s => s._product.ProID == id);
            if (item != null)
            {
                if (items.Find(s => s._product.ProID > _new_quan) != null)
                    item._quantity = _new_quan;
                else item._quantity = 1;
            }
        }
        public void Remove_CartItem(int id)
        {
            items.RemoveAll(s => s._product.ProID == id);
        }
        public void ClearCart()
        {
            items.Clear();
        }
        //lấy từ bảng product
        public int ProID { get; set; }
        public string ProImage { get; set; }
        public double Price { get; set; }

        public int IDVoucher { get; set; }
        public string NameVoucher { get; set; }
        public Nullable<double> DiscountLevel { get; set; }
        public ICollection<Cart> RelatedProduct { get; set; }

    }
}