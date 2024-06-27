namespace HideAndSeek
{
    /// <summary>
    /// Location is an abstract class that can be inherited from.
    /// As well it can declare reference variables of type Location, cant instantiate it.
    /// We are passing in a parameter inside Location constructor.
    /// </summary>
    abstract public class Location
    {
        // constructor sets name field, which is readonly Name property.
        public Location(string name)
        {
            Name = name;
        }

        // exits field is an array of location references that keeps track
        // of all of the other places that this location connects to.
        public Location[] Exits;

        public string Name { get; private set; }

        // desc. is virtual because will have to override it later.

        /* The base description property returns a string that describes the room,
        including name and a list of all the locations it connects to
        which it finds in the exits[] field. Subs will need to change desc.,
        so they will override it. */

        // the Room class will override and extend Description to add the decor,
        // and outside will add the temp.
        public virtual string Description
        {
            get
            {
                string description = "You’re standing in the " + Name
                    + ". You see exits to the following places: ";
                for (int i = 0; i < Exits.Length; i++)
                {
                    description += " " + Exits[i].Name;
                    if (i != Exits.Length - 1)
                        description += ",";
                }
                description += ".";
                return description;
            }
        }
    }
}