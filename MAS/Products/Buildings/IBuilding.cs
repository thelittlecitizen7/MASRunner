namespace MAS.Products.Buildings
{
    public interface IBuilding : IProduct
    {
        int RoomNumbers { get; set; }
        bool HasAirConditioner { get; set; }

        bool HasRoomProtection { get; set; }

        public bool IsNextToMainRoad { get; set; }

        bool HasAccessForDisabled { get; set; }

        int NumberRoomBathroom { get; set; }

        bool HasDiningRoom { get; set; }
    }
}
