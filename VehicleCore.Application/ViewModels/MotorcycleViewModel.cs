using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleCore.DomainModel.Enums;

namespace VehicleCore.Application.ViewModels;

public class MotorcycleViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string TrackingCode { get; set; } = string.Empty;
    public string Fuel { get; set; } = string.Empty;
}
