using System.Collections.Generic;

namespace PosLibrary.Model.Entities.Fiscal
{
    public class NcfType
    {
        public int NcfId { get; set; }
        public bool IsDefaultSale { get; set; }
        public bool IsDefaultCreditMemo { get; set; }
        public string Description { get; set; }
        public virtual ICollection<NcfHistory> NcfHistories { get; set; } = null;
        public virtual ICollection<NcfSequenceDetail> NcfSequenceDetails { get; set; } = null;
    }
}
