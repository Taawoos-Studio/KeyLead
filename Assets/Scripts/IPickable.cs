public interface IPickable
{
    public PickableType pickableType { get; }
    public bool TryPick(PlayerBehaviour playerBehaviour);
}

public enum PickableType {
    Key,
    Trap
}