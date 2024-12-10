public class VisualPipeline : Pipeline
{
    public bool IsActive { get; private set; } = true;

    public void Disable()
    {
        IsActive = false;
        this.ClearAll();
    }

    public void Enable()
    {
        IsActive = true;
    }
}