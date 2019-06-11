using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class ThanksCard
    {
        public int Id { get; set; }


        public int FromId { get; set; }
        public virtual User From { get; set; }
        public int ToId { get; set; }
        public virtual User To { get; set; }
        public DateTime PostDate { get; set; }

        [MaxLength(20)]
        public string Title { get; set; }

        [MaxLength(400)]
        public string Body { get; set; }

        public int ThanksCount { get; set; }
        public bool IsReaded { get; set; }
        public bool IsReadedAdmin { get; set; }
        public bool IsRepresentative { get; set; }

    }
}