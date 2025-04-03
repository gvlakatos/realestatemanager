using RealEstate.Core.Enums;

namespace RealEstate.Core.Models.Reports;

public record PropertiesByStatus(EPropertyStatus Status, int Count);