namespace App.Entities
{
    public class HouseMemebrRelationship
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int RelativeId { get; set; }
        public string Relationship { get; set; }
    }
}