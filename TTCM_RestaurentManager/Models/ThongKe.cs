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
        public String dishImg;

        public int soLuong;

        public ThongKe(int dishId, String dishName, string dishImg)
        {
            this.dishId = dishId;
            this.dishName = dishName;
            this.dishImg = dishImg;
        }
    }
}
