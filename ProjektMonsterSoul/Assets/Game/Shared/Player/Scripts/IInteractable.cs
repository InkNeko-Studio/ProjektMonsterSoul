namespace Game.Shared.Player.Scripts
{
    public interface IInteractable
    {
        public void OnInteract();
        public void OnInteractionEnter();
        public void OnInteractionLeave();
    }
}