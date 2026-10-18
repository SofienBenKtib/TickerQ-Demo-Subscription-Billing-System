namespace SubscriptionBillingSystem.Services;

public interface IBillingService
{
    Task ProcessDueSubscriptionsAsync(CancellationToken cancellationToken);
}