//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookVarApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class Orders
    {
        public int OrderID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> ShopID { get; set; }
        public int OrderProductCount { get; set; }
        public int OrderTotalPrice { get; set; }
        public string OrderStatus { get; set; }
        public System.DateTime OrderData { get; set; }
        public string OrderPaymentMethod { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Product Product { get; set; }
        public virtual Shop Shop { get; set; }
    }
}
