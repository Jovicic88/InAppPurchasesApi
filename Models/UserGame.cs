//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InAppPurchasesApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserGame
    {
        public int UserGameId { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public string Achivements { get; set; }
        public Nullable<int> Coins { get; set; }
        public Nullable<int> Diamonds { get; set; }
        public Nullable<bool> Premium { get; set; }
        public bool HasGame { get; set; }
    
        public virtual Game Game { get; set; }
        public virtual User User { get; set; }
    }
}
