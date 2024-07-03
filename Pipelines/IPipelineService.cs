namespace Workshop.Pipelines
{
    public interface IPipelineService<TIn>
    {
        Task Execute(TIn context);
    }
}
