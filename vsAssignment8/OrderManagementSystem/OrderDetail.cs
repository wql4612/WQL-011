﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem
{
    public class OrderDetail
    {
        public string Id { get; set; }

        public int Index { get; set; }

        public string ProductId { get; set; }

        public Product Product { get; set; }

        public String ProductName { get => Product != null ? this.Product.Name : ""; }

        public double UnitPrice { get => Product != null ? this.Product.Price : 0.0; }

        public string OrderId { get; set; }

        public int Quantity { get; set; }

        public OrderDetail()
        {
            Id = Guid.NewGuid().ToString();
        }

        public OrderDetail(int index, Product goods, int quantity)
        {
            this.Index = index;
            this.Product = goods;
            this.Quantity = quantity;
        }

        public double TotalPrice
        {
            get => Product == null ? 0.0 : Product.Price * Quantity;
        }

        public override string ToString()
        {
            return $"[No.:{Index},goods:{ProductName},quantity:{Quantity},totalPrice:{TotalPrice}]";
        }

        public override bool Equals(object obj)
        {
            var item = obj as OrderDetail;
            return item != null &&
                   ProductName == item.ProductName;
        }

        public override int GetHashCode()
        {
            var hashCode = -2127770830;
            hashCode = hashCode * -1521134295 + Index.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ProductName);
            hashCode = hashCode * -1521134295 + Quantity.GetHashCode();
            return hashCode;
        }
    }
}
