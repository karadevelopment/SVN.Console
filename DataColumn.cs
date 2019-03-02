using SVN.Core.Linq;
using System.Linq;

namespace SVN.Console2
{
    public class DataColumn
    {
        private string Content { get; set; }

        public int Length
        {
            get => this.Content.Length;
        }

        public DataColumn()
        {
        }

        public void SetContent<T>(T obj)
        {
            this.Content = obj.ToString();
        }

        private string GetWhiteSpaces(int length)
        {
            return Enumerable.Range(1, length - this.Content.Length).Select(x => " ").Join("");
        }

        public string ToString(int length)
        {
            return $"{this.GetWhiteSpaces(length)}{this.Content}";
        }
    }
}