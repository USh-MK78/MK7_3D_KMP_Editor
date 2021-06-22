using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MK7_KMP_Editor_For_PG_
{
    class DataTableHelper
    {
        public DataTable MargeDataTable(List<DataTable> dt_List)
        {
            DataTable FirstDataTable = new DataTable();
            if (dt_List != null)
            {
                FirstDataTable = null;
            }
            if(dt_List.Count == 0)
            {
                FirstDataTable = dt_List[0];
            }
            if(dt_List.Count > 0)
            {
                FirstDataTable = dt_List[0];

                for (int Count = 1; Count < dt_List.Count; Count++)
                {
                    FirstDataTable.Merge(dt_List[Count]);
                }
            }

            return FirstDataTable;
        }
    }
}
