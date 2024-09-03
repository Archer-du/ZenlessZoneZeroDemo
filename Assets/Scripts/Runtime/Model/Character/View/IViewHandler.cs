namespace View
{
    public interface IViewHandler
    {
        public IMovementComponent Movement { get; }
        
        public IAnimationComponent animation { get; }
    }

    public interface IMovementComponent
    {
        
    }

    public interface IAnimationComponent
    {
        
    }
}