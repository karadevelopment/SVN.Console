using SVN.Core.Linq;
using System.Collections.Generic;
using System.Linq;

namespace SVN.Console2
{
    public class DataRow
    {
        private List<DataColumn> Columns { get; } = new List<DataColumn>();

        public DataRow()
        {
        }

        public void AddColumn<T>(T obj)
        {
            var column = new DataColumn();
            column.SetContent(obj);
            this.Columns.Add(column);
        }

        public override string ToString()
        {
            return $"{this.Columns.Select(x => x.ToString()).Join(" ")}";
        }
    }
}