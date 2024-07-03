namespace Workshop.Pipelines
{
    public interface IPipelineStep<TIn>
    {
        Task Execute(TIn context);
    }
}
