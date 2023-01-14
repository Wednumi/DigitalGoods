using DigitalGoods.Core.Enums;

namespace DigitalGoods.Core.Entities
{
    public class PaymentRecord : BaseEntity
    {
        public User User { get; set; }

        public string UserId { get; set; }

        public DateTime Date { get; set; }

        public float Amount { get; set; }

        public PayMethod PayMethod { get; set; }

        public bool Input { get; set; }

        public bool Finished { get; set; }

        private PaymentRecord()
        { }

        public PaymentRecord(User user, DateTime date, float amount, PayMethod payMethod, bool input)
        {
            User = user;
            UserId = user.Id;
            Date = date;
            Amount = amount;
            PayMethod = payMethod;
            Finished = false;
            Input = input;
        }
    }
}
