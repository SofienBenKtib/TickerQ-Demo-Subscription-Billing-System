namespace SubscriptionBillingSystem.Models;

public class Subscription
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal MonthlyPrice { get; set; }
    public DateTime NextBillingDate { get; set; }
    public bool IsActive { get; set; }
    public int FailedAttempts { get; set; }
}
