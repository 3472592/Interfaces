namespace HideAndSeek
{
    public class Room : Location
    {
        private readonly string _decoration;

        public Room(string name, string decoration) : base(name)
        {
            _decoration = decoration;
        }

        public override string Description
        {
            get
            {
                return base.Description + " You see " + _decoration + ".";
            }
        }
    }
}
