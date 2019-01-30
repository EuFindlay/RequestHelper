using System;
using System.Collections.Generic;
using System.Text;

namespace RequestHelper.Test.ModelsForConvert
{
    public class NullableModel
    {
        public string NameSearchText { get; set; }
        public int? WeightFrom { get; set; }
        public int? WeightTo { get; set; }
        public int? HeightFrom { get; set; }
        public int? HeightTo { get; set; }

        public List<int> SelectedGradeIds { get; set; }
    }
}
