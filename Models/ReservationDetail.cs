//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace hotelManagementSystem20.models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReservationDetail
    {
        public int Reservation_ID { get; set; }
        public int Room_ID { get; set; }
        public Nullable<System.DateTime> DateDepart { get; set; }
        public Nullable<System.DateTime> DateArrivee { get; set; }
    
        public virtual Reservation Reservation { get; set; }
        public virtual room room { get; set; }
    }
}
