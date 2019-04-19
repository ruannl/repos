using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruann.Linde.Database.Models {
    [ComplexType]
    public class Version {
        public DateTime? EffectiveFromDateTime { get; set; }
        public DateTime? EffectiveToDateTime { get; set; }
    }
}