using SVN.Core.Linq;
using SVN.Math2;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SVN.Console2
{
    public class DataTable : IDisposable
    {
        private List<DataRow> Rows { get; } = new List<DataRow>();
        private int[] Lengths { get; set; }

        public DataTable()
        {
        }

        public void Dispose()
        {
            this.Write();
        }

        public void AddRow<T>(params T[] objects)
        {
            var row = new DataRow();
            foreach (var obj in objects)
            {
                row.AddColumn(obj);
            }
            this.Rows.Add(row);
        }

        private void CalculateLengths()
        {
            var result = new List<int>();

            var rowLengths = this.Rows.Select(x => x.Lengths).ToArray();
            var maxCols = rowLengths.Max(x => x.Length);

            for (var i = 1; i <= maxCols; i++)
            {
                var length = rowLengths.Max(x => x.ElementAtOrDefault(i - 1));
                result.Add(length);
            }

            this.Lengths = result.ToArray();
        }

        public void Write()
        {
            this.CalculateLengths();
            Console.WriteLine(this);
        }

        private string DrawFullRow()
        {
            return $"##{this.Lengths.Select(x => Enumerable.Range(1, x).Select(y => "#").Join("")).Join("###")}##";
        }

        private string DrawEmptyRow()
        {
            return $"# {this.Lengths.Select(x => Enumerable.Range(1, x).Select(y => " ").Join("")).Join(" # ")} #";
        }

        private string DrawRow(DataRow row)
        {
            return $"# {row.ToString(this.Lengths)} #";
        }

        private IEnumerable<string> DrawRows()
        {
            foreach (var row in this.Rows)
            {
                yield return this.DrawFullRow();
                yield return this.DrawEmptyRow();
                yield return this.DrawRow(row);
                yield return this.DrawEmptyRow();
            }
            yield return this.DrawFullRow();
        }

        public override string ToString()
        {
            var rows = this.DrawRows().ToList();
            var maxRowLength = rows.Max(x => x.Length);
            var width = (maxRowLength * 1.01).CeilingToInt();

            width = Math.Max(width, Console.WindowWidth);
            width = Math.Min(width, 240);

            Console.WindowWidth = width;
            return $"{rows.Join("\n")}";
        }
    }
}