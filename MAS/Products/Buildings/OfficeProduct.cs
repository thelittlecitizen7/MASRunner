using MAS.Products.Buildings;
using System;

namespace MAS.Products
{
    public class OfficeProduct : BuildingBase
    {
        public OfficeProduct()
            : base("OfficeProduct", "Place to work inside", 3, false, true, false, true, 2, true)
        {
        }

        public override string ToString()
        {
            string msg = $"{base.ToString()} {Environment.NewLine}";
            msg += $"room number : {RoomNumbers} {Environment.NewLine}";
            msg += $"has airconditioner : {HasAirConditioner}";
            return msg;
        }

    }
}
