using System;

namespace PosLibrary.Model.Entities.Fiscal
{
    public class NcfSequenceDetail : CommonEntity
    {
        public int NcfId { get; set; }
        public string Serie { get; set; }
        public int SeqStart { get; set; }
        public int SeqEnd { get; set; }
        public int SeqNext { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int SeqStatus { get; set; }
        public string DGIIDescription { get; set; }

        public virtual NcfType NcfType { get; set; }
    }
}
