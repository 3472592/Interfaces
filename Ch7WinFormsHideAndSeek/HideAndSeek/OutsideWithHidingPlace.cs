namespace HideAndSeek
{
    public class OutsideWithHidingPlace : Outside, IHidingPlace
    {
        public string HidingPlaceName { get; private set; }

        public OutsideWithHidingPlace(string name, bool hot, string hidingPlaceName) : base(name, hot)
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
