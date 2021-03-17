namespace fncPar.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Data
    {
        [Key]
        public int Random { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime EventDate { get; set; }


    }
}
