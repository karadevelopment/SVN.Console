using SVN.Core.Linq;
using System.Collections.Generic;
using System.Linq;

namespace SVN.Console2
{
    public class DataRow
    {
        private List<DataColumn> Columns { get; } = new List<DataColumn>();

        public int[] Lengths
        {
            get => this.Columns.Select(x => x.Length).ToArray();
        }

        public DataRow()
        {
        }

        public void AddColumn<T>(T obj)
        {
            var column = new DataColumn();
            column.SetContent(obj);
            this.Columns.Add(column);
        }

        private IEnumerable<string> DrawColumns(int[] lengths)
        {
            for (var i = 1; i <= lengths.Length; i++)
            {
                var length = lengths[i - 1];
                var column = this.Columns.ElementAtOrDefault(i - 1)?.ToString(length) ?? Enumerable.Range(1, length).Select(x => " ").Join("");
                yield return column;
            }
        }

        public string ToString(int[] lengths)
        {
            return $"{this.DrawColumns(lengths).ToList().Join(" # ")}";
        }
    }
}