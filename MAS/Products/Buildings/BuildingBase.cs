using System;

namespace MAS.Products.Buildings
{
    public abstract class BuildingBase : ProductBase, IBuilding
    {

        public int RoomNumbers { get; set; }
        public bool HasAirConditioner { get; set; }
        public bool HasRoomProtection { get; set; }
        public bool IsNextToMainRoad { get; set; }
        public bool HasAccessForDisabled { get; set; }
        public int NumberRoomBathroom { get; set; }
        public bool HasDiningRoom { get; set; }

        public BuildingBase(string name, string description, int roomNumber, bool hasAirConditioner,
            bool hasRoomProtection, bool isNextToMainRoad, bool hasAccessForDisabled,
            int numberRoomBathroom, bool hasDiningRoom) : base(name, description)
        {
            RoomNumbers = roomNumber;
            HasRoomProtection = hasRoomProtection;
            HasAirConditioner = hasAirConditioner;
            IsNextToMainRoad = isNextToMainRoad;
            HasAccessForDisabled = hasAccessForDisabled;
            NumberRoomBathroom = numberRoomBathroom;
            HasDiningRoom = hasDiningRoom;
        }


        public override string ToString()
        {
            string msg = $"{base.ToString()} {Environment.NewLine}";
            msg += $"Room number : {NumberRoomBathroom} {Environment.NewLine}";
            msg += $"Has airConditioner : {HasAirConditioner} {Environment.NewLine}";
            msg += $"Has roomProtection : {HasRoomProtection} {Environment.NewLine}";
            msg += $"Has Is next to main road : {IsNextToMainRoad} {Environment.NewLine}";
            msg += $"Has Access For Disabled : {HasAccessForDisabled} {Environment.NewLine}";
            msg += $"Has Access For Disabled : {NumberRoomBathroom} {Environment.NewLine}";
            msg += $"Has Dining Room : {HasDiningRoom} {Environment.NewLine}";
            return msg;
        }
    }
}
