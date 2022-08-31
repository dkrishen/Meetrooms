using System;

namespace MRA.Rooms.Models
{
    public partial class Room
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
