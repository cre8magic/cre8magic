using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace ToSic.Module.Cre8magicTests.Models
{
    [Table("ToSicCre8magicTests")]
    public class Cre8magicTests : IAuditable
    {
        [Key]
        public int Cre8magicTestsId { get; set; }
        public int ModuleId { get; set; }
        public string Name { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
