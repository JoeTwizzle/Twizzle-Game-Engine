
namespace TGE
{
    public abstract class GameState
    {
        public Game Parent;
        public string Name;
        public GameState(Game Parent, string Name)
        {
            this.Parent = Parent;
            this.Name = Name;
        }

        public virtual void Initialize() { }

        public virtual void Start() { }

        public virtual void Update() { }

        public virtual void LateUpdate() { }

        public virtual void Draw() { }
    }
}
