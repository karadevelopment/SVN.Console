using SVN.Core.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SVN.Console2
{
    public class DataTable : IDisposable
    {
        private List<DataRow> Rows { get; } = new List<DataRow>();

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

        public void Write()
        {
            Console.WriteLine(this);
        }

        public override string ToString()
        {
            return $"{this.Rows.Select(x => x.ToString()).Join("\n")}";
        }
    }
}