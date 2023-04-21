namespace TTCM_RestaurentManager.Models
{
    [Serializable]
    public class CartItem
    {
        public MonAn MonAn { get; set; }
        public int Quantity { get; set; }
    }
    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        { 
            get { return items; }
        }
        public void Add(MonAn cart,int quantity = 1)
        {
            var item = items.FirstOrDefault(x => x.MonAn.MaMa == cart.MaMa);
            if (item != null)
            {
                items.Add(new CartItem
                {
                    MonAn = cart,
                    Quantity = quantity
                });
            }
            else
            {
                item.Quantity += quantity;
            }
        }
    }
}
