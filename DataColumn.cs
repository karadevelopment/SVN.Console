namespace SVN.Console2
{
    public class DataColumn
    {
        private string Content { get; set; }

        public DataColumn()
        {
        }

        public void SetContent<T>(T obj)
        {
            this.Content = obj.ToString();
        }

        public override string ToString()
        {
            return $"{this.Content}";
        }
    }
}