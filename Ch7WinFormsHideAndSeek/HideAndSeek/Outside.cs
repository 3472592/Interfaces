namespace HideAndSeek
{
    public class Outside : Location
    {
        private readonly bool _hot;

        public Outside(string name, bool hot) : base(name)
        {
            _hot = hot;
        }

        public override string Description
        {
            get
            {
                string newDescription = base.Description;
                if (_hot)
                    newDescription += " It's very hot";
                return newDescription;
            }
        }
    }
}
