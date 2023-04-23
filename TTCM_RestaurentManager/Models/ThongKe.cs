namespace TTCM_RestaurentManager.Models
{
    public class ThongKe
    {
        public int dishId;

        public ThongKe(int dishId)
        {
            this.dishId = dishId;
        }

        public String dishName;

        public int soLuong;

        public ThongKe(int dishId, String dishName)
        {
            this.dishId = dishId;
            this.dishName = dishName;
        }
    }
}
