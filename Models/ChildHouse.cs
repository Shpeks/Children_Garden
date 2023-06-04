using System;
using System.Collections.Generic;

namespace Diplom.Models
{
    public class ChildHouse
    {
        public int Id { get; set; }
        public string NameHouse { get; set; }
        public int ChildCount { get; set; }
        public int IdVaultNote { get; set; }
        public VaultNote VaultNote { get; set; }
    }
}