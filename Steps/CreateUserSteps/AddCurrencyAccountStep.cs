using Workshop.Models;
using Workshop.Pipelines;

namespace Workshop.Steps.CreateUserSteps
{
    public class AddCurrencyAccountStep : IPipelineStep<User>
    {
        private readonly CurrencyAccount _account;

        public AddCurrencyAccountStep(CurrencyAccount account)
        {
            _account = account;
        }

        public Task Execute(User context)
        {
            context.CurrencyAccounts.Add(_account);

            return Task.CompletedTask;
        }
    }
}
