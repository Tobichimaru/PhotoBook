

namespace DAL.Interfacies.DTO
{
    public class DalLike : IEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int PhotoId { get; set; }
    }
}
