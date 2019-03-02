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
            Console.Clear();
            Console.WriteLine(this);
        }

        private string DrawColumnWhiteSpaces()
        {
            return Enumerable.Range(1, 3).Select(x => " ").Join("");
        }

        private string DrawRowEmpty()
        {
            return $"{this.DrawColumnWhiteSpaces()}  {this.Lengths.Select(x => Enumerable.Range(1, x).Select(y => " ").Join("")).Join("   ")}  {this.DrawColumnWhiteSpaces()}";
        }

        private string DrawRowWall()
        {
            return $"{this.DrawColumnWhiteSpaces()}##{this.Lengths.Select(x => Enumerable.Range(1, x).Select(y => "#").Join("")).Join("###")}##{this.DrawColumnWhiteSpaces()}";
        }

        private string DrawRowInputless()
        {
            return $"{this.DrawColumnWhiteSpaces()}# {this.Lengths.Select(x => Enumerable.Range(1, x).Select(y => " ").Join("")).Join(" # ")} #{this.DrawColumnWhiteSpaces()}";
        }

        private string DrawRow(DataRow row)
        {
            return $"{this.DrawColumnWhiteSpaces()}# {row.ToString(this.Lengths)} #{this.DrawColumnWhiteSpaces()}";
        }

        private IEnumerable<string> DrawRows()
        {
            yield return this.DrawRowEmpty();
            foreach (var row in this.Rows)
            {
                yield return this.DrawRowWall();
                yield return this.DrawRowInputless();
                yield return this.DrawRow(row);
                yield return this.DrawRowInputless();
            }
            yield return this.DrawRowWall();
            yield return this.DrawRowEmpty();
        }

        public override string ToString()
        {
            var rows = this.DrawRows().ToList();
            var maxRowLength = rows.Max(x => x.Length);
            var scaling = 1.005;

            var width = (maxRowLength * scaling).CeilingToInt();
            var height = (rows.Count * scaling).CeilingToInt();

            //width = Math.Max(width, Console.WindowWidth);
            width = Math.Min(width, 240);
            //height = Math.Max(height, Console.WindowHeight);
            height = Math.Min(height, 60);

            Console.WindowWidth = width;
            Console.WindowHeight = height;
            return $"{rows.Join("\n")}";
        }
    }
}