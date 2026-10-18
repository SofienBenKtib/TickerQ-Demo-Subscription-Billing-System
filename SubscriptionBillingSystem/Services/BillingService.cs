namespace SubscriptionBillingSystem.Services;

public class BillingService : IBillingService
{
    private readonly AppDbContext _db;

    public BillingService(AppDbContext db)
    {
        _db = db;
    }

    public async Task ProcessDueSubscriptionsAsync(CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        var dueSubs = await _db.Subscriptions
            .Where(s => s.IsActive && s.NextBillingDate <= now)
            .ToListAsync(cancellationToken);

        foreach (var sub in dueSubs)
        {
            // fake payment processing
            var success = true; // call payment gateway here

            if (success)
            {
                sub.NextBillingDate = now.AddMonths(1);
                sub.FailedAttempts = 0;
            }
            else
            {
                sub.FailedAttempts++;

                if (sub.FailedAttempts >= 3)
                    sub.IsActive = false;
            }
        }

        await _db.SaveChangesAsync(cancellationToken);
    }
}