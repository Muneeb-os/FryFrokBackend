namespace FryFrokBackend.Model
{
    public class AddCart
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        public string UserName {  get; set; }
        public string Phone { get; set; }
        public string UserAddress {  get; set; }
        public string status {  get; set; }
        public string Bill {  get; set; }
        public DateTime Created { get; set; }
    }
}
