namespace HideAndSeek
{
    class RoomWithHidingPlace : Room, IHidingPlace
    {
        public string HidingPlaceName { get; private set; }

        public RoomWithHidingPlace(string name, string decoration, string hidingPlaceName) : base(name, decoration)
        {
            HidingPlaceName = hidingPlaceName;
        }

        public override string Description
        {
            get
            {
                return base.Description + " Someone could hide " + HidingPlaceName + ".";
            }
        }
    }
}
