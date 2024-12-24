namespace проект0._1
{
    public class product
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }
        public int CostPerOne { get; set; }
        public int Cost
        {
            get
            {
                return Quantity * CostPerOne;
            }
            set
            {
                return;
            }
        }
        public int Mass { get; set; }
    }
}