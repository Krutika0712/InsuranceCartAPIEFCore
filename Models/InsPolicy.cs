using System;
using System.Collections.Generic;

namespace InsuranceCartAPIEFCore.Models
{
    public partial class InsPolicy
    {
        public InsPolicy()
        {
            TransactionDetails = new HashSet<TransactionDetail>();
        }

        public int PolicyId { get; set; }
        public string PolicyName { get; set; } = null!;
        public string Category { get; set; } = null!;
        public int? SumAssurance { get; set; }
        public int? Premium { get; set; }
        public int? Tenure { get; set; }
        public DateTime? CreatedOn { get; set; }

        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }
    }
}
