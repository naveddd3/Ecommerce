namespace Domain.Entities
{
    public class OTPRequest
    {
        public string Email { get; set; }
        public int OTP { get; set; }
        public string Method { get; set; }
    }
}
