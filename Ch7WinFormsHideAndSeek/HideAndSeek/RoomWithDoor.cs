namespace HideAndSeek
{
    internal class RoomWithDoor : RoomWithHidingPlace, IHasExteriorDoor
    {
        public string DoorDescription { get; private set; }
        public Location DoorLocation { get; set; }

        public RoomWithDoor(string name, string decoration,
        string hidingPlaceName, string doorDescription) : base(name, decoration, hidingPlaceName)
        {
            DoorDescription = doorDescription;
        }

        public override string Description 
        {
            get { return base.Description + " You see " + DoorDescription + "."; }
        }
    }
}
