using System.ComponentModel.DataAnnotations;

namespace RealEstate.Core.Enums;

public enum EPropertyStatus
{
    // [Display(Name = "ProperyStatusAvailable", ResourceType = typeof(Resources.PropertyStatusResource))]
    Available = 1,
    // [Display(Name = "ProperyStatusRented", ResourceType = typeof(Resources.PropertyStatusResource))]
    Rented = 2,
    // [Display(Name = "ProperyStatusUnavailable", ResourceType = typeof(Resources.PropertyStatusResource))]
    Unavailable = 3
}