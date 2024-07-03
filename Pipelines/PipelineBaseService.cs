namespace Workshop.Pipelines
{
    public abstract class PipelineBaseService<TIn>
        : IPipelineService<TIn>
    {
        public async Task Execute(TIn context)
        {
            foreach (var step in GetSteps())
            {
                await step.Execute(context);
            }
        }

        protected abstract List<IPipelineStep<TIn>> GetSteps();
    }
}
