using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

namespace Proprete.Data.Tables
{
    public class Warehouse
    {
        public int ItemID { get; set; }
        public DateTime DateTime { get; set; }
        public int Count { get; set; }

        [ForeignKey("ItemID")]
        [Required]
        public Item Item { get; set; }

        //public Warehouse(Item item) {
        //    DateTime =;
        //}
    }
}
